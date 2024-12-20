namespace AkilliAlisveris
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dataGridViewProducts = new DataGridView();
            listBoxShoppingList = new ListBox();
            buttonAddToList = new Button();
            buttonCreateReport = new Button();
            labelTotalPrice = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewProducts.Location = new Point(26, 28);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.RowTemplate.Height = 24;
            dataGridViewProducts.Size = new Size(788, 375);
            dataGridViewProducts.TabIndex = 0;
            // 
            // listBoxShoppingList
            // 
            listBoxShoppingList.FormattingEnabled = true;
            listBoxShoppingList.ItemHeight = 15;
            listBoxShoppingList.Location = new Point(831, 28);
            listBoxShoppingList.Name = "listBoxShoppingList";
            listBoxShoppingList.Size = new Size(263, 244);
            listBoxShoppingList.TabIndex = 1;
            // 
            // buttonAddToList
            // 
            buttonAddToList.Location = new Point(831, 300);
            buttonAddToList.Name = "buttonAddToList";
            buttonAddToList.Size = new Size(105, 38);
            buttonAddToList.TabIndex = 2;
            buttonAddToList.Text = "Listeye Ekle";
            buttonAddToList.UseVisualStyleBackColor = true;
            buttonAddToList.Click += buttonAddToList_Click;
            // 
            // buttonCreateReport
            // 
            buttonCreateReport.Location = new Point(989, 394);
            buttonCreateReport.Name = "buttonCreateReport";
            buttonCreateReport.Size = new Size(105, 38);
            buttonCreateReport.TabIndex = 3;
            buttonCreateReport.Text = "Listeyi Oluştur";
            buttonCreateReport.UseVisualStyleBackColor = true;
            buttonCreateReport.Click += buttonCreateReport_Click;
            // 
            // labelTotalPrice
            // 
            labelTotalPrice.AutoSize = true;
            labelTotalPrice.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTotalPrice.Location = new Point(831, 356);
            labelTotalPrice.Name = "labelTotalPrice";
            labelTotalPrice.Size = new Size(329, 19);
            labelTotalPrice.TabIndex = 4;
            labelTotalPrice.Text = "Toplam Tutar: 0.00 ₺ | Gidilecek Marketler: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1220, 502);
            Controls.Add(labelTotalPrice);
            Controls.Add(buttonCreateReport);
            Controls.Add(buttonAddToList);
            Controls.Add(listBoxShoppingList);
            Controls.Add(dataGridViewProducts);
            Name = "Form1";
            Text = "Akıllı Alışveriş Uygulaması";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.ListBox listBoxShoppingList;
        private System.Windows.Forms.Button buttonAddToList;
        private System.Windows.Forms.Button buttonCreateReport;
        private System.Windows.Forms.Label labelTotalPrice;
    }
}
