using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AkilliAlisveris
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=DESKTOP-GE9UJQI; Database=AkilliAlisveris; Integrated Security=True";
        List<Product> shoppingList = new List<Product>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized; // Tam ekran yap
            LoadProducts();  // Ürünleri yükle
            StyleDataGridView();  // DataGridView'i stilize et
        }

        // Ürünleri ve fiyatları yükle
        private void LoadProducts()
        {
            string query = @"
                SELECT 
                    P.productName AS [Ürün Adı],
                    MAX(CASE WHEN M.marketName = 'A101' THEN P.price ELSE NULL END) AS [A101 Fiyatı],
                    MAX(CASE WHEN M.marketName = 'Bim' THEN P.price ELSE NULL END) AS [Bim Fiyatı],
                    MAX(CASE WHEN M.marketName = 'Şok' THEN P.price ELSE NULL END) AS [Şok Fiyatı]
                FROM 
                    Products P
                JOIN 
                    Markets M ON P.marketId = M.marketId
                GROUP BY 
                    P.productName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Ürünleri DataGridView'e ekle
                dataGridViewProducts.DataSource = dt;
            }
        }

        // DataGridView'i stilize et
        private void StyleDataGridView()
        {
            dataGridViewProducts.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridViewProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dataGridViewProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewProducts.EnableHeadersVisualStyles = false;
            dataGridViewProducts.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridViewProducts.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewProducts.RowTemplate.Height = 40;
        }

        // Ürünleri alışveriş listesine ekle
        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewProducts.SelectedRows[0];
                string productName = selectedRow.Cells["Ürün Adı"].Value.ToString();
                decimal priceA101 = selectedRow.Cells["A101 Fiyatı"].Value != DBNull.Value ? Convert.ToDecimal(selectedRow.Cells["A101 Fiyatı"].Value) : decimal.MaxValue;
                decimal priceBim = selectedRow.Cells["Bim Fiyatı"].Value != DBNull.Value ? Convert.ToDecimal(selectedRow.Cells["Bim Fiyatı"].Value) : decimal.MaxValue;
                decimal priceSok = selectedRow.Cells["Şok Fiyatı"].Value != DBNull.Value ? Convert.ToDecimal(selectedRow.Cells["Şok Fiyatı"].Value) : decimal.MaxValue;

                using (Form marketForm = new Form())
                {
                    marketForm.Text = "Market Seçimi";
                    marketForm.Size = new Size(300, 200);

                    ComboBox comboBox = new ComboBox
                    {
                        DataSource = new List<string> { "A101", "Bim", "Şok" },
                        Location = new Point(50, 50),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    marketForm.Controls.Add(comboBox);

                    Button buttonOk = new Button
                    {
                        Text = "Tamam",
                        Location = new Point(50, 100)
                    };
                    buttonOk.Click += (s, eArgs) => { marketForm.DialogResult = DialogResult.OK; marketForm.Close(); };
                    marketForm.Controls.Add(buttonOk);

                    if (marketForm.ShowDialog() == DialogResult.OK)
                    {
                        string selectedMarket = comboBox.SelectedItem.ToString();
                        decimal selectedPrice = selectedMarket == "A101" ? priceA101
                                           : selectedMarket == "Bim" ? priceBim
                                           : selectedMarket == "Şok" ? priceSok
                                           : 0;

                        var product = new Product
                        {
                            Name = productName,
                            Price = selectedPrice,
                            Market = selectedMarket
                        };
                        shoppingList.Add(product);

                        string shoppingItem = $"{productName} - {selectedPrice:C} ({selectedMarket})";
                        listBoxShoppingList.Items.Add(shoppingItem); // Alışveriş listesine ekle

                        UpdateTotalPriceAndMarkets(); // Toplam fiyatı ve marketleri güncelle
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ürün seçin.");
            }
        }

        // Toplam fiyatı ve marketleri güncelle
        private void UpdateTotalPriceAndMarkets()
        {
            decimal totalPrice = shoppingList.Sum(p => p.Price);
            var markets = shoppingList.Select(p => p.Market).Distinct();
            labelTotalPrice.Text = $"Toplam Tutar: {totalPrice:C} | Gidilecek Marketler: {string.Join(", ", markets)}";
        }

        // Alışveriş listesini ve raporu görüntüle
        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            if (shoppingList.Count == 0)
            {
                MessageBox.Show("Lütfen alışveriş listesine ürün ekleyin.");
                return;
            }

            string report = CreateReport();
            MessageBox.Show(report, "Alışveriş Listesi Raporu");

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                FileName = "AlisverisListesiRaporu.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, report);
                MessageBox.Show("Rapor kaydedildi.", "Bilgi");
            }
        }

        // Rapor oluştur
        private string CreateReport()
        {
            decimal totalPrice = shoppingList.Sum(p => p.Price);
            var markets = shoppingList.Select(p => p.Market).Distinct();
            string report = "Alışveriş Listesi Raporu:\n\n";

            foreach (var product in shoppingList)
            {
                report += $"{product.Name} - {product.Price:C} ({product.Market})\n";
            }

            report += $"\nToplam Tutar: {totalPrice:C}\n";
            report += "Gidilecek Marketler: " + string.Join(", ", markets);

            return report;
        }
    }

    // Ürün sınıfı
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Market { get; set; }
    }
}
