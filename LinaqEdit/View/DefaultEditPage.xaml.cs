using LinaqEdit.Model;
using LinaqEdit.ViewModel;
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

namespace LinaqEdit.View
{
    /// <summary>
    /// Interaction logic for DefaultEditPage.xaml
    /// </summary>
    public partial class DefaultEditPage : Page
    {
        public DefaultEditPage()
        {
            InitializeComponent();
            DataContext = new DefaultEditPageViewModel();
        }

        public DefaultEditPage(Document dc)
        {
            InitializeComponent();
            DataContext = new DefaultEditPageViewModel(dc);
        }
    }
}
