using TexnikaReyestri.Controls;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmJournalPage : PageBase
    {
        public AdmJournalPage() { InitializeComponent(); grid.RowTemplate.Height = 32; }
        public override async Task OnShownAsync()
        {
            grid.Rows.Clear();
            foreach (System.Data.DataRow r in (await SistemRepo.AdminJurnalAsync()).Rows) grid.Rows.Add(Vaxt.Yaz((long)r["Tarix"]), r["Kim"], r["Emeliyyat"]);
        }
    }
}
