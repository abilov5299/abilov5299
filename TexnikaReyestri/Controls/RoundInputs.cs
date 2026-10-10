using System.ComponentModel;
using System.Runtime.InteropServices;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>Yumru künclü sahələr üçün ortaq çəkmə köməkçisi.</summary>
    internal static class InputFrame
    {
        public const int Radius = 7;
        [DllImport("user32.dll")] public static extern IntPtr GetWindowDC(IntPtr h);
        [DllImport("user32.dll")] public static extern int ReleaseDC(IntPtr h, IntPtr dc);
        [DllImport("user32.dll")] public static extern bool RedrawWindow(IntPtr h, IntPtr r, IntPtr rgn, int flags);
        [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr h, IntPtr after, int x, int y, int cx, int cy, int flags);
        public static void Frame(Control c) { if (c.IsHandleCreated) RedrawWindow(c.Handle, IntPtr.Zero, IntPtr.Zero, 0x0400 | 0x0001 | 0x0100); } // FRAME|INVALIDATE|UPDATENOW

        public static Color Border(Control c, bool focus) => !c.Enabled ? Theme.Mix(Theme.Line, Theme.Card, 0.4f) : focus ? Theme.Accent : Theme.Mix(Theme.Line, Theme.Muted, 0.35f);

        /// <summary>Çöl çərçivə: künclərdə valideyn fonu, içi sahə rəngi, yumru kənar xətti. hole — toxunulmayan daxili hissə.</summary>
        public static void Draw(Graphics g, Control c, Rectangle full, Rectangle hole, bool focus)
        {
            var st = g.Save();
            if (!hole.IsEmpty) g.ExcludeClip(hole);
            using (var bb = new SolidBrush(Gfx.BackOf(c))) g.FillRectangle(bb, full);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var p = Gfx.Round(new RectangleF(full.X + .5f, full.Y + .5f, full.Width - 1.5f, full.Height - 1.5f), Radius);
            using (var fb = new SolidBrush(c.BackColor)) g.FillPath(fb, p);
            using (var pen = new Pen(Border(c, focus), focus ? 1.6f : 1f)) g.DrawPath(pen, p);
            g.Restore(st);
        }

        public static void Chevron(Graphics g, Rectangle r, Color col)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            float cx = r.X + r.Width / 2f, cy = r.Y + r.Height / 2f;
            using var pen = new Pen(col, 1.6f);
            g.DrawLines(pen, new[] { new PointF(cx - 4, cy - 2), new PointF(cx, cy + 2), new PointF(cx + 4, cy - 2) });
        }
    }

    /// <summary>Yumru künclü mətn sahəsi. Adi TextBox-dur (bütün xassələri eynidir), yalnız kənarı fərqli çəkilir.</summary>
    [ToolboxItem(true), Description("Yumru künclü TextBox")]
    public class RoundTextBox : TextBox, IThemed
    {
        private const int PadX = 9;
        public RoundTextBox()
        {
            BorderStyle = BorderStyle.None; AutoSize = false; Height = 34;
            Font = new Font("Segoe UI", 9.75f);
        }

        [DefaultValue(BorderStyle.None)] public new BorderStyle BorderStyle { get => base.BorderStyle; set => base.BorderStyle = BorderStyle.None; }

        private int PadY => Multiline ? 7 : Math.Max(2, (Height - FontHeight) / 2);

        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); Yenile(); }
        protected override void OnSizeChanged(EventArgs e) { base.OnSizeChanged(e); Yenile(); }
        protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); Yenile(); }
        protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); InputFrame.Frame(this); }
        protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); InputFrame.Frame(this); }
        protected override void OnEnabledChanged(EventArgs e) { base.OnEnabledChanged(e); InputFrame.Frame(this); }
        private void Yenile() { if (IsHandleCreated) InputFrame.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, 0x0027); } // NOSIZE|NOMOVE|NOZORDER|FRAMECHANGED

        [StructLayout(LayoutKind.Sequential)] private struct RECT { public int L, T, R, B; }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0083) // WM_NCCALCSIZE: daxili boşluq üçün kənar sahə ayır
            {
                var r = Marshal.PtrToStructure<RECT>(m.LParam);
                r.L += PadX; r.R -= PadX; r.T += PadY; r.B -= PadY;
                if (r.B < r.T) r.B = r.T;
                Marshal.StructureToPtr(r, m.LParam, false); m.Result = IntPtr.Zero; return;
            }
            if (m.Msg == 0x0085) // WM_NCPAINT
            {
                IntPtr dc = InputFrame.GetWindowDC(Handle);
                try
                {
                    using var g = Graphics.FromHdc(dc);
                    InputFrame.Draw(g, this, new Rectangle(0, 0, Width, Height), new Rectangle(PadX, PadY, Width - PadX * 2, Height - PadY * 2), Focused);
                }
                finally { InputFrame.ReleaseDC(Handle, dc); }
                m.Result = IntPtr.Zero; return;
            }
            base.WndProc(ref m);
        }

        public void OnThemeChanged() { BackColor = Theme.Card; ForeColor = Theme.Ink; InputFrame.Frame(this); }
    }

    /// <summary>Yumru künclü seçim siyahısı (adi ComboBox, kənarı və oxu yenidən çəkilir).</summary>
    [ToolboxItem(true), Description("Yumru künclü ComboBox")]
    public class RoundComboBox : ComboBox, IThemed
    {
        public RoundComboBox() { FlatStyle = FlatStyle.Flat; Font = new Font("Segoe UI", 9.75f); IntegralHeight = false; }

        protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); Invalidate(); }
        protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); Invalidate(); }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x000F || m.Msg == 0x0085) // WM_PAINT / WM_NCPAINT — standart çəkilişin üstündən
            {
                using var g = Graphics.FromHwnd(Handle);
                int bw = SystemInformation.VerticalScrollBarWidth;
                var full = ClientRectangle;
                // ox hissəsini təmizlə, sonra yumru kənarı çək (içindəki mətn toxunulmur)
                var arrow = new Rectangle(full.Right - bw - 2, 1, bw + 1, full.Height - 2);
                using (var b = new SolidBrush(BackColor)) g.FillRectangle(b, arrow);
                InputFrame.Draw(g, this, full, Rectangle.Inflate(new Rectangle(0, 0, full.Width - bw - 2, full.Height), -3, -3), Focused || DroppedDown);
                InputFrame.Chevron(g, arrow, Enabled ? Theme.Muted : Theme.Line);
            }
        }

        public void OnThemeChanged() { BackColor = Theme.Card; ForeColor = Theme.Ink; Invalidate(); }
    }

    /// <summary>Yumru künclü tarix seçici.</summary>
    [ToolboxItem(true), Description("Yumru künclü DateTimePicker")]
    public class RoundDatePicker : DateTimePicker, IThemed
    {
        public RoundDatePicker() { Font = new Font("Segoe UI", 9.75f); Format = DateTimePickerFormat.Custom; CustomFormat = "dd.MM.yyyy"; }

        protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); Invalidate(); }
        protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); Invalidate(); }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x000F)
            {
                using var g = Graphics.FromHwnd(Handle);
                var full = new Rectangle(0, 0, Width, Height);
                int bw = 26; var arrow = new Rectangle(Width - bw - 3, 2, bw, Height - 4);
                using (var b = new SolidBrush(Theme.Card)) g.FillRectangle(b, arrow);
                InputFrame.Draw(g, this, full, Rectangle.Inflate(new Rectangle(0, 0, Width - bw - 3, Height), -3, -3), Focused);
                InputFrame.Chevron(g, arrow, Enabled ? Theme.Muted : Theme.Line);
            }
        }

        public void OnThemeChanged() => Invalidate();
    }
}
