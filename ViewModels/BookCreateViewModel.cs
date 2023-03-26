using LibraryManagementSystem.Commands;
using LibraryManagementSystem.DAO;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utility;
using LibraryManagementSystem.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.ViewModels
{
    class BookCreateViewModel : BaseViewModel
    {
        Window _view;
        public BookCreateViewModel(Window view) 
        {
            _view= view;
            BookObject = new BookDTO();
            BookObject.QuantityInStock = 1;
            createBookCommand = new RelayCommand(createBook);
            uploadImageCommand = new RelayCommand(uploadImage);
            saveBookCommand = new RelayCommand(saveBook);
            returnHomeCommand = new RelayCommand(returnHome);
            image = "../Resources/default_book.png";
        }

        BookDTO bookObject;
        public BookDTO BookObject
        {
            get => bookObject;
            set { bookObject = value; OnPropertyChanged(); }
        }

        string image;
        public string Image
        {
            get => image;
            set { image = value; OnPropertyChanged(); }
        }

        #region Create Book Command
        RelayCommand createBookCommand;
        public RelayCommand CreateBookCommand
        {
            get => createBookCommand;
        }
        private void createBook(object parameter)
        {
            try
            {
                BookObject.Image = Image;
                int insertBook = BookDAO.Instance.Create(BookObject);
                if (insertBook > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Add book successfully, please review and update if necessary!");
                    BookDetail window = new BookDetail(insertBook);
                    window.Show();
                    _view.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Upload Image Command
        RelayCommand uploadImageCommand;
        public RelayCommand UploadImageCommand
        {
            get => uploadImageCommand;
        }
        private void uploadImage(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an image";
            openFileDialog.Filter = "Image files|*.jpg;*.jpeg;*.png";
            if(openFileDialog.ShowDialog() == true)
            {
                string fileType = Path.GetExtension(openFileDialog.FileName);
                if(!fileType.Equals(".jpg") && !fileType.Equals(".png") && !fileType.Equals(".jpeg"))
                {
                    MessageBox.Show("Please choose file with extension .jpg / .png / .jpeg!");
                }
                else
                {
                    Image = openFileDialog.FileName;
                }
            }
        }
        #endregion

        #region Save Book Command
        RelayCommand saveBookCommand;
        public RelayCommand SaveBookCommand
        {
            get => saveBookCommand;
        }
        private void saveBook(object parameter)
        {
            try
            {
                if (BookObject.Publisheddate == null)
                    throw new Exception("Please fill in the published date!");
                int createdId = BookDAO.Instance.Create(BookObject);
                if(createdId > 0)
                {
                    BookDTO bookToUpdateImage = BookDAO.Instance.GetBookById(createdId);
                    if (!Image.Equals("../Resources/default_book.png"))
                    {
                        bookToUpdateImage.Image = ImageHandler.Instance.uploadImage(Image);
                    }
                    else
                    {
                        bookToUpdateImage.Image = "../Resources/default_book.png";
                    }
                    BookDAO.Instance.Update(bookToUpdateImage);
                    MessageBox.Show("Book has been added successfully, please review and update if necessary!");
                    BookDetail bookDetail = new BookDetail(createdId);
                    bookDetail.Show();
                    _view.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Return Home Command
        RelayCommand returnHomeCommand;
        public RelayCommand ReturnHomeCommand { get => returnHomeCommand;}
        private void returnHome(object parameter)
        {
            ListBook window= new ListBook();
            window.Show();
            _view.Close();
        }
        #endregion
    }
}
