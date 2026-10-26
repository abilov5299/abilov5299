using System.ComponentModel;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Controls
{
    /// <summary>Ana səhifə, Reyestr və İcmalda eyni 6 statistika kartı. Klik → KartClicked(qrup), qrup ReyestrFiltr.Qrup-dur.</summary>
    [ToolboxItem(true), Description("6 statistika kartı")]
    public class KartPaneli : TableLayoutPanel
    {
        private static readonly (string qrup, string ad, string glyph, AccentKind rang)[] Tarif =
        {
            ("cemi", "Cəmi texnika", Icons.List, AccentKind.Nav),
            ("davam", "Təmiri davam edən texnikalar", Icons.Repair, AccentKind.Info),
            ("gozl", "Təmir tamamlanıb, təhvil gözlənilir", Icons.Check, AccentKind.Warn),
            ("tehvil", "Təhvil verilmiş texnikalar", Icons.Forward, AccentKind.Ok),
            ("yaxin", "İcra müddətinə 0–{0} gün qalıb", Icons.Clock, AccentKind.Bad),
            ("late", "İcra müddəti keçmiş texnikalar", Icons.Warning, AccentKind.Purple),
        };
        private readonly StatCard[] _k = new StatCard[6];
        public event EventHandler<string> KartClicked;

        public KartPaneli()
        {
            ColumnCount = 6; RowCount = 1; Dock = DockStyle.Top; Height = 96; Padding = new Padding(0, 4, 0, 8);
            RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            for (int i = 0; i < 6; i++)
            {
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 6));
                var t = Tarif[i];
                var c = _k[i] = new StatCard { Title = string.Format(t.ad, ReyestrFiltr.Yaxin), Glyph = t.glyph, Accent = t.rang, FilterKey = t.qrup, Dock = DockStyle.Fill, Margin = new Padding(0, 0, i < 5 ? 10 : 0, 0) };
                c.Click += (s, e) => KartClicked?.Invoke(this, ((StatCard)s).FilterKey);
                Controls.Add(c, i, 0);
            }
        }

        /// <summary>Admin parametrini (BildirisYaxin) oxuyur və 5-ci kartın başlığını yeniləyir.</summary>
        public static async Task<int> YaxinGunAsync()
        {
            ReyestrFiltr.Sari = await SistemRepo.ParamAsync("IcraSari", 20);
            return ReyestrFiltr.Yaxin = await SistemRepo.ParamAsync("BildirisYaxin", 3);
        }

        public void Goster(KartStat s, int yaxinGun)
        {
            // "İcra müddətinə 0–N gün qalıb" kartı yalnız zavodlarda görünür
            bool zavod = Session.Tip == QurumTip.Zavod;
            if (_k[4].Visible != zavod)
            {
                _k[4].Visible = zavod;
                for (int i = 0; i < 6; i++) ColumnStyles[i] = new ColumnStyle(SizeType.Percent, i == 4 && !zavod ? 0f : 100f / (zavod ? 6 : 5));
            }
            _k[4].Title = string.Format(Tarif[4].ad, yaxinGun);
            long[] v = { s.Cemi, s.Davam, s.Gozlenilir, s.Tehvil, s.Yaxin, s.Kecmis };
            for (int i = 0; i < 6; i++) _k[i].Value = v[i].ToString("#,0").Replace(",", " ");
        }
    }
}
