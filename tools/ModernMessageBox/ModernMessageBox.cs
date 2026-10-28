// ModernMessageBox — Təmir Reyestri-dəki təsdiq pəncərəsinin asılılıqsız (tək fayllı) variantı.
// Standart System.Windows.Forms.MessageBox-un bütün imkanları: eyni Show imzaları (sahibli və sahibsiz), bütün düymə dəstləri,
// nişanlar, defolt düymə, seçimlər (RightAlign, RtlReading, DefaultDesktopOnly, ServiceNotification), Kömək düyməsi və kömək faylı,
// sistem səsi, Ctrl+C ilə mətni kopyalama, Esc / Alt+F4 / Enter / F1, sahibsiz çağırışda aktiv pəncərənin üstündə açılma.
// İstifadə qaydası: README.md (eyni qovluqda).

using System.Drawing.Drawing2D;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ModernUI
{
    public static class ModernMessageBox
    {
        // ===================== Görünüş (öz proqramınıza uyğun dəyişin)
        public static Color Primary = ColorTranslator.FromHtml("#0F4C4A");      // əsas düymə
        public static Color Secondary = ColorTranslator.FromHtml("#D4E2E0");    // digər düymələr
        public static Color SecondaryText = ColorTranslator.FromHtml("#0F4C4A");
        public static Color Danger = ColorTranslator.FromHtml("#C94A44");       // xəbərdarlıq/xəta ilə təsdiq düyməsi
        public static Color Card = Color.White;                                 // fon
        public static Color Border = ColorTranslator.FromHtml("#D4E2E0");       // çərçivə
        public static Color TitleColor = ColorTranslator.FromHtml("#0F4C4A");   // başlıq
        public static Color TextColor = ColorTranslator.FromHtml("#1B2B2A");    // mətn
        public static string TitleFont = "Bahnschrift SemiBold", TextFont = "Segoe UI";
        public static int Width = 440;                                          // pəncərənin eni (uzun mətn sətirlərə bölünür)
        public static bool PlaySound = true;                                    // nişana görə Windows səsi

        /// <summary>Düymələrin mətni (dil dəyişəndə buranı yeniləyin).</summary>
        public static readonly Dictionary<DialogResult, string> ButtonText = new()
        {
            [DialogResult.OK] = "OK", [DialogResult.Cancel] = "Ləğv et", [DialogResult.Yes] = "Bəli", [DialogResult.No] = "Xeyr",
            [DialogResult.Retry] = "Təkrar", [DialogResult.Abort] = "Dayandır", [DialogResult.Ignore] = "Keç",
            [DialogResult.TryAgain] = "Yenidən cəhd et", [DialogResult.Continue] = "Davam et",
        };
        public static string HelpText = "Kömək";

        private static readonly IWin32Window NoOwner = null;

        // ===================== MessageBox.Show ilə eyni imzalar (sahibsiz)
        public static DialogResult Show(string text) => Show(NoOwner, text);
        public static DialogResult Show(string text, string caption) => Show(NoOwner, text, caption);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons) => Show(NoOwner, text, caption, buttons);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) => Show(NoOwner, text, caption, buttons, icon);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            => Show(NoOwner, text, caption, buttons, icon, defaultButton);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
            => Show(NoOwner, text, caption, buttons, icon, defaultButton, options);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
            => Goster(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new Komek() : null);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
            => Show(NoOwner, text, caption, buttons, icon, defaultButton, options, helpFilePath);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
            => Show(NoOwner, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
            => Show(NoOwner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator);
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
            => Show(NoOwner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param);

        // ===================== Sahibli (owner) imzalar
        public static DialogResult Show(IWin32Window owner, string text) => Show(owner, text, "");
        public static DialogResult Show(IWin32Window owner, string text, string caption) => Show(owner, text, caption, MessageBoxButtons.OK);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons) => Show(owner, text, caption, buttons, MessageBoxIcon.None);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => Show(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
            => Show(owner, text, caption, buttons, icon, defaultButton, 0);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
            => Goster(owner, text, caption, buttons, icon, defaultButton, options, null);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
            => Goster(owner, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new Komek() : null);
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
            => Goster(owner, text, caption, buttons, icon, defaultButton, options, new Komek { Fayl = helpFilePath });
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
            => Goster(owner, text, caption, buttons, icon, defaultButton, options, new Komek { Fayl = helpFilePath, Acar = keyword });
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
            => Goster(owner, text, caption, buttons, icon, defaultButton, options, new Komek { Fayl = helpFilePath, Nav = navigator });
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
            => Goster(owner, text, caption, buttons, icon, defaultButton, options, new Komek { Fayl = helpFilePath, Nav = navigator, Param = param });

        // ===================== Daxili
        private sealed class Komek { public string Fayl, Acar; public HelpNavigator? Nav; public object Param; }

        private static DialogResult Goster(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                                           MessageBoxDefaultButton defaultButton, MessageBoxOptions options, Komek komek)
        {
            // Standart MessageBox kimi: DefaultDesktopOnly/ServiceNotification sahibi qəbul etmir və pəncərə hamısının üstündə açılır
            bool ustde = (options & (MessageBoxOptions.DefaultDesktopOnly | MessageBoxOptions.ServiceNotification)) != 0;
            if (ustde && owner != null) throw new ArgumentException("DefaultDesktopOnly və ServiceNotification seçimləri ilə owner verilə bilməz.", nameof(owner));
            owner ??= ustde ? null : Form.ActiveForm;
            using var f = new Pencere(owner, text ?? "", caption ?? "", buttons, icon, defaultButton, options, komek);
            if (ustde) f.TopMost = true;
            if (PlaySound) Ses(icon);
            if (owner == null) { f.StartPosition = FormStartPosition.CenterScreen; return f.ShowDialog(); }
            return f.ShowDialog(owner);
        }

        private static void Ses(MessageBoxIcon i)
        {
            switch (i)
            {
                case MessageBoxIcon.Error: SystemSounds.Hand.Play(); break;           // Error = Hand = Stop
                case MessageBoxIcon.Warning: SystemSounds.Exclamation.Play(); break;  // Warning = Exclamation
                case MessageBoxIcon.Information: SystemSounds.Asterisk.Play(); break; // Information = Asterisk
                case MessageBoxIcon.Question: SystemSounds.Question.Play(); break;
                default: SystemSounds.Beep.Play(); break;
            }
        }

        private sealed class Pencere : Form
        {
            private readonly IWin32Window _owner; private readonly Komek _komek;
            private readonly string _metn, _basliq; private readonly DialogResult[] _neticeler;

            public Pencere(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
                           MessageBoxDefaultButton defaultButton, MessageBoxOptions options, Komek komek)
            {
                _owner = owner; _komek = komek; _metn = text; _basliq = caption; Text = caption;
                FormBorderStyle = FormBorderStyle.None; StartPosition = FormStartPosition.CenterParent; ShowInTaskbar = owner == null;
                AutoScaleMode = AutoScaleMode.Dpi; AutoScaleDimensions = new SizeF(96F, 96F);
                BackColor = Border; Padding = new Padding(1); KeyPreview = true;
                bool sag = (options & MessageBoxOptions.RightAlign) != 0, rtl = (options & MessageBoxOptions.RtlReading) != 0;
                var body = new Panel { Dock = DockStyle.Fill, BackColor = Card };
                Controls.Add(body);

                int W = ModernMessageBox.Width, x0 = 24;
                var (simvol, renk) = Simvol(icon);
                Label ikon = null;
                if (simvol != null) { ikon = new Label { Text = simvol, Font = new Font("Segoe MDL2 Assets", 20f), ForeColor = renk, AutoSize = true, Location = new Point(22, 22), BackColor = Card }; body.Controls.Add(ikon); x0 = 66; }
                var lt = new Label { Text = caption, Font = new Font(TitleFont, 15f), ForeColor = TitleColor, AutoSize = false, Location = new Point(x0, 20), Size = new Size(W - x0 - 24, 30),
                                     AutoEllipsis = true, BackColor = Card, UseMnemonic = false, Visible = caption.Length > 0 };
                int ty = caption.Length == 0 ? 26 : 62;
                var lm = new Label { Text = text, Font = new Font(TextFont, 10f), ForeColor = TextColor, AutoSize = false, Location = new Point(x0 + 2, ty), BackColor = Card, UseMnemonic = false };
                int mw = W - x0 - 26;
                int mh = Math.Max(24, TextRenderer.MeasureText(text, lm.Font, new Size(mw, 100000), TextFormatFlags.WordBreak).Height + 4);
                int maxH = Screen.FromPoint(Cursor.Position).WorkingArea.Height - 220;
                Control mesaj = lm;
                if (mh > maxH)
                {
                    // Çox uzun mətn: sürüşdürmə zolağı ilə
                    mesaj = new TextBox { Text = text.Replace("\r\n", "\n").Replace("\n", "\r\n"), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, BorderStyle = BorderStyle.None,
                                          BackColor = Card, ForeColor = TextColor, Font = lm.Font, Location = lm.Location, Size = new Size(mw, maxH), TabStop = false };
                }
                else lm.Size = new Size(mw, mh);
                body.Controls.Add(lt); body.Controls.Add(mesaj);
                if (sag || rtl) { lt.TextAlign = ContentAlignment.MiddleRight; lm.TextAlign = ContentAlignment.TopRight; }
                if (rtl)
                {
                    lm.RightToLeft = lt.RightToLeft = RightToLeft.Yes;
                    // RTL: nişan sağda, mətn solda
                    if (ikon != null) ikon.Left = W - 22 - ikon.Width;
                    lt.Left = 24; mesaj.Left = 24;
                }

                _neticeler = buttons switch
                {
                    MessageBoxButtons.OKCancel => new[] { DialogResult.OK, DialogResult.Cancel },
                    MessageBoxButtons.YesNo => new[] { DialogResult.Yes, DialogResult.No },
                    MessageBoxButtons.YesNoCancel => new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel },
                    MessageBoxButtons.RetryCancel => new[] { DialogResult.Retry, DialogResult.Cancel },
                    MessageBoxButtons.AbortRetryIgnore => new[] { DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore },
                    MessageBoxButtons.CancelTryContinue => new[] { DialogResult.Cancel, DialogResult.TryAgain, DialogResult.Continue },
                    _ => new[] { DialogResult.OK }
                };
                bool tehlukeli = icon is MessageBoxIcon.Warning or MessageBoxIcon.Error && _neticeler.Length > 1;

                // Düymələr standart sıra ilə (Button1, Button2, ...) soldan sağa; Kömək ən sonda. Defolt düymə əsas rəngdədir.
                int def = defaultButton switch { MessageBoxDefaultButton.Button2 => 1, MessageBoxDefaultButton.Button3 => 2, MessageBoxDefaultButton.Button4 => 3, _ => 0 };
                var duymeler = new List<Duyme>();
                foreach (var r in _neticeler) duymeler.Add(new Duyme { Text = ButtonText[r], DialogResult = r });
                if (komek != null) { var h = new Duyme { Text = HelpText }; h.Click += (_, _) => KomekGoster(); duymeler.Add(h); }
                if (def >= duymeler.Count) def = 0;
                for (int i = 0; i < duymeler.Count; i++)
                {
                    bool esas = i == def && duymeler[i].DialogResult != DialogResult.None;
                    duymeler[i].Back = esas ? (tehlukeli ? Danger : Primary) : Secondary;
                    duymeler[i].Fore = esas ? Color.White : SecondaryText;
                    duymeler[i].Size = new Size(Math.Max(100, TextRenderer.MeasureText(duymeler[i].Text, duymeler[i].Font).Width + 36), 36);
                    duymeler[i].TabIndex = i;
                }
                int by = mesaj.Bottom + 16, cem = duymeler.Sum(b => b.Width) + 10 * (duymeler.Count - 1);
                if (cem > W - 48) W = cem + 48;   // düymələr sığmırsa pəncərə genişlənir
                int bx = rtl ? 24 : W - 24 - cem;
                var sira = rtl ? Enumerable.Reverse(duymeler) : duymeler;
                foreach (var b in sira) { b.Location = new Point(bx, by); bx += b.Width + 10; body.Controls.Add(b); }

                var secilen = duymeler[def];
                if (secilen.DialogResult != DialogResult.None) AcceptButton = secilen;
                Shown += (_, _) => { Activate(); secilen.Focus(); };
                // Esc / Alt+F4: standart kimi — Cancel varsa Cancel; yalnız OK varsa OK; əks halda (YesNo, AbortRetryIgnore) bağlanmır
                DialogResult? esc = _neticeler.Contains(DialogResult.Cancel) ? DialogResult.Cancel : _neticeler.Length == 1 ? DialogResult.OK : null;
                FormClosing += (_, e) =>
                {
                    if (e.CloseReason != CloseReason.UserClosing || _neticeler.Contains(DialogResult)) return;
                    if (esc is DialogResult x) DialogResult = x; else e.Cancel = true;
                };
                KeyDown += (_, e) =>
                {
                    if (e.KeyCode == Keys.Escape) { if (esc is DialogResult x) DialogResult = x; e.Handled = true; }
                    else if (e.Control && e.KeyCode is Keys.C or Keys.Insert) { Kopyala(); e.Handled = true; }   // Ctrl+C: standart formatda mətn
                    else if (e.KeyCode == Keys.F1 && komek != null) { KomekGoster(); e.Handled = true; }
                    else if (e.KeyCode is Keys.Left or Keys.Right && ActiveControl is Duyme d)   // oxlarla düymələr arasında
                    {
                        int i = duymeler.IndexOf(d) + (e.KeyCode == Keys.Right ^ rtl ? 1 : -1);
                        if (i >= 0 && i < duymeler.Count) duymeler[i].Focus();
                        e.Handled = true;
                    }
                };
                ClientSize = new Size(W + 2, by + 36 + 18);

                // Başlıqdan sürüşdürmək
                foreach (Control c in new Control[] { body, lt }) c.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, 0xA1, 2, 0); } };
            }

            private void Kopyala()
            {
                const string xett = "---------------------------";
                string d = string.Join("   ", _neticeler.Select(r => ButtonText[r]));
                string s = $"{xett}\r\n{_basliq}\r\n{xett}\r\n{_metn}\r\n{xett}\r\n{d}\r\n{xett}\r\n";
                void Yaz() { try { Clipboard.SetText(s); } catch (ExternalException) { } }
                if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) Yaz();
                else { var t = new Thread(Yaz); t.SetApartmentState(ApartmentState.STA); t.Start(); t.Join(); }   // [STAThread] olmayan proqramda da işləsin
            }

            /// <summary>Kömək faylı verilibsə açılır; verilməyibsə sahib pəncərənin HelpRequested hadisəsi işə düşür.</summary>
            private void KomekGoster()
            {
                var ctl = _owner as Control ?? (_owner != null ? Control.FromHandle(_owner.Handle) : null);
                if (!string.IsNullOrEmpty(_komek?.Fayl))
                {
                    if (_komek.Nav is HelpNavigator n) Help.ShowHelp(ctl, _komek.Fayl, n, _komek.Param);
                    else if (_komek.Acar != null) Help.ShowHelp(ctl, _komek.Fayl, _komek.Acar);
                    else Help.ShowHelp(ctl, _komek.Fayl);
                }
                else if (ctl != null)
                    typeof(Control).GetMethod("OnHelpRequested", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(ctl, new object[] { new HelpEventArgs(Cursor.Position) });
            }

            private static (string, Color) Simvol(MessageBoxIcon i) => i switch
            {
                MessageBoxIcon.Error => ("", ColorTranslator.FromHtml("#C94A44")),       // Error = Hand = Stop
                MessageBoxIcon.Warning => ("", ColorTranslator.FromHtml("#E08A00")),     // Warning = Exclamation
                MessageBoxIcon.Information => ("", ColorTranslator.FromHtml("#3C8DBC")), // Information = Asterisk
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
                Font = new Font("Segoe UI Semibold", 9.5f); Cursor = Cursors.Hand; FlatStyle = FlatStyle.Flat; FlatAppearance.BorderSize = 0; UseMnemonic = false;
            }
            protected override void OnMouseEnter(EventArgs e) { _ust = true; Invalidate(); base.OnMouseEnter(e); }
            protected override void OnMouseLeave(EventArgs e) { _ust = false; Invalidate(); base.OnMouseLeave(e); }
            protected override void OnMouseDown(MouseEventArgs e) { _bas = true; Invalidate(); base.OnMouseDown(e); }
            protected override void OnMouseUp(MouseEventArgs e) { _bas = false; Invalidate(); base.OnMouseUp(e); }
            protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
            protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }
            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Parent?.BackColor ?? Card);
                Color b = _bas ? Qarisiq(Back, Color.Black, 0.15f) : _ust ? Qarisiq(Back, Color.White, 0.12f) : Back;
                using (var p = Yumru(new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f), 8)) using (var br = new SolidBrush(b)) g.FillPath(br, p);
                TextRenderer.DrawText(g, Text, Font, ClientRectangle, Fore, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                // Fokus: klaviatura ilə seçilən düymə (Tab / ox) halqa ilə görünür
                if (Focused) using (var pen = new Pen(Qarisiq(Fore, b, 0.35f), 1.5f)) using (var p = Yumru(new RectangleF(3f, 3f, Width - 7f, Height - 7f), 6)) g.DrawPath(pen, p);
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
