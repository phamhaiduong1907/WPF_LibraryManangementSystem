using LibraryManagementSystem.ViewModels;
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
using System.Windows.Shapes;

namespace LibraryManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for ListRequest.xaml
    /// </summary>
    public partial class ListRequest : Window
    {
        public ListRequest()
        {
            InitializeComponent();
            this.DataContext = new ListRequestViewModel(this);
        }
    }
}
