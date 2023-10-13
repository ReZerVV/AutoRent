using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoRent.Application.Views
{
    /// <summary>
    /// Interaction logic for SearchCarsView.xaml
    /// </summary>
    public partial class SearchCarsView : UserControl
    {
        public SearchCarsView()
        {
            InitializeComponent();
        }

        private void AllowOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}
