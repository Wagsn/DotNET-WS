namespace WS.Reader
{
    partial class ViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RichTextBox
            // 
            this.RichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBox.Location = new System.Drawing.Point(0, 0);
            this.RichTextBox.Name = "RichTextBox";
            this.RichTextBox.ReadOnly = true;
            this.RichTextBox.Size = new System.Drawing.Size(800, 450);
            this.RichTextBox.TabIndex = 0;
            this.RichTextBox.Text = "这是第一行\n这是第二行\n这是第三行";
            this.RichTextBox.WordWrap = false;
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RichTextBox);
            this.Name = "ViewForm";
            this.Text = "ViewForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextBox;
    }
}