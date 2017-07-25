namespace Automation
{
    partial class ChatHistory
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
            this.label1 = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bitcoin Sent Address: ";
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(121, 12);
            this.address.Name = "address";
            this.address.ReadOnly = true;
            this.address.Size = new System.Drawing.Size(329, 20);
            this.address.TabIndex = 1;
            this.address.TabStop = false;
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(12, 48);
            this.messageBox.Name = "messageBox";
            this.messageBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.messageBox.Size = new System.Drawing.Size(438, 499);
            this.messageBox.TabIndex = 2;
            this.messageBox.Text = "";
            // 
            // ChatHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 559);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.address);
            this.Controls.Add(this.label1);
            this.Name = "ChatHistory";
            this.Text = "ChatID - ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.RichTextBox messageBox;
    }
}