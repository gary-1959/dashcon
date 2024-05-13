using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace dashcon
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DateTime buildDate = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;

            copyright.Text = "DASHCON for Windows Version " + version + "\n";
            copyright.Text += "Date: " + buildDate + "\n";
            copyright.Text += "Copyright (C) CONTRELEC Ltd. All rights reserved.";
        }

        private void contrelecLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.contrelec.co.uk");
        }
    }
}
