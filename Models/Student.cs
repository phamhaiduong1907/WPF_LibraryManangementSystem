using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Student
{
    public string Studentcode { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<BorrowedInfo> BorrowedInfos { get; } = new List<BorrowedInfo>();
}
