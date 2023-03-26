using LibraryManagementSystem.Commands;
using LibraryManagementSystem.DAO;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utility;
using LibraryManagementSystem.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace LibraryManagementSystem.ViewModels
{
    class BookDetailViewModel : BaseViewModel
    {
        Window _view;
        int _bookid;
        public BookDetailViewModel(Window view, int bookid)
        {
            _view = view;
            _bookid = bookid;
            bookObject = BookDAO.Instance.GetBookById(bookid);
            if (PseudoSession.Role == 1)
            {
                isReadOnly = false;
                isDatePickerEnabled = true;
                isVisible = Visibility.Visible;
                isRequestVisible = Visibility.Collapsed;
                _view.Height = 550;
            }
            else 
            { 
                isDatePickerEnabled = false;
                isReadOnly = true; 
                isVisible = Visibility.Collapsed;
                isRequestVisible = Visibility.Visible;
            }
            Quantity = 1;
            Duedate = DateTime.Now.AddMonths(1);
            returnHomeCommand = new RelayCommand(returnHome);
            updateBookCommand = new RelayCommand(updateBook);
            uploadImageCommand = new RelayCommand(uploadImage);
            createRequestCommand = new RelayCommand(createRequest);
            clearInputCommand = new RelayCommand(clearInput);
        }

        bool isReadOnly;
        public bool IsReadOnly
        {
            get => isReadOnly;
        }

        bool isDatePickerEnabled;
        public bool IsDatePickerEnabled
        {
            get => isDatePickerEnabled;
        }


        Visibility isVisible;
        public Visibility IsVisible
        {
            get => isVisible;
        }

        Visibility isRequestVisible;
        public Visibility IsRequestVisible
        {
            get => isRequestVisible;
        }

        private BookDTO bookObject;
        public BookDTO BookObject
        {
            get => bookObject;
            set
            {
                bookObject = value;
                OnPropertyChanged();
                OnPropertyChanged("QuantityAvailable");
                OnPropertyChanged("RequestFieldVisibility");
                OnPropertyChanged("MessageFieldVisibility");
            }
        }

        public int QuantityAvailable
        {
            get => BookObject.QuantityInStock - BookObject.QuantityRequested;
        }

        public Visibility RequestFieldVisibility
        {
            get => QuantityAvailable > 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        
        public Visibility MessageFieldVisibility
        {
            get => QuantityAvailable > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        int quantity;
        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(); }
        }

        DateTime? duedate;
        public DateTime? Duedate
        {
            get => duedate;
            set { duedate = value; OnPropertyChanged(); }
        }

        #region Return Home Page
        private RelayCommand returnHomeCommand;
        public RelayCommand ReturnHomeCommand
        {
            get => returnHomeCommand;
        }
        private void returnHome(object parameter)
        {
            ListBook window = new ListBook();
            window.Show();
            _view.Close();
        }
        #endregion

        #region Clear Command
        private RelayCommand clearInputCommand;
        public RelayCommand ClearInputCommand
        {
            get => clearInputCommand;
        }
        private void clearInput(object parameter)
        {
            BookObject = BookDAO.Instance.GetBookById(_bookid);
        }
        #endregion

        #region Upload Image
        private RelayCommand uploadImageCommand;
        public RelayCommand UploadImageCommand
        {
            get => uploadImageCommand;
        }
        private void uploadImage(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an image";
            openFileDialog.Filter = "Image files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileType = Path.GetExtension(openFileDialog.FileName);
                if (!fileType.Equals(".jpg") && !fileType.Equals(".png") && !fileType.Equals(".jpeg"))
                {
                    MessageBox.Show("Please choose file with extension .jpg / .png / .jpeg!");
                }
                else
                {
                    BookObject.Image = openFileDialog.FileName;
                }
            }
        }
        #endregion

        #region Update Book
        private RelayCommand updateBookCommand;
        public RelayCommand UpdateBookCommand
        {
            get => updateBookCommand;
        }
        private void updateBook(object parameter)
        {
            try
            {
                BookDTO oldBookObject = BookDAO.Instance.GetBookById(_bookid);
                if(!oldBookObject.Image.Equals(BookObject.Image))
                {
                    BookObject.Image = ImageHandler.Instance.uploadImage(BookObject.Image);
                }
                bool isSuccess = BookDAO.Instance.Update(BookObject);
                if(isSuccess)
                {
                    MessageBox.Show("Update book successfully!");
                    BookObject = BookDAO.Instance.GetBookById(_bookid);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Create Request
        private RelayCommand createRequestCommand;
        public RelayCommand CreateRequestCommand
        {
            get => createRequestCommand;
        }
        private void createRequest(object parameter)
        {
            ObservableCollection<BorrowedInfoDTO> brDTOs = BorrowInfoDAO.Instance.GetNotificationBorrowedInfo(PseudoSession.StudentCode);
            if(brDTOs.Any(br => br.Status == 3))
            {
                MessageBox.Show("You borrowed this book and it has been overdue now. Please check and return before submitting new request!");
                return;
            }
            try
            {
                if (duedate == null)
                    throw new Exception("Please select due date!");
                if(DateTime.Compare(DateTime.Now, (DateTime)duedate) >= 0)
                {
                    throw new Exception("Please choose the suitable due date!");
                }
                bool create = BorrowInfoDAO.Instance.CreateRequest(_bookid, PseudoSession.StudentCode, quantity, (DateTime)duedate);
                if (create)
                {
                    MessageBox.Show("Send request successfully, please wait admin approve your request!");

                    Duedate = DateTime.Now.AddMonths(1);
                    BookObject = BookDAO.Instance.GetBookById(_bookid);
                    Quantity = 1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
