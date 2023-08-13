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
    public partial class Screen_Home : Form
    {
        public Screen_Home()
        {
            InitializeComponent(); //تقوم بتهيئة المكونات المرئية والتحكمات التي تم إضافتها إلى نافذة التطبيق
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Screen_Add screen_add = new Screen_Add();
            screen_add.ShowDialog();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScreenSales screenSales = new ScreenSales();
            screenSales.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void panel_Control_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_add_devices_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            ScreenShowSell screenShow = new ScreenShowSell();
            screenShow.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ScreenShowMerchandise screenStoreMerchandise = new ScreenShowMerchandise();
            screenStoreMerchandise.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ScreenShowDevices screenShowDevices = new ScreenShowDevices();
            screenShowDevices.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
