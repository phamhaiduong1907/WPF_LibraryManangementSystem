using LibraryManagementSystem.Constants;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using(var context = new LibMangagementSysContext())
            {
                List<BorrowedInfo> borrowedInfos = context.BorrowedInfos.Where(br => DateTime.Compare(br.Duedate, DateTime.Now) < 0 && br.Status == Constant.BORROWING_VALUE).ToList();
                foreach(var br in borrowedInfos)
                {
                    br.Status = Constant.OVERDUE_VALUE;
                }
                context.BorrowedInfos.UpdateRange(borrowedInfos);
                context.SaveChanges();
            }
        }
    }
}
