using System;
using System.Windows.Forms;

namespace WS.Editor
{
    partial class CmdForm
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
            //IDisposable disposable = cmd as IDisposable;
            //if (disposable != null)
            //    disposable.Dispose();
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
            this.label1 = new System.Windows.Forms.Label();
            this.comEdit = new System.Windows.Forms.TextBox();
            this.cmdLogTextArea = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "命令";
            // 
            // comEdit
            // 
            this.comEdit.Location = new System.Drawing.Point(48, 10);
            this.comEdit.Name = "comEdit";
            this.comEdit.Size = new System.Drawing.Size(251, 21);
            this.comEdit.TabIndex = 1;
            this.comEdit.Text = "ping 127.0.0.1";
            this.comEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComEdit_KeyDown);
            // 
            // cmdLogTextArea
            // 
            this.cmdLogTextArea.Location = new System.Drawing.Point(13, 47);
            this.cmdLogTextArea.Multiline = true;
            this.cmdLogTextArea.Name = "cmdLogTextArea";
            this.cmdLogTextArea.Size = new System.Drawing.Size(775, 391);
            this.cmdLogTextArea.TabIndex = 4;
            // 
            // CmdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmdLogTextArea);
            this.Controls.Add(this.comEdit);
            this.Controls.Add(this.label1);
            this.Name = "CmdForm";
            this.Text = "Cmd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox comEdit;
        public System.Windows.Forms.TextBox cmdLogTextArea;
    }
}