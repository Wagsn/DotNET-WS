using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Reader
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewForm view = new ViewForm();
            view.MdiParent = this;
            view.Dock = DockStyle.Fill;
            view.FormBorderStyle = FormBorderStyle.None;
            view.Show();
            ViewForm = view;
        }

        private ViewForm ViewForm { get; set; }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult DialogResult = OpenFileDialog.ShowDialog();
            if (DialogResult == DialogResult.Cancel)
                return;
            string fileName = this.OpenFileDialog.FileName;
            if (DialogResult == DialogResult.OK && fileName.Length > 0)
            {
                try
                {
                    if (System.IO.File.Exists(fileName))
                    {
                        var richTextBox = (RichTextBox)ViewForm.Controls.Find("RichTextBox", true).FirstOrDefault();
                        if (richTextBox != null)
                        {
                            richTextBox.LoadFile(fileName, RichTextBoxStreamType.PlainText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "发生错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
