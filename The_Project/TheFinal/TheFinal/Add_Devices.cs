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
    public partial class Add_Devices_Screen : Form
    {   // سلسلة الاتصال بقاعدة البيانات
        private string connectionString = @"server = DesktopKareem\SQLEXPRESS;database=DataFinal;Integrated Security=True";
        private int nextID = 1; // تعريف متغير يبدا ب1 الذي يحتوي على الرقم التسلسلي للجهاز المضاف

        public Add_Devices_Screen()
        {
            InitializeComponent();
        }

        private int GetMaxNumber() // دالة تعيد الرقم الأكبر للجهاز الموجود في قاعدة البيانات
        {
            int maxNum = 1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string SQL = "SELECT MAX(ID) FROM DataPhone";
                SqlCommand cmd = new SqlCommand(SQL, connection);
                connection.Open();
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    maxNum = Convert.ToInt32(result) + 1; // تحويل النتيجة إلى عدد صحيح وإضافة واحد للحصول على القيمة القصوى التالية
                }
            }
            return maxNum;
        }

        private void Save() // دالة تقوم بحفظ بيانات الجهاز المدخلة في قاعدة البيانات
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string SQL = "INSERT INTO DataPhone (ID, CustomerName, DeviceName, PhoneNumber, PhoneStatus, DateOfAddition, Category) " +
                             "VALUES (@ID, @CustomerName, @DeviceName, @PhoneNumber, @PhoneStatus, @DateOfAddition, @Category)";
                SqlCommand cmd = new SqlCommand(SQL, connection);
                cmd.CommandType = CommandType.Text; // SQL تحديد نوع الأمر كنص
                // تعيين قيم المعاملات في الاستعلام
                cmd.Parameters.AddWithValue("@ID", nextID);
                cmd.Parameters.AddWithValue("@CustomerName", TextBoxFullName.Text);
                cmd.Parameters.AddWithValue("@DeviceName", TextBoxNameDevice.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", TextBoxPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@PhoneStatus", TextBoxDeviceStatus.Text);
                cmd.Parameters.AddWithValue("@DateOfAddition", dateTimeDevice.Value);
                cmd.Parameters.AddWithValue("@Category", comboBoxCategory.Text);
                connection.Open(); // فتح الاتصال بقاعدة البيانات
                cmd.ExecuteNonQuery(); // تنفيذ الاستعلام
                MessageBox.Show("تم إضافة جهاز جديد"); // عرض رسالة تأكيد عند إضافة الجهاز بنجاح

                ClearFields(); // استدعاء دالة لمسح قيم الحقول في واجهة المستخدم
                nextID++; // زيادة قيمة الأي دي بمقدار 1 وتحديث حقل العرض المخصص لها
                textBoxNoID.Text = nextID.ToString(); 
            }
        }

        private void Button_Add_Device_Click(object sender, EventArgs e)
        {   //  تقوم بالتحقق من الحقول المدخلة وثم حفظ 
            Save();
        }

        private void Add_Devices_Screen_Load(object sender, EventArgs e)
        {   // دالة تستدعى عند تحميل نافذة إضافة الأجهزة. تقوم بتعيين الرقم التسلسلي المقبل وعرضه في حقل الرقم
            nextID = GetMaxNumber();
            textBoxNoID.Text = nextID.ToString();
        }

        private void ClearFields() // دالة تقوم بمسح محتوى جميع الحقول في نافذة إضافة الأجهزة
        {
            TextBoxFullName.Text = "";
            TextBoxNameDevice.Text = "";
            TextBoxPhoneNumber.Text = "";
            TextBoxDeviceStatus.Text = "";
            dateTimeDevice.Value = DateTime.Now;
            comboBoxCategory.SelectedIndex = -1;
        }
    }
}
