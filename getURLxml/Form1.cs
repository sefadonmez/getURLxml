using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace getURLxml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadXML()
        {
            string url = "https://www.yenitoptanci.com/TicimaxXmlV2/82319370DB1A4D88B463B4DD60599779/";

            try
            {
                // XML verilerini yüklemek için XmlDocument oluştur
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);

                // DataGridView'i temizle
                dataGridViewProducts.Rows.Clear();
                dataGridViewProducts.Columns.Clear();

                // XML verilerini DataGridView'e ekle
                XmlNodeList products = xmlDoc.SelectNodes("//Root//Urunler//Urun");
                foreach (XmlNode product in products)
                {
                    if (dataGridViewProducts.Columns.Count == 0)
                    {
                        // İlk satırı sütun başlıkları olarak ekle
                        foreach (XmlNode node in product.ChildNodes)
                        {
                            dataGridViewProducts.Columns.Add(node.Name, node.Name);
                        }
                    }

                    // Satırı DataGridView'e ekle
                    DataGridViewRow dgvRow = new DataGridViewRow();
                    dgvRow.CreateCells(dataGridViewProducts);

                    int i = 0;
                    foreach (XmlNode node in product.ChildNodes)
                    {
                        dgvRow.Cells[i].Value = node.InnerText;
                        i++;
                    }

                    dataGridViewProducts.Rows.Add(dgvRow);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını göster
                MessageBox.Show("Hata: " + ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadXML();
        }

        private void loadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadXML();
        }
    }
}
