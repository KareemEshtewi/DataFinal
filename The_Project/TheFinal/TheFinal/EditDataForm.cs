using System;
using System.Windows.Forms;

namespace TheFinal
{
    public partial class EditDataForm : Form
    {
        private Button buttonSave;
        private TextBox textBoxData;
        private System.Windows.Forms.Label labelColumnName;


        public string NewData { get; private set; }

        public EditDataForm(string columnName, string cellValue)
        {
            InitializeComponent();
            labelColumnName.Text = columnName;
            textBoxData.Text = cellValue;
            buttonSave.Click += buttonSave_Click;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            NewData = textBoxData.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void InitializeComponent()
        {
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.labelColumnName = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(101, 158);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(50, 44);
            this.textBoxData.Multiline = true;
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(174, 47);
            this.textBoxData.TabIndex = 1;
            // 
            // EditDataForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.buttonSave);
            this.Name = "EditDataForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
