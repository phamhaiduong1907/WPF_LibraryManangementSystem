using LibraryManagementSystem.Commands;
using LibraryManagementSystem.DAO;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Views;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        Window _view;
        public LoginViewModel(Window view)
        {
            _view = view;
            Student = new StudentDTO();
            loginCommand = new RelayCommand(login);
        }
        private StudentDTO student;
        public StudentDTO Student
        {
            get => student;
            set { student = value; OnPropertyChanged(); }
        }

        #region Login
        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get => loginCommand;
        }
        private void login(object parameter)
        {
            try
            {
                StudentDTO studentDTO = StudentDAO.Instance.GetStudentByStudentCode(Student.Studentcode);
                var conf = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
                string adminUsername = conf["Admin:username"];
                string adminPassword = conf["Admin:password"];
                if (studentDTO != null)
                {
                    PseudoSession.Name = studentDTO.Name;
                    PseudoSession.Role = 2;
                    PseudoSession.StudentCode = studentDTO.Studentcode;
                    ListBook window = new ListBook();
                    window.Show();
                    _view.Close();
                }
                else
                {
                    if (Student.Studentcode.ToLower().Equals(adminUsername.ToLower()) && Student.Password.ToLower().Equals(adminPassword.ToLower()))
                    {
                        PseudoSession.Name = "admin";
                        PseudoSession.Role = 1;
                        ListBook window = new ListBook();
                        window.Show();
                        _view.Close();
                    }
                    else
                    {
                        throw new Exception("Username or password might be incorrect! Please check again!");
                    }
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
