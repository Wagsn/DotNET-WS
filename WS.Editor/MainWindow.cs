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
using System.Web;
using Newtonsoft.Json;

namespace WS.Editor
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

        /// <summary>
        /// 当前编辑框
        /// </summary>
        private RichTextBox CurrEidtBox { get; set; }

        public MainWindow()
        {
            WindowId = Guid.NewGuid().ToString();

            InitializeComponent();

            //OutputForm output = new OutputForm();
            //output.TopLevel = false;
            //output.Parent = Output;
            ////output.MdiParent = this;
            //output.FormBorderStyle = FormBorderStyle.None;
            //output.Dock = DockStyle.Fill;
            ////this.Output.Controls.Add(output);
            //output.Show();

            SetCurrStatus($"就绪-窗口：{WindowId}");

            TabAdapter = new TabAdapter
            {
                MainWindow = this,
                TabControl = MainTabControl,
            };
        }

        public void SetCurrStatus(string text)
        {
            // 状态栏输出
            CurrStatus.Text = text;
            // 控制台输出
            Console.WriteLine(text);
            // 输出框输出
            var outpuTextBox = (TextBox)OtherTabWraper.Controls.Find("OutputTextBox", true).FirstOrDefault();

            if(outpuTextBox != null)
            {
                outpuTextBox.AppendText(text + "\r\n");
                outpuTextBox.ScrollToCaret();
            }
            // TODO 同时输出到TextBox、Console、File，自定义或引入NLOG，引入NLOG参考：https://www.crifan.com/csharp_implement_log_system_same_time_output_to_file_and_terminal/
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
            var budle = TabAdapter.TabBundles[MainTabControl.SelectedIndex];
            if (budle.IsNew)
            {
                var saveFileName = Path.HasExtension(budle.TabTitle) ? budle.TabTitle : budle.TabTitle + ".txt";
                var saveFileDialog = this.SaveFileDialog;

                //SaveFileDialog.DefaultExt = "txt";
                saveFileDialog.Filter = "Normol text file (*.txt)|*.txt|All files (*.*)|*.*";
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
                SetCurrStatus($"取消另存：{TabAdapter.TabBundles[MainTabControl.SelectedIndex].TabTitle}");
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
            SetCurrStatus($"打开文件夹：");
        }

        private void SaveTabContextMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrStatus($"保存当前项：{MainTabControl.SelectedTab.Text}");
            SaveMenuItem_Click(sender, e);
        }

        private void MainTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < MainTabControl.TabPages.Count; i++)
                {
                    if (MainTabControl.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        MainTabControl.SelectedIndex = i;
                        //var node = MainTabControl.TabPages[i].Tag as ITabPageNode;
                        //if(node != null)
                        //{
                        //    SetCurrStatus($"Tab Page Data: {JsonConvert.SerializeObject(node)}");
                        //}
                        SetCurrStatus($"右键打开并操作：{MainTabControl.TabPages[i].Text}");
                    }
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < MainTabControl.TabPages.Count; i++)
                {
                    if (MainTabControl.GetTabRect(i).Contains(e.Location))
                    {
                        var tab = MainTabControl.TabPages[i];
                        SetCurrStatus($"拖动开始：{tab.Text}");
                        tab.DoDragDrop(tab, DragDropEffects.All);
                    }
                }
            }
        }

        private void CloseTabContextMenuItem_Click(object sender, EventArgs e)
        {
            CloseMenuItem_Click(sender, e);
        }

        private void CloseAllTabContextMenuItem_Click(object sender, EventArgs e)
        {
            // TODO Save editing file.
            MainTabControl.TabPages.Clear();
            TabAdapter.TabBundles.Clear();
        }

        private void CloseOtherTabContextMenuItem_Click(object sender, EventArgs e)
        {
            // TODO Save editing file.
            var page = MainTabControl.SelectedTab;
            var index = MainTabControl.SelectedIndex;
            var bundle = TabAdapter.TabBundles[index];
            MainTabControl.TabPages.Clear();
            MainTabControl.TabPages.Add(page);
            TabAdapter.TabBundles.Clear();
            TabAdapter.TabBundles.Add(bundle);
        }

        private void PrintPreviewMenuItem_Click(object sender, EventArgs e)
        {
            //PrintDocument.
            var page = MainTabControl.SelectedTab;
            PrintPreviewDialog.ShowDialog(page);
        }

        private void MainTabControl_DragOver(object sender, DragEventArgs e)
        {
            TabPage source = (TabPage)e.Data.GetData(typeof(TabPage));
            //SetCurrStatus($"MainTabControl_DragOver：{source}");
            if (source != null)
            {
                for (int i = 0; i < MainTabControl.TabPages.Count; i++)
                {
                    if (MainTabControl.GetTabRect(i).Contains(MainTabControl.PointToClient(new Point(e.X, e.Y))))
                    {
                        if(MainTabControl.TabPages.IndexOf(source) != i)
                        {
                            e.Effect = DragDropEffects.Move;
                            TabAdapter.Swap(MainTabControl.TabPages.IndexOf(source), i);
                            MainTabControl.SelectedTab = source;
                            SetCurrStatus($"拖动结束：{source.Text}");
                            return;
                        }
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void MainTabControl_DragDrop(object sender, DragEventArgs e)
        {
            TabPage source = (TabPage)e.Data.GetData(typeof(TabPage));
            SetCurrStatus($"MainTabControl_DragDrop：{source}");
            if (source != null)
            {
                for (int i = 0; i < MainTabControl.TabPages.Count; i++)
                {
                    if (MainTabControl.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        //var tab = MainTabControl.TabPages[i];
                        //SetCurrStatus($"拖动开始：{MainTabControl.TabPages[i].Text}，AlowDrop：{tab.AllowDrop}");
                        //tab.DoDragDrop(e, DragDropEffects.Move);
                        if (MainTabControl.TabPages.IndexOf(source) != i)
                        {
                            e.Effect = DragDropEffects.Move;
                            TabAdapter.Swap(MainTabControl.TabPages.IndexOf(source), i);
                            SetCurrStatus($"拖动结束：{e.Data}");
                            return;
                        }
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void PrintMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrStatus($"打印：{MainTabControl.SelectedTab.Text}");
        }

        private void AutoLinefeedMenuItem_Click(object sender, EventArgs e)
        {
            var editTextBox = (RichTextBox)MainTabControl.SelectedTab.Controls.Find("RichTextBox", true).FirstOrDefault();
            if (editTextBox != null)
            {
                editTextBox.WordWrap = !editTextBox.WordWrap;
                AutoLinefeedMenuItem.Checked = !AutoLinefeedMenuItem.Checked;
            }
        }

        private void CommentToolButton_Click(object sender, EventArgs e)
        {
            var page = MainTabControl.SelectedTab;
            if(page == null)
            {
                SetCurrStatus($"不存在标签页");
                return;
            }
            var editTextBox = (RichTextBox)page.Controls.Find("RichTextBox", true).FirstOrDefault();
            if(editTextBox != null)
            {
                SetCurrStatus($"光标所在位置 行: {editTextBox.GetLineFromCharIndex(editTextBox.GetFirstCharIndexOfCurrentLine())}, 列: {editTextBox.SelectionStart - editTextBox.GetFirstCharIndexOfCurrentLine()}");
            }
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            var editTextBox = (RichTextBox)MainTabControl.SelectedTab.Controls.Find("RichTextBox", true).FirstOrDefault();
            if(editTextBox != null)
            {
                editTextBox.Copy();
                SetCurrStatus($"Copy Text: {editTextBox.SelectedText}");
            }
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            var editTextBox = (RichTextBox)MainTabControl.SelectedTab.Controls.Find("RichTextBox", true).FirstOrDefault();
            if(editTextBox != null)
            {
                editTextBox.Paste();
                if (Clipboard.ContainsText())
                {
                    SetCurrStatus($"Paste Text: {editTextBox.SelectedText}");
                }
                else
                {
                    SetCurrStatus($"Clipboard has not Text!");
                }
            }
        }

        private void OutputMenuItem_Click(object sender, EventArgs e)
        {
            if (OutputMenuItem.Checked)
            {
                OutputMenuItem.Checked = !OutputMenuItem.Checked;
                OtherTabWraper.Hide();
            }
            else
            {
                OutputMenuItem.Checked = !OutputMenuItem.Checked;
                OtherTabWraper.Show();
            }
        }

        private void MainTabControl_MouseMove(object sender, MouseEventArgs e)
        {
            //SetCurrStatus($"MainTabControl_MouseMove: {sender}");
        }

        private void ConsoleMenuItem_Click(object sender, EventArgs e)
        {
            if (ConsoleMenuItem.Checked)
            {
                // 关闭控制台
                ConsoleMenuItem.Checked = !ConsoleMenuItem.Checked;
                NativeMethods.CloseConsole();
            }
            else
            {
                // 开启控制台
                ConsoleMenuItem.Checked = !ConsoleMenuItem.Checked;
                NativeMethods.OpenConsole();
            }
        }
    }
}
