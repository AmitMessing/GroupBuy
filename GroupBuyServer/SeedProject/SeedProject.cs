﻿using System.Windows.Forms;

namespace SeedProject
{
    public partial class SeedProject : Form
    {
        private const string Suucess = " added successfuly!";
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
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            lb_categories.Text = Prossess;
            int seddCategories = SeedLogic.SeddCategories(cb_createBat.Checked);
            lb_categories.Text = seddCategories + Suucess;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            lb_products.Text = Prossess;
            int seddProducts = SeedLogic.SeddProducts();
            lb_products.Text = seddProducts + Suucess;
        }
    }
}
