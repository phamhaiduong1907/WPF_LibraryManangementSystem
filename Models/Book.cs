using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Book
{
    public int Bookid { get; set; }

    public string Bookname { get; set; } = null!;

    public string? Image { get; set; }

    public string Author { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public DateTime? Publisheddate { get; set; }

    public string? Edition { get; set; }

    public string? Isbn { get; set; }

    public string? Subjectcode { get; set; }

    public int QuantityInStock { get; set; }

    public bool IsCurriculum { get; set; }

    public int? QuantityRequested { get; set; }

    public virtual ICollection<BorrowedInfo> BorrowedInfos { get; } = new List<BorrowedInfo>();
}
