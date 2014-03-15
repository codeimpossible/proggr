USE [master]
GO

/****** Object:  Database [proggr]    Script Date: 3/14/2014 11:07:45 PM ******/
DROP DATABASE [proggr]
GO


USE [master]
GO

/****** Object:  Database [proggr]    Script Date: 3/14/2014 11:07:16 PM ******/
CREATE DATABASE [proggr]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'proggr', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS2012\MSSQL\DATA\proggr.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'proggr_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS2012\MSSQL\DATA\proggr_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [proggr] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [proggr].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [proggr] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [proggr] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [proggr] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [proggr] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [proggr] SET ARITHABORT OFF 
GO

ALTER DATABASE [proggr] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [proggr] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [proggr] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [proggr] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [proggr] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [proggr] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [proggr] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [proggr] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [proggr] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [proggr] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [proggr] SET  DISABLE_BROKER 
GO

ALTER DATABASE [proggr] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [proggr] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [proggr] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [proggr] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [proggr] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [proggr] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [proggr] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [proggr] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [proggr] SET  MULTI_USER 
GO

ALTER DATABASE [proggr] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [proggr] SET DB_CHAINING OFF 
GO

ALTER DATABASE [proggr] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [proggr] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [proggr] SET  READ_WRITE 
GO

USE [proggr]
GO

CREATE TABLE [dbo].[Languages]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(200),
	[short_name] NVARCHAR(20)
)

GO

CREATE TABLE [dbo].[Users]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[login] NVARCHAR(300) NOT NULL,
	[avatar_url] NVARCHAR(MAX) NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT( getdate() ),
	[name] NVARCHAR(500) NULL
)

GO

CREATE INDEX [IX_Users_login] ON [dbo].[Users] ([login])

GO

CREATE TABLE [dbo].[Projects]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(100) NOT NULL,
	[url] NVARCHAR(500) NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT( getdate() ),
	[user_id] INT NOT NULL FOREIGN KEY REFERENCES Users(id),
	[primary_language_id] INT NULL FOREIGN KEY REFERENCES Languages(id)
)

GO

CREATE INDEX [IX_Projects_name] ON [dbo].[Users] ([name])

GO

CREATE TABLE [dbo].[Workers]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT( newid() ),
	[user_id] INT NOT NULL FOREIGN KEY REFERENCES Users(id),
	[created_at] DATETIME NOT NULL DEFAULT( getdate() ),
	[last_report] DATETIME
)

GO

CREATE TABLE [dbo].[Jobs]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[type] INT NOT NULL, -- 1 analyze...
	[worker_id] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Workers(id),
	[status] INT NOT NULL DEFAULT(-1),
	[created_at] DATETIME NOT NULL DEFAULT( getdate() )
)

GO

CREATE TABLE [dbo].[Extensions]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[extension] NVARCHAR(12) NOT NULL,
	[language_id] INT NOT NULL FOREIGN KEY REFERENCES Languages(id)
)

GO

-- bootstrap the languages table
SET IDENTITY_INSERT Languages ON

INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (1,  'ERB Template', 'erb')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (2,  'HAML Template', 'haml')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (3,  'SASS Style Sheet', 'sass')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (4,  'LESS Style Sheet', 'less')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (5,  'HTML Template', 'html')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (6,  'Cascading Style Sheet', 'css')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (7,  'Razor Template', 'razor')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (8,  'ASPX WebForms Template', 'aspx')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (9,  'C#', 'csharp')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (10, 'Visual Basic', 'vb')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (11, 'Visual Basic Script', 'vbs')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (12, 'Power Shell', 'powershell')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (13, 'JavaScript', 'js')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (14, 'CoffeeScript', 'coffeescript')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (15, 'Ruby', 'ruby')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (16, 'Python', 'python')
INSERT INTO [dbo].[Languages] ( id, name, short_name) VALUES (17, 'Perl', 'perl')

SET IDENTITY_INSERT Languages OFF

SET IDENTITY_INSERT Extensions ON

INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (1,  'erb', 1)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (2,  'haml', 2)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (3,  'sass', 3)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (4,  'less', 4)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (5,  'html', 5)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (6,  'css', 6)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (7,  'cshtml', 7)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (8,  'vbhtml', 7)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (9, 'aspx', 8)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (10,  'cs', 9)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (11, 'vb', 10)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (12, 'vbs', 11)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (13, 'ps1', 12)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (14, 'js', 13)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (15, 'coffee', 14)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (16, 'rb', 15)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (17, 'py', 16)
INSERT INTO [dbo].[Extensions] ( id, extension, language_id ) VALUES (18, 'pl', 17)

SET IDENTITY_INSERT Extensions OFF
