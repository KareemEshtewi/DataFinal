using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinal
{
    class DataBase
    {
        public SqlConnection conn;

        public DataBase()
        {
            // إنشاء كائن SqlConnection وتعيين سلسلة الاتصال بقاعدة البيانات
            conn = new SqlConnection(@"server = DesktopKareem\SQLEXPRESS;database=DataFinal;Integrated Security=True");
        }

        public SqlConnection connectToServer()
        {
            conn.Open(); // فتح اتصال قاعدة البيانات
            return conn; // إرجاع الكائن SqlConnection المفتوح
        }

        public void disconnectFromServer()
        {
            if (conn.State == ConnectionState.Open) // التحقق من أن الاتصال مفتوحًا
                conn.Close(); // إغلاق اتصال قاعدة البيانات
        }
    }
}
