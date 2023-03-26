using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Models;

public partial class LibMangagementSysContext : DbContext
{
    public LibMangagementSysContext()
    {
    }

    public LibMangagementSysContext(DbContextOptions<LibMangagementSysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowedInfo> BorrowedInfos { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS; database=LibMangagementSys; Integrated security = true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.Bookid).HasColumnName("bookid");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("author");
            entity.Property(e => e.Bookname)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("bookname");
            entity.Property(e => e.Edition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("edition");
            entity.Property(e => e.Image)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.IsCurriculum).HasColumnName("isCurriculum");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("isbn");
            entity.Property(e => e.Publisheddate)
                .HasColumnType("date")
                .HasColumnName("publisheddate");
            entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("publisher");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantityInStock");
            entity.Property(e => e.QuantityRequested).HasColumnName("quantityRequested");
            entity.Property(e => e.Subjectcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("subjectcode");
        });

        modelBuilder.Entity<BorrowedInfo>(entity =>
        {
            entity.HasKey(e => e.BorrowedId);

            entity.ToTable("BorrowedInfo", tb =>
                {
                    tb.HasTrigger("lendBook");
                    tb.HasTrigger("returnBook");
                });

            entity.Property(e => e.BorrowedId).HasColumnName("borrowedId");
            entity.Property(e => e.Bookid).HasColumnName("bookid");
            entity.Property(e => e.Borrowdate)
                .HasColumnType("date")
                .HasColumnName("borrowdate");
            entity.Property(e => e.Duedate)
                .HasColumnType("date")
                .HasColumnName("duedate");
            entity.Property(e => e.IsAccepted).HasColumnName("isAccepted");
            entity.Property(e => e.Note)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Requestdate)
                .HasColumnType("date")
                .HasColumnName("requestdate");
            entity.Property(e => e.Returndate)
                .HasColumnType("date")
                .HasColumnName("returndate");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Studentcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("studentcode");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowedInfos)
                .HasForeignKey(d => d.Bookid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowedInfo_Book1");

            entity.HasOne(d => d.StudentcodeNavigation).WithMany(p => p.BorrowedInfos)
                .HasForeignKey(d => d.Studentcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowedInfo_Student1");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentcode);

            entity.ToTable("Student");

            entity.Property(e => e.Studentcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("studentcode");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
