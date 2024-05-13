namespace dashcon
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewConversionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllToPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linesToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symbolsToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.findLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSymbolIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom25ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom200ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom400ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutDASHCONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrelecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Khaki;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(967, 28);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.viewConversionReportToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exportAsPNGToolStripMenuItem,
            this.exportAsPDFToolStripMenuItem,
            this.exportAllToPDFToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.openFileToolStripMenuItem.Text = "Open DASH File(s)";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // viewConversionReportToolStripMenuItem
            // 
            this.viewConversionReportToolStripMenuItem.Name = "viewConversionReportToolStripMenuItem";
            this.viewConversionReportToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.viewConversionReportToolStripMenuItem.Text = "View Conversion Report";
            this.viewConversionReportToolStripMenuItem.Click += new System.EventHandler(this.viewConversionReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(239, 6);
            // 
            // exportAsPNGToolStripMenuItem
            // 
            this.exportAsPNGToolStripMenuItem.Name = "exportAsPNGToolStripMenuItem";
            this.exportAsPNGToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.exportAsPNGToolStripMenuItem.Text = "Export as PNG";
            this.exportAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportAsPNGToolStripMenuItem_Click);
            // 
            // exportAsPDFToolStripMenuItem
            // 
            this.exportAsPDFToolStripMenuItem.Name = "exportAsPDFToolStripMenuItem";
            this.exportAsPDFToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.exportAsPDFToolStripMenuItem.Text = "Export as PDF";
            this.exportAsPDFToolStripMenuItem.Click += new System.EventHandler(this.exportAsPDFToolStripMenuItem_Click);
            // 
            // exportAllToPDFToolStripMenuItem
            // 
            this.exportAllToPDFToolStripMenuItem.Name = "exportAllToPDFToolStripMenuItem";
            this.exportAllToPDFToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.exportAllToPDFToolStripMenuItem.Text = "Export all to PDF";
            this.exportAllToPDFToolStripMenuItem.Click += new System.EventHandler(this.exportAllToPDFToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(239, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linesToExcelToolStripMenuItem,
            this.textToExcelToolStripMenuItem,
            this.symbolsToExcelToolStripMenuItem,
            this.toolStripMenuItem1,
            this.findLineToolStripMenuItem,
            this.findTextToolStripMenuItem,
            this.findSymbolToolStripMenuItem,
            this.showSymbolIDsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.refreshToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // linesToExcelToolStripMenuItem
            // 
            this.linesToExcelToolStripMenuItem.Name = "linesToExcelToolStripMenuItem";
            this.linesToExcelToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.linesToExcelToolStripMenuItem.Text = "Lines to Excel";
            this.linesToExcelToolStripMenuItem.Click += new System.EventHandler(this.linesToExcel_Click);
            // 
            // textToExcelToolStripMenuItem
            // 
            this.textToExcelToolStripMenuItem.Name = "textToExcelToolStripMenuItem";
            this.textToExcelToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.textToExcelToolStripMenuItem.Text = "Text to Excel";
            this.textToExcelToolStripMenuItem.Click += new System.EventHandler(this.textToExcel_Click);
            // 
            // symbolsToExcelToolStripMenuItem
            // 
            this.symbolsToExcelToolStripMenuItem.Name = "symbolsToExcelToolStripMenuItem";
            this.symbolsToExcelToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.symbolsToExcelToolStripMenuItem.Text = "Symbols to Excel";
            this.symbolsToExcelToolStripMenuItem.Click += new System.EventHandler(this.symbolsToExcel_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 6);
            // 
            // findLineToolStripMenuItem
            // 
            this.findLineToolStripMenuItem.Name = "findLineToolStripMenuItem";
            this.findLineToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.findLineToolStripMenuItem.Text = "Find Line";
            this.findLineToolStripMenuItem.Click += new System.EventHandler(this.findLineToolStripMenuItem_Click);
            // 
            // findTextToolStripMenuItem
            // 
            this.findTextToolStripMenuItem.Name = "findTextToolStripMenuItem";
            this.findTextToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.findTextToolStripMenuItem.Text = "Find Text";
            this.findTextToolStripMenuItem.Click += new System.EventHandler(this.findTextToolStripMenuItem_Click);
            // 
            // findSymbolToolStripMenuItem
            // 
            this.findSymbolToolStripMenuItem.Name = "findSymbolToolStripMenuItem";
            this.findSymbolToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.findSymbolToolStripMenuItem.Text = "Find Symbol";
            this.findSymbolToolStripMenuItem.Click += new System.EventHandler(this.findSymbolToolStripMenuItem_Click);
            // 
            // showSymbolIDsToolStripMenuItem
            // 
            this.showSymbolIDsToolStripMenuItem.Name = "showSymbolIDsToolStripMenuItem";
            this.showSymbolIDsToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.showSymbolIDsToolStripMenuItem.Text = "Show Symbol IDs";
            this.showSymbolIDsToolStripMenuItem.Click += new System.EventHandler(this.showSymbolIDsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshPage_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoom10ToolStripMenuItem,
            this.zoom25ToolStripMenuItem,
            this.zoom50ToolStripMenuItem,
            this.zoom100ToolStripMenuItem,
            this.zoom200ToolStripMenuItem,
            this.zoom400ToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoom10ToolStripMenuItem
            // 
            this.zoom10ToolStripMenuItem.Name = "zoom10ToolStripMenuItem";
            this.zoom10ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.zoom10ToolStripMenuItem.Tag = "10";
            this.zoom10ToolStripMenuItem.Text = "Zoom 10%";
            this.zoom10ToolStripMenuItem.Click += new System.EventHandler(this.zoom10ToolStripMenuItem_Click);
            // 
            // zoom25ToolStripMenuItem
            // 
            this.zoom25ToolStripMenuItem.Name = "zoom25ToolStripMenuItem";
            this.zoom25ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.zoom25ToolStripMenuItem.Tag = "25";
            this.zoom25ToolStripMenuItem.Text = "Zoom 25%";
            this.zoom25ToolStripMenuItem.Click += new System.EventHandler(this.zoom10ToolStripMenuItem_Click);
            // 
            // zoom50ToolStripMenuItem
            // 
            this.zoom50ToolStripMenuItem.Name = "zoom50ToolStripMenuItem";
            this.zoom50ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.zoom50ToolStripMenuItem.Tag = "50";
            this.zoom50ToolStripMenuItem.Text = "Zoom 50%";
            this.zoom50ToolStripMenuItem.Click += new System.EventHandler(this.zoom10ToolStripMenuItem_Click);
            // 
            // zoom100ToolStripMenuItem
            // 
            this.zoom100ToolStripMenuItem.Name = "zoom100ToolStripMenuItem";
            this.zoom100ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.zoom100ToolStripMenuItem.Tag = "100";
            this.zoom100ToolStripMenuItem.Text = "Zoom 100%";
            this.zoom100ToolStripMenuItem.Click += new System.EventHandler(this.zoom10ToolStripMenuItem_Click);
            // 
            // zoom200ToolStripMenuItem
            // 
            this.zoom200ToolStripMenuItem.Name = "zoom200ToolStripMenuItem";
            this.zoom200ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.zoom200ToolStripMenuItem.Tag = "200";
            this.zoom200ToolStripMenuItem.Text = "Zoom 200%";
            this.zoom200ToolStripMenuItem.Click += new System.EventHandler(this.zoom10ToolStripMenuItem_Click);
            // 
            // zoom400ToolStripMenuItem
            // 
            this.zoom400ToolStripMenuItem.Name = "zoom400ToolStripMenuItem";
            this.zoom400ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.zoom400ToolStripMenuItem.Tag = "400";
            this.zoom400ToolStripMenuItem.Text = "Zoom 400%";
            this.zoom400ToolStripMenuItem.Click += new System.EventHandler(this.zoom10ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutDASHCONToolStripMenuItem,
            this.contrelecToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutDASHCONToolStripMenuItem
            // 
            this.aboutDASHCONToolStripMenuItem.Name = "aboutDASHCONToolStripMenuItem";
            this.aboutDASHCONToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
            this.aboutDASHCONToolStripMenuItem.Text = "About DASHCON";
            this.aboutDASHCONToolStripMenuItem.Click += new System.EventHandler(this.aboutDASHCONToolStripMenuItem_Click);
            // 
            // contrelecToolStripMenuItem
            // 
            this.contrelecToolStripMenuItem.Name = "contrelecToolStripMenuItem";
            this.contrelecToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
            this.contrelecToolStripMenuItem.Text = "Contrelec";
            this.contrelecToolStripMenuItem.Click += new System.EventHandler(this.contrelecToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.AppearancePage.Header.Options.UseTextOptions = true;
            this.tabControl1.AppearancePage.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisPath;
            this.tabControl1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.False;
            this.tabControl1.HeaderButtons = DevExpress.XtraTab.TabButtons.Close;
            this.tabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.True;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabPage = this.xtraTabPage1;
            this.tabControl1.Size = new System.Drawing.Size(967, 467);
            this.tabControl1.TabIndex = 17;
            this.tabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.tabControl1.TabPageWidth = 250;
            this.tabControl1.CloseButtonClick += new System.EventHandler(this.tabControl1_CloseButtonClick);
            this.tabControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.tabControl1_DragOver);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            this.tabControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseMove);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(961, 436);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(961, 436);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 495);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "DASHCON Dash File Converter";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linesToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symbolsToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem findSymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem findLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exportAsPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsPDFToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom25ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom50ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom200ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom400ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSymbolIDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutDASHCONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrelecToolStripMenuItem;
        private DevExpress.XtraTab.XtraTabControl tabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.ToolStripMenuItem exportAllToPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewConversionReportToolStripMenuItem;
    }
}

