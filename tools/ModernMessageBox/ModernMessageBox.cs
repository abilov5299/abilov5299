// ModernMessageBox — Təmir Reyestri-dəki təsdiq pəncərəsinin asılılıqsız (tək fayllı) variantı.
// İstənilən WinForms (.NET 6/8) layihəsinə bu faylı əlavə edin və MessageBox.Show əvəzinə ModernMessageBox.Show yazın.
//
//   ModernMessageBox.Show("Yadda saxlanıldı");
//   ModernMessageBox.Show(this, "Silinsin?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//   ModernMessageBox.Show(this, "Saxlansın?", "Diqqət", MessageBoxButtons.YesNoCancel);
//
// Qaytarılan DialogResult standart MessageBox ilə eynidir (OK, Cancel, Yes, No, Retry, Abort, Ignore).
// Rənglər, şrift və düymə adları aşağıdakı "Görünüş" bölməsində dəyişdirilir.

using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ModernUI
{
    public static class ModernMessageBox
    {
        // ===================== Görünüş (öz proqramınıza uyğun dəyişin)
        public static Color Primary = ColorTranslator.FromHtml("#0F4C4A");    // əsas düymə
        public static Color Secondary = ColorTranslator.FromHtml("#D4E2E0");  // ikinci düymə
        public static Color SecondaryText = ColorTranslator.FromHtml("#0F4C4A");
        public static Color Danger = ColorTranslator.FromHtml("#C94A44");     // xəbərdarlıq/xəta ilə "Bəli"
        public static Color Card = Color.White;
        public static Color Border = ColorTranslator.FromHtml("#D4E2E0");
        public static Color Title = ColorTranslator.FromHtml("#0F4C4A");
        public static Color TextColor = ColorTranslator.FromHtml("#1B2B2A");
        public static string TitleFont = "Bahnschrift SemiBold", TextFont = "Segoe UI";

        /// <summary>Düymələrin mətni (dil dəyişəndə buranı yeniləyin).</summary>
        public static readonly Dictionary<DialogResult, string> Ad = new()
        {
            [DialogResult.OK] = "OK", [DialogResult.Cancel] = "Ləğv et", [DialogResult.Yes] = "Bəli", [DialogResult.No] = "Xeyr",
            [DialogResult.Retry] = "Təkrar", [DialogResult.Abort] = "Dayandır", [DialogResult.Ignore] = "Keç",
        };

        // ===================== MessageBox.Show ilə eyni imzalar
        public static DialogResult Show(string text) => Show(null, text, "", MessageBoxButtons.OK, MessageBoxIcon.None);
        public static DialogResult Show(string text, string caption) => Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons) => Show(null, text, caption, buttons, MessageBoxIcon.None);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) => Show(null, text, caption, buttons, icon);
        public static DialogResult Show(IWin32Window owner, string text) => Show(owner, text, "", MessageBoxButtons.OK, MessageBoxIcon.None);
        public static DialogResult Show(IWin32Window owner, string text, string caption) => Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons) => Show(owner, text, caption, buttons, MessageBoxIcon.None);

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using var f = new Pencere(text ?? "", caption ?? "", buttons, icon);
            if (owner == null) { f.StartPosition = FormStartPosition.CenterScreen; return f.ShowDialog(); }
            return f.ShowDialog(owner);
        }

        // ===================== Pəncərə
        private sealed class Pencere : Form
        {
            public Pencere(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            {
                FormBorderStyle = FormBorderStyle.None; StartPosition = FormStartPosition.CenterParent; ShowInTaskbar = false;
                AutoScaleMode = AutoScaleMode.Dpi; AutoScaleDimensions = new SizeF(96F, 96F);
                BackColor = Border; Padding = new Padding(1); KeyPreview = true;
                var body = new Panel { Dock = DockStyle.Fill, BackColor = Card };
                Controls.Add(body);

                int W = 440, x0 = 24;
                var (simvol, renk) = Simvol(icon);
                if (simvol != null) { body.Controls.Add(new Label { Text = simvol, Font = new Font("Segoe MDL2 Assets", 20f), ForeColor = renk, AutoSize = true, Location = new Point(22, 22), BackColor = Card }); x0 = 66; }
                if (string.IsNullOrEmpty(caption)) caption = Application.ProductName;
                var lt = new Label { Text = caption, Font = new Font(TitleFont, 15f), ForeColor = Title, AutoSize = true, Location = new Point(x0, 20), BackColor = Card };
                var lm = new Label { Text = text, Font = new Font(TextFont, 10f), ForeColor = TextColor, AutoSize = false, Location = new Point(x0 + 2, 62), BackColor = Card };
                int mw = W - x0 - 26;
                int mh = Math.Max(40, TextRenderer.MeasureText(text, lm.Font, new Size(mw, 4000), TextFormatFlags.WordBreak).Height + 4);
                lm.Size = new Size(mw, Math.Min(mh, 500));
                body.Controls.Add(lt); body.Controls.Add(lm);

                // Düymələr: əsas (birinci, Enter) ən sağda, qalanları onun solunda
                var neticeler = buttons switch
                {
                    MessageBoxButtons.OKCancel => new[] { DialogResult.OK, DialogResult.Cancel },
                    MessageBoxButtons.YesNo => new[] { DialogResult.Yes, DialogResult.No },
                    MessageBoxButtons.YesNoCancel => new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel },
                    MessageBoxButtons.RetryCancel => new[] { DialogResult.Retry, DialogResult.Cancel },
                    MessageBoxButtons.AbortRetryIgnore => new[] { DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore },
                    _ => new[] { DialogResult.OK }
                };
                int by = lm.Bottom + 16, bx = W - 24;
                for (int i = 0; i < neticeler.Length; i++)
                {
                    var r = neticeler[i];
                    bool esas = i == 0;
                    var b = new Duyme
                    {
                        Text = Ad[r], DialogResult = r, Size = new Size(114, 36),
                        Back = esas ? (icon is MessageBoxIcon.Warning or MessageBoxIcon.Error && neticeler.Length > 1 ? Danger : Primary) : Secondary,
                        Fore = esas ? Color.White : SecondaryText,
                    };
                    b.Width = Math.Max(114, TextRenderer.MeasureText(b.Text, b.Font).Width + 36);
                    bx -= b.Width; b.Location = new Point(bx, by); bx -= 10;
                    body.Controls.Add(b);
                    if (esas) AcceptButton = b;
                }
                var esc = neticeler.Contains(DialogResult.Cancel) ? DialogResult.Cancel : neticeler.Contains(DialogResult.No) ? DialogResult.No : neticeler[^1];
                KeyDown += (_, e) => { if (e.KeyCode == Keys.Escape) DialogResult = esc; };
                ClientSize = new Size(W + 2, by + 36 + 18);

                // Başlıqdan sürüşdürmək
                foreach (Control c in new Control[] { body, lt }) c.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, 0xA1, 2, 0); } };
            }

            private static (string, Color) Simvol(MessageBoxIcon i) => i switch
            {
                MessageBoxIcon.Error => ("", ColorTranslator.FromHtml("#C94A44")),
                MessageBoxIcon.Warning => ("", ColorTranslator.FromHtml("#E08A00")),
                MessageBoxIcon.Information => ("", ColorTranslator.FromHtml("#3C8DBC")),
                MessageBoxIcon.Question => ("", ColorTranslator.FromHtml("#0F4C4A")),
                _ => (null, Color.Empty)
            };

            protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ClassStyle |= 0x20000; return cp; } }   // kölgə
            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                if (Environment.OSVersion.Version.Build >= 22000) { int v = 2; DwmSetWindowAttribute(Handle, 33, ref v, sizeof(int)); }   // Windows 11: yumru künc
                else Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width + 1, Height + 1, 16, 16));
            }

            [DllImport("dwmapi.dll")] private static extern int DwmSetWindowAttribute(IntPtr h, int a, ref int v, int s);
            [DllImport("gdi32.dll")] private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int w, int h);
            [DllImport("user32.dll")] private static extern bool ReleaseCapture();
            [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr h, int m, int w, int l);
        }

        /// <summary>Yumru künclü düz düymə.</summary>
        private sealed class Duyme : Button
        {
            public Color Back, Fore; private bool _ust, _bas;
            public Duyme()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
                Font = new Font("Segoe UI Semibold", 9.5f); Cursor = Cursors.Hand; FlatStyle = FlatStyle.Flat; FlatAppearance.BorderSize = 0;
            }
            protected override void OnMouseEnter(EventArgs e) { _ust = true; Invalidate(); base.OnMouseEnter(e); }
            protected override void OnMouseLeave(EventArgs e) { _ust = false; Invalidate(); base.OnMouseLeave(e); }
            protected override void OnMouseDown(MouseEventArgs e) { _bas = true; Invalidate(); base.OnMouseDown(e); }
            protected override void OnMouseUp(MouseEventArgs e) { _bas = false; Invalidate(); base.OnMouseUp(e); }
            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Parent?.BackColor ?? Card);
                Color b = _bas ? Qarisiq(Back, Color.Black, 0.15f) : _ust ? Qarisiq(Back, Color.White, 0.12f) : Back;
                using (var p = Yumru(new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f), 8)) using (var br = new SolidBrush(b)) g.FillPath(br, p);
                TextRenderer.DrawText(g, Text, Font, ClientRectangle, Fore, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                if (Focused && ShowFocusCues) using (var pen = new Pen(Qarisiq(b, Color.White, 0.5f))) using (var p = Yumru(new RectangleF(2.5f, 2.5f, Width - 5.5f, Height - 5.5f), 6)) g.DrawPath(pen, p);
            }
            private static GraphicsPath Yumru(RectangleF r, float d)
            {
                var p = new GraphicsPath(); float k = d * 2;
                p.AddArc(r.X, r.Y, k, k, 180, 90); p.AddArc(r.Right - k, r.Y, k, k, 270, 90);
                p.AddArc(r.Right - k, r.Bottom - k, k, k, 0, 90); p.AddArc(r.X, r.Bottom - k, k, k, 90, 90); p.CloseFigure(); return p;
            }
            private static Color Qarisiq(Color a, Color b, float t) => Color.FromArgb((int)(a.R + (b.R - a.R) * t), (int)(a.G + (b.G - a.G) * t), (int)(a.B + (b.B - a.B) * t));
        }
    }
}
