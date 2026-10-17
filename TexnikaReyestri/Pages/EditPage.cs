using System.Globalization;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Pages
{
    /// <summary>
    /// Texnika daxil etmə və redaktə. Hər qrup ayrıca tabdır; hər sahə Designer-də "in_SaheKodu" adlı kontroldur
    /// (TextBox, ComboBox və ya DateTimePicker). Yeni sahə əlavə etmək: Saheler.cs + bu formada eyni adla kontrol.
    /// </summary>
    public partial class EditPage : PageBase
    {
        private long? _id;

        public EditPage()
        {
            InitializeComponent();
            // Seçim siyahıları Secimler.cs-dən gəlir (səhifə görünməzdən əvvəl də doldurulmalıdır)
            foreach (var (kod, list) in Secimler.Sahe)
                if (In(kod) is ComboBox cb) { cb.Items.Clear(); cb.Items.Add(""); cb.Items.AddRange(list); }
        }

        private void EditPage_Load(object sender, EventArgs e)
        {
            foreach (TabPage t in tabs.TabPages) t.BackColor = Theme.Card;
        }

        private Control In(string kod) => Controls.Find("in_" + kod, true).FirstOrDefault();

        /// <summary>Forma hazırda boş "yeni texnika" rejimindədir (menyudan təkrar açanda yazılanlar silinməsin).</summary>
        public bool YeniRejim { get; private set; }

        public async Task YukleAsync(long? id)
        {
            _id = id; YeniRejim = id == null; tabs.SelectedIndex = 0;
            lblTitle.Text = id == null ? Lang.T("Texnika daxil et") : Lang.T("Redaktə") + "…";
            Dictionary<string, object> r = id == null ? new() : await TexnikaRepo.TapAsync(id.Value) ?? new();
            if (id != null) lblTitle.Text = $"{Lang.T("Redaktə")}: {r.GetValueOrDefault("Marka")} · {r.GetValueOrDefault("ZavodNo")}";
            if (id == null && Session.ZavodFiltri != null) r["TemirZavodu"] = Session.ZavodFiltri;
            if (id == null) { r["Status"] = "Qəbul edilib"; r["Merhele"] = "Qəbul"; r["DaxilolmaTarixi"] = Vaxt.Iso(DateTime.Today); r["MesulIcraci"] = Session.AdSoyad; }   // məsul icraçı: daxil edən istifadəçi (dəyişdirilə bilər)
            foreach (var s in Saheler.Hamisi)
            {
                var v = r.GetValueOrDefault(s.Kod); string str = v is null or DBNull ? "" : Convert.ToString(v, CultureInfo.InvariantCulture);
                switch (In(s.Kod))
                {
                    case ComboBox cb: cb.SelectedIndex = Math.Max(0, cb.Items.IndexOf(str)); if (cb.SelectedIndex == 0 && str.Length > 0) { cb.Items.Add(str); cb.SelectedItem = str; } break;
                    case DateTimePicker dp: if (DateTime.TryParse(str, out var d)) { dp.Value = d; dp.Checked = true; } else { dp.Value = DateTime.Today; dp.Checked = false; } break;
                    case TextBox tb: tb.Text = str; break;
                }
            }
            // Zavod istifadəçisi başqa zavod adına texnika yaza bilməz
            if (In("TemirZavodu") is ComboBox z) z.Enabled = Session.ZavodFiltri == null;
            Duymeler();
        }

        public override Task OnShownAsync() => Task.CompletedTask;

        private Dictionary<string, object> Topla(out List<string> xetalar)
        {
            xetalar = new(); var d = new Dictionary<string, object>();
            foreach (var s in Saheler.Hamisi)
            {
                object v = In(s.Kod) switch
                {
                    ComboBox cb => cb.SelectedItem as string is { Length: > 0 } x ? x : null,
                    DateTimePicker dp => dp.Checked ? Vaxt.Iso(dp.Value) : null,
                    TextBox tb => tb.Text.Trim() is { Length: > 0 } t ? t : null,
                    _ => null
                };
                if (v is string str && s.Nov == SaheNov.Reqem)
                {
                    if (double.TryParse(str.Replace(" ", "").Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var n)) v = n;
                    else xetalar.Add($"{Lang.T(s.Ad)}: {Lang.T("rəqəm olmalıdır")}");
                }
                if (s.Mecburi && v == null) xetalar.Add($"{Lang.T(s.Ad)}: {Lang.T("Məcburi sahə boşdur")}");
                d[s.Kod] = v;
            }
            return d;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var d = Topla(out var x);
            if (x.Count > 0)
            {
                // İlk xətalı sahənin tabına keç və onu fokusla
                var kod = Saheler.Hamisi.First(s => x[0].StartsWith(Lang.T(s.Ad))).Kod;
                if (In(kod) is { } c) { var tp = c.Parent; while (tp is not TabPage && tp != null) tp = tp.Parent; if (tp is TabPage t) tabs.SelectedTab = t; c.Focus(); }
                Toast.Show(this, string.Join("\n", x.Take(3)), ToastKind.Error); return;
            }
            btnSave.Enabled = false;
            try
            {
                bool yeni = _id == null;
                long id = await TexnikaRepo.SaxlaAsync(_id, d);
                _id = id; YeniRejim = false;   // yadda saxlandı: menyudan "daxil et" basanda forma təmiz açılsın, təkrar basanda dublikat yaranmasın
                await SistemRepo.LogAsync(yeni ? "Əlavə" : "Redaktə", $"{(yeni ? "Yeni texnika" : "Dəyişdirildi")}: {d["Marka"]} ({d["ZavodNo"]})");
                if (yeni) await SistemRepo.BildirisAsync($"new:{id}", "new", "Yeni texnika qəbul edildi", $"{d["Marka"]} ({d["ZavodNo"]}) {d["TemirZavodu"]}-ə daxil oldu", d["TemirZavodu"] as string, id);
                Toast.Show(this, yeni ? "Texnika reyestrə əlavə edildi" : "Dəyişikliklər yadda saxlanıldı");
                await MainForm.Current.BadgeYenileAsync();
                await MainForm.Current.DetalAcAsync(id);
            }
            catch (Exception ex) { Toast.Show(this, ex.Message.Contains("UNIQUE") ? Lang.T("Bu zavod nömrəsi artıq bazada var.") : ex.Message, ToastKind.Error); }
            finally { btnSave.Enabled = true; }
        }

        private void btnPrevG_Click(object sender, EventArgs e) { if (tabs.SelectedIndex > 0) tabs.SelectedIndex--; }
        private void btnNextG_Click(object sender, EventArgs e) { if (tabs.SelectedIndex < tabs.TabCount - 1) tabs.SelectedIndex++; }
        private void tabs_SelectedIndexChanged(object sender, EventArgs e) => Duymeler();
        private void Duymeler() { btnPrevG.Enabled = tabs.SelectedIndex > 0; btnNextG.Enabled = tabs.SelectedIndex < tabs.TabCount - 1; }

    }
}
