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
using System.Windows.Forms.DataVisualization.Charting;
namespace NetCafeManager.UserControls
{
    public partial class UC_Setting : UserControl
    {
        private byte[] productImageBytes = null;
        private Dictionary<string, int> feedbackIdMap;
        public UC_Setting()
        {
            InitializeComponent();
            LoadServiceNames();
            LoadRevenueChart();
            LoadFeedback();
        }
        private void LoadRevenueChart()
        {
            string query = "SELECT TotalFoodRevenue, TotalTimeRevenue FROM Revenue";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No revenue data available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow row = dt.Rows[0];
            decimal totalFoodRevenue = row["TotalFoodRevenue"] != DBNull.Value ? Convert.ToDecimal(row["TotalFoodRevenue"]) : 0;
            decimal totalTimeRevenue = row["TotalTimeRevenue"] != DBNull.Value ? Convert.ToDecimal(row["TotalTimeRevenue"]) : 0;

            Chart revenueChart = new Chart();
            revenueChart.Size = new Size(ChartPanel.Width - 20, ChartPanel.Height - 20);
            revenueChart.Location = new Point(10, 10);
            revenueChart.BackColor = Color.FromArgb(50, 50, 50);

            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.FromArgb(50, 50, 50);
            revenueChart.ChartAreas.Add(chartArea);

            Series series = new Series("Revenue");
            series.ChartType = SeriesChartType.Pie;
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.White;
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            if (totalFoodRevenue > 0)
                series.Points.AddXY("Food Revenue", totalFoodRevenue);
            if (totalTimeRevenue > 0)
                series.Points.AddXY("Time Revenue", totalTimeRevenue);

            if (series.Points.Count > 0)
            {
                series.Points[0].Color = Color.FromArgb(19, 250, 168);
                if (series.Points.Count > 1)
                    series.Points[1].Color = Color.FromArgb(94, 148, 255);
            }

            revenueChart.Series.Add(series);

            Legend legend = new Legend();
            legend.BackColor = Color.FromArgb(50, 50, 50);
            legend.ForeColor = Color.White;
            legend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            revenueChart.Legends.Add(legend);

            ChartPanel.Controls.Clear();
            ChartPanel.Controls.Add(revenueChart);
        }
        private void LoadServiceNames()
        {
            string query = "SELECT Name FROM Service";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            ServiceNameComboBox.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ServiceNameComboBox.Items.Add(row["Name"].ToString());
            }
            if (ServiceNameComboBox.Items.Count > 0)
            {
                ServiceNameComboBox.SelectedIndex = 0;
            }
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            AddFoodForm addFoodForm = new AddFoodForm();
            addFoodForm.ShowDialog();
            LoadServiceNames();
        }

        private void ServiceNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServiceNameComboBox.SelectedItem == null) return;

            string selectedServiceName = ServiceNameComboBox.SelectedItem.ToString();
            string query = "SELECT Price, Status, Image FROM Service WHERE Name = @Name";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", selectedServiceName)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                PriceTextBox.Text = row["Price"].ToString();
                StatusCheckBox.Checked = Convert.ToInt32(row["Status"]) == 1;
                if (row["Image"] != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])row["Image"];
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        ptbProductImage.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    ptbProductImage.Image = null;
                }
            }
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
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
                        MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (ServiceNameComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một món ăn để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedServiceName = ServiceNameComboBox.SelectedItem.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa món '{selectedServiceName}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Service WHERE Name = @Name";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Name", selectedServiceName)
                };

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadServiceNames(); 
                }
                else
                {
                    MessageBox.Show("Xóa món ăn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ServiceNameComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một món ăn để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PriceTextBox.Text) || !decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedServiceName = ServiceNameComboBox.SelectedItem.ToString();
            int status = StatusCheckBox.Checked ? 1 : 0;

            string query = "UPDATE Service SET Price = @Price, Status = @Status, Image = @Image WHERE Name = @Name";
            SqlParameter[] parameters;

            if (productImageBytes != null)
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Price", price),
                    new SqlParameter("@Status", status),
                    new SqlParameter("@Image", productImageBytes),
                    new SqlParameter("@Name", selectedServiceName)
                };
            }
            else
            {
                query = "UPDATE Service SET Price = @Price, Status = @Status WHERE Name = @Name";
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@Price", price),
                    new SqlParameter("@Status", status),
                    new SqlParameter("@Name", selectedServiceName)
                };
            }

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Cập nhật món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadServiceNames();
            }
            else
            {
                MessageBox.Show("Cập nhật món ăn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFeedback()
        {
            lstFeedback.Items.Clear();

            if (feedbackIdMap == null)
            {
                feedbackIdMap = new Dictionary<string, int>();
            }
            feedbackIdMap.Clear();

            string query = @"
        SELECT f.FeedbackID, f.Content, f.CreatedAt, c.FullName
        FROM Feedback f
        LEFT JOIN Customer c ON f.UserID = c.UserID
        ORDER BY f.CreatedAt DESC";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                int feedbackID = Convert.ToInt32(row["FeedbackID"]);
                string fullName = row["FullName"] != DBNull.Value ? row["FullName"].ToString() : "Anonymous";
                string createdAt = Convert.ToDateTime(row["CreatedAt"]).ToString("yyyy-MM-dd HH:mm");
                string content = row["Content"].ToString();

                if (fullName.Length > 25)
                    fullName = fullName.Substring(0, 9) + "...";

                if (content.Length > 40)
                    content = content.Substring(0, 22) + "...";

                string nameAndContent = $"{fullName}: {content}";
                string timeLine = $"Time: ({createdAt})";
                lstFeedback.Items.Add(nameAndContent);
                lstFeedback.Items.Add(timeLine);
                feedbackIdMap[nameAndContent] = feedbackID;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFeedback();
            LoadServiceNames();
            LoadRevenueChart();
        }

        private void lstFeedback_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lstFeedback.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
            {
                MessageBox.Show("No item selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                if (index % 2 != 0)
                {
                    index--;
                }

                string nameAndContent = lstFeedback.Items[index].ToString();
                if (!feedbackIdMap.ContainsKey(nameAndContent))
                {
                    MessageBox.Show("Feedback ID not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int feedbackID = feedbackIdMap[nameAndContent];
                string query = "SELECT Content FROM Feedback WHERE FeedbackID = @FeedbackID";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@FeedbackID", feedbackID)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    string fullContent = dt.Rows[0]["Content"].ToString();
                    MessageBox.Show(fullContent, "Feedback Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Feedback not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {

        }
    }
}
