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
    public partial class OutputForm : Form
    {
        public OutputForm()
        {
            InitializeComponent();
        }

        public void AppendLine(string text)
        {
            OutputTextBox.AppendText(text + "\r\n");
        }

        public void AppendText(string text)
        {
            OutputTextBox.AppendText(text);
        }
    }
}
