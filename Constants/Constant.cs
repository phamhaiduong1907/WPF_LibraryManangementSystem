using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Constants
{
    class Constant
    {
        public static readonly string PENDING_REQUEST = "Pending";
        public static readonly int PENDING_VALUE = 1;
        public static readonly string APPROVED_REQUEST = "Approved";
        public static readonly int APPROVED_VALUE = 2;
        public static readonly string REJECTED_REQUEST = "Rejected";
        public static readonly int REJECTED_VALUE = 3;
        public static readonly string BORROWING_STATUS = "Borrowing";
        public static readonly int BORROWING_VALUE = 1;
        public static readonly string RETURNED_STATUS = "Returned";
        public static readonly int RETURNED_VALUE = 2;
        public static readonly string OVERDUE_STATUS = "Overdue";
        public static readonly int OVERDUE_VALUE = 3;
    }
}
