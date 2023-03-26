using LibraryManagementSystem.Models;
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
    /// Interaction logic for ListBook.xaml
    /// </summary>
    public partial class ListBook : Window
    {
        public ListBook()
        {
            InitializeComponent();
            this.DataContext = new ListBookViewModel(this);
            //List<Book> books = new List<Book>()
            //{
            //    new Book() {Bookid=1, Bookname="Intermediate C: Networking and Razor Pages With many other insights in ASP.NET", Author = "Raman van der Vatt",Publisher="Cambridge University", Edition="1st", QuantityInStock=50, Subjectcode="PRN221"}
            //};
            //lvBooks.ItemsSource = books;
        }
    }
}
