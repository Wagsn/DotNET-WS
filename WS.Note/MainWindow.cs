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
        /// 窗口GUID
        /// </summary>
        public string WindowId { get; }

        /// <summary>
        /// 选项卡管理器，主要用于动态添加删除选项卡
        /// </summary>
        public TabAdapter TabAdapter { get; }

        public MainWindow()
        {
            WindowId = Guid.NewGuid().ToString("N");

            InitializeComponent();

            CurrStatus.Text = "就绪";

            TabAdapter = new TabAdapter
            {
                MainWindow = this,
                TabControl = MainTabControl,
            };
        }

        public void SetCurrStatus(string text)
        {
            CurrStatus.Text = text;
        }

        /// <summary>
        /// 主窗口加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // 选项卡初始化
            TabAdapter.MainWindow_Loaded();
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
        private void StatusbarMenuItem_Click(object sender, EventArgs e)
        {
            MainStatus.Visible = !MainStatus.Visible;
        }

        /// <summary>
        /// 工具栏菜单被点击
        /// </summary>
        /// <param name="sender">ToolStripMenuItem</param>
        /// <param name="e"></param>
        private void ToolbarMenuItem_Click(object sender, EventArgs e)
        {
            MainTool.Visible = !MainTool.Visible;
        }

        /// <summary>
        /// 新建文件菜单--菜单>文件>新建
        /// </summary>
        /// <param name="sender">新建文件菜单对象</param>
        /// <param name="e"></param>
        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            var budle = new TabBundle
            {
                IsNew = true,
                TabTitle = "新增选项卡" + TabAdapter.NextNo.ToString().PadLeft(3, '0'),
            };
            TabAdapter.Add(budle);
            SetCurrStatus($"新建标题：{budle.TabTitle}");
        }

        /// <summary>
        /// 关闭当前文档（关闭当前选项卡）--菜单>文件>关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            this.TabAdapter.Remove(this.MainTabControl.SelectedIndex);
        }

        /// <summary>
        /// 文件保存--菜单>文件>保存
        /// </summary>
        /// <param name="sender">文件保存菜单项</param>
        /// <param name="e"></param>
        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)MainTabControl.SelectedTab.Controls.Find("RichTextBox", true).FirstOrDefault();
            //Console.WriteLine($"保存的内容为：\r\n{richTextBox.Text}");
            // 需要打开时保存文件的路径
            var budle = TabAdapter.TabBudles[MainTabControl.SelectedIndex];
            if (budle.IsNew)
            {
                var saveFileName = Path.HasExtension(budle.TabTitle) ? budle.TabTitle : budle.TabTitle + ".txt";
                var saveFileDialog = this.SaveFileDialog;

                //SaveFileDialog.DefaultExt = "txt";
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FileName = saveFileName;
                var dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                {
                    SetCurrStatus($"取消保存：{budle.TabTitle}");
                    return;
                }
                string fileName = saveFileDialog.FileName;
                if (dialogResult == DialogResult.OK && fileName.Length > 0)
                {
                    richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                    budle.IsNew = false;
                    budle.SrcPath = saveFileDialog.FileName;
                    budle.TabTitle = Path.GetFileName(fileName);
                    SetCurrStatus($"保存成功：{budle.SrcPath}");
                }
            }
            else
            {
                richTextBox.SaveFile(budle.SrcPath, RichTextBoxStreamType.PlainText);
                SetCurrStatus($"保存成功：{budle.SrcPath}");
            }
        }

        /// <summary>
        /// 打开文件--菜单>文件>打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    TabAdapter.OpenFile(fileName);
                    SetCurrStatus($"打开文件：{fileName}");
                }
                catch(Exception ex)
                {
                    SetCurrStatus($"打开失败：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 将文件另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            var page = MainTabControl.SelectedTab;
            RichTextBox richTextBox = (RichTextBox)page.Controls.Find("RichTextBox", true).FirstOrDefault();

            string content = richTextBox.Text;
            Console.WriteLine("另存的内容为：\r\n" + content);

            SaveFileDialog.DefaultExt = "txt";
            SaveFileDialog.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
            SaveFileDialog.FileName = page.Text;
            DialogResult DialogResult = SaveFileDialog.ShowDialog();
            if (DialogResult == DialogResult.Cancel)
            {
                SetCurrStatus($"取消另存：{TabAdapter.TabBudles[MainTabControl.SelectedIndex].TabTitle}");
                return;
            }
            string fileName = this.SaveFileDialog.FileName;
            if (DialogResult == DialogResult.OK && fileName.Length > 0)
            {
                richTextBox.SaveFile(fileName, RichTextBoxStreamType.PlainText);
                SetCurrStatus($"保存成功：{fileName}");
            }
        }

        /// <summary>
        /// 关闭主窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenFolderMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
