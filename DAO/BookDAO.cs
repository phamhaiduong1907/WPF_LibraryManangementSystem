using LibraryManagementSystem.Constants;
using LibraryManagementSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAO
{
    class BookDAO
    {
        LibMangagementSysContext context = new LibMangagementSysContext();
        static BookDAO instance;
        static readonly object instanceLock = new object();
        private BookDAO() { }
        public static BookDAO Instance
        {
            get
            {
                instance = new BookDAO();
                return instance;
            }
        }
        public ObservableCollection<BookDTO> GetAll()
        {
            ObservableCollection<BookDTO> books = new ObservableCollection<BookDTO>();
            var query = from book in context.Books
                        select book;
            foreach(var book in query)
            {
                books.Add(new BookDTO
                {
                    Bookid = book.Bookid,
                    Bookname = book.Bookname,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Subjectcode = book.Subjectcode,
                    Edition = book.Edition,
                    QuantityInStock = book.QuantityInStock,
                    Image = book.Image,
                    Isbn = book.Isbn,
                    IsCurriculum = book.IsCurriculum,
                    Publisheddate = book.Publisheddate,
                    QuantityRequested = (int)book.QuantityRequested
                });
            }
            return books;
        }

        public ObservableCollection<BookDTO> GetBooksByBookNameAndSubjectCode(string bookName, string subjectCode, bool isRefernced)
        {

            ObservableCollection<BookDTO> books = new ObservableCollection<BookDTO>();
            var query = from book in context.Books
                        select book;
            if (!subjectCode.IsNullOrEmpty())
                query = query.Where(b => b.Subjectcode.ToLower().Contains(subjectCode.ToLower()));
            if(!bookName.IsNullOrEmpty())
                query = query.Where(b => b.Bookname.ToLower().Contains(bookName.ToLower()));
            if (isRefernced)
                query = query.Where(b => b.Subjectcode == null);
            foreach (var book in query.ToList())
            {
                books.Add(new BookDTO
                {
                    Bookid = book.Bookid,
                    Bookname = book.Bookname,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Subjectcode = book.Subjectcode,
                    Edition = book.Edition,
                    QuantityInStock = book.QuantityInStock,
                    Image = book.Image,
                    Isbn = book.Isbn,
                    IsCurriculum = book.IsCurriculum,
                    Publisheddate = book.Publisheddate,
                    QuantityRequested = (int)book.QuantityRequested
                });
            }
            return books;
        }

        public BookDTO GetBookById(int id)
        {
            BookDTO bookDTO = null;
            try
            {
                Book book = context.Books.Where(b => b.Bookid == id).FirstOrDefault();
                if(book != null)
                {
                    bookDTO = new BookDTO 
                    { 
                        Bookid = book.Bookid, Bookname = book.Bookname, Author = book.Author, Publisher = book.Publisher, 
                        Subjectcode = book.Subjectcode, Edition = book.Edition, QuantityInStock = book.QuantityInStock,
                        Image = book.Image, Isbn = book.Isbn, IsCurriculum = book.IsCurriculum, Publisheddate = book.Publisheddate,
                        QuantityRequested = (int)book.QuantityRequested
                    };
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return bookDTO;
        }

        public void Delete(int bookid)
        {
            try
            {
                var bookToDelete = context.Books.Where(b => b.Bookid == bookid).FirstOrDefault();
                int count = context.BorrowedInfos.Count(b => b.Status == Constant.BORROWING_VALUE || b.Status == Constant.OVERDUE_VALUE);
                if (count == 0)
                {
                    context.Books.Remove(bookToDelete);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Can't delete this book because there are still borrowing ones!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Create(BookDTO bookDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(bookDTO.Subjectcode)){
                    bookDTO.IsCurriculum = false;
                }
                Book book = new Book
                {
                    Bookid= bookDTO.Bookid, Author= bookDTO.Author, Bookname= bookDTO.Bookname, Edition= bookDTO.Edition,
                    Isbn= bookDTO.Isbn, IsCurriculum = bookDTO.IsCurriculum, Publisheddate = bookDTO.Publisheddate, Publisher = bookDTO.Publisher,
                    QuantityInStock = bookDTO.QuantityInStock, QuantityRequested= 0, Subjectcode = bookDTO.Subjectcode
                };
                context.Books.Add(book);
                context.SaveChanges();
                return book.Bookid;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(BookDTO bookDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(bookDTO.Subjectcode)){
                    bookDTO.IsCurriculum = false;
                }
                Book book = context.Books.Find(bookDTO.Bookid);
                book.Author = bookDTO.Author;
                book.Bookname = bookDTO.Bookname;
                book.Edition = bookDTO.Edition;
                book.Image = bookDTO.Image;
                book.Isbn = bookDTO.Isbn;
                book.IsCurriculum = bookDTO.IsCurriculum;
                book.Publisheddate = bookDTO.Publisheddate;
                book.Publisher = bookDTO.Publisher;
                book.QuantityInStock = bookDTO.QuantityInStock;
                book.QuantityRequested = bookDTO.QuantityRequested;
                book.Subjectcode = bookDTO.Subjectcode;
                context.Books.Update(book);
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
