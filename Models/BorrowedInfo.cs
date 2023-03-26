using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class BorrowedInfo
{
    public int Bookid { get; set; }

    public string Studentcode { get; set; } = null!;

    public DateTime? Borrowdate { get; set; }

    public DateTime? Returndate { get; set; }

    public int? Status { get; set; }

    public int Quantity { get; set; }

    public int IsAccepted { get; set; }

    public DateTime Requestdate { get; set; }

    public string? Note { get; set; }

    public int BorrowedId { get; set; }

    public DateTime Duedate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Student StudentcodeNavigation { get; set; } = null!;
}
