using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TexnikaReyestri.Core
{
    public enum Language { Az, En, Ru }

    /// <summary>
    /// 3 dil. Formaları Designer-də Azərbaycan dilində yazırsınız; Apply() işləyəndə mətnlər seçilmiş dilə çevrilir.
    /// Orijinal mətn yadda saxlanılır, ona görə dillər arasında istənilən qədər keçid etmək olar.
    /// </summary>
    public static partial class Lang
    {
        public static Language Current { get; private set; } = Language.Az;
        public static event EventHandler Changed;

        private static readonly ConditionalWeakTable<object, string> Orig = new();

        private static readonly (Regex re, string en, string ru)[] Pat =
        {
            (new(@"^(\d+) gün$"), "$1 days", "$1 дн."),
            (new(@"^Zavod (\d+)$"), "Plant $1", "Завод $1"),
            (new(@"^Göstərilir: (\S+) / (\S+)$"), "Showing: $1 / $2", "Показано: $1 / $2"),
            (new(@"^(\d+) oxunmamış bildiriş$"), "$1 unread notifications", "$1 непрочитанных уведомлений"),
            (new(@"^\+ (\d+) hissə gözləyir$"), "+ $1 awaiting parts", "+ $1 ожидают запчасти"),
            (new(@"^(\d+)% aktiv təmirlərin$"), "$1% of active repairs", "$1% активных ремонтов"),
            (new(@"^(.+) · (\d+) gün gecikir$"), "$1 · $2 days overdue", "$1 · просрочено на $2 дн."),
        };

        public static void Set(Language l) { Current = l; Changed?.Invoke(null, EventArgs.Empty); }

        /// <summary>Kodda yazılan mətnlər üçün: Lang.T("Yadda saxla")</summary>
        public static string T(string az) => T(az, Current);

        /// <summary>Mətn bu orijinalın istənilən dildəki tərcüməsidirmi (dil dəyişəndə köhnə dildəki mətni tanımaq üçün).</summary>
        private static bool Tercumesi(string az, string current) => current == az || current == T(az, Language.En) || current == T(az, Language.Ru);

        private static string T(string az, Language Current)
        {
            if (Current == Language.Az || string.IsNullOrEmpty(az)) return az;
            string s = az.Trim();
            if (D.TryGetValue(s, out var v)) return Current == Language.En ? v.En : v.Ru;
            foreach (var (re, en, ru) in Pat)
                if (re.IsMatch(s)) return re.Replace(s, Current == Language.En ? en : ru);
            int i = s.IndexOf(": ", StringComparison.Ordinal);   // "Zavod: Hamısı" kimi
            if (i > 0 && D.TryGetValue(s[..i], out var a) && D.TryGetValue(s[(i + 2)..], out var b))
                return Current == Language.En ? $"{a.En}: {b.En}" : $"{a.Ru}: {b.Ru}";
            return az;
        }

        public static void Apply(Control root)
        {
            if (root == null) return;
            One(root);
            foreach (Control c in root.Controls) Apply(c);
        }

        private static void One(Control c)
        {
            if (c is TextBox or NumericUpDown or DateTimePicker) return;   // istifadəçinin yazdığı mətnə toxunma
            if (c is ComboBox cb) { TrItems(cb); return; }
            if (!string.IsNullOrEmpty(c.Text) && c is not Form || c is Form) c.Text = T(Keep(c, c.Text));
            if (c is DataGridView g)
                foreach (DataGridViewColumn col in g.Columns) col.HeaderText = T(Keep(col, col.HeaderText));
            if (c is ILocalizable lc) lc.OnLanguageChanged();
        }

        // ComboBox elementləri: yalnız string elementlər (məs. "Hamısı") tərcümə olunur
        private static void TrItems(ComboBox cb)
        {
            if (cb.DataSource != null) return;
            for (int i = 0; i < cb.Items.Count; i++)
                if (cb.Items[i] is string s) { var key = Keep(cb, "#" + i, s); cb.Items[i] = T(key); }
        }

        private static string Keep(object o, string current)
        {
            if (Orig.TryGetValue(o, out var az))
            {
                // Mətn kodla dəyişibsə (tərcümə deyil), yeni dəyəri orijinal kimi qəbul et
                if (!Tercumesi(az, current)) { Orig.AddOrUpdate(o, current); return current; }
                return az;
            }
            Orig.Add(o, current);
            return current;
        }

        private static readonly ConditionalWeakTable<ComboBox, Dictionary<string, string>> ItemOrig = new();
        private static string Keep(ComboBox cb, string slot, string current)
        {
            var d = ItemOrig.GetOrCreateValue(cb);
            if (d.TryGetValue(slot, out var az) && Tercumesi(az, current)) return az;
            d[slot] = current;
            return current;
        }
    }

    /// <summary>Özü mətn çəkən kontrollar (məs. StatCard) dil dəyişəndə yenidən çəkilir.</summary>
    public interface ILocalizable { void OnLanguageChanged(); }
}
