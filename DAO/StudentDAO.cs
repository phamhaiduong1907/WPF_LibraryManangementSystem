using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAO
{
    class StudentDAO
    {
        private LibMangagementSysContext context = new LibMangagementSysContext();
        private static StudentDAO instance;
        private static readonly object instanceLock = new object();
        private StudentDAO() { }
        public static StudentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StudentDAO();
                    }
                    return instance;
                }
            }
        }
        
        public StudentDTO GetStudentByStudentCode(string studentcode)
        {
            Student student = context.Students.Where(s => s.Studentcode.ToLower().Equals(studentcode.ToLower())).FirstOrDefault();
            StudentDTO studentDTO = null;
            if (student != null)
                studentDTO = new StudentDTO { Studentcode = student.Studentcode, Name = student.Name, Password = student.Password };
            return studentDTO;
        }
    }
}
