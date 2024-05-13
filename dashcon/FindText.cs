using System;
using System.Windows.Forms;

namespace dashcon
{

    public partial class FindText: Form
    {
        public string SearchText
        {
            get
            {
                return (searchText.Text);
            }
            set
            {
                searchText.Text = value;
            }
        }
    public FindText()
        {
            InitializeComponent();
        }

        private void FindLine_Load(object sender, EventArgs e)
        {

        }
    }
}
