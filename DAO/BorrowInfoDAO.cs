using LibraryManagementSystem.Constants;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAO
{
    class BorrowInfoDAO
    {
        LibMangagementSysContext context = new LibMangagementSysContext();
        static BorrowInfoDAO instance;
        static readonly object instanceLock = new object();
        private BorrowInfoDAO() { }
        public static BorrowInfoDAO Instance
        {
            get
            {
                instance = new BorrowInfoDAO();
                return instance;
            }
        }

        public BorrowedInfoDTO GetBorrowInfoById(int borrowid)
        {
            BorrowedInfoDTO brInfoDTO = null;
            BorrowedInfo brInfoModel = context.BorrowedInfos.Include(br => br.Book).Include(br => br.StudentcodeNavigation).Where(br => br.BorrowedId == borrowid).FirstOrDefault();
            if(brInfoModel != null)
            {
                brInfoDTO = new BorrowedInfoDTO();
                brInfoDTO.BorrowedId = brInfoModel.BorrowedId;
                brInfoDTO.Bookid = brInfoModel.Bookid;
                brInfoDTO.Studentcode = brInfoModel.Studentcode;
                brInfoDTO.Borrowdate = brInfoModel.Borrowdate;
                brInfoDTO.Requestdate = brInfoModel.Requestdate;
                brInfoDTO.Duedate = brInfoModel.Duedate;
                brInfoDTO.Returndate = brInfoModel.Returndate;
                brInfoDTO.Status = brInfoModel.Status;
                brInfoDTO.IsAccepted = brInfoModel.IsAccepted;
                brInfoDTO.Quantity = brInfoModel.Quantity;
                brInfoDTO.BookNavigation = new BookDTO
                {
                    Bookname = brInfoModel.Book.Bookname
                };
                brInfoDTO.StudentNavigation = new StudentDTO
                {
                    Name = brInfoModel.StudentcodeNavigation.Name
                };
            }
            return brInfoDTO;
        }

        public ObservableCollection<BorrowedInfoDTO> GetAll(int role, string studentcode)
        {
            ObservableCollection<BorrowedInfoDTO> borrowedInfoDTOs = new ObservableCollection<BorrowedInfoDTO>();
            var query = from obj in context.BorrowedInfos.Include(b => b.Book).Include(b => b.StudentcodeNavigation).OrderByDescending(b => b.Requestdate)
                        select obj;
            if (role != 1)
                query = query.Where(br => br.Studentcode.ToLower().Equals(studentcode.ToLower()));
            foreach(var br in query)
            {
                var borrowInfoObject = new BorrowedInfoDTO
                {
                    BorrowedId = br.BorrowedId,
                    Bookid = br.Bookid,
                    Studentcode = br.Studentcode,
                    Borrowdate = br.Borrowdate,
                    Duedate = br.Duedate,
                    IsAccepted = br.IsAccepted,
                    Note = br.Note,
                    Quantity = br.Quantity,
                    Requestdate = br.Requestdate,
                    Returndate = br.Returndate,
                    Status = br.Status
                };
                borrowInfoObject.BookNavigation = new BookDTO
                {
                    Bookname = br.Book.Bookname
                };
                borrowInfoObject.StudentNavigation = new StudentDTO
                {
                    Name = br.StudentcodeNavigation.Name
                };
                borrowedInfoDTOs.Add(borrowInfoObject);
            }
            return borrowedInfoDTOs;
        }

        public ObservableCollection<BorrowedInfoDTO> GetByCondition(int role, string studentcode, string bookname, bool requestdate, bool borrowdate, bool duedate, bool returndate, DateTime? chosendate,
            DateTime? startdate, DateTime? enddate, int requeststatus, int borrowstatus)
        {
            ObservableCollection<BorrowedInfoDTO> borrowedInfoDTOs = new ObservableCollection<BorrowedInfoDTO>();
            var query = from obj in context.BorrowedInfos.Include(b => b.Book).Include(b => b.StudentcodeNavigation).OrderByDescending(b => b.Requestdate)
                        select obj;
            if (role != 1)
                query = query.Where(br => br.Studentcode.ToLower().Equals(studentcode.ToLower()));
            if (!string.IsNullOrEmpty(studentcode))
                query = query.Where(br => br.Studentcode.ToLower().Equals(studentcode.ToLower()));
            if (!string.IsNullOrEmpty(bookname))
                query = query.Where(br => br.Book.Bookname.ToLower().Contains(bookname.ToLower()));
            if (chosendate != null)
            {
                if (requestdate)
                    query = query.Where(br => br.Requestdate.Equals(chosendate.Value));
                if (borrowdate)
                    query = query.Where(br => br.Borrowdate.Equals(chosendate.Value));
                if (duedate)
                    query = query.Where(br => br.Duedate.Equals(chosendate.Value));
                if (returndate)
                    query = query.Where(br => br.Returndate.Equals(chosendate.Value));
            }
            if(startdate != null)
            {
                if (requestdate)
                    query = query.Where(br => br.Requestdate >= startdate.Value);
                if (borrowdate)
                    query = query.Where(br => br.Borrowdate >= startdate.Value);
                if (duedate)
                    query = query.Where(br => br.Duedate >= startdate.Value);
                if (returndate)
                    query = query.Where(br => br.Returndate >= startdate.Value);
            }
            if(enddate != null)
            {
                if (requestdate)
                    query = query.Where(br => br.Requestdate <= enddate.Value);
                if (borrowdate)
                    query = query.Where(br => br.Borrowdate <= enddate.Value);
                if (duedate)
                    query = query.Where(br => br.Duedate <= enddate.Value);
                if (returndate)
                    query = query.Where(br => br.Returndate <= enddate.Value);
            }
            if (requeststatus > 0)
                query = query.Where(br => br.IsAccepted == requeststatus);
            if (borrowstatus > 0)
                query = query.Where(br => br.Status == borrowstatus);
        
            foreach (var br in query)
            {
                BorrowedInfoDTO brDTOToSelect = new BorrowedInfoDTO
                {
                    BorrowedId = br.BorrowedId,
                    Bookid = br.Bookid,
                    Studentcode = br.Studentcode,
                    Borrowdate = br.Borrowdate,
                    Duedate = br.Duedate,
                    IsAccepted = br.IsAccepted,
                    Note = br.Note,
                    Quantity = br.Quantity,
                    Requestdate = br.Requestdate,
                    Returndate = br.Returndate,
                    Status = br.Status
                };
                brDTOToSelect.StudentNavigation = new StudentDTO { Name =  br.StudentcodeNavigation.Name };
                brDTOToSelect.BookNavigation = new BookDTO { Bookname = br.Book.Bookname };
                borrowedInfoDTOs.Add(brDTOToSelect);
            }
            return borrowedInfoDTOs;
        }

        public ObservableCollection<BorrowedInfoDTO> GetNotificationBorrowedInfo(string studentcode)
        {
            var query = from obj in context.BorrowedInfos.Include(br => br.Book).Include(br => br.StudentcodeNavigation)
                        where obj.Status == 1 || obj.Status == 3
                        select obj;
            query = query.Where(br => br.Studentcode == studentcode);
            ObservableCollection<BorrowedInfoDTO> brInfos = new ObservableCollection<BorrowedInfoDTO>();
            foreach(var br in query)
            {
                BorrowedInfoDTO brDTO = new BorrowedInfoDTO
                {
                    BorrowedId = br.BorrowedId,
                    Bookid = br.Bookid,
                    Studentcode = br.Studentcode,
                    Borrowdate = br.Borrowdate,
                    Duedate = br.Duedate,
                    IsAccepted = br.IsAccepted,
                    Note = br.Note,
                    Quantity = br.Quantity,
                    Requestdate = br.Requestdate,
                    Returndate = br.Returndate,
                    Status = br.Status
                };
                brDTO.StudentNavigation = new StudentDTO { Name = br.StudentcodeNavigation.Name };
                brDTO.BookNavigation = new BookDTO { Bookname = br.Book.Bookname };
                brInfos.Add(brDTO);
            }
            return brInfos;
        }

        //int bookid, string studentcode, int quantity, DateTime requestdate, DateTime duedate, int isAccepted
        public bool CreateRequest(int bookid, string studentcode, int quantity, DateTime duedate)
        {
            try
            {
                var brInfo = new BorrowedInfo();
                brInfo.Bookid = bookid;
                brInfo.Studentcode = studentcode;
                brInfo.Quantity = quantity;
                brInfo.Requestdate = DateTime.Now;
                brInfo.Duedate = duedate;
                brInfo.IsAccepted = Constant.PENDING_VALUE;
                context.BorrowedInfos.Add(brInfo);
                return context.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ProcessRequest(int borrowid, int status)
        {
            try
            {
                var brInfo = context.BorrowedInfos.Where(br => br.BorrowedId == borrowid).FirstOrDefault();
                brInfo.IsAccepted = status;
                context.BorrowedInfos.Update(brInfo);
                return context.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
        }

        public bool CheckoutRequest(int borrowid)
        {
            try
            {
                var brInfo = context.BorrowedInfos.Where(br => br.BorrowedId == borrowid).FirstOrDefault();
                brInfo.Status = 2;
                context.BorrowedInfos.Update(brInfo);
                return context.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
