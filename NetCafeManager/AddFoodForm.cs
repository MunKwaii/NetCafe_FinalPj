using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager
{
    public partial class AddFoodForm : Form
    {
        private byte[] productImageBytes = null;
        public AddFoodForm()
        {
            InitializeComponent();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddImageBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Select a Product Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ptbProductImage.Image = Image.FromFile(openFileDialog.FileName);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            ptbProductImage.Image.Save(ms, ptbProductImage.Image.RawFormat);
                            productImageBytes = ms.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodNameTextBox.Text))
            {
                MessageBox.Show("Please enter the item name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PriceTextBox.Text) || !decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid price!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productImageBytes == null)
            {
                MessageBox.Show("Please select an image for the item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string checkQuery = "SELECT COUNT(*) FROM Service WHERE Name = @Name";
            SqlParameter[] checkParams = new SqlParameter[]
            {
                new SqlParameter("@Name", FoodNameTextBox.Text)
            };
            int count = (int)DatabaseHelper.ExecuteScalar(checkQuery, checkParams);
            if (count > 0)
            {
                MessageBox.Show("The item name already exists! Please choose a different name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maxIdQuery = "SELECT MAX(ID) FROM Service";
            object maxIdResult = DatabaseHelper.ExecuteScalar(maxIdQuery);
            int newId = 1; 

            if (maxIdResult != DBNull.Value && maxIdResult != null)
            {
                newId = Convert.ToInt32(maxIdResult) + 1;
            }

            string query = "INSERT INTO Service (ID, Name, Price, Image, Status) VALUES (@ID, @Name, @Price, @Image, @Status)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", newId),
                new SqlParameter("@Name", FoodNameTextBox.Text),
                new SqlParameter("@Price", price),
                new SqlParameter("@Image", productImageBytes),
                new SqlParameter("@Status", 1)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Item added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
