USE [master]
GO
/****** Object:  Database [LAB1SQL]    Script Date: 2023-12-10 23:35:26 ******/
CREATE DATABASE [LAB1SQL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LAB1SQL', FILENAME = N'C:\Users\huany\LAB1SQL.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LAB1SQL_log', FILENAME = N'C:\Users\huany\LAB1SQL_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LAB1SQL] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LAB1SQL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LAB1SQL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LAB1SQL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LAB1SQL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LAB1SQL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LAB1SQL] SET ARITHABORT OFF 
GO
ALTER DATABASE [LAB1SQL] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [LAB1SQL] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LAB1SQL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LAB1SQL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LAB1SQL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LAB1SQL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LAB1SQL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LAB1SQL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LAB1SQL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LAB1SQL] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LAB1SQL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LAB1SQL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LAB1SQL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LAB1SQL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LAB1SQL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LAB1SQL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LAB1SQL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LAB1SQL] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LAB1SQL] SET  MULTI_USER 
GO
ALTER DATABASE [LAB1SQL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LAB1SQL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LAB1SQL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LAB1SQL] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LAB1SQL] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LAB1SQL] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LAB1SQL] SET QUERY_STORE = OFF
GO
USE [LAB1SQL]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseId] [int] NOT NULL,
	[CourseName] [nvarchar](50) NOT NULL,
	[Term] [int] NULL,
	[PersonnelId_FK] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personnel]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personnel](
	[PersonnelId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](40) NULL,
	[Role] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonnelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](40) NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [varchar](20) NULL,
	[ClassId_FK] [int] NULL,
 CONSTRAINT [PK__Students__32C52B99C0BF9D39] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[GradeId] [int] IDENTITY(1,1) NOT NULL,
	[Grade] [decimal](2, 0) NULL,
	[GradeDate] [date] NULL,
	[StudentId_FK] [int] NULL,
	[CourseId_FK] [int] NULL,
	[PersonnelId_FK] [int] NULL,
 CONSTRAINT [PK__Grades__54F87A5718228259] PRIMARY KEY CLUSTERED 
(
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GradesLast30Days]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                        CREATE VIEW [dbo].[GradesLast30Days] AS
                        SELECT 
                            Students.FirstName AS StudentFirstName,
                            Students.LastName AS StudentLastName,
                            Grade,
                            Courses.CourseName AS CourseName,
                            Personnel.FirstName AS TeacherFirstName,
                            Personnel.LastName AS TeacherLastName,
                            GradeDate
                        FROM Grades
                        INNER JOIN Students ON Grades.StudentId_FK = Students.StudentId
                        INNER JOIN Courses ON Grades.CourseId_FK = Courses.CourseId
                        INNER JOIN Personnel ON Grades.PersonnelId_FK = Personnel.PersonnelId
                        WHERE GradeDate >= DATEADD(day, -30, GETDATE())
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassId] [int] NOT NULL,
	[ClassName] [nvarchar](50) NULL,
	[PersonnelId_FK] [int] NULL,
	[ClassCode] [nvarchar](6) NULL,
	[ClassYear] [int] NULL,
 CONSTRAINT [PK__Classes__CB1927C00E1FE948] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollments]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId_FK] [int] NULL,
	[StudentId_FK] [int] NULL,
	[GradeId_FK] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetClassStudents]    Script Date: 2023-12-10 23:35:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE VIEW GradesLast30Days AS
--SELECT 
--    Students.FirstName AS StudentFirstName,
--    Students.LastName AS StudentLastName,
--    Grade,
--    Courses.CourseName AS CourseName,
--    Personnel.FirstName AS TeacherFirstName,
--    Personnel.LastName AS TeacherLastName,
--    GradeDate
--FROM Grades
--INNER JOIN Students ON Grades.StudentId_FK = Students.StudentId
--INNER JOIN Courses ON Grades.CourseId_FK = Courses.CourseId
--INNER JOIN Personnel ON Grades.PersonnelId_FK = Personnel.PersonnelId
--WHERE GradeDate >= DATEADD(day, -30, GETDATE())

CREATE PROCEDURE [dbo].[GetClassStudents]
	@ClassName NVARCHAR(50)
AS
BEGIN
	SELECT * FROM Students 
	INNER JOIN Classes ON Students.ClassId_FK = Classes.ClassId
	WHERE ClassName = @ClassName;
END;
GO
USE [master]
GO
ALTER DATABASE [LAB1SQL] SET  READ_WRITE 
GO
