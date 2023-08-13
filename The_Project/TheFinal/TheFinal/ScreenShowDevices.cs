using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TheFinal
{
    public partial class ScreenShowDevices : Form
    {
        public ScreenShowDevices()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {   // للرجوع للصفحة السابقة
            this.Close();
        }

        // تحميل الشاشة
        private void ScreenShowDevices_Load(object sender, EventArgs e)
        {   
            LoadData();
        }

        // تحميل البيانات من قاعدة البيانات وعرضها في جدول البيانات
        private void LoadData()
        {
            DataBase dataBase = new DataBase();
            dataBase.disconnectFromServer();
            string SQL = "SELECT * FROM DataPhone"; // تعريف استعلام SQL لاسترداد جميع الأجهزة من الجدول DataPhone
            DataTable dataTable = new DataTable(); // إنشاء DataTable لتخزين البيانات المستردة من قاعدة البيانات
            SqlCommand command = new SqlCommand(SQL, dataBase.connectToServer()); // إنشاء كائن SqlCommand وتمرير استعلام SQL وكائن الاتصال بقاعدة البيانات
            SqlDataReader reader = command.ExecuteReader(); // قراءة البيانات المستردة باستخدام SqlDataReader
            dataTable.Load(reader); // تحميل البيانات في DataTable
            dataGridView1.DataSource = dataTable; // عرض البيانات في جدول البيانات
            dataBase.disconnectFromServer(); // قطع الاتصال بقاعدة البيانات
        }

        // لحذف السجل المحدد
        private void button1_Click(object sender, EventArgs e)
        {   // عرض مربع حوار تأكيد الحذف
            DialogResult result = MessageBox.Show("هل أنت متأكد من رغبتك في حذف هذا السجل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            // إذا تم النقر على "نعم" في مربع الحوار
            if (result == DialogResult.Yes)
            {   // سلسلة الاتصال بقاعدة البيانات
                string connectionString = "Data Source=DesktopKareem\\SQLEXPRESS;Initial Catalog=DataFinal;Integrated Security=True";
                // الحصول على صف البيانات المحدد
                int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
                // الحصول على قيمة العمود "ID" للصف المحدد
                int ID = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["ID"].Value);
                // إنشاء اتصال بقاعدة البيانات
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // استعلام SQL لحذف السجل المحدد بناءً على قيمة العمود "ID"
                    string query = $"DELETE FROM DataPhone WHERE ID = {ID}";
                    // إنشاء كائن SqlCommand وتمرير استعلام الحذف وكائن الاتصال بقاعدة البيانات
                    SqlCommand command = new SqlCommand(query, connection);
                    // تنفيذ الاستعلام الذي يقوم بحذف السجل
                    command.ExecuteNonQuery();
                }
                // إعادة تحميل البيانات في جدول البيانات بعد الحذف
                LoadData();
            }
        }
    }
}
