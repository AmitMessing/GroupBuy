namespace SeedProject
{
    partial class SeedProject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_seed = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_users = new System.Windows.Forms.Label();
            this.lb_categories = new System.Windows.Forms.Label();
            this.lb_products = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_seed
            // 
            this.btn_seed.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_seed.Location = new System.Drawing.Point(126, 22);
            this.btn_seed.Name = "btn_seed";
            this.btn_seed.Size = new System.Drawing.Size(93, 45);
            this.btn_seed.TabIndex = 0;
            this.btn_seed.Text = "Seed";
            this.btn_seed.UseVisualStyleBackColor = true;
            this.btn_seed.Click += new System.EventHandler(this.btn_seed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "users:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "categories:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "products:";
            // 
            // lb_users
            // 
            this.lb_users.AutoSize = true;
            this.lb_users.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_users.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_users.Location = new System.Drawing.Point(135, 90);
            this.lb_users.Name = "lb_users";
            this.lb_users.Size = new System.Drawing.Size(32, 17);
            this.lb_users.TabIndex = 4;
            this.lb_users.Text = "wait";
            // 
            // lb_categories
            // 
            this.lb_categories.AutoSize = true;
            this.lb_categories.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_categories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_categories.Location = new System.Drawing.Point(135, 118);
            this.lb_categories.Name = "lb_categories";
            this.lb_categories.Size = new System.Drawing.Size(32, 17);
            this.lb_categories.TabIndex = 5;
            this.lb_categories.Text = "wait";
            // 
            // lb_products
            // 
            this.lb_products.AutoSize = true;
            this.lb_products.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_products.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_products.Location = new System.Drawing.Point(134, 145);
            this.lb_products.Name = "lb_products";
            this.lb_products.Size = new System.Drawing.Size(32, 17);
            this.lb_products.TabIndex = 6;
            this.lb_products.Text = "wait";
            // 
            // SeedProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 187);
            this.Controls.Add(this.lb_products);
            this.Controls.Add(this.lb_categories);
            this.Controls.Add(this.lb_users);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_seed);
            this.Name = "SeedProject";
            this.Text = "Seed Groupbuy DB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_seed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_users;
        private System.Windows.Forms.Label lb_categories;
        private System.Windows.Forms.Label lb_products;

    }
}

