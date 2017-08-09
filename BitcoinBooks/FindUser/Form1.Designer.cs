namespace FindUser
{
    partial class Form1
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
            this.Address = new System.Windows.Forms.TextBox();
            this.userButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(13, 13);
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(259, 20);
            this.Address.TabIndex = 0;
            // 
            // userButton
            // 
            this.userButton.Location = new System.Drawing.Point(13, 39);
            this.userButton.Name = "userButton";
            this.userButton.Size = new System.Drawing.Size(259, 37);
            this.userButton.TabIndex = 1;
            this.userButton.Text = "Find User";
            this.userButton.UseVisualStyleBackColor = true;
            this.userButton.Click += new System.EventHandler(this.userButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 90);
            this.Controls.Add(this.userButton);
            this.Controls.Add(this.Address);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Address;
        private System.Windows.Forms.Button userButton;
    }
}

