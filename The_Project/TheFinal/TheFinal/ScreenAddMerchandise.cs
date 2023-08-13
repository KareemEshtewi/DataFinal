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
    public partial class ScreenAddMerchandise : Form
    {   // تعريف سلسلة الاتصال بقاعدة البيانات
        private string connectionString = @"server = DesktopKareem\SQLEXPRESS;database=DataFinal;Integrated Security=True";
        private int nextID = 1; // تعريف المتغير الذي يحدد الرقم التالي للسلعة المضافة

        public ScreenAddMerchandise()
        {
            InitializeComponent();
        }

        private int GetMaxNumber() // للحصول على الرقم الأعلى للسلعة
        {
            int maxNum = 1; //تعريف المتغير المستخدم لتخزين القيمة القصوى
            // فتح اتصال بقاعدة البيانات
            using (SqlConnection connection = new SqlConnection(connectionString))
            {   
                string SQL = "SELECT MAX(NameVariety) FROM DataVarietie";
                SqlCommand cmd = new SqlCommand(SQL, connection);
                connection.Open();
                // استرداد النتيجة والتحقق من عدم كونها فارغة
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {   //تحويل النتيجة إلى عدد صحيح وزيادة القيمة بمقدار واحد
                    maxNum = Convert.ToInt32(result) + 1;
                }
            }
            return maxNum;
        }

        private void Save() // حفظ بيانات السلعة
        {   // إنشاء اتصال بقاعدة البيانات وتنفيذ استعلام INSERT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string SQL = "INSERT INTO DataVarietie (NameVariety, NameMerchant, PhoneNumberMerchant, ItemDetails, dateTimeDeviceVariety, TypeVariety, Lssue) " +
                             "VALUES (@NameVariety, @NameMerchant, @PhoneNumberMerchant, @ItemDetails, @dateTimeDeviceVariety, @TypeVariety, @Lssue)";
                SqlCommand cmd = new SqlCommand(SQL, connection);
                cmd.CommandType = CommandType.Text;
                // تعيين قيم المعاملات (البارامترات) في الاستعلام
                cmd.Parameters.AddWithValue("@NameVariety", nextID);
                cmd.Parameters.AddWithValue("@NameMerchant", TextBoxNameMerchant.Text);
                cmd.Parameters.AddWithValue("@PhoneNumberMerchant", TextBoxPhoneNumberMerchant.Text);
                cmd.Parameters.AddWithValue("@ItemDetails", TextBoxItemDetails.Text);
                cmd.Parameters.AddWithValue("@dateTimeDeviceVariety", dateTimeDeviceVariety.Text);
                cmd.Parameters.AddWithValue("@TypeVariety", comboBoxTypeVariety.Text);
                cmd.Parameters.AddWithValue("@Lssue", textBoxIssue.Text);
                connection.Open();
                // تنفيذ الاستعلام INSERT
                cmd.ExecuteNonQuery();
                MessageBox.Show("تمت الاضافة");

                ClearFields();
                // زيادة قيمة nextID بمقدار واحد وعرضها في TextBoxNameVariety
                nextID++;
                TextBoxNameVariety.Text = nextID.ToString();
            }
        }

        private void Button_Add_Device_Click(object sender, EventArgs e)
        { // النقر على زر إضافة السلعة
            Save();
        }

        private void ScreenAddMerchandise_Load_1(object sender, EventArgs e)
        { // تحميل الشاشة
            nextID = GetMaxNumber();
            TextBoxNameVariety.Text = nextID.ToString();
            // الحصول على الرقم الأعلى للسلعة وعرضه في TextBoxNameVariety
        }

        private void ClearFields() // مسح قيم حقول واجهة المستخدم
        {
            TextBoxNameMerchant.Text = "";
            TextBoxPhoneNumberMerchant.Text = "";
            TextBoxItemDetails.Text = "";
            dateTimeDeviceVariety.Value = DateTime.Now;
            comboBoxTypeVariety.SelectedIndex = -1;
            textBoxIssue.Text = "";
        }
    }
}
