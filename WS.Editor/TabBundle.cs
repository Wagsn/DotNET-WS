using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Editor
{
    /// <summary>
    /// 将选项卡与文件绑定
    /// IsNew TabTitle TabPage SrcPath EditText
    /// </summary>
    public class TabBundle
    {
        //public string UriDeCode { get; set; }

        /// <summary>
        /// 是否是新建的
        /// </summary>
        public bool IsNew { get; set; } = true;

        /// <summary>
        /// 是否被编辑（针对已存在的文件）
        /// </summary>
        public bool IsEdit { get; set; }

        private string _tableTitle = "";

        /// <summary>
        /// 选项卡的标题，修改会反应到TabPage的Text上
        /// </summary>
        public string TabTitle
        {
            get => _tableTitle;
            set
            {
                if(value == null) return;
                _tableTitle = value;
                if (TabPage != null)
                {
                    TabPage.Text = _tableTitle;
                }
            }
        }

        public RichTextBox EditBox { get; set; }

        /// <summary>
        /// 绑定的选项卡
        /// </summary>
        public TabPage TabPage { get; set; }

        /// <summary>
        /// 原始文件路径（打开文件的位置）
        /// </summary>
        public string SrcPath { get; set; }

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
