namespace WS.Editor
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreviewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤消UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重复RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.剪切TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.状态栏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具栏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoLinefeedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsoleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自定义CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.内容CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.索引IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatus = new System.Windows.Forms.StatusStrip();
            this.CurrStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurrTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainTool = new System.Windows.Forms.ToolStrip();
            this.CommentToolButton = new System.Windows.Forms.ToolStripButton();
            this.TabContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SaveTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseAllTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseOtherTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.PrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.OtherTabWraper = new System.Windows.Forms.Panel();
            this.OtherTabControl = new System.Windows.Forms.TabControl();
            this.OutputTab = new System.Windows.Forms.TabPage();
            this.CommandTab = new System.Windows.Forms.TabPage();
            this.CommandTextBox = new System.Windows.Forms.RichTextBox();
            this.ContentWraper = new System.Windows.Forms.Panel();
            this.MainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.OpenCmdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomCmdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.MainStatus.SuspendLayout();
            this.MainTool.SuspendLayout();
            this.TabContextMenu.SuspendLayout();
            this.OtherTabWraper.SuspendLayout();
            this.OtherTabControl.SuspendLayout();
            this.OutputTab.SuspendLayout();
            this.CommandTab.SuspendLayout();
            this.ContentWraper.SuspendLayout();
            this.MainToolStripContainer.ContentPanel.SuspendLayout();
            this.MainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.编辑EToolStripMenuItem,
            this.格式ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1014, 25);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "菜单";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripMenuItem,
            this.打开OToolStripMenuItem,
            this.OpenFolderMenuItem,
            this.关闭CToolStripMenuItem,
            this.toolStripSeparator,
            this.保存SToolStripMenuItem,
            this.另存为AToolStripMenuItem,
            this.toolStripSeparator1,
            this.PrintMenuItem,
            this.PrintPreviewMenuItem,
            this.toolStripSeparator2,
            this.RestartMenuItem,
            this.退出XToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 新建NToolStripMenuItem
            // 
            this.新建NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripMenuItem.Image")));
            this.新建NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripMenuItem.Name = "新建NToolStripMenuItem";
            this.新建NToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建NToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.新建NToolStripMenuItem.Text = "新建(&N)";
            this.新建NToolStripMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripMenuItem.Image")));
            this.打开OToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // OpenFolderMenuItem
            // 
            this.OpenFolderMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenFolderMenuItem.Image")));
            this.OpenFolderMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFolderMenuItem.Name = "OpenFolderMenuItem";
            this.OpenFolderMenuItem.Size = new System.Drawing.Size(180, 22);
            this.OpenFolderMenuItem.Text = "打开文件夹(&F)";
            this.OpenFolderMenuItem.Click += new System.EventHandler(this.OpenFolderMenuItem_Click);
            // 
            // 关闭CToolStripMenuItem
            // 
            this.关闭CToolStripMenuItem.Name = "关闭CToolStripMenuItem";
            this.关闭CToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.关闭CToolStripMenuItem.Text = "关闭(&C)";
            this.关闭CToolStripMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // 保存SToolStripMenuItem
            // 
            this.保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripMenuItem.Image")));
            this.保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
            this.保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存SToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.保存SToolStripMenuItem.Text = "保存(&S)";
            this.保存SToolStripMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // 另存为AToolStripMenuItem
            // 
            this.另存为AToolStripMenuItem.Name = "另存为AToolStripMenuItem";
            this.另存为AToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.另存为AToolStripMenuItem.Text = "另存为(&A)";
            this.另存为AToolStripMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // PrintMenuItem
            // 
            this.PrintMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintMenuItem.Image")));
            this.PrintMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintMenuItem.Name = "PrintMenuItem";
            this.PrintMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.PrintMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PrintMenuItem.Text = "打印(&P)";
            this.PrintMenuItem.Click += new System.EventHandler(this.PrintMenuItem_Click);
            // 
            // PrintPreviewMenuItem
            // 
            this.PrintPreviewMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintPreviewMenuItem.Image")));
            this.PrintPreviewMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintPreviewMenuItem.Name = "PrintPreviewMenuItem";
            this.PrintPreviewMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PrintPreviewMenuItem.Text = "打印预览(&V)";
            this.PrintPreviewMenuItem.Click += new System.EventHandler(this.PrintPreviewMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            this.退出XToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // 编辑EToolStripMenuItem
            // 
            this.编辑EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤消UToolStripMenuItem,
            this.重复RToolStripMenuItem,
            this.toolStripSeparator3,
            this.剪切TToolStripMenuItem,
            this.CopyMenuItem,
            this.PasteMenuItem,
            this.toolStripSeparator4,
            this.全选AToolStripMenuItem});
            this.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem";
            this.编辑EToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.编辑EToolStripMenuItem.Text = "编辑(&E)";
            // 
            // 撤消UToolStripMenuItem
            // 
            this.撤消UToolStripMenuItem.Name = "撤消UToolStripMenuItem";
            this.撤消UToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.撤消UToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.撤消UToolStripMenuItem.Text = "撤消(&U)";
            // 
            // 重复RToolStripMenuItem
            // 
            this.重复RToolStripMenuItem.Name = "重复RToolStripMenuItem";
            this.重复RToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.重复RToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.重复RToolStripMenuItem.Text = "重复(&R)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(158, 6);
            // 
            // 剪切TToolStripMenuItem
            // 
            this.剪切TToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("剪切TToolStripMenuItem.Image")));
            this.剪切TToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.剪切TToolStripMenuItem.Name = "剪切TToolStripMenuItem";
            this.剪切TToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.剪切TToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.剪切TToolStripMenuItem.Text = "剪切(&T)";
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyMenuItem.Image")));
            this.CopyMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyMenuItem.Name = "CopyMenuItem";
            this.CopyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyMenuItem.Size = new System.Drawing.Size(161, 22);
            this.CopyMenuItem.Text = "复制(&C)";
            this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // PasteMenuItem
            // 
            this.PasteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PasteMenuItem.Image")));
            this.PasteMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteMenuItem.Name = "PasteMenuItem";
            this.PasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteMenuItem.Size = new System.Drawing.Size(161, 22);
            this.PasteMenuItem.Text = "粘贴(&P)";
            this.PasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(158, 6);
            // 
            // 全选AToolStripMenuItem
            // 
            this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
            this.全选AToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.全选AToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.全选AToolStripMenuItem.Text = "全选(&A)";
            // 
            // 格式ToolStripMenuItem
            // 
            this.格式ToolStripMenuItem.Name = "格式ToolStripMenuItem";
            this.格式ToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
            this.格式ToolStripMenuItem.Text = "格式(&O)";
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.状态栏ToolStripMenuItem,
            this.工具栏ToolStripMenuItem,
            this.AutoLinefeedMenuItem,
            this.OutputMenuItem,
            this.ConsoleMenuItem,
            this.OpenCmdMenuItem,
            this.CustomCmdMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.视图ToolStripMenuItem.Text = "视图(&V)";
            // 
            // 状态栏ToolStripMenuItem
            // 
            this.状态栏ToolStripMenuItem.Checked = true;
            this.状态栏ToolStripMenuItem.CheckOnClick = true;
            this.状态栏ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.状态栏ToolStripMenuItem.Name = "状态栏ToolStripMenuItem";
            this.状态栏ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.状态栏ToolStripMenuItem.Text = "状态栏(&S)";
            this.状态栏ToolStripMenuItem.Click += new System.EventHandler(this.StatusbarMenuItem_Click);
            // 
            // 工具栏ToolStripMenuItem
            // 
            this.工具栏ToolStripMenuItem.Checked = true;
            this.工具栏ToolStripMenuItem.CheckOnClick = true;
            this.工具栏ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.工具栏ToolStripMenuItem.Name = "工具栏ToolStripMenuItem";
            this.工具栏ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.工具栏ToolStripMenuItem.Text = "工具栏(&T)";
            this.工具栏ToolStripMenuItem.Click += new System.EventHandler(this.ToolbarMenuItem_Click);
            // 
            // AutoLinefeedMenuItem
            // 
            this.AutoLinefeedMenuItem.Name = "AutoLinefeedMenuItem";
            this.AutoLinefeedMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AutoLinefeedMenuItem.Text = "自动换行(&F)";
            this.AutoLinefeedMenuItem.Click += new System.EventHandler(this.AutoLinefeedMenuItem_Click);
            // 
            // OutputMenuItem
            // 
            this.OutputMenuItem.Checked = true;
            this.OutputMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OutputMenuItem.Name = "OutputMenuItem";
            this.OutputMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.OutputMenuItem.Size = new System.Drawing.Size(180, 22);
            this.OutputMenuItem.Text = "输出(&O)";
            this.OutputMenuItem.Click += new System.EventHandler(this.OutputMenuItem_Click);
            // 
            // ConsoleMenuItem
            // 
            this.ConsoleMenuItem.Name = "ConsoleMenuItem";
            this.ConsoleMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ConsoleMenuItem.Text = "控制台(&C)";
            this.ConsoleMenuItem.Click += new System.EventHandler(this.ConsoleMenuItem_Click);
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自定义CToolStripMenuItem,
            this.选项OToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // 自定义CToolStripMenuItem
            // 
            this.自定义CToolStripMenuItem.Name = "自定义CToolStripMenuItem";
            this.自定义CToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.自定义CToolStripMenuItem.Text = "自定义(&C)";
            // 
            // 选项OToolStripMenuItem
            // 
            this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
            this.选项OToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.选项OToolStripMenuItem.Text = "选项(&O)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内容CToolStripMenuItem,
            this.索引IToolStripMenuItem,
            this.搜索SToolStripMenuItem,
            this.toolStripSeparator5,
            this.关于AToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 内容CToolStripMenuItem
            // 
            this.内容CToolStripMenuItem.Name = "内容CToolStripMenuItem";
            this.内容CToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.内容CToolStripMenuItem.Text = "内容(&C)";
            // 
            // 索引IToolStripMenuItem
            // 
            this.索引IToolStripMenuItem.Name = "索引IToolStripMenuItem";
            this.索引IToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.索引IToolStripMenuItem.Text = "索引(&I)";
            // 
            // 搜索SToolStripMenuItem
            // 
            this.搜索SToolStripMenuItem.Name = "搜索SToolStripMenuItem";
            this.搜索SToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.搜索SToolStripMenuItem.Text = "搜索(&S)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(122, 6);
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.关于AToolStripMenuItem.Text = "关于(&A)...";
            // 
            // MainStatus
            // 
            this.MainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrStatus,
            this.CurrTime});
            this.MainStatus.Location = new System.Drawing.Point(0, 521);
            this.MainStatus.Name = "MainStatus";
            this.MainStatus.Size = new System.Drawing.Size(1014, 22);
            this.MainStatus.TabIndex = 2;
            this.MainStatus.Text = "状态栏";
            // 
            // CurrStatus
            // 
            this.CurrStatus.Name = "CurrStatus";
            this.CurrStatus.Size = new System.Drawing.Size(20, 17);
            this.CurrStatus.Text = "无";
            // 
            // CurrTime
            // 
            this.CurrTime.Name = "CurrTime";
            this.CurrTime.Size = new System.Drawing.Size(135, 17);
            this.CurrTime.Text = "当前时间：14 : 23 假的";
            this.CurrTime.Visible = false;
            // 
            // MainTool
            // 
            this.MainTool.Dock = System.Windows.Forms.DockStyle.None;
            this.MainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CommentToolButton});
            this.MainTool.Location = new System.Drawing.Point(0, 0);
            this.MainTool.Name = "MainTool";
            this.MainTool.Padding = new System.Windows.Forms.Padding(0);
            this.MainTool.Size = new System.Drawing.Size(1014, 25);
            this.MainTool.Stretch = true;
            this.MainTool.TabIndex = 3;
            this.MainTool.Text = "工具栏";
            // 
            // CommentToolButton
            // 
            this.CommentToolButton.Image = ((System.Drawing.Image)(resources.GetObject("CommentToolButton.Image")));
            this.CommentToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CommentToolButton.Name = "CommentToolButton";
            this.CommentToolButton.Size = new System.Drawing.Size(52, 22);
            this.CommentToolButton.Text = "注释";
            this.CommentToolButton.Click += new System.EventHandler(this.CommentToolButton_Click);
            // 
            // TabContextMenu
            // 
            this.TabContextMenu.AllowDrop = true;
            this.TabContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveTabContextMenuItem,
            this.CloseTabContextMenuItem,
            this.CloseAllTabContextMenuItem,
            this.CloseOtherTabContextMenuItem});
            this.TabContextMenu.Name = "文件";
            this.TabContextMenu.Size = new System.Drawing.Size(125, 92);
            // 
            // SaveTabContextMenuItem
            // 
            this.SaveTabContextMenuItem.Name = "SaveTabContextMenuItem";
            this.SaveTabContextMenuItem.Size = new System.Drawing.Size(124, 22);
            this.SaveTabContextMenuItem.Text = "保存";
            this.SaveTabContextMenuItem.Click += new System.EventHandler(this.SaveTabContextMenuItem_Click);
            // 
            // CloseTabContextMenuItem
            // 
            this.CloseTabContextMenuItem.Name = "CloseTabContextMenuItem";
            this.CloseTabContextMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CloseTabContextMenuItem.Text = "关闭";
            this.CloseTabContextMenuItem.Click += new System.EventHandler(this.CloseTabContextMenuItem_Click);
            // 
            // CloseAllTabContextMenuItem
            // 
            this.CloseAllTabContextMenuItem.Name = "CloseAllTabContextMenuItem";
            this.CloseAllTabContextMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CloseAllTabContextMenuItem.Text = "关闭所有";
            this.CloseAllTabContextMenuItem.Click += new System.EventHandler(this.CloseAllTabContextMenuItem_Click);
            // 
            // CloseOtherTabContextMenuItem
            // 
            this.CloseOtherTabContextMenuItem.Name = "CloseOtherTabContextMenuItem";
            this.CloseOtherTabContextMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CloseOtherTabContextMenuItem.Text = "关闭其它";
            this.CloseOtherTabContextMenuItem.Click += new System.EventHandler(this.CloseOtherTabContextMenuItem_Click);
            // 
            // PrintPreviewDialog
            // 
            this.PrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.PrintPreviewDialog.Enabled = true;
            this.PrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPreviewDialog.Icon")));
            this.PrintPreviewDialog.Name = "PrintPreviewDialog";
            this.PrintPreviewDialog.Visible = false;
            // 
            // MainTabControl
            // 
            this.MainTabControl.AllowDrop = true;
            this.MainTabControl.ContextMenuStrip = this.TabContextMenu;
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.Padding = new System.Drawing.Point(0, 0);
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(1014, 379);
            this.MainTabControl.TabIndex = 0;
            this.MainTabControl.SelectedIndexChanged += new System.EventHandler(this.MainTabControl_SelectedIndexChanged);
            this.MainTabControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainTabControl_DragDrop);
            this.MainTabControl.DragOver += new System.Windows.Forms.DragEventHandler(this.MainTabControl_DragOver);
            this.MainTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTabControl_MouseDown);
            this.MainTabControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainTabControl_MouseMove);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputTextBox.Location = new System.Drawing.Point(0, 0);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputTextBox.Size = new System.Drawing.Size(1006, 66);
            this.OutputTextBox.TabIndex = 0;
            this.OutputTextBox.Text = "输出：\r\n";
            this.OutputTextBox.WordWrap = false;
            // 
            // OtherTabWraper
            // 
            this.OtherTabWraper.Controls.Add(this.OtherTabControl);
            this.OtherTabWraper.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OtherTabWraper.Location = new System.Drawing.Point(0, 379);
            this.OtherTabWraper.Name = "OtherTabWraper";
            this.OtherTabWraper.Size = new System.Drawing.Size(1014, 92);
            this.OtherTabWraper.TabIndex = 4;
            this.OtherTabWraper.Tag = "";
            // 
            // OtherTabControl
            // 
            this.OtherTabControl.Controls.Add(this.OutputTab);
            this.OtherTabControl.Controls.Add(this.CommandTab);
            this.OtherTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OtherTabControl.Location = new System.Drawing.Point(0, 0);
            this.OtherTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.OtherTabControl.Name = "OtherTabControl";
            this.OtherTabControl.SelectedIndex = 0;
            this.OtherTabControl.Size = new System.Drawing.Size(1014, 92);
            this.OtherTabControl.TabIndex = 1;
            // 
            // OutputTab
            // 
            this.OutputTab.Controls.Add(this.OutputTextBox);
            this.OutputTab.Location = new System.Drawing.Point(4, 22);
            this.OutputTab.Name = "OutputTab";
            this.OutputTab.Size = new System.Drawing.Size(1006, 66);
            this.OutputTab.TabIndex = 0;
            this.OutputTab.Text = "输出";
            // 
            // CommandTab
            // 
            this.CommandTab.Controls.Add(this.CommandTextBox);
            this.CommandTab.Location = new System.Drawing.Point(4, 22);
            this.CommandTab.Name = "CommandTab";
            this.CommandTab.Size = new System.Drawing.Size(1006, 66);
            this.CommandTab.TabIndex = 1;
            this.CommandTab.Text = "命令";
            this.CommandTab.UseVisualStyleBackColor = true;
            // 
            // CommandTextBox
            // 
            this.CommandTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommandTextBox.Location = new System.Drawing.Point(0, 0);
            this.CommandTextBox.Name = "CommandTextBox";
            this.CommandTextBox.Size = new System.Drawing.Size(1006, 66);
            this.CommandTextBox.TabIndex = 0;
            this.CommandTextBox.Text = "";
            this.CommandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandTextBox_KeyDown);
            // 
            // ContentWraper
            // 
            this.ContentWraper.Controls.Add(this.MainToolStripContainer);
            this.ContentWraper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentWraper.Location = new System.Drawing.Point(0, 25);
            this.ContentWraper.Name = "ContentWraper";
            this.ContentWraper.Size = new System.Drawing.Size(1014, 496);
            this.ContentWraper.TabIndex = 5;
            // 
            // MainToolStripContainer
            // 
            this.MainToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // MainToolStripContainer.ContentPanel
            // 
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.MainTabControl);
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.OtherTabWraper);
            this.MainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(1014, 471);
            this.MainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStripContainer.LeftToolStripPanelVisible = false;
            this.MainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.MainToolStripContainer.Name = "MainToolStripContainer";
            this.MainToolStripContainer.RightToolStripPanelVisible = false;
            this.MainToolStripContainer.Size = new System.Drawing.Size(1014, 496);
            this.MainToolStripContainer.TabIndex = 5;
            this.MainToolStripContainer.Text = "toolStripContainer1";
            // 
            // MainToolStripContainer.TopToolStripPanel
            // 
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.MainTool);
            // 
            // OpenCmdMenuItem
            // 
            this.OpenCmdMenuItem.Name = "OpenCmdMenuItem";
            this.OpenCmdMenuItem.Size = new System.Drawing.Size(180, 22);
            this.OpenCmdMenuItem.Text = "打开CMD(&M)";
            this.OpenCmdMenuItem.Click += new System.EventHandler(this.OpenCmdMenuItem_Click);
            // 
            // CustomCmdMenuItem
            // 
            this.CustomCmdMenuItem.Name = "CustomCmdMenuItem";
            this.CustomCmdMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CustomCmdMenuItem.Text = "自定义CMD";
            this.CustomCmdMenuItem.Click += new System.EventHandler(this.CustomCmdMenuItem_Click);
            // 
            // RestartMenuItem
            // 
            this.RestartMenuItem.Name = "RestartMenuItem";
            this.RestartMenuItem.Size = new System.Drawing.Size(180, 22);
            this.RestartMenuItem.Text = "重启(&R)";
            this.RestartMenuItem.Click += new System.EventHandler(this.RestartMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 543);
            this.Controls.Add(this.ContentWraper);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.MainStatus);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainWindow";
            this.Text = "Editor";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainStatus.ResumeLayout(false);
            this.MainStatus.PerformLayout();
            this.MainTool.ResumeLayout(false);
            this.MainTool.PerformLayout();
            this.TabContextMenu.ResumeLayout(false);
            this.OtherTabWraper.ResumeLayout(false);
            this.OtherTabControl.ResumeLayout(false);
            this.OutputTab.ResumeLayout(false);
            this.OutputTab.PerformLayout();
            this.CommandTab.ResumeLayout(false);
            this.ContentWraper.ResumeLayout(false);
            this.MainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ResumeLayout(false);
            this.MainToolStripContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.StatusStrip MainStatus;
        private System.Windows.Forms.ToolStripStatusLabel CurrStatus;
        private System.Windows.Forms.ToolStrip MainTool;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具栏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton CommentToolButton;
        private System.Windows.Forms.ToolStripMenuItem 格式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel CurrTime;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 状态栏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自定义CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 内容CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 索引IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜索SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem 保存SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为AToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem PrintMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintPreviewMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤消UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重复RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 剪切TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoLinefeedMenuItem;
        private System.Windows.Forms.ContextMenuStrip TabContextMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveTabContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseTabContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseAllTabContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseOtherTabContextMenuItem;
        private System.Drawing.Printing.PrintDocument PrintDocument;
        private System.Windows.Forms.PrintPreviewDialog PrintPreviewDialog;
        private System.Windows.Forms.ToolStripMenuItem OutputMenuItem;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Panel OtherTabWraper;
        private System.Windows.Forms.Panel ContentWraper;
        private System.Windows.Forms.ToolStripContainer MainToolStripContainer;
        private System.Windows.Forms.TabControl OtherTabControl;
        private System.Windows.Forms.TabPage OutputTab;
        private System.Windows.Forms.TabPage CommandTab;
        private System.Windows.Forms.ToolStripMenuItem ConsoleMenuItem;
        private System.Windows.Forms.RichTextBox CommandTextBox;
        private System.Windows.Forms.ToolStripMenuItem OpenCmdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CustomCmdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RestartMenuItem;
    }
}

