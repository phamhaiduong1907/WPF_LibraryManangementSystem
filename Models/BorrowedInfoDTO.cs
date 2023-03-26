using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    class BorrowedInfoDTO : BaseDTO
    {
        int bookid;
        public int Bookid { get => bookid; set { bookid = value; OnPropertyChanged(); } }

        string studentcode;
        public string Studentcode { get => studentcode; set { studentcode = value; OnPropertyChanged(); } }

        DateTime? borrowdate;
        public DateTime? Borrowdate { get => borrowdate; set { borrowdate = value; OnPropertyChanged(); } }

        DateTime? returndate;
        public DateTime? Returndate { get => returndate; set { returndate = value; OnPropertyChanged(); } }

        int? status;
        public int? Status { get => status; set { status = value; OnPropertyChanged(); } }

        int quantity;
        public int Quantity { get => quantity; set { quantity = value; OnPropertyChanged(); } }

        int isAccepted;
        public int IsAccepted { get => isAccepted; set { isAccepted = value; OnPropertyChanged(); } }

        DateTime requestdate;
        public DateTime Requestdate { get => requestdate; set { requestdate = value; OnPropertyChanged(); } }

        string? note;
        public string? Note { get => note; set { note = value; OnPropertyChanged(); } }

        int borrowedid;
        public int BorrowedId { get => borrowedid; set { borrowedid = value; OnPropertyChanged(); } }

        DateTime duedate;
        public DateTime Duedate { get => duedate; set { duedate = value; OnPropertyChanged(); } }

        BookDTO book;
        public BookDTO BookNavigation { get => book; set { book = value; OnPropertyChanged(); } }

        StudentDTO student;
        public StudentDTO StudentNavigation { get => student; set { student = value; OnPropertyChanged(); } }
    }
}
