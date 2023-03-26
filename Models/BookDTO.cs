using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    class BookDTO : BaseDTO
    {
        int bookid;
        public int Bookid { get => bookid; set { bookid = value; OnPropertyChanged(); } }

        string bookname;
        public string Bookname { get => bookname; set { bookname = value; OnPropertyChanged(); } }

        string? image;
        public string? Image { get => image; set { image = value; OnPropertyChanged(); } }

        string author;
        public string Author { get => author; set { author = value; OnPropertyChanged(); } }

        string publisher;
        public string Publisher { get => publisher; set { publisher = value; OnPropertyChanged(); } }

        DateTime? publisheddate;
        public DateTime? Publisheddate { get => publisheddate; set { publisheddate = value; OnPropertyChanged(); } }

        string? edition;
        public string? Edition { get => edition; set { edition = value; OnPropertyChanged(); } }

        string? isbn;
        public string? Isbn { get => isbn; set { isbn = value; OnPropertyChanged(); } }

        string? subjectcode;
        public string? Subjectcode { get => subjectcode; set { subjectcode = value; OnPropertyChanged(); } }

        int quantityInStock;
        public int QuantityInStock { get => quantityInStock; set { quantityInStock = value; OnPropertyChanged(); } }

        bool isCurriculum;
        public bool IsCurriculum { get => isCurriculum; set { isCurriculum = value; OnPropertyChanged(); } }

        int quantityRequested;
        public int QuantityRequested { get => quantityRequested; set { quantityRequested = value; OnPropertyChanged(); } }
    }
}
