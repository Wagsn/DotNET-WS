using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Editor
{
    public partial class CmdForm : Form
    {
        private CmdUtils cmd { get; set; } = new CmdUtils();

        public CmdForm()
        {
            InitializeComponent();

            new Thread(new ThreadStart(CmdLoop)).Start();
        }

        /// <summary>
        /// 控制台循环
        /// </summary>
        private void CmdLoop()
        {
            cmd.Loop(this);
        }

        private void ComEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmd.Command(comEdit.Text);
            }
        }
    }
}
