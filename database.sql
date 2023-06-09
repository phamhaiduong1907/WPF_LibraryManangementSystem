USE [master]
GO
/****** Object:  Database [LibMangagementSys]    Script Date: 3/20/2023 12:35:28 AM ******/
CREATE DATABASE [LibMangagementSys]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibMangagementSys', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LibMangagementSys.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibMangagementSys_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LibMangagementSys_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LibMangagementSys] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibMangagementSys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibMangagementSys] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibMangagementSys] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibMangagementSys] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibMangagementSys] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibMangagementSys] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibMangagementSys] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [LibMangagementSys] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibMangagementSys] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibMangagementSys] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibMangagementSys] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibMangagementSys] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibMangagementSys] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibMangagementSys] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibMangagementSys] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibMangagementSys] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LibMangagementSys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibMangagementSys] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibMangagementSys] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibMangagementSys] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibMangagementSys] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibMangagementSys] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibMangagementSys] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibMangagementSys] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LibMangagementSys] SET  MULTI_USER 
GO
ALTER DATABASE [LibMangagementSys] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibMangagementSys] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibMangagementSys] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibMangagementSys] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibMangagementSys] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibMangagementSys] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LibMangagementSys] SET QUERY_STORE = OFF
GO
USE [LibMangagementSys]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 3/20/2023 12:35:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[bookid] [int] IDENTITY(1,1) NOT NULL,
	[bookname] [varchar](500) NOT NULL,
	[image] [varchar](1000) NULL,
	[author] [varchar](100) NOT NULL,
	[publisher] [varchar](100) NOT NULL,
	[publisheddate] [date] NULL,
	[edition] [varchar](50) NULL,
	[isbn] [varchar](50) NULL,
	[subjectcode] [varchar](10) NULL,
	[quantityInStock] [int] NOT NULL,
	[isCurriculum] [bit] NOT NULL,
	[quantityRequested] [int] NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[bookid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BorrowedInfo]    Script Date: 3/20/2023 12:35:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowedInfo](
	[bookid] [int] NOT NULL,
	[studentcode] [varchar](10) NOT NULL,
	[borrowdate] [date] NULL,
	[returndate] [date] NULL,
	[status] [int] NULL,
	[quantity] [int] NOT NULL,
	[isAccepted] [int] NOT NULL,
	[requestdate] [date] NOT NULL,
	[note] [varchar](1000) NULL,
	[borrowedId] [int] IDENTITY(1,1) NOT NULL,
	[duedate] [date] NOT NULL,
 CONSTRAINT [PK_BorrowedInfo] PRIMARY KEY CLUSTERED 
(
	[borrowedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 3/20/2023 12:35:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[studentcode] [varchar](10) NOT NULL,
	[password] [varchar](200) NOT NULL,
	[name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[studentcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (1, N'Discrete Mathematics and its applications Part 2', N'http://res.cloudinary.com/dw9re61vc/image/upload/v1678866384/eqmtxtv0hawvzyxfapbg.jpg', N'Kenneth Rosen', N'Mc.Graw Hill, IE', CAST(N'2007-01-01' AS Date), N'Edition 07', N'978-0073383095', N'MAD101', 69, 1, 1)
INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (4, N'Data Structures and Algorithms in Java', N'../Resources/default_book.png', N'Adam Drozdek', N'Cengage Learning Asia', CAST(N'2008-04-30' AS Date), N'3rd Edition', N'978-9814239233', N'MAD101', 42, 1, 3)
INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (5, N'Software Modeling and Design: UML, Use Cases, Patterns, and Software Architectures', N'../Resources/default_book.png', N'H. Gomaa', N'Cambridge University Press', CAST(N'2011-02-01' AS Date), N'1st Edition', N'9780521764148', N'SWD392', 48, 1, 0)
INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (7, N'The Mind of the Leader: How to Lead Yourself, Your People, and Your Organization for Extraordinary Results', N'../Resources/default_book.png', N'Ramus Hougaard', N'Harvard Business Review Press', CAST(N'2018-03-13' AS Date), N'1st Edition', N'978-1633693425', NULL, 43, 0, -4)
INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (8, N'The Power of Habit: Why We Do What We Do in Life and Business', N'../Resources/default_book.png', N'Charles Duhigg', N'Random House Trade Paperbacks
', CAST(N'2014-01-07' AS Date), N'2nd Ed', N'978-0812981605', NULL, 50, 0, 0)
INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (10, N'Dragon Ball Z episode 1', N'http://res.cloudinary.com/dw9re61vc/image/upload/v1678862124/cw7dtwyfsabpo3fpkb5k.jpg', N'Akira Toriyama', N'NXB Kim Dong', CAST(N'2005-03-15' AS Date), N'1st', N'', NULL, 49, 0, 5)
INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (11, N'Core Java Volume I--Fundamentals (Core Series)', N'http://res.cloudinary.com/dw9re61vc/image/upload/v1679235515/rremdtjmp4icepx0lrzr.jpg', N'Cay Horstmann', N'Pearson', CAST(N'2018-05-19' AS Date), N'11th Edition', N'978-0135166307', N'PRO192', 49, 0, 0)
INSERT [dbo].[Book] ([bookid], [bookname], [image], [author], [publisher], [publisheddate], [edition], [isbn], [subjectcode], [quantityInStock], [isCurriculum], [quantityRequested]) VALUES (12, N'Core Java, Volume II--Advanced Features (Core Series)', N'http://res.cloudinary.com/dw9re61vc/image/upload/v1679235631/tljafaw6nvqfl8pao33d.jpg', N'Cay Horstmann', N'Pearson', CAST(N'2019-02-01' AS Date), N'11th Edition', N'978-0135166314', N'PRO192', 100, 0, 1)
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
SET IDENTITY_INSERT [dbo].[BorrowedInfo] ON 

INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (10, N'HE160485', CAST(N'2023-03-16' AS Date), CAST(N'2023-03-16' AS Date), 2, 5, 2, CAST(N'2023-03-16' AS Date), NULL, 26, CAST(N'2023-04-16' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (7, N'HE160485', CAST(N'2023-03-19' AS Date), CAST(N'2023-03-19' AS Date), 2, 5, 2, CAST(N'2023-03-16' AS Date), NULL, 27, CAST(N'2023-04-16' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (12, N'HG654321', NULL, NULL, NULL, 1, 3, CAST(N'2023-03-19' AS Date), NULL, 28, CAST(N'2023-04-19' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (1, N'HH123456', CAST(N'2023-03-19' AS Date), CAST(N'2023-03-19' AS Date), 2, 1, 2, CAST(N'2023-03-19' AS Date), NULL, 29, CAST(N'2023-04-19' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HH123456', CAST(N'2023-03-19' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-19' AS Date), NULL, 30, CAST(N'2023-04-19' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (11, N'HH123456', CAST(N'2023-03-19' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-19' AS Date), NULL, 31, CAST(N'2023-04-19' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (7, N'HH123456', CAST(N'2023-03-19' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-19' AS Date), NULL, 32, CAST(N'2023-04-19' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (5, N'HG654321', CAST(N'2023-03-19' AS Date), NULL, 1, 2, 2, CAST(N'2023-03-19' AS Date), NULL, 33, CAST(N'2023-04-19' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (7, N'HG654321', NULL, NULL, NULL, 1, 3, CAST(N'2023-03-19' AS Date), NULL, 34, CAST(N'2023-04-04' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (7, N'HG654321', CAST(N'2023-03-19' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-19' AS Date), NULL, 35, CAST(N'2023-04-19' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (10, N'HG654321', CAST(N'2023-03-19' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-19' AS Date), NULL, 36, CAST(N'2023-03-30' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (1, N'HE160485', NULL, NULL, NULL, 1, 3, CAST(N'2023-03-20' AS Date), NULL, 37, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (8, N'HE160485', CAST(N'2023-03-20' AS Date), CAST(N'2023-03-20' AS Date), 2, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 38, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (1, N'HE160485', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 39, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HE160485', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 40, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HE160485', NULL, NULL, NULL, 1, 3, CAST(N'2023-03-20' AS Date), NULL, 41, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HE160485', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 42, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HE160485', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 43, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HE160485', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 44, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HG654321', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 45, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HG654321', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 46, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HG654321', CAST(N'2023-03-20' AS Date), NULL, 1, 1, 2, CAST(N'2023-03-20' AS Date), NULL, 47, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HG654321', NULL, NULL, NULL, 1, 1, CAST(N'2023-03-20' AS Date), NULL, 48, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (4, N'HG654321', NULL, NULL, NULL, 1, 1, CAST(N'2023-03-20' AS Date), NULL, 49, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (10, N'HH123456', NULL, NULL, NULL, 1, 1, CAST(N'2023-03-20' AS Date), NULL, 50, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (10, N'HH123456', NULL, NULL, NULL, 1, 1, CAST(N'2023-03-20' AS Date), NULL, 51, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (10, N'HH123456', NULL, NULL, NULL, 1, 1, CAST(N'2023-03-20' AS Date), NULL, 52, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (10, N'HH123456', NULL, NULL, NULL, 1, 1, CAST(N'2023-03-20' AS Date), NULL, 53, CAST(N'2023-04-20' AS Date))
INSERT [dbo].[BorrowedInfo] ([bookid], [studentcode], [borrowdate], [returndate], [status], [quantity], [isAccepted], [requestdate], [note], [borrowedId], [duedate]) VALUES (10, N'HH123456', NULL, NULL, NULL, 1, 1, CAST(N'2023-03-20' AS Date), NULL, 54, CAST(N'2023-04-20' AS Date))
SET IDENTITY_INSERT [dbo].[BorrowedInfo] OFF
GO
INSERT [dbo].[Student] ([studentcode], [password], [name]) VALUES (N'HE160485', N'1234567', N'Pham Hai Duong')
INSERT [dbo].[Student] ([studentcode], [password], [name]) VALUES (N'HG654321', N'1234567', N'Death Pool')
INSERT [dbo].[Student] ([studentcode], [password], [name]) VALUES (N'HH123456', N'1234567', N'JohnyDepp')
GO
ALTER TABLE [dbo].[BorrowedInfo]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedInfo_Book1] FOREIGN KEY([bookid])
REFERENCES [dbo].[Book] ([bookid])
GO
ALTER TABLE [dbo].[BorrowedInfo] CHECK CONSTRAINT [FK_BorrowedInfo_Book1]
GO
ALTER TABLE [dbo].[BorrowedInfo]  WITH CHECK ADD  CONSTRAINT [FK_BorrowedInfo_Student1] FOREIGN KEY([studentcode])
REFERENCES [dbo].[Student] ([studentcode])
GO
ALTER TABLE [dbo].[BorrowedInfo] CHECK CONSTRAINT [FK_BorrowedInfo_Student1]
GO
/****** Object:  Trigger [dbo].[lendBook]    Script Date: 3/20/2023 12:35:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[lendBook] on [dbo].[BorrowedInfo] after insert as
begin
	update Book set quantityRequested = 
	quantityRequested + (SELECT quantity from inserted
	where bookid = Book.bookid) from Book join inserted
	on Book.bookid = inserted.bookid
end
GO
ALTER TABLE [dbo].[BorrowedInfo] ENABLE TRIGGER [lendBook]
GO
/****** Object:  Trigger [dbo].[returnBook]    Script Date: 3/20/2023 12:35:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[returnBook] on [dbo].[BorrowedInfo] after update as
begin
	declare @isAccepted int;
	declare @borrowid int;
	declare @bookid int;
	declare @status int;
	declare @quantity int;
	select @bookid = bookid, @status = [status], 
	@quantity = quantity, @isAccepted = isAccepted,
	@borrowid = borrowedId
	from inserted
	if @isAccepted = 2 and @status is null
		begin
			update BorrowedInfo set borrowdate = GETDATE(), [status] = 1
			where borrowedId = @borrowid
			update Book set quantityInStock = quantityInStock - @quantity,
			quantityRequested = quantityRequested - @quantity
			where bookid = @bookid
		end
	if @status = 2
	begin
		update Book set quantityInStock = quantityInStock + @quantity
		where bookid = @bookid
		update BorrowedInfo set returndate = GETDATE() where borrowedId = @borrowid
	end
end
GO
ALTER TABLE [dbo].[BorrowedInfo] ENABLE TRIGGER [returnBook]
GO
USE [master]
GO
ALTER DATABASE [LibMangagementSys] SET  READ_WRITE 
GO
