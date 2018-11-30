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
    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// 用来记录from是否打开过
        /// </summary>
        public List<int> IsSelectedList = new List<int>();

        /// <summary>
        /// 选项卡管理器，主要用于动态添加删除选项卡
        /// </summary>
        public TabManager TabManager = new TabManager();

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
            // 选项卡初始化
            TabManager.Init(control:MainTabControl);
            TabManager.MainWindow_Loadd();

            //// 初始默认添加一个TabPage
            //string Title = "新增选项卡 " + (MainTabControl.TabCount + 1).ToString();
            //TabPage MyTabPage = new TabPage(Title);  // 创建TabPage对象
            //MyTabPage.Tag = "WS.Note.EditForm";   // Tag 表示选项卡添加的Form的全称类名
            //// 使用TabControl控件的TabPages 属性的Add方法添加新的选项卡
            //MainTabControl.TabPages.Add(MyTabPage);
            //IsSelectedList.Add(0);

            //// 反射生成窗体  
            //Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(MyTabPage.Tag.ToString());

            ////设置窗体没有边框 加入到选项卡中  
            //fm.FormBorderStyle = FormBorderStyle.None;
            //fm.TopLevel = false;
            //fm.Parent = MainTabControl.SelectedTab;  // 将新生成的Form添加到选择的TabPage里面去
            //fm.ControlBox = false;
            //fm.Dock = DockStyle.Fill;
            //fm.Show();

            //IsSelectedList[0] = 1;
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
            fm.Parent = ((TabControl)sender).SelectedTab;  // 将新生成的Form添加到选择的TabPage里面去
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            IsSelectedList[((TabControl)sender).SelectedIndex] = 1;

        }

        /// <summary>
        /// 选项卡切换
        /// </summary>
        /// <param name="sender">MainTabControl</param>
        /// <param name="e"></param>
        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //// 还存在选项卡
            //if (MainTabControl.SelectedIndex >= 0)
            //{
            //    //只生成一次
            //    if (IsSelectedList[MainTabControl.SelectedIndex] == 0)
            //    {
            //        btn_Click(sender, e);
            //    }
            //    //// 显示选择的选项卡是哪一个
            //    //string P_str_TabName = MainTabControl.SelectedTab.Text;//获取选择的选项卡名称
            //    //MessageBox.Show("您选择的选项卡为：" + P_str_TabName, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);//弹出信息提示
            //}
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

        /// <summary>
        /// 新建文件菜单--菜单>文件>新建
        /// </summary>
        /// <param name="sender">新建文件菜单对象</param>
        /// <param name="e"></param>
        private void 新建ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TabManager.Add();
        }

        /// <summary>
        /// 关闭当前文档（关闭当前选项卡）--菜单>文件>关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabManager.Remove();
        }

        /// <summary>
        /// 文件保存--菜单>文件>保存
        /// </summary>
        /// <param name="sender">文件保存菜单项</param>
        /// <param name="e"></param>
        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }


    /// <summary>
    /// 选项卡管理器，用来封装TabControl
    /// </summary>
    public class TabManager
    {
        /// <summary>
        /// 用来记录from是否打开过
        /// </summary>
        public List<int> IsSelectedList = new List<int>();

        /// <summary>
        /// 外部引用注意是否会被其它人修改
        /// </summary>
        public TabControl TabControl { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="control"></param>
        public void Init(TabControl control)
        {
            TabControl = control;
        }

        /// <summary>
        /// 当主界面加载后调用
        /// </summary>
        public void MainWindow_Loadd()
        {
            // 初始默认添加一个TabPage
            string Title = "新增选项卡 " + (TabControl.TabCount + 1).ToString();
            TabPage MyTabPage = new TabPage(Title);  // 创建TabPage对象
            MyTabPage.Tag = "WS.Note.EditForm";   // Tag 表示选项卡添加的Form的全称类名
            // 使用TabControl控件的TabPages 属性的Add方法添加新的选项卡
            TabControl.TabPages.Add(MyTabPage);
            IsSelectedList.Add(0);

            // 反射生成窗体  
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(MyTabPage.Tag.ToString());

            //设置窗体没有边框 加入到选项卡中  
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = TabControl.SelectedTab;  // 将新生成的Form添加到选择的TabPage里面去
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            IsSelectedList[0] = 1;
        }

        /// <summary>
        /// 末尾添加
        /// </summary>
        public void Add()
        {
            //声明一个字符串变量，用于生成新增选项卡的名称
            string Title = "新增选项卡 " + (TabControl.TabCount + 1).ToString();
            TabPage MyTabPage = new TabPage(Title);  // 创建TabPage对象
            MyTabPage.Tag = "WS.Note.EditForm";   // Tag 表示选项卡添加的Form的全称类名
            // 使用TabControl控件的TabPages 属性的Add方法添加新的选项卡
            TabControl.TabPages.Add(MyTabPage);
            IsSelectedList.Add(0);

            // 反射生成窗体  
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(MyTabPage.Tag.ToString());

            //设置窗体没有边框 加入到选项卡中  
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = MyTabPage;  // 将新生成的Form添加到选择的TabPage里面去
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            IsSelectedList[IsSelectedList.Count-1] = 1;
            ////MessageBox.Show("现有" + TabControl.TabCount + "个选项卡");  //获取选项卡个数
        }

        /// <summary>
        /// 删除当前选中的选项卡
        /// </summary>
        public void Remove()
        {
            // 删除时判断是否还存在TabPage
            if (TabControl.SelectedIndex > -1)
            {
                //使用TabControl控件的TabPages属性的Remove方法移除指定的选项卡
                int removeId = TabControl.SelectedIndex;
                TabControl.TabPages.Remove(TabControl.SelectedTab);
                IsSelectedList.RemoveAt(removeId);
            }
        }
    }

    /// <summary>
    /// 将选项卡与文件绑定
    /// </summary>
    public class Bundle
    {
        /// <summary>
        /// 是否是新建的
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 绑定的选项卡
        /// </summary>
        public TabPage TabPage { get; set; }

        /// <summary>
        /// 原始文件路径（打开文件的位置）
        /// </summary>
        public string SrcFilePath { get; set; }

        /// <summary>
        /// 选项卡 EditForm 中保存的文本
        /// </summary>
        public string EditText { get; set; }
    }
}
