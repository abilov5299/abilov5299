using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Pages
{
    /// <summary>Tənzimləmə: "Ehtiyat nüsxə" və "Loglar" səhifələri bir bölmədə, yuxarıda nişanlarla.</summary>
    public class TenzimlemePage : PageBase
    {
        private readonly PageBase[] _p = { new BackupPage(), new LogsPage() };
        private readonly FlatButton[] _t = new FlatButton[2];
        private int _sec = -1;

        public TenzimlemePage()
        {
            Padding = new Padding(0);
            var bar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(22, 12, 22, 0), BackColor = Color.Transparent };
            string[] ad = { "Ehtiyat nüsxə", "Loglar" }; string[] g = { "", "" };
            for (int i = 0; i < 2; i++)
            {
                int k = i;
                _t[i] = new FlatButton { Text = ad[i], Glyph = g[i], Kind = ButtonKind.Ghost, Size = new Size(170, 36), Margin = new Padding(0, 0, 8, 0) };
                _t[i].Click += async (_, _) => await SecAsync(k);
                bar.Controls.Add(_t[i]);
            }
            foreach (var p in _p) { p.Dock = DockStyle.Fill; p.Visible = false; Controls.Add(p); }
            Controls.Add(bar);
        }

        public override Task OnShownAsync() => SecAsync(_sec < 0 ? 0 : _sec);

        /// <summary>0 = Ehtiyat nüsxə, 1 = Loglar.</summary>
        public async Task SecAsync(int i)
        {
            _sec = i;
            for (int k = 0; k < 2; k++) { _t[k].Kind = k == i ? ButtonKind.Primary : ButtonKind.Ghost; _p[k].Visible = k == i; }
            _p[i].BringToFront();
            await _p[i].OnShownAsync();
        }
    }
}
