using TexnikaQeydiyyat.Data;

namespace TexnikaQeydiyyat.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            try { await AuthService.SeedAsync(); }
            catch (Exception ex) { lblStatus.Text = "Serverə qoşulmaq olmadı: " + ex.Message; }
        }

        private async void btnDaxil_Click(object sender, EventArgs e)
        {
            btnDaxil.Enabled = false;
            lblStatus.Text = "Yoxlanılır...";
            try
            {
                if (await AuthService.LoginAsync(txtLogin.Text.Trim(), txtParol.Text))
                {
                    DialogResult = DialogResult.OK;
                    return;
                }
                lblStatus.Text = "Login və ya parol yanlışdır.";
            }
            catch (Exception ex) { lblStatus.Text = "Xəta: " + ex.Message; }
            finally { btnDaxil.Enabled = true; }
        }
    }
}
