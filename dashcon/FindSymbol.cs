using System.Windows.Forms;

namespace dashcon
{

    public partial class FindSymbol : Form
    {
        public TextBox SymbolNumber
        {
            get
            {
                return (symbolNumber);
            }
            set
            {
                symbolNumber = value;
            }
        }
    public FindSymbol()
        {
            InitializeComponent();
        }
    }
}
