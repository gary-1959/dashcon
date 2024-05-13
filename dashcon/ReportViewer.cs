using System.Windows.Forms;

namespace dashcon
{
    public partial class ReportViewer : Form
    {
        public string ReportText
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }
        public ReportViewer()
        {
            InitializeComponent();
        }
    }
}
