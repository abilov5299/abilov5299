using System.Globalization;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>"Geniş bax": texnikanın bütün sahələri qruplar üzrə, mərhələ xətti və dəyişiklik tarixçəsi.</summary>
    public partial class DetailPage : PageBase
    {
        private long _id; private IReadOnlyList<long> _siyahi = Array.Empty<long>();
        private Dictionary<string, object> _r;
        // Kartların sütunlara bölgüsü (Designer-də yerini dəyişsəniz, bu siyahını da uyğunlaşdırın)
        private RoundPanel[][] Sutunlar => new[]
        {
            new[] { rpHist, rpPasport, rpTemirtarixcesi, rpIstismargoster },
            new[] { rpSilahsistemi, rpTexnikiveziyye, rpCaritemir },
            new[] { rpQebulveicra, rpEhtiyathissele, rpTehvil }
        };

        public DetailPage()
        {
            InitializeComponent();
            scroll.Resize += (_, _) => { if (scroll.ClientSize.Width != _enSon) Duzle(); };
        }

        public async Task YukleAsync(long id, IReadOnlyList<long> siyahi)
        {
            _id = id; if (siyahi != null) _siyahi = siyahi;
            await Ui.Is(this, "Texnika məlumatları açılır...", async _ =>
            {
                _r = await TexnikaRepo.TapAsync(id) ?? throw new InvalidOperationException(Lang.T("Qeyd tapılmadı"));
                var tc = await TexnikaRepo.TarixceAsync(id);
                Doldur(tc);
            });
        }

        public override Task OnShownAsync() => _id > 0 && _r == null ? YukleAsync(_id, null) : Task.CompletedTask;

        private void Doldur(List<TarixceQeyd> tc)
        {
            string V(string k) => _r.GetValueOrDefault(k) is { } v and not DBNull ? Convert.ToString(v, CultureInfo.InvariantCulture) : null;
            lblName.Text = $"{V("Marka")} · {V("ZavodNo")}";
            lblSub.Text = $"{Lang.T(V("Nov") ?? "")} · {Lang.T(V("TemirZavodu") ?? "")} · {Lang.T("daxil olub")} {Vaxt.Tarix(V("DaxilolmaTarixi")) ?? "—"}";
            chipStatus.Text = V("Status") ?? "—"; chipStatus.Left = lblName.Right + 12;
            bool gec = V("GozlenilenBitme") is string g && string.CompareOrdinal(g, Vaxt.Iso(DateTime.Today)) < 0 && Secimler.AktivStatus.Contains(V("Status"));
            chipLate.Visible = gec; chipLate.Left = chipStatus.Right + 6;
            int m = Array.IndexOf(Secimler.Merhele, V("Merhele"));
            stepper.CurrentStep = V("Status") == "Təhvil verilib" ? Secimler.Merhele.Length : Math.Max(0, m);
            foreach (var s in Saheler.Hamisi)
            {
                if (Controls.Find("val_" + s.Kod, true).FirstOrDefault() is not Label l) continue;
                string v = V(s.Kod);
                l.Text = v == null ? "—" : s.Nov == SaheNov.Tarix ? Vaxt.Tarix(v) : s.Nov == SaheNov.Secim ? Lang.T(v) : v;
                l.ForeColor = v == null ? Theme.Muted : s.Kod == "GozlenilenBitme" && gec ? Theme.Bad : Theme.Ink;
            }
            // Tarixçə
            flHist.SuspendLayout(); flHist.Controls.Clear();
            foreach (var t in tc.Take(30))
            {
                var p = new TableLayoutPanel { ColumnCount = 2, AutoSize = true, Margin = new Padding(0, 0, 0, 6), Width = rpHist.Width - 30 };
                p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112)); p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                var a = new Label { AutoSize = true, ForeColor = Theme.Muted, Font = new Font("Segoe UI", 8f), Text = $"{Vaxt.Yaz(t.Tarix)}\n{t.Istifadeci}" };
                var b = new Label { AutoSize = true, MaximumSize = new Size(rpHist.Width - 160, 0), Font = new Font("Segoe UI", 8.5f), ForeColor = Theme.Ink,
                                    Text = $"{Lang.T(t.Sahe)}\n{(t.Kohne == null ? "" : (t.Kohne.Length == 0 ? Lang.T("boş") : t.Kohne) + " → ")}{t.Yeni}" };
                p.Controls.Add(a, 0, 0); p.Controls.Add(b, 1, 0); flHist.Controls.Add(p);
            }
            if (tc.Count == 0) flHist.Controls.Add(new Label { AutoSize = true, ForeColor = Theme.Muted, Text = Lang.T("Hələ dəyişiklik yoxdur.") });
            flHist.ResumeLayout();
            int i = IndexOf(); btnPrev.Enabled = i > 0; btnNext.Enabled = i >= 0 && i < _siyahi.Count - 1;
            btnEdit.Visible = Session.YazaBiler;
            scroll.ScrollToTop(); Duzle();
        }

        private int IndexOf() { for (int i = 0; i < _siyahi.Count; i++) if (_siyahi[i] == _id) return i; return -1; }

        /// <summary>Kartları 3 sütunda yerləşdirir və hündürlüklərini məzmuna görə təyin edir.</summary>
        // Cədvəlin hündürlüyü: hər sətirdə ən hündür yazının (verilən endə) ölçüsü — köhnə yerləşmədən asılı deyil
        private static int TlpH(TableLayoutPanel t, int w)
        {
            var cw = new int[t.ColumnCount]; float tot = t.ColumnStyles.Cast<ColumnStyle>().Sum(c => c.Width);
            for (int i = 0; i < cw.Length; i++) cw[i] = (int)(w * (i < t.ColumnStyles.Count && tot > 0 ? t.ColumnStyles[i].Width / tot : 1f / cw.Length));
            var rows = new Dictionary<int, int>();
            foreach (Control c in t.Controls)
            {
                if (!c.Visible) continue;
                var pos = t.GetPositionFromControl(c); int col = Math.Clamp(pos.Column, 0, cw.Length - 1);
                int ph = c.GetPreferredSize(new Size(Math.Max(10, cw[col] - c.Margin.Horizontal), 0)).Height + c.Margin.Vertical;
                rows[pos.Row] = Math.Max(rows.GetValueOrDefault(pos.Row), ph);
            }
            return rows.Values.Sum() + t.Padding.Vertical;
        }

        private int _enSon;
        private void Duzle()
        {
            if (scroll.Width < 100) return;
            _enSon = scroll.ClientSize.Width;
            using var _ = Ui.Dondur(scroll);
            int gap = 12, w = (scroll.ClientSize.Width - gap * 2) / 3;
            scroll.SuspendLayout();
            var cols = Sutunlar;
            for (int c = 0; c < cols.Length; c++)
            {
                int top = 0;
                foreach (var card in cols[c])
                {
                    card.Width = w; card.Left = c * (w + gap);
                    foreach (Control ch in card.Controls)
                    {
                        if (ch is TableLayoutPanel t)
                            foreach (Control l in t.Controls) if (l is Label lb) lb.MaximumSize = new Size((int)(w * (lb.Name.StartsWith("cap_") ? 0.46 : 0.5)), 0);
                    }
                    // Hündürlük məzmunun "istədiyi" ölçüdən hesablanır (köhnə yerləşmədən yox) — pəncərə böyüyəndə kartlar sıxılmır
                    int iw = w - card.Padding.Horizontal, h = card.Padding.Top;
                    foreach (Control ch in card.Controls)
                    {
                        if (!ch.Visible) continue;
                        h += ch switch { TableLayoutPanel t => TlpH(t, iw), FlowLayoutPanel f => f.Controls.Cast<Control>().Sum(x => x.Height + x.Margin.Vertical) + f.Padding.Vertical, _ => ch.Height };
                    }
                    h += card.Padding.Bottom + 6;
                    card.Height = Math.Max(60, h);
                    card.Top = top - scroll.Offset; top += card.Height + gap;
                }
            }
            scroll.ResumeLayout();
        }

        private async void btnBack_Click(object sender, EventArgs e) => await MainForm.Current.AcAsync("list");
        private async void btnPrev_Click(object sender, EventArgs e) { int i = IndexOf(); if (i > 0) await YukleAsync(_siyahi[i - 1], null); }
        private async void btnNext_Click(object sender, EventArgs e) { int i = IndexOf(); if (i >= 0 && i < _siyahi.Count - 1) await YukleAsync(_siyahi[i + 1], null); }
        private async void btnEdit_Click(object sender, EventArgs e) => await MainForm.Current.RedakteAcAsync(_id);

        private async void btnPdf_Click(object sender, EventArgs e)
        {
            using var d = new SaveFileDialog { Filter = "PDF|*.pdf", FileName = $"Pasport_{_r?.GetValueOrDefault("ZavodNo")}.pdf" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            if (await Ui.Is(this, "PDF hazırlanır...", _ => Task.Run(() => PdfService.Pasport(d.FileName, _r))))
            {
                Toast.Show(this, "Pasport PDF kimi saxlanıldı");
                try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(d.FileName) { UseShellExecute = true }); } catch { }
            }
        }
    }
}
