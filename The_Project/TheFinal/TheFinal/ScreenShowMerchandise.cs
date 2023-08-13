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
    public partial class ScreenShowMerchandise : Form
    {
        public ScreenShowMerchandise()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // الرجوع للصفحة السابقة
        }

        int MaxNumbers() // إرجاع أكبر عدد موجود في جدول
        {
            DataBase DB = new DataBase(); // إنشاء كائن للاتصال بقاعدة البيانات
            DB.disconnectFromServer(); // قطع الاتصال بقاعدة البيانات
            string SQL = "select max(NoID) from DataVarietie"; // استعلام SQL لاسترداد أكبر رقم
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = DB.connectToServer();
            cmd.ExecuteNonQuery();
            var MAXNUm = cmd.ExecuteScalar(); // تنفيذ الاستعلام واسترداد القيمة الناتجة
            return Int32.Parse(MAXNUm.ToString()); // تحويل القيمة إلى نوع بيانات صحيح وإرجاعها
        }

        // البحث في قاعدة البيانات بناءً على القيمة المدخلة في TextBox
        void Search()
        {
            DataBase dataBase = new DataBase(); // إنشاء كائن للاتصال بقاعدة البيانات
            dataBase.disconnectFromServer(); // قطع الاتصال بقاعدة البيانات

            string SQL = "SELECT * FROM DataVarietie where NameVariety like '%" + textBoxSearch.Text + "%'"; // استعلام SQL للبحث

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SQL, dataBase.connectToServer()); // تنفيذ الاستعلام واسترداد البيانات
            SqlDataReader rd = cmd.ExecuteReader();
            dt.Load(rd);

            dataGridView1.DataSource = dt; // عرض البيانات في عنصر التحكم DataGridView

            dataBase.disconnectFromServer(); // قطع الاتصال بقاعدة البيانات
        }

        private void ScreenStoreMerchandise_Load(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase(); // إنشاء كائن للاتصال بقاعدة البيانات
            dataBase.disconnectFromServer(); // قطع الاتصال بقاعدة البيانات
            string SQL = "SELECT * FROM DataVarietie"; // استعلام SQL لاسترداد البيانات
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SQL, dataBase.connectToServer()); // تنفيذ الاستعلام واسترداد البيانات
            SqlDataReader rd = cmd.ExecuteReader();
            dt.Load(rd);

            dataGridView1.DataSource = dt; // عرض البيانات في عنصر التحكم DataGridView

            dataBase.disconnectFromServer(); // قطع الاتصال بقاعدة البيانات
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            Search(); // استدعاء الدالة للبحث عند تغيير النص في TextBox
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonDeleteDevices_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل أنت متأكد من رغبتك في حذف هذا السجل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string connectionString = "Data Source=DesktopKareem\\SQLEXPRESS;Initial Catalog=DataFinal;Integrated Security=True"; // سلسلة الاتصال بقاعدة البيانات
                int selectedRow = dataGridView1.SelectedCells[0].RowIndex; // الحصول على صف السجل المحدد في عنصر التحكم DataGridView
                string nameVariety = dataGridView1.Rows[selectedRow].Cells["NameVariety"].Value.ToString(); // استرداد قيمة العمود "NameVariety"

                using (SqlConnection connection = new SqlConnection(connectionString)) // إنشاء كائن اتصال بقاعدة البيانات
                {
                    connection.Open(); // فتح الاتصال
                    string query = $"DELETE FROM DataVarietie WHERE NameVariety = '{nameVariety}'"; // استعلام SQL لحذف السجل المحدد
                    SqlCommand command = new SqlCommand(query, connection); // تنفيذ الاستعلام
                    command.ExecuteNonQuery();
                }

                LoadData(); // إعادة تحميل البيانات بعد الحذف
            }
        }

        private void LoadData()
        {
            string connectionString = "Data Source=DesktopKareem\\SQLEXPRESS;Initial Catalog=DataFinal;Integrated Security=True"; // سلسلة الاتصال بقاعدة البيانات
            string query = "SELECT * FROM DataVarietie"; // استعلام SQL لاسترداد البيانات

            using (SqlConnection connection = new SqlConnection(connectionString)) // إنشاء كائن اتصال بقاعدة البيانات
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection); // إنشاء كائن محول البيانات
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable); // استرداد البيانات وتعبئة DataTable

                dataGridView1.DataSource = dataTable; // عرض البيانات في عنصر التحكم DataGridView

                // تمكين خاصية ReadOnly لكل خلية بشكل فردي
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.OwningColumn.Name != "NameVariety")
                        {
                            cell.ReadOnly = false;
                        }
                    }
                }
            }
        }

        // النقر على زر تعديل البضاعة
        private void buttonEditMerchandise_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRow = dataGridView1.SelectedRows[0].Index; // الحصول على صف البيانات المحدد
                string columnName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText;
                string cellValue = dataGridView1.Rows[selectedRow].Cells[dataGridView1.CurrentCell.ColumnIndex].Value.ToString();

                using (EditDataForm editForm = new EditDataForm(columnName, cellValue))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        string newData = editForm.NewData; // القيمة الجديدة بعد التعديل
                        dataGridView1.Rows[selectedRow].Cells[dataGridView1.CurrentCell.ColumnIndex].Value = newData;

                        // قم بتنفيذ العمليات اللازمة لتحديث البيانات في قاعدة البيانات

                        MessageBox.Show("تم حفظ البيانات بنجاح.", "تأكيد الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("يرجى تحديد صف واحد على الأقل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
