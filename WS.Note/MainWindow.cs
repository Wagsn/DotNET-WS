using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace WS.Note
{
    public partial class MainWindow : Form
    {
        public int[] IsSelectedList = { 0, 0 };         //用来记录from是否打开过  

        public MainWindow()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /// <summary>
        /// 主窗口加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            //初始打开时就加载Form2  
            string formClass = "WS.Note.EditForm";
            GenerateForm(formClass, MainTabControl);
        }

        /// <summary>
        /// 反射生成窗口，利用反射动态的加载窗体到对应的TabPage的。
        /// </summary>
        /// <param name="formClassName"></param>
        /// <param name="sender"></param>
        public void GenerateForm(string formClassName, object sender)
        {
            // 反射生成窗体  
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(formClassName);

            //设置窗体没有边框 加入到选项卡中  
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            IsSelectedList[((TabControl)sender).SelectedIndex] = 1;

        }

        /// <summary>
        /// 选项卡切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //只生成一次
            if (IsSelectedList[MainTabControl.SelectedIndex] == 0)
            {
                btn_Click(sender, e);
            }
        }

        /// <summary>  
        /// 通用按钮点击选项卡 在选项卡上显示对应的窗体  
        /// </summary>  
        private void btn_Click(object sender, EventArgs e)
        {
            string formClass = ((TabControl)sender).SelectedTab.Tag.ToString();
            GenerateForm(formClass, sender);
        }

        /// <summary>
        /// 菜单-视图-状态栏选则框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 状态栏菜单被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 状态栏ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // MainStatusStrip
            if (!MainStatusStrip.Visible)
            {
                MainStatusStrip.Visible = true;
            }
            else
            {
                MainStatusStrip.Visible = false;
            }
        }

        /// <summary>
        /// 工具栏菜单被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MainToolStrip
            if (!MainToolStrip.Visible)
            {
                MainToolStrip.Visible = true;
            }
            else
            {
                MainToolStrip.Visible = false;
            }
        }
    }
}
