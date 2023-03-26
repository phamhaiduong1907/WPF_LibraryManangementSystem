using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    class StudentDTO : BaseDTO
    {
        string studentcode;
        public string Studentcode { get => studentcode; set { studentcode = value; OnPropertyChanged(); } }

        string password;
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

        string name;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
    }
}
