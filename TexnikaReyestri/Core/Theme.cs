using System.ComponentModel;

namespace TexnikaReyestri.Core
{
    public enum ThemeMode { Light, Dark }

    /// <summary>Kontrolun rəng rolu. Designer-də kontrolun <c>Tag</c> xassəsinə bu adlardan biri yazılır.</summary>
    /// card | alt | title | muted | nav | navtext | danger | page
    public static class Theme
    {
        public static ThemeMode Mode { get; private set; } = ThemeMode.Light;
        public static event EventHandler Changed;

        // Bütün rənglər bir yerdədir: dəyişmək üçün yalnız bu iki metodu redaktə edin
        public static Color Page, Bg, Card, Alt, Line, Sel, Ink, Title, Muted, Nav, NavOn, NavText,
                            Accent, Ok, Warn, Bad, Info, Gray, Chart1, Chart2, Chart3, Glow1, Glow2, Shadow, Watermark;

        static Theme() => Set(ThemeMode.Light, false);

        public static void Set(ThemeMode m, bool notify = true)
        {
            Mode = m;
            // Sabit rənglər (hər iki temada eyni)
            Nav = C("#0F4C4A"); NavOn = C("#1C6B68"); NavText = C("#A9C9C6");
            Accent = C("#E08A00"); Ok = C("#2E9B57"); Warn = C("#E08A00"); Bad = C("#C94A44"); Info = C("#3C8DBC"); Gray = C("#9AA9A8");
            if (m == ThemeMode.Light)
            {
                Page = C("#DCE5E3"); Bg = C("#EEF3F2"); Card = Color.White; Alt = C("#F6F9F9"); Line = C("#D4E2E0"); Sel = C("#DCEFEC");
                Ink = C("#1B2B2A"); Title = C("#0F4C4A"); Muted = C("#5D7472");
                Chart1 = C("#0F4C4A"); Chart2 = C("#6FB1A9"); Chart3 = C("#D9A441");
                Glow1 = Color.FromArgb(18, 15, 76, 74); Glow2 = Color.FromArgb(13, 224, 138, 0); Shadow = Color.FromArgb(30, 15, 76, 74); Watermark = Color.FromArgb(9, 15, 76, 74);
            }
            else
            {
                Page = C("#0A1112"); Bg = C("#0F1718"); Card = C("#162122"); Alt = C("#1B2829"); Line = C("#2A3A3B"); Sel = C("#1E3836");
                Ink = C("#DCE8E6"); Title = C("#8FD3C9"); Muted = C("#8DA3A1");
                Chart1 = C("#5FB8AD"); Chart2 = C("#2E6E68"); Chart3 = C("#E0B25A");
                Glow1 = Color.FromArgb(26, 111, 177, 169); Glow2 = Color.FromArgb(15, 224, 138, 0); Shadow = Color.FromArgb(90, 0, 0, 0); Watermark = Color.FromArgb(15, 143, 211, 201);
            }
            if (notify) Changed?.Invoke(null, EventArgs.Empty);
        }

        public static void Toggle() => Set(Mode == ThemeMode.Light ? ThemeMode.Dark : ThemeMode.Light);

        /// <summary>Status mətninə görə nişan rəngi (fon, yazı).</summary>
        public static (Color back, Color fore) StatusColors(string status)
        {
            bool d = Mode == ThemeMode.Dark;
            return status switch
            {
                "Təmirdə" => d ? (C("#17344A"), C("#8CC3EA")) : (C("#DCEBF6"), C("#1F5F8B")),
                "Hissə gözləyir" or "Təminat gözləyir" or "Təminat gözlənilir" or "Təmir tamamlanıb, təhvil gözlənilir" => d ? (C("#3D2C12"), C("#F2B05E")) : (C("#FCE7D2"), C("#A65A10")),
                "Qismən təmin edilib" => d ? (C("#2E2440"), C("#C3A6F0")) : (C("#ECE3FA"), C("#5B3A99")),
                "Təmin edilməyib" => d ? (C("#3D1A1A"), C("#F28E89")) : (C("#F8DCDB"), C("#A3302C")),
                "Tam təmin edilib" or "Təhvil verilib" => d ? (C("#123A2A"), C("#6FD39A")) : (C("#DDF1E3"), C("#23703E")),
                "Təmirə yararsız" or "Gecikir" or "Bloklanıb" or "Rədd edildi" => d ? (C("#3D1A1A"), C("#F28E89")) : (C("#F8DCDB"), C("#A3302C")),
                "Aktiv" or "Uğurlu" => d ? (C("#123A2A"), C("#6FD39A")) : (C("#DDF1E3"), C("#23703E")),
                _ => d ? (C("#263334"), C("#B5C4C3")) : (C("#E7ECEB"), C("#4A5A59")),
            };
        }

        public static Color StatusBar(string status) => status switch
        {
            "Təmirdə" => Info, "Hissə gözləyir" or "Təminat gözləyir" or "Qismən təmin edilib" or "Təmir tamamlanıb, təhvil gözlənilir" => Warn, "Təhvil verilib" => Ok, "Təmirə yararsız" => Bad, _ => Gray
        };

        /// <summary>Standart kontrolları temaya uyğun rəngləyir. Xüsusi kontrollar (IThemed) özləri çəkilir.</summary>
        public static void Apply(Control root)
        {
            if (root == null) return;
            ApplyOne(root);
            Solid(root);
            foreach (Control c in root.Controls) Apply(c);
        }

        private static void ApplyOne(Control c)
        {
            if (c is IThemed t) { t.OnThemeChanged(); return; }
            string role = c.Tag as string;
            switch (c)
            {
                case Form f: f.BackColor = Bg; f.ForeColor = Ink; break;
                case DataGridView: break; // ModernGrid özü rənglənir
                case TextBox tb: tb.BackColor = Card; tb.ForeColor = Ink; break;
                case ComboBox cb: cb.BackColor = Card; cb.ForeColor = Ink; break;
                case NumericUpDown nu: nu.BackColor = Card; nu.ForeColor = Ink; break;
                case DateTimePicker dp: dp.CalendarMonthBackground = Card; dp.CalendarForeColor = Ink; break;
                case CheckBox or RadioButton: c.ForeColor = role == "muted" ? Muted : Ink; break;
                case LinkLabel ll:
                    ll.BackColor = Color.Transparent; ll.LinkBehavior = LinkBehavior.HoverUnderline;
                    ll.LinkColor = ll.VisitedLinkColor = Title; ll.ActiveLinkColor = Accent; ll.ForeColor = Title;
                    break;
                case Label l:
                    l.BackColor = Color.Transparent;
                    l.ForeColor = role switch { "title" => Title, "muted" => Muted, "navtext" => NavText, "danger" => Bad, "white" => Color.White, "accent" => Accent, _ => Ink };
                    break;
                case TabPage tp: tp.BackColor = Card; tp.ForeColor = Ink; break;
                default:
                    if (c is Panel or TableLayoutPanel or FlowLayoutPanel or UserControl or SplitContainer)
                        c.BackColor = role switch { "card" => Card, "alt" => Alt, "nav" => Nav, "page" => Page, "transparent" => Color.Transparent, _ => c is UserControl ? Bg : Color.Transparent };
                    break;
            }
        }

        // Şəffaf yazı/panel kartın içindədirsə, kartın rəngini alır: şəffaflıq hər çəkilişdə valideyni təkrar çəkdirir və UI-ni ləngidir
        private static void Solid(Control c)
        {
            if (c.BackColor != Color.Transparent || c is IThemed || c is UserControl || !(c is Label or Panel or TableLayoutPanel or FlowLayoutPanel or CheckBox or RadioButton)) return;
            for (var p = c.Parent; p != null; p = p.Parent)
            {
                if (p is Controls.RoundPanel rp) { if (rp.Fill != Controls.PanelFill.Transparent) c.BackColor = rp.FillColor; return; }
                if (p is UserControl || p is Form) return;
                if (p.BackColor != Color.Transparent) { c.BackColor = p.BackColor; return; }
            }
        }

        public static Color C(string hex) => ColorTranslator.FromHtml(hex);
        public static Color Mix(Color a, Color b, float t) => Color.FromArgb(
            (int)(a.A + (b.A - a.A) * t), (int)(a.R + (b.R - a.R) * t), (int)(a.G + (b.G - a.G) * t), (int)(a.B + (b.B - a.B) * t));
    }

    /// <summary>Özü çəkilən kontrollar üçün: tema dəyişəndə yenidən çəkilir.</summary>
    public interface IThemed { void OnThemeChanged(); }

    internal static class DesignMode
    {
        public static bool Active => LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
}
