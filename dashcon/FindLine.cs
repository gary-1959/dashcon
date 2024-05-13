using System;
using System.Drawing;
using System.Windows.Forms;

namespace dashcon
{

    public partial class FindLine: Form
    {
        public Point LinePoint
        {
            get
            {
                return (new Point(Convert.ToInt32(lineX.Text), Convert.ToInt32(lineY.Text)));
            }
            set
            {
                Point p = (Point)value;
                lineX.Text = value.X.ToString();
                lineY.Text = value.Y.ToString();
            }
        }
    public FindLine()
        {
            InitializeComponent();
        }

        private void FindLine_Load(object sender, EventArgs e)
        {

        }
    }
}
