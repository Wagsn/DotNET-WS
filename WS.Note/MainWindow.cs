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
using System.IO;

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
        }

        /// <summary>
        /// 状态栏菜单--菜单>视图>状态栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// <param name="sender">ToolStripMenuItem</param>
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
            RichTextBox richTextBox = (RichTextBox)MainTabControl.SelectedTab.Controls.Find("RichTextBox", true).FirstOrDefault();

            string content = richTextBox.Text;
            Console.WriteLine("保存的内容为：\r\n"+content);

            // 需要打开时保存文件的路径
            // Save the contents of the RichTextBox into the file.
            // richTextBox.SaveFile("", RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// 打开文件--菜单>文件>打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TabManager.Add();
            RichTextBox richTextBox = (RichTextBox)MainTabControl.SelectedTab.Controls.Find("RichTextBox", true).FirstOrDefault();

            DialogResult DialogResult = OpenFileDialog.ShowDialog();
            if (DialogResult == DialogResult.Cancel)
                return;
            string FileName = this.OpenFileDialog.FileName;
            if (DialogResult == DialogResult.OK && FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                richTextBox.LoadFile(OpenFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        /// <summary>
        /// 将文件另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)MainTabControl.SelectedTab.Controls.Find("RichTextBox", true).FirstOrDefault();

            string content = richTextBox.Text;
            Console.WriteLine("另存的内容为：\r\n" + content);

            if (richTextBox.Text == "")
                return;
            SaveFileDialog.DefaultExt = "txt";
            SaveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            DialogResult DialogResult = SaveFileDialog.ShowDialog();
            if (DialogResult == DialogResult.Cancel)
                return;
            string FileName = this.SaveFileDialog.FileName;
            if (DialogResult == DialogResult.OK && FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                richTextBox.SaveFile(SaveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("文件已成功保存");
            }
        }
    }


    /// <summary>
    /// 选项卡管理器，用来封装TabControl
    /// </summary>
    public class TabManager
    {
        /// <summary>
        /// 外部引用注意是否会被其它人修改
        /// </summary>
        public TabControl TabControl { get; set; }

        /// <summary>
        /// 用来持有TabPage和文件路径，方便添加
        /// </summary>
        public List<Bundle> TabBudles = new List<Bundle>();

        /// <summary>
        /// 初始化，添加TabControl的引用
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
            Add();
        }

        /// <summary>
        /// 末尾添加
        /// </summary>
        public void Add()
        {
            // 声明一个字符串变量，用于生成新增选项卡的名称
            string Title = "新增选项卡 " + (TabControl.TabCount + 1).ToString();
            TabPage page = CreateTabPage(Title, (Form)Assembly.GetExecutingAssembly().CreateInstance("WS.Note.EditForm"));
            TabBudles.Add(new Bundle
            {
                IsNew = true,
                TabPage =page
            });
            // 使用 TabControl 控件的 TabPages 属性的 Add 方法添加新的选项卡
            TabControl.TabPages.Add(page);
            ////MessageBox.Show("现有" + TabControl.TabCount + "个选项卡");  //获取选项卡个数
        }

        /// <summary>
        /// 末尾添加一个指定标题的TabPage
        /// </summary>
        /// <param name="title">TabPage的标题</param>
        public void Add(string title)
        {
            Add(title, "WS.Note.EditForm");
        }

        /// <summary>
        /// 在选项卡列表末尾添加一个TabPage，并填充一个Form
        /// </summary>
        /// <param name="title"></param>
        /// <param name="FormClassName"></param>
        public void Add(string title, string FormClassName)
        {
            Add(title, (Form)Assembly.GetExecutingAssembly().CreateInstance(FormClassName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle">绑定TabPage与FilePath</param>
        public void Add(Bundle bundle)
        {
            // 创建TabPage
            TabPage page = CreateTabPage(bundle.TabTitle, (Form)Assembly.GetExecutingAssembly().CreateInstance("WS.Note.EditForm"));
            if (!string.IsNullOrWhiteSpace(bundle.SrcFilePath)/* && bundle.SrcFilePath.isPath()*/)
            {
                ((RichTextBox)page.Controls.Find("RichTextBox", true).FirstOrDefault()).LoadFile(bundle.SrcFilePath, RichTextBoxStreamType.PlainText);
            }
            bundle.TabPage = page;
        }

        /// <summary>
        /// Tabs末尾追加填充Form的TabPage
        /// </summary>
        /// <param name="title"></param>
        /// <param name="form"></param>
        public void Add(string title, Form form)
        {
            TabControl.TabPages.Add(CreateTabPage(title, form));
        }

        /// <summary>
        /// 将以Form填充的TabPage添加到Tabs的指定位置
        /// 注1：如果索引为负数，将从末尾向前索引
        /// 注2：如果索引超限，将做取模操作
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="form">Form窗口</param>
        /// <param name="position">添加的位置（插入模式）</param>
        public void Add(string title, Form form, int position)
        {
            // 获取自然数索引（从0开始）
            int index = (position % TabControl.TabCount + TabControl.TabCount) % TabControl.TabCount;

            // 使用TabControl控件的TabPages 属性的Add方法添加新的选项卡
            TabControl.TabPages.Insert(index, CreateTabPage(title, form));
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
                int removeIndex = TabControl.SelectedIndex;
                TabControl.TabPages.Remove(TabControl.SelectedTab);
            }
        }

        /// <summary>
        /// 交换两个索引所在TabPage的位置
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void Swap(int index1, int index2)
        {
            // 参数检查 - 索引是否超限 -TODO 取模操作避免超限
            if (index1 < 0 || index1 >= TabControl.TabPages.Count || index1 < 0 || index1 >= TabControl.TabPages.Count)
            {
                throw new IndexOutOfRangeException("Tab Page Index out of the Range");
            }
            // 交换TabPage
            TabPage page = TabControl.TabPages[index1];
            TabControl.TabPages.RemoveAt(index1);
            TabControl.TabPages.Insert(index2, page);
        }

        /// <summary>
        /// 生成一个新的TabPage，用Form填充
        /// </summary>
        /// <param name="title">Tab Page Title</param>
        /// <param name="form">Form</param>
        /// <returns></returns>
        private static TabPage CreateTabPage(string title, Form form)
        {
            TabPage page = new TabPage
            {
                Text = title,
                Tag = form.GetType().FullName
            };
            // 设置窗体没有边框 加入到选项卡中  
            form.FormBorderStyle = FormBorderStyle.None;
            form.TopLevel = false;
            form.Parent = page;  // 将新生成的Form添加到选择的TabPage里面去
            form.ControlBox = false;
            form.Dock = DockStyle.Fill;
            form.Show();

            return page;
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
        /// 选项卡的名字
        /// </summary>
        public string TabTitle { get; set; }

        /// <summary>
        /// 绑定的选项卡
        /// </summary>
        public TabPage TabPage { get; set; }

        /// <summary>
        /// 原始文件路径（打开文件的位置）
        /// </summary>
        public string SrcFilePath { get; set; }

        ///// <summary>
        ///// 文件
        ///// </summary>
        //public FileInfo File { get; set; }

        /// <summary>
        /// 选项卡 EditForm.RichTextBox 中保存的文本
        /// </summary>
        public string EditText { get; set; }
    }
}
