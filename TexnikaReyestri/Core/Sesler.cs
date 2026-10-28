using System.Runtime.InteropServices;
using System.Text;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Core
{
    /// <summary>
    /// Proqram səsləri: açılış və klik (mp3/wav, Windows MCI ilə).
    /// Admin seçdiyi fayllar C:\ProgramData\TexnikaReyestri\Sesler-ə köçürülür; yoxdursa proqramla gələn Assets\Sesler işlənir.
    /// Aktiv/passiv: Parametrler → GirisSes, KlikSes (Admin → Sistem).
    /// </summary>
    public static class Sesler
    {
        public const string Acilis = "acilis", Klik = "klik";
        public static bool KlikAktiv { get; set; }
        private static readonly HashSet<string> _aciq = new();

        [DllImport("winmm.dll", CharSet = CharSet.Unicode)]
        private static extern int mciSendString(string cmd, StringBuilder ret, int len, IntPtr cb);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetShortPathName(string uzun, StringBuilder qisa, int len);

        /// <summary>MCI uzun yolları (≈128 simvoldan çox) aça bilmir, ona görə qısa (8.3) yol verilir.</summary>
        private static string Qisa(string f) { var sb = new StringBuilder(512); return GetShortPathName(f, sb, sb.Capacity) > 0 ? sb.ToString() : f; }

        private static string Qovluq => Path.Combine(AppPaths.Root, "Sesler");

        /// <summary>Səsin faylı: əvvəl adminin seçdiyi, sonra proqramla gələn.</summary>
        public static string Fayl(string ad)
        {
            foreach (var q in new[] { Qovluq, Path.Combine(AppContext.BaseDirectory, "Assets", "Sesler") })
                if (Directory.Exists(q) && Directory.GetFiles(q, ad + ".*").FirstOrDefault() is { } f) return f;
            return null;
        }

        /// <summary>Adminin seçdiyi yeni faylı köçürür (köhnəsi silinir).</summary>
        public static void Deyis(string ad, string menbe)
        {
            // Windows aça bilmirsə (məs. çox qısa mp3), köhnə səs qalsın
            string tip = Path.GetExtension(menbe).Equals(".wav", StringComparison.OrdinalIgnoreCase) ? "waveaudio" : "mpegvideo";
            if (mciSendString($"open \"{Qisa(menbe)}\" type {tip} alias tr_yoxla", null, 0, IntPtr.Zero) != 0)
                throw new InvalidOperationException("Bu fayl səsləndirilə bilmir. Başqa fayl və ya WAV seçin.");
            mciSendString("close tr_yoxla", null, 0, IntPtr.Zero);
            Bagla(ad);
            Directory.CreateDirectory(Qovluq);
            foreach (var f in Directory.GetFiles(Qovluq, ad + ".*")) File.Delete(f);
            File.Copy(menbe, Path.Combine(Qovluq, ad + Path.GetExtension(menbe).ToLowerInvariant()));
        }

        /// <summary>Adminin faylını silir, proqramla gələn səsə qayıdır.</summary>
        public static void Susmaya(string ad)
        {
            Bagla(ad);
            if (Directory.Exists(Qovluq)) foreach (var f in Directory.GetFiles(Qovluq, ad + ".*")) File.Delete(f);
        }

        public static void Cal(string ad)
        {
            try
            {
                if (!_aciq.Contains(ad))
                {
                    var f = Fayl(ad); if (f == null) return;
                    string tip = f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) ? "waveaudio" : "mpegvideo";
                    if (mciSendString($"open \"{Qisa(f)}\" type {tip} alias tr_{ad}", null, 0, IntPtr.Zero) != 0) return;
                    _aciq.Add(ad);
                }
                mciSendString($"play tr_{ad} from 0", null, 0, IntPtr.Zero);
            }
            catch { }   // səs olmasa da proqram işləməlidir
        }

        private static void Bagla(string ad)
        {
            if (_aciq.Remove(ad)) mciSendString($"close tr_{ad}", null, 0, IntPtr.Zero);
        }

        /// <summary>Düymə, mətn sahəsi, siyahı və s. üzərində sol klikdə klik səsi.</summary>
        public sealed class KlikFiltr : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg != 0x0201 || !KlikAktiv) return false;   // WM_LBUTTONDOWN
                var c = Control.FromChildHandle(m.HWnd);
                if (c is { Enabled: true } and (ButtonBase or TextBoxBase or ComboBox or NavButton or StatusChip or NumericUpDown or DateTimePicker or ListBox)) Cal(Klik);
                return false;
            }
        }
    }
}
