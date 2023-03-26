using LibraryManagementSystem.Commands;
using LibraryManagementSystem.Constants;
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
    class ListRequestViewModel : BaseViewModel
    {
        Window _view;
        public ListRequestViewModel(Window view)
        {
            _view = view;
            username = "Welcome, " + PseudoSession.Name;
            if (PseudoSession.Role == 2)
            {
                studentcode = PseudoSession.StudentCode;
                isStudentCodeInputReadonly = true;
                actionColumnVisibility = 0;
                requestStatusVisibility = 150;
            }
            else
            {
                isStudentCodeInputReadonly = false;
                actionColumnVisibility = 220;
                requestStatusVisibility = 150;
            }
            Load();
            IsChosenDateEnabled = false;
            IsStartdateEnabled= false;
            IsEnddateEnabled= false;
            IsOnInterval = false;
            approveRequestCommand = new RelayCommand(approveRequest);
            rejectRequestCommand = new RelayCommand(rejectRequest);
            checkoutBookCommand = new RelayCommand(checkoutBook);
            searchCommand = new RelayCommand(search);
            clearCommand = new RelayCommand(clear);
            returnHomeCommand = new RelayCommand(returnHome);
            logoutCommand = new RelayCommand(logout);
        }

        void Load()
        {
            BorrowedInfoList = BorrowInfoDAO.Instance.GetAll(PseudoSession.Role, PseudoSession.StudentCode);
        }

        bool isStudentCodeInputReadonly;
        public bool IsStudentCodeInputReadonly
        {
            get { return isStudentCodeInputReadonly; }
        }

        string studentcode;
        public string StudentCode
        {
            get => studentcode;
            set { studentcode = value; OnPropertyChanged(); }
        }

        string username;
        public string Username
        {
            get => username;
        }

        ObservableCollection<BorrowedInfoDTO> borrowedInfoList;
        public ObservableCollection<BorrowedInfoDTO> BorrowedInfoList
        {
            get { return borrowedInfoList; }
            set { borrowedInfoList= value; OnPropertyChanged(); }
        }

        int actionColumnVisibility;
        public int ActionColumnVisibility
        {
            get => actionColumnVisibility;
        }
        int requestStatusVisibility;
        public int RequestStatusVisibility
        {
            get => requestStatusVisibility;
        }

        bool? requestdate;
        public bool? RequestDate
        {
            get => requestdate.HasValue?requestdate.Value:false;
            set 
            {
                requestdate = value.HasValue ? value.Value : false;
                if (value == true)
                {
                    if (IsOnInterval == true)
                    {
                        IsEnddateEnabled = true;
                        IsStartdateEnabled = true;
                    }
                    else
                    {
                        IsChosenDateEnabled = true;
                    }
                }
                else
                {
                    if (BorrowDate != true && DueDate != true && ReturnDate != true)
                    {
                        IsEnddateEnabled = false;
                        IsStartdateEnabled = false;
                        IsChosenDateEnabled = false;
                    }
                }
                OnPropertyChanged(); 
            }
        }
        bool? borrowdate;
        public bool? BorrowDate
        {
            get => borrowdate.HasValue?borrowdate.Value:false;
            set { 
                borrowdate = value.HasValue ? value.Value : false;
                if (value == true)
                {
                    if (IsOnInterval == true)
                    {
                        IsEnddateEnabled = true;
                        IsStartdateEnabled = true;
                    }
                    else
                    {
                        IsChosenDateEnabled = true;
                    }
                }
                else
                {
                    if (RequestDate != true && DueDate != true && ReturnDate != true)
                    {
                        IsEnddateEnabled = false;
                        IsStartdateEnabled = false;
                        IsChosenDateEnabled = false;
                    }
                }
                OnPropertyChanged(); 
            }
        }
        bool? duedate;
        public bool? DueDate
        {
            get => duedate.HasValue ? duedate.Value : false; 
            set
            {
                duedate = value.HasValue ? value.Value : false;
                if (value == true)
                {
                    if (IsOnInterval == true)
                    {
                        IsEnddateEnabled = true;
                        IsStartdateEnabled = true;
                    }
                    else
                    {
                        IsChosenDateEnabled = true;
                    }
                }
                else
                {
                    if (BorrowDate != true && RequestDate != true && ReturnDate != true)
                    {
                        IsEnddateEnabled = false;
                        IsStartdateEnabled = false;
                        IsChosenDateEnabled = false;
                    }
                }
                OnPropertyChanged();
            }
        }
        bool? returndate;
        public bool? ReturnDate
        {
            get => returndate.HasValue ? returndate.Value : false; 
            set
            {
                returndate = value.HasValue ? value.Value : false;
                if (value == true)
                {
                    if (IsOnInterval == true)
                    {
                        IsEnddateEnabled = true;
                        IsStartdateEnabled = true;
                    }
                    else
                    {
                        IsChosenDateEnabled = true;
                    }
                }
                else
                {
                    if (BorrowDate != true && DueDate != true && RequestDate != true)
                    {
                        IsEnddateEnabled = false;
                        IsStartdateEnabled = false;
                        IsChosenDateEnabled = false;
                    }
                }
                OnPropertyChanged();
            }
        }

        DateTime? chosendate;
        public DateTime? ChosenDate
        {
            get => chosendate; 
            set
            {
                chosendate = value;
                OnPropertyChanged();
            }
        }

        DateTime? startdate;
        public DateTime? Startdate
        {
            get => startdate;
            set { startdate = value; OnPropertyChanged(); }
        }
        DateTime? enddate;
        public DateTime? Enddate
        {
            get => enddate; 
            set
            {
                enddate = value;
                OnPropertyChanged();
            }
        }
        string bookname;
        public string Bookname
        {
            get => bookname; 
            set
            {
                bookname = value;
                OnPropertyChanged();
            }
        }

        int borrowstatus;
        public int BorrowStatus
        {
            get => borrowstatus;
            set
            {
                borrowstatus = value;
                OnPropertyChanged();
            }
        }

        int requeststatus;
        public int RequestStatus
        {
            get => requeststatus;
            set
            {
                requeststatus = value;
                OnPropertyChanged();
            }
        }

        bool? isOnInterval;
        public bool? IsOnInterval
        {
            get => isOnInterval; 
            set
            {
                isOnInterval = value;
                if(BorrowDate == true || RequestDate == true || DueDate == true || ReturnDate == true)
                {
                    if (value == true)
                    {
                        IsChosenDateEnabled = false;
                        IsEnddateEnabled = true;
                        IsStartdateEnabled = true;
                    }
                    else
                    {
                        IsChosenDateEnabled = true;
                        IsEnddateEnabled = false;
                        IsStartdateEnabled = false;
                    }
                }
                OnPropertyChanged();
            }
        }

        bool? isChosenDateEnabled;
        public bool? IsChosenDateEnabled
        {
            get => isChosenDateEnabled;
            set
            {
                isChosenDateEnabled = value;
                if(value != true)
                {
                    ChosenDate = null;
                }
                OnPropertyChanged();
            }
        }
        bool? isStartdateEnabled;
        public bool? IsStartdateEnabled
        {
            get => isStartdateEnabled;
            set
            {
                isStartdateEnabled = value;
                if(value != true)
                {
                    Startdate = null;
                }
                OnPropertyChanged();
            }
        }
        bool? isEnddateEnabled;
        public bool? IsEnddateEnabled
        {
            get => isEnddateEnabled; 
            set
            {
                isEnddateEnabled = value;
                if(value != true)
                {
                    Enddate = null;
                }
                OnPropertyChanged();
            }
        }

        #region Search Command
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get => searchCommand;
        }
        private void search(object parameter)
        {
            //MessageBox.Show($"Start Date: {Startdate} - End Date: {Enddate} - Chosen Date: {ChosenDate}\n" +
            //    $"Is Request Date: {RequestDate} - Is Borrow Date: {BorrowDate} - Is Due Date: {DueDate} - Is Returned Date: {ReturnDate}\n" +
            //    $"StudentCode: {StudentCode} - Book Name: {Bookname}\n" +
            //    $"Request Status: {RequestStatus} - Borrow Status: {BorrowStatus}");

            BorrowedInfoList = BorrowInfoDAO.Instance.GetByCondition(PseudoSession.Role, StudentCode, Bookname, (bool)RequestDate, (bool)BorrowDate, (bool)DueDate, (bool)ReturnDate, ChosenDate, Startdate, Enddate, RequestStatus, BorrowStatus);
        }
        #endregion

        #region Clear Command
        RelayCommand clearCommand;
        public RelayCommand ClearCommand
        {
            get => clearCommand;
        }
        private void clear(object parameter)
        {
            RequestDate = false;
            BorrowDate = false;
            DueDate = false;
            ReturnDate = false;
            IsOnInterval = false;
            if (PseudoSession.Role == 1)
                StudentCode = "";
            Bookname = "";
            RequestStatus = 0;
            BorrowStatus= 0;
        }
        #endregion

        #region Approve Request Command
        private RelayCommand approveRequestCommand;
        public RelayCommand ApproveRequestCommand
        {
            get => approveRequestCommand;
        }
        private void approveRequest(object parameter)
        {
            int borrowid = (int)parameter;
            bool process = BorrowInfoDAO.Instance.ProcessRequest(borrowid, Constant.APPROVED_VALUE);
            if (process)
            {
                for (int i = 0; i < BorrowedInfoList.Count(); i++)
                {
                    if (BorrowedInfoList[i].BorrowedId == borrowid)
                    {
                        BorrowedInfoDTO brDTO = BorrowInfoDAO.Instance.GetBorrowInfoById(borrowid);
                        BorrowedInfoList[i].BookNavigation.Bookname = brDTO.BookNavigation.Bookname;
                        BorrowedInfoList[i].Bookid = brDTO.Bookid;
                        BorrowedInfoList[i].Studentcode = brDTO.Studentcode;
                        BorrowedInfoList[i].StudentNavigation.Name = brDTO.StudentNavigation.Name;
                        BorrowedInfoList[i].Requestdate = brDTO.Requestdate;
                        BorrowedInfoList[i].Borrowdate = brDTO.Borrowdate;
                        BorrowedInfoList[i].Duedate = brDTO.Duedate;
                        BorrowedInfoList[i].Returndate = brDTO.Returndate;
                        BorrowedInfoList[i].Quantity = brDTO.Quantity;
                        BorrowedInfoList[i].Status = brDTO.Status;
                        BorrowedInfoList[i].IsAccepted = brDTO.IsAccepted;
                        MessageBox.Show($"Status: {brDTO.Status} - Borrow Date: {brDTO.Borrowdate}");
                        break;
                    }
                }
                MessageBox.Show("Process successfully!");
            }
            else
            {
                MessageBox.Show("Process failed!");
            }
        }
        #endregion

        #region Reject Request Command
        private RelayCommand rejectRequestCommand;
        public RelayCommand RejectRequestCommand
        {
            get => rejectRequestCommand;
        }
        private void rejectRequest(object parameter)
        {
            int borrowid = (int)parameter;
            bool process = BorrowInfoDAO.Instance.ProcessRequest(borrowid, Constant.REJECTED_VALUE);
            if (process)
            {
                for (int i = 0; i < BorrowedInfoList.Count(); i++)
                {
                    if (BorrowedInfoList[i].BorrowedId == borrowid)
                    {
                        BorrowedInfoDTO brDTO = BorrowInfoDAO.Instance.GetBorrowInfoById(borrowid);
                        BorrowedInfoList[i].BookNavigation.Bookname = brDTO.BookNavigation.Bookname;
                        BorrowedInfoList[i].Bookid = brDTO.Bookid;
                        BorrowedInfoList[i].Studentcode = brDTO.Studentcode;
                        BorrowedInfoList[i].StudentNavigation.Name = brDTO.StudentNavigation.Name;
                        BorrowedInfoList[i].Requestdate = brDTO.Requestdate;
                        BorrowedInfoList[i].Borrowdate = brDTO.Borrowdate;
                        BorrowedInfoList[i].Duedate = brDTO.Duedate;
                        BorrowedInfoList[i].Returndate = brDTO.Returndate;
                        BorrowedInfoList[i].Quantity = brDTO.Quantity;
                        BorrowedInfoList[i].Status = brDTO.Status;
                        BorrowedInfoList[i].IsAccepted = brDTO.IsAccepted;
                        break;
                    }
                }
                MessageBox.Show("Process successfully!");
            }
            else
            {
                MessageBox.Show("Process failed!");
            }
        }
        #endregion

        #region Checkout Returned Book Command
        private RelayCommand checkoutBookCommand;
        public RelayCommand CheckoutBookCommand
        {
            get => checkoutBookCommand;
        }
        private void checkoutBook(object parameter)
        {
            int borrowid = (int)parameter;
            bool process = BorrowInfoDAO.Instance.CheckoutRequest(borrowid);
            if (process)
            {
                for (int i = 0; i < BorrowedInfoList.Count(); i++)
                {
                    if (BorrowedInfoList[i].BorrowedId == borrowid)
                    {
                        BorrowedInfoDTO brDTO = BorrowInfoDAO.Instance.GetBorrowInfoById(borrowid);
                        BorrowedInfoList[i].BookNavigation.Bookname = brDTO.BookNavigation.Bookname;
                        BorrowedInfoList[i].Bookid = brDTO.Bookid;
                        BorrowedInfoList[i].Studentcode = brDTO.Studentcode;
                        BorrowedInfoList[i].StudentNavigation.Name = brDTO.StudentNavigation.Name;
                        BorrowedInfoList[i].Requestdate = brDTO.Requestdate;
                        BorrowedInfoList[i].Borrowdate = brDTO.Borrowdate;
                        BorrowedInfoList[i].Duedate = brDTO.Duedate;
                        BorrowedInfoList[i].Returndate = brDTO.Returndate;
                        BorrowedInfoList[i].Quantity = brDTO.Quantity;
                        BorrowedInfoList[i].Status = brDTO.Status;
                        BorrowedInfoList[i].IsAccepted = brDTO.IsAccepted;
                        break;
                    }
                }
                MessageBox.Show("Process successfully!");
            }
            else
            {
                MessageBox.Show("Process failed!");
            }
        }
        #endregion

        #region Return Home Command
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
