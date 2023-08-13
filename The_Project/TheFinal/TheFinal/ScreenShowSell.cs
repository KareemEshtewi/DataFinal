using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace TheFinal
{
    public partial class ScreenShowSell : Form
    {
        public ScreenShowSell()
        {
            InitializeComponent();
        }

        private void ScreenShowSell_Load(object sender, EventArgs e)
        {
            LoadData(); // تقوم بتحميل البيانات
        }

        private void LoadData() // تقوم بتحميل البيانات من قاعدة البيانات وعرضها في الجدول
        {
            DataBase dataBase = new DataBase(); // إنشاء كائن للاتصال بقاعدة البيانات
            dataBase.disconnectFromServer(); // يتم قطع الاتصال بقاعدة البيانات
            string SQL = "SELECT * FROM DataSell"; //  SQL لاسترداد جميع البيانات من 
            DataTable dataTable = new DataTable(); // لتخزين البيانات المستردة
            SqlCommand command = new SqlCommand(SQL, dataBase.connectToServer()); // يتم انشاء كائن من فئة command لتنفيذ الاستعلام على قاعدة البيانات SqlCommand
            SqlDataReader reader = command.ExecuteReader(); // لقراءة البيانات
            dataTable.Load(reader); // لتحميل البيانات المقروءة
            dataGridView1.DataSource = dataTable; // عرض البيانات في الجدول
            dataBase.disconnectFromServer(); // قطع الاتصال بقاعدة البيانات
        }

        private void deleteButton_Click(object sender, EventArgs e) // زر الحذف
        {   // يتم عرض مربع حوار للمستخدم لتأكيد الحذف
            DialogResult result = MessageBox.Show("هل أنت متأكد من رغبتك في حذف هذا السجل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            // إذا قام المستخدم بتأكيد الحذف، يتم حذف السجل من قاعدة البيانات
            if (result == DialogResult.Yes)
            {   
                string connectionString = "Data Source=DesktopKareem\\SQLEXPRESS;Initial Catalog=DataFinal;Integrated Security=True";
                int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
                int IDNumberSell = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["IDNumberSell"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // فتح الاتصال بقاعدة البيانات
                    // يتم إنشاء استعلام SQL لحذف السجل المحدد استنادًا إلى قيمة "IDNumberSell"
                    string query = $"DELETE FROM DataSell WHERE IDNumberSell = {IDNumberSell}";
                    // لتنفيذ الاستعلام على قاعدة البيانات
                    SqlCommand command = new SqlCommand(query, connection);
                    // يتم تنفيذ الاستعلام على قاعدة البيانات دون استرداد أي بيانات
                    command.ExecuteNonQuery();
                }

                LoadData(); // لإعادة تحميل البيانات بعد الحذف
            }
        }

        private void button2_Click(object sender, EventArgs e) // لاغلاق الصفحة
        {
            this.Close(); 
        }
    }
}
