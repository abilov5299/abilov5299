using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Pages
{
    /// <summary>
    /// Təlimat: üç istifadəçi təlimatı (Zavod, Xidmət, İdarə). Təlimatlar proqram qovluğundakı Telimat\*.html fayllarıdır
    /// (docs/telimat/qur.py ilə qurulur) və standart brauzerdə açılır. İstifadəçinin öz təlimatı birinci göstərilir.
    /// </summary>
    public class TelimatPage : PageBase
    {
        private static readonly (string fayl, string ad, string izah, QurumTip tip)[] Telimatlar =
        {
            ("Zavod.html", "Təmir zavodu", "Texnikanın qəbulu, 5 bölməli forma, ehtiyat hissələri və məktublar, tamamlanma və təhvil, USB-yə ixrac.", QurumTip.Zavod),
            ("Xidmet.html", "Təmir və Texniki Təminat xidməti", "Zavodlardan USB paketlərinin qəbulu, bütün zavodlar üzrə nəzarət, hesabatlar, İdarəyə ixrac.", QurumTip.UstQurum),
            ("Idare.html", "Müdafiə Sənayesi İdarəsi", "Xidmətdən USB paketlərinin qəbulu, ümumi vəziyyət, hesabatlar və bildirişlər.", QurumTip.EnUstQurum),
        };

        private readonly FlowLayoutPanel _fl = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, BackColor = Color.Transparent };

        public TelimatPage()
        {
            var head = new Panel { Dock = DockStyle.Top, Height = 74 };
            head.Controls.Add(new Label { Text = "Təlimat", Tag = "title", AutoSize = true, Font = new Font("Bahnschrift SemiBold", 18f), Location = new Point(0, 4) });
            head.Controls.Add(new Label { Text = "İstifadəçi təlimatları proqramdan istifadə qaydasını şəkillərlə izah edir. Təlimat brauzerdə açılır; oradan çap etmək də mümkündür.",
                                          Tag = "muted", AutoSize = true, Font = new Font("Segoe UI", 9.5f), Location = new Point(2, 44) });
            Controls.Add(_fl); Controls.Add(head);
            foreach (var t in Telimatlar.OrderBy(t => t.tip != Session.Tip)) _fl.Controls.Add(Kart(t));
        }

        private Control Kart((string fayl, string ad, string izah, QurumTip tip) t)
        {
            bool oz = t.tip == Session.Tip;
            var rp = new RoundPanel { Size = new Size(760, 112), Margin = new Padding(0, 0, 0, 12), Padding = new Padding(20, 14, 20, 14) };
            rp.Controls.Add(new Label { Text = Icons.Journal, Font = Icons.Font(26f), ForeColor = oz ? Theme.Accent : Theme.Nav, AutoSize = true, Location = new Point(20, 30) });
            rp.Controls.Add(new Label { Text = t.ad + (oz ? "  ·  " + Lang.T("sizin təlimatınız") : ""), Font = new Font("Segoe UI Semibold", 12f), Tag = "title", AutoSize = true, Location = new Point(76, 18) });
            rp.Controls.Add(new Label { Text = t.izah, Font = new Font("Segoe UI", 9.5f), Tag = "muted", AutoSize = false, Size = new Size(470, 44), Location = new Point(77, 48) });
            var b = new FlatButton { Text = "Təlimatı aç", Glyph = Icons.Forward, Kind = oz ? ButtonKind.Primary : ButtonKind.Secondary, Size = new Size(170, 38), Location = new Point(570, 37) };
            b.Click += (_, _) => Ac(t.fayl);
            rp.Controls.Add(b);
            return rp;
        }

        private void Ac(string fayl)
        {
            string yol = Path.Combine(AppContext.BaseDirectory, "Telimat", fayl);
            if (!File.Exists(yol)) { Toast.Show(this, Lang.T("Təlimat faylı tapılmadı") + ": " + yol, ToastKind.Error); return; }
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(yol) { UseShellExecute = true }); }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }

        public override Task OnShownAsync() => Task.CompletedTask;
    }
}
