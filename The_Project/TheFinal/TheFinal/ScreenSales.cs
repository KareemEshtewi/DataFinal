using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheFinal
{
    public partial class ScreenSales : Form
    {   // تعريف سلسلة الاتصال بقاعدة البيانات
        private string connectionString = @"server = DesktopKareem\SQLEXPRESS;database=DataFinal;Integrated Security=True";

        public ScreenSales()
        {
            InitializeComponent();
        }

        private int GetMaxNumber() // الحصول على الرقم الأعلى لعملية البيع
        {
            int maxNum = 1; // تعريف المتغير المستخدم لتخزين القيمة القصوى

            // إنشاء اتصال بقاعدة البيانات وتنفيذ استعلام SELECT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string SQL = "SELECT MAX(IDNumberSell) FROM DataSell";
                SqlCommand cmd = new SqlCommand(SQL, connection);
                connection.Open();
                // استرداد النتيجة والتحقق من عدم كونها فارغة أو DBNull
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {   // تحويل النتيجة إلى عدد صحيح وزيادة القيمة بمقدار واحد
                    maxNum = Convert.ToInt32(result) + 1;
                }
            }
            return maxNum;
        }

        private void Button_Sell_Click(object sender, EventArgs e)
        {   // النقر على زر البيع
            Save();
        }

        private void Save() // حفظ بيانات عملية البيع
        {
            // التحقق من صحة البيانات
            if (!IsValidData())
            {
                MessageBox.Show("يرجى تحديث البيانات المطلوبة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string SQL = "INSERT INTO DataSell (IDNumberSell,Type, Price, Issue, DateOfSale) VALUES (@IDNumberSell,@Type, @Price, @Issue, @DateOfSale)";
                SqlCommand cmd = new SqlCommand(SQL, connection);
                cmd.CommandType = CommandType.Text;

                int maxNumber = GetMaxNumber(); // الحصول على الرقم الأقصى
                int newNumber = maxNumber + 1; // زيادة الرقم بمقدار 1
                cmd.Parameters.AddWithValue("@IDNumberSell", newNumber.ToString()); // تعيين الرقم الجديد
                cmd.Parameters.AddWithValue("@Type", comboBoxType.Text);
                cmd.Parameters.AddWithValue("@Price", TextBoxPrice.Text);
                cmd.Parameters.AddWithValue("@Issue", TextBoxIssue.Text);
                cmd.Parameters.AddWithValue("@DateOfSale", DateOfSale.Text);
                connection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("تم");
            }
        }

        private bool IsValidData() // التحقق من صحة البيانات
        {
            // التحقق من صحة البيانات المطلوبة
            if (string.IsNullOrWhiteSpace(comboBoxType.Text) || string.IsNullOrWhiteSpace(TextBoxPrice.Text) || string.IsNullOrWhiteSpace(TextBoxIssue.Text))
            {
                return false;
            }

            // يمكنك إضافة المزيد من التحققات هنا حسب احتياجاتك

            return true;
        }



        private void ClearFields() // مسح قيم حقول واجهة المستخدم
        {
            IDNumberSell.Text = "";
            comboBoxType.Text = "";
            TextBoxPrice.Text = "";
            TextBoxIssue.Text = "";
            DateOfSale.Value = DateTime.Now;

        }
    }
}
