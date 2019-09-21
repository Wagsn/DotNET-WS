using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Editor
{
    /// <summary>
    /// 选项卡管理器，用来封装TabControl
    /// </summary>
    public class TabAdapter
    {
        /// <summary>
        /// 主窗口的句柄
        /// </summary>
        public MainWindow MainWindow { get; set; }

        /// <summary>
        /// 外部引用注意是否会被其它人修改
        /// </summary>
        public TabControl TabControl { get; set; }

        /// <summary>
        /// 用来持有TabPage和文件路径，方便添加
        /// </summary>
        public List<TabBundle> TabBundles = new List<TabBundle>();

        private static string EditFormClassName { get; } = typeof(EditForm).FullName;

        public static List<string> Exts { get; } = new List<string> { ".txt", ".json", ".xml", ".html", ".java", ".py" };

        private int Count = 0;
        public int NextNo { get { Count++; return Count; } }

        public TabAdapter()
        {

        }

        /// <summary>
        /// 当主界面加载后调用
        /// </summary>
        public void MainWindow_Loaded()
        {
            Add(new TabBundle
            {
                TabTitle = $"新增选项卡{NextNo.ToString().PadLeft(3, '0')}",
            });
        }

        public void OpenFile(string path)
        {
            if (!File.Exists(path))
            {
                MainWindow.SetCurrStatus("文件不存在");
                return;
            }
            var title = Path.GetFileName(path);
            var bundle = new TabBundle
            {
                IsNew = false,
                IsEdit = false,
                SrcPath = path,
                TabTitle = title,
                TabPage = CreateEditPage(title),
            };
            bundle.TabPage.AllowDrop = true;
            bundle.TabPage.DragDrop += OnDragDrop;

            ((RichTextBox)bundle.TabPage.Controls.Find("RichTextBox", true).FirstOrDefault()).LoadFile(bundle.SrcPath, RichTextBoxStreamType.PlainText);
            TabControl.TabPages.Add(bundle.TabPage);
            var index = TabControl.TabPages.IndexOf(bundle.TabPage);
            TabControl.SelectedIndex = index;
            TabBundles.Add(bundle);
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            TabPage source = (TabPage)e.Data.GetData(typeof(TabPage));
            MainWindow.SetCurrStatus($"OnDragDrop：{source}");
            if (source != null)
            {
                for (int i = 0; i < TabControl.TabPages.Count; i++)
                {
                    if (TabControl.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        //var tab = MainTabControl.TabPages[i];
                        //SetCurrStatus($"拖动开始：{MainTabControl.TabPages[i].Text}，AlowDrop：{tab.AllowDrop}");
                        //tab.DoDragDrop(e, DragDropEffects.Move);
                        if (TabControl.TabPages.IndexOf(source) != i)
                        {
                            e.Effect = DragDropEffects.Move;
                            Swap(TabControl.TabPages.IndexOf(source), i);
                            MainWindow.SetCurrStatus($"拖动结束：{e.Data}");
                            return;
                        }
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// 添加TabPage通过TabBundle
        /// 1. 打开文件
        /// 2. 新建文件
        /// </summary>
        /// <param name="bundle">绑定TabPage与FilePath</param>
        public void Add(TabBundle bundle)
        {
            // 创建TabPage
            var form = CreateForm(EditFormClassName);

            TabPage page = CreateTabPage(bundle.TabTitle, CreateForm(EditFormClassName));
            //page.AllowDrop = true;
            TabBundles.Add(new TabBundle
            {
                TabTitle = bundle.TabTitle,
                IsNew = !((!string.IsNullOrWhiteSpace(bundle.SrcPath)) && File.Exists(bundle.SrcPath)),
                TabPage = page
            });
            if ((!string.IsNullOrWhiteSpace(bundle.SrcPath)) && File.Exists(bundle.SrcPath) /* && bundle.SrcFilePath.isPath()*/)
            {
                ((RichTextBox)page.Controls.Find("RichTextBox", true).FirstOrDefault()).LoadFile(bundle.SrcPath, RichTextBoxStreamType.PlainText);
            }
            bundle.TabPage = page;
            TabControl.TabPages.Add(page);
            var index = TabControl.TabPages.IndexOf(page);
            TabControl.SelectedIndex = index;
        }

        /// <summary>
        /// 删除当前选中的选项卡
        /// </summary>
        public void Remove() => Remove(TabControl.SelectedIndex);

        /// <summary>
        /// 删除当前选中的选项卡
        /// </summary>
        public void Remove(int index)
        {
            // TODO Save editing file.
            if (index > -1 && TabControl.TabPages.Count > 0 && TabControl.TabPages.Count > index)
            {
                var tab = TabControl.TabPages[index];
                TabControl.TabPages.Remove(tab);
                var budle = TabBundles[index];
                MainWindow.SetCurrStatus($"关闭当现项：{budle.TabTitle}");
                TabBundles.RemoveAt(index);
            }
        }

        /// <summary>
        /// 交换两个索引所在TabPage的位置
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void Swap(int index1, int index2) => Swap(TabControl, index1, index2);

        #region << 静态工具函数 >>
        /// <summary>
        /// 根据Form类的类全名反射生成对象实例
        /// </summary>
        /// <param name="formClassFullName"></param>
        /// <returns></returns>
        public static Form CreateForm(string formClassFullName) => (Form)Assembly.GetExecutingAssembly().CreateInstance(formClassFullName);

        private static TabPage CreateEditPage(string title) => CreateTabPage(title, CreateForm(EditFormClassName));

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


        public static void Swap(TabControl tabControl, int index1, int index2)
        {
            // TODO 取模操作避免超限
            if (NotInRange(index1, 0, tabControl.TabPages.Count) || NotInRange(index2, 0, tabControl.TabPages.Count))
            {
                throw new IndexOutOfRangeException("Tab Page Index out of the Range");
            }
            TabPage page1 = tabControl.TabPages[index1];
            TabPage page2 = tabControl.TabPages[index2];

            tabControl.TabPages[index2] = page1;
            tabControl.TabPages[index1] = page2;
        }

        /// <summary>
        /// 大于等于最小值，小于最大值
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static  bool IsInRange(int curr, int min, int max) => curr >= min && curr < max;

        public static bool NotInRange(int curr, int min, int max) => !IsInRange(curr, min, max);
        #endregion
    }
}
