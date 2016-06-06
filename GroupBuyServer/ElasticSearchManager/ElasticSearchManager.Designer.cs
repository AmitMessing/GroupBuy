namespace ElasticSearchManager
{
    partial class ElasticSearchManager
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
            this.btnDeleteIndex = new System.Windows.Forms.Button();
            this.btnIndexAllProducts = new System.Windows.Forms.Button();
            this.btnCreateIndex = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDeleteIndex
            // 
            this.btnDeleteIndex.Location = new System.Drawing.Point(124, 12);
            this.btnDeleteIndex.Name = "btnDeleteIndex";
            this.btnDeleteIndex.Size = new System.Drawing.Size(107, 23);
            this.btnDeleteIndex.TabIndex = 3;
            this.btnDeleteIndex.Text = "Delete index";
            this.btnDeleteIndex.UseVisualStyleBackColor = true;
            this.btnDeleteIndex.Click += new System.EventHandler(this.btnDeleteIndex_Click);
            // 
            // btnIndexAllProducts
            // 
            this.btnIndexAllProducts.Location = new System.Drawing.Point(240, 12);
            this.btnIndexAllProducts.Name = "btnIndexAllProducts";
            this.btnIndexAllProducts.Size = new System.Drawing.Size(106, 23);
            this.btnIndexAllProducts.TabIndex = 2;
            this.btnIndexAllProducts.Text = "Index all products";
            this.btnIndexAllProducts.UseVisualStyleBackColor = true;
            this.btnIndexAllProducts.Click += new System.EventHandler(this.btnIndexAllProducts_Click);
            // 
            // btnCreateIndex
            // 
            this.btnCreateIndex.Location = new System.Drawing.Point(12, 12);
            this.btnCreateIndex.Name = "btnCreateIndex";
            this.btnCreateIndex.Size = new System.Drawing.Size(106, 23);
            this.btnCreateIndex.TabIndex = 4;
            this.btnCreateIndex.Text = "Create index";
            this.btnCreateIndex.UseVisualStyleBackColor = true;
            this.btnCreateIndex.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // ElasticSearchManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 51);
            this.Controls.Add(this.btnCreateIndex);
            this.Controls.Add(this.btnDeleteIndex);
            this.Controls.Add(this.btnIndexAllProducts);
            this.MaximumSize = new System.Drawing.Size(374, 89);
            this.MinimumSize = new System.Drawing.Size(374, 89);
            this.Name = "ElasticSearchManager";
            this.Text = "ElasticSearchManager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDeleteIndex;
        private System.Windows.Forms.Button btnIndexAllProducts;
        private System.Windows.Forms.Button btnCreateIndex;

    }
}

