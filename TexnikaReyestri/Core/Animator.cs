namespace TexnikaReyestri.Core
{
    /// <summary>
    /// Səhifə keçidi. Səhifənin şəklini çəkmək (DrawToBitmap) zəif kompüterlərdə çox yavaşdır, ona görə istifadə olunmur:
    /// ekranın çəkilişi dondurulur, köhnə səhifə gizlənir, yeni səhifə yerləşir və hamısı bir dəfəyə çəkilir — qırpılma və yarımçıq görüntü olmur.
    /// </summary>
    public static class Animator
    {
        public static bool Aktiv { get; set; } = true;   // köhnə parametrlə uyğunluq üçün saxlanılıb

        public static Task KecidAsync(Control host, Control kohne, Control yeni)
        {
            using (Ui.Dondur(host))
            {
                yeni.Dock = DockStyle.Fill;
                if (kohne != null) kohne.Visible = false;
                yeni.Visible = true; yeni.BringToFront();
                host.PerformLayout();
            }
            return Task.CompletedTask;
        }
    }
}
