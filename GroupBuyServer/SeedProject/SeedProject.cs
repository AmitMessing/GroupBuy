using System.Windows.Forms;

namespace SeedProject
{
    public partial class SeedProject : Form
    {
        private const string Suucess = "added successfuly!";
        private const string Prossess = "...";
        public SeedLogic SeedLogic { get; set; }

        public SeedProject()
        {
            InitializeComponent();
            SeedLogic = new SeedLogic();
        }

        private void btn_seed_Click(object sender, System.EventArgs e)
        {
            lb_users.Text = Prossess;
            int seddUsers = SeedLogic.SeddUsers();
            lb_users.Text = seddUsers + Suucess;

            lb_categories.Text = Prossess;
            int seddCategories = SeedLogic.SeddCategories();
            lb_categories.Text = seddCategories + Suucess;
        }
    }
}
