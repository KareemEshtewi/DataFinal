using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheFinal
{
    public partial class Screen_Add : Form
    {
        public Screen_Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Devices_Screen add_Devices_Screen = new Add_Devices_Screen();
            add_Devices_Screen.ShowDialog();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScreenAddMerchandise screenMerchandise = new ScreenAddMerchandise();
            screenMerchandise.ShowDialog();
        }
    }
}
