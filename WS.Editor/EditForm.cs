using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Editor
{
    /// <summary>
    /// 文本编辑框
    /// </summary>
    public partial class EditForm : Form
    {
        /// <summary>
        /// 正文
        /// </summary>
        public string Content {
            get
            {
                return ""; // RichTextBox.
            } set
            {

            }
        }

        public EditForm()
        {
            InitializeComponent();


            //System.Drawing.Text.PrivateFontCollection privateFonts = new System.Drawing.Text.PrivateFontCollection();
            //privateFonts.AddFontFile("./res/font/Inconsolata.otf");
            //Font font = new Font(privateFonts.Families[0], 10);
            //this.RichTextBox.Font = font;

            this.RichTextBox.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
        }
    }

}
