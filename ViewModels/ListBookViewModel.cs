using LibraryManagementSystem.Commands;
using LibraryManagementSystem.DAO;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.ViewModels
{
    class ListBookViewModel : BaseViewModel
    {
        Window _view;
        public ListBookViewModel(Window view) 
        {
            username = "Welcome, " + PseudoSession.Name;
            logoutCommand = new RelayCommand(logout);
            searchCommand = new RelayCommand(search);
            requestPageNavigate = new RelayCommand(requestPageOpen);
            deleteCommand = new RelayCommand(delete);
            viewPageNavigate = new RelayCommand(openViewPage);
            createPageNavigate = new RelayCommand(openCreatePage);
            notifyCommand = new RelayCommand(notify);
            if (PseudoSession.Role == 1)
                btnDeleteVisibility = Visibility.Visible;
            else
                btnDeleteVisibility = Visibility.Collapsed;
            if (PseudoSession.Role == 1)
                btnCreateVisibility = Visibility.Visible;
            else
                btnCreateVisibility = Visibility.Collapsed;
            _view = view;
            Load();
        }

        private void Load() => Books = BookDAO.Instance.GetBooksByBookNameAndSubjectCode(Bookname, Subjectcode, false);

        Visibility btnDeleteVisibility;
        public Visibility BtnDeleteVisibility
        {
            get => btnDeleteVisibility;
        }

        Visibility btnCreateVisibility;
        public Visibility BtnCreateVisibility
        {
            get => btnCreateVisibility;
        }

        string username;
        public string Username
        {
            get => username;
        }

        private bool isTextBoxReadOnly;
        public bool IsTextBoxReadOnly
        {
            get => isTextBoxReadOnly;
            set { isTextBoxReadOnly = value; Subjectcode = ""; OnPropertyChanged(); }
        }

        private string subjectcode;
        public string Subjectcode
        {
            get => subjectcode;
            set { subjectcode = value; OnPropertyChanged();}
        }

        private string bookname;
        public string Bookname
        {
            get => bookname;
            set { bookname = value; OnPropertyChanged();}
        }

        private ObservableCollection<BookDTO> books;
        public ObservableCollection<BookDTO> Books
        {
            get => books;
            set { books = value; OnPropertyChanged(); }
        }

        #region Notification Operation
        RelayCommand notifyCommand;
        public RelayCommand NotifyCommand
        {
            get => notifyCommand;
        }
        private void notify(object parameter)
        {
            if(PseudoSession.Role == 2)
            {
                ObservableCollection<BorrowedInfoDTO> brDTOs = BorrowInfoDAO.Instance.GetNotificationBorrowedInfo(PseudoSession.StudentCode);
                string message = "Please be noticed your book borrowing status!\n";
                if(brDTOs.Where(br => br.Status == 3).Count() > 0)
                {
                    message += "Overdue books:";
                    foreach(var br in brDTOs)
                    {
                        message += $"\n- {br.BookNavigation.Bookname} - Due Date: {br.Duedate.ToString("dd/MM/yyyy")} - Over {(int)Math.Floor((br.Duedate.Date - DateTime.Now).TotalDays)} day(s)";
                    }
                }
                if(brDTOs.Where(br => br.Status == 1).Count() > 0)
                {
                    message += "Borrowing books:";
                    foreach (var br in brDTOs)
                    {
                        message += $"\n- {br.BookNavigation.Bookname} - Due Date: {br.Duedate.ToString("dd/MM/yyyy")} - {(int)Math.Floor((br.Duedate.Date - DateTime.Now).TotalDays)} day(s) to due date";
                    }
                }
                if(brDTOs.Count() == 0)
                {
                    return;
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            //ObservableCollection<BorrowedInfoDTO> brDTOs = BorrowInfoDAO.Instance.GetNotificationBorrowedInfo(PseudoSession.StudentCode);
            //MessageBox.Show($"Welcome {PseudoSession.Name} - {PseudoSession.Role} - books: {brDTOs.Count()}");
        }
        #endregion

        #region Search Operation
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get => searchCommand; 
            set { searchCommand = value; OnPropertyChanged();}
        }
        private void search(object parameter)
        {
            try
            {
                ObservableCollection<BookDTO> bookDTOs = BookDAO.Instance.GetBooksByBookNameAndSubjectCode(Bookname, Subjectcode, IsTextBoxReadOnly);
                Books = bookDTOs;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Open Request Page
        private RelayCommand requestPageNavigate;
        public RelayCommand RequestPageNavigate
        {
            get => requestPageNavigate;
        }
        private void requestPageOpen(object parameter)
        {
            ListRequest window = new ListRequest();
            window.Show();
            _view.Close();
        }
        #endregion

        #region Delete Operation
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get => deleteCommand;
        }
        private void delete(object parameter)
        {
            try
            {
                int bookid = (int)parameter;
                BookDAO.Instance.Delete(bookid);
                Load();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Open View Page
        private RelayCommand viewPageNavigate;
        public RelayCommand ViewPageNavigate
        {
            get => viewPageNavigate;
        }
        private void openViewPage(object parameter)
        {
            int bookid = (int)parameter;
            BookDetail window = new BookDetail(bookid);
            window.Show();
            _view.Close();
        }
        #endregion

        #region Open Create Page
        private RelayCommand createPageNavigate;
        public RelayCommand CreatePageNavigate
        {
            get => createPageNavigate;
        }
        private void openCreatePage(object parameter)
        {
            BookCreate window = new BookCreate();
            window.Show();
            _view.Close();
        }
        #endregion

        #region Logout
        RelayCommand logoutCommand;
        public RelayCommand LogoutCommand
        {
            get => logoutCommand;
        }
        private void logout(object parameter)
        {
            MainWindow login = new MainWindow();
            PseudoSession.Name = "";
            PseudoSession.Role = 0;
            login.Show();
            _view.Close();
        }
        #endregion
    }
}
