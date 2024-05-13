namespace dashcon
{
    partial class FindSymbol
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
            this.symbolNumber = new System.Windows.Forms.TextBox();
            this.goButton = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // symbolNumber
            // 
            this.symbolNumber.Location = new System.Drawing.Point(12, 21);
            this.symbolNumber.Name = "symbolNumber";
            this.symbolNumber.Size = new System.Drawing.Size(100, 22);
            this.symbolNumber.TabIndex = 0;
            // 
            // goButton
            // 
            this.goButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.goButton.Location = new System.Drawing.Point(118, 21);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "Search";
            // 
            // FindSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 72);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.symbolNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindSymbol";
            this.Text = "Find Symbol";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox symbolNumber;
        private DevExpress.XtraEditors.SimpleButton goButton;
    }
}