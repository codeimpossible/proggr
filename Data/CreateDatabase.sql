USE [proggr]
GO
/****** Object:  StoredProcedure [dbo].[GetNextJob]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP PROCEDURE [dbo].[GetNextJob]
GO
ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_Files_Commits]
GO
ALTER TABLE [dbo].[Commits] DROP CONSTRAINT [FK_Commits_CodeLocations]
GO
ALTER TABLE [dbo].[CodeLocations] DROP CONSTRAINT [FK_CodeLocations_Commits]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  Index [IX_Jobs_State]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [IX_Jobs_State] ON [dbo].[Jobs]
GO
/****** Object:  Index [IX_CodeLocations_FullName]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [IX_CodeLocations_FullName] ON [dbo].[CodeLocations]
GO
/****** Object:  Index [UserNameIndex]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[Workers]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[Workers]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[Jobs]
GO
/****** Object:  Table [dbo].[GithubJsonData]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[GithubJsonData]
GO
/****** Object:  Table [dbo].[GitHubApiData]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[GitHubApiData]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[Files]
GO
/****** Object:  Table [dbo].[Commits]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[Commits]
GO
/****** Object:  Table [dbo].[CodeLocations]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[CodeLocations]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO
USE [master]
GO
/****** Object:  Database [proggr]    Script Date: 10/11/2015 10:20:36 PM ******/
DROP DATABASE [proggr]
GO
/****** Object:  Database [proggr]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE DATABASE [proggr]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'proggr', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\proggr.mdf' , SIZE = 60416KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'proggr_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\proggr_log.ldf' , SIZE = 3840KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [proggr] SET COMPATIBILITY_LEVEL = 120
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
ALTER DATABASE [proggr] SET DELAYED_DURABILITY = DISABLED 
GO
USE [proggr]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[GithubUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CodeLocations]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodeLocations](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_CodeLocations_Id]  DEFAULT (newid()),
	[FullName] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[IsPublic] [bit] NOT NULL CONSTRAINT [DF_CodeLocations_IsPublic]  DEFAULT ((0)),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_CodeLocations_DateCreated]  DEFAULT (getdate()),
	[CreatedBy] [nvarchar](128) NOT NULL,
	[Head] [nvarchar](40) NULL,
	[Branch] [nvarchar](128) NOT NULL CONSTRAINT [DF_CodeLocations_Branch]  DEFAULT (N'master'),
 CONSTRAINT [PK_CodeLocations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Commits]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commits](
	[Sha] [nvarchar](40) NOT NULL,
	[Message] [text] NULL,
	[MessageShort] [nvarchar](80) NULL,
	[AuthorEmail] [nvarchar](256) NOT NULL,
	[AuthorUserName] [nvarchar](128) NOT NULL,
	[CommitterEmail] [nvarchar](256) NOT NULL,
	[CommitterUserName] [nvarchar](128) NOT NULL,
	[DateAuthored] [datetime] NOT NULL,
	[DateCommitted] [datetime] NOT NULL,
	[RepositoryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Commits] PRIMARY KEY CLUSTERED 
(
	[Sha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Files]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](512) NULL,
	[Ext] [nvarchar](50) NULL,
	[RelativePath] [nvarchar](max) NOT NULL,
	[CommitId] [nvarchar](40) NOT NULL,
	[HashRaw] [binary](16) NULL,
	[HashNoWhiteSpace] [binary](16) NULL,
	[HashNoNewLines] [binary](16) NULL,
	[ContentType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GitHubApiData]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GitHubApiData](
	[UserName] [nvarchar](128) NOT NULL,
	[KeyName] [nvarchar](128) NOT NULL,
	[DataRaw] [text] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_GitHubApiData_DateCreated]  DEFAULT (getdate()),
	[DateUpdated] [datetime] NOT NULL CONSTRAINT [DF_GitHubApiData_DateUpdated]  DEFAULT (getdate()),
 CONSTRAINT [PK_GitHubApiData] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC,
	[KeyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GithubJsonData]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GithubJsonData](
	[GithubUserName] [nvarchar](128) NOT NULL,
	[ApiToken] [nvarchar](128) NOT NULL,
	[UserJsonData] [text] NOT NULL,
	[ReposJsonData] [text] NULL,
	[DateUpdated] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_GithubJsonData_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_GithubJsonData] PRIMARY KEY CLUSTERED 
(
	[GithubUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Jobs_Id]  DEFAULT (newid()),
	[JobType] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Jobs_DateCreated]  DEFAULT (getdate()),
	[DateUpdated] [datetime] NOT NULL CONSTRAINT [DF_Jobs_DateUpdated]  DEFAULT (getdate()),
	[State] [nvarchar](50) NOT NULL,
	[Arguments] [text] NULL,
	[DateCompleted] [datetime] NULL,
	[CompletedBy] [uniqueidentifier] NULL,
	[CompletedArguments] [text] NULL,
 CONSTRAINT [PK_Jobs_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Workers]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workers](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Workers_DateCreated]  DEFAULT (getdate()),
	[DateUpdated] [datetime] NOT NULL CONSTRAINT [DF_Workers_DateUpdated]  DEFAULT (getdate()),
	[MachineName] [nvarchar](128) NOT NULL,
	[Ipv4] [nvarchar](15) NOT NULL,
	[CurrentJob] [uniqueidentifier] NULL,
	[State] [tinyint] NOT NULL CONSTRAINT [DF_Workers_State]  DEFAULT ((0)),
 CONSTRAINT [PK_Workers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CodeLocations_FullName]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_CodeLocations_FullName] ON [dbo].[CodeLocations]
(
	[FullName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Jobs_State]    Script Date: 10/11/2015 10:20:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_Jobs_State] ON [dbo].[Jobs]
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CodeLocations]  WITH CHECK ADD  CONSTRAINT [FK_CodeLocations_Commits] FOREIGN KEY([Head])
REFERENCES [dbo].[Commits] ([Sha])
GO
ALTER TABLE [dbo].[CodeLocations] CHECK CONSTRAINT [FK_CodeLocations_Commits]
GO
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD  CONSTRAINT [FK_Commits_CodeLocations] FOREIGN KEY([RepositoryId])
REFERENCES [dbo].[CodeLocations] ([Id])
GO
ALTER TABLE [dbo].[Commits] CHECK CONSTRAINT [FK_Commits_CodeLocations]
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Commits] FOREIGN KEY([CommitId])
REFERENCES [dbo].[Commits] ([Sha])
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Commits]
GO
/****** Object:  StoredProcedure [dbo].[GetNextJob]    Script Date: 10/11/2015 10:20:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNextJob]
	-- Add the parameters for the stored procedure here
	@worker_id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @jobid as uniqueidentifier;

	IF EXISTS(SELECT TOP 1 * FROM dbo.Jobs WHERE State = 'New') BEGIN
		SELECT TOP 1 @jobid = Id FROM dbo.Jobs WHERE State = 'New' ORDER BY DateCreated ASC; -- older jobs first, son

		UPDATE Workers SET CurrentJob = @jobid, DateUpdated = GETDATE() WHERE Id = @worker_id;
	END ELSE BEGIN
		UPDATE Workers SET CurrentJob = NULL, DateUpdated = GETDATE() WHERE Id = @worker_id;
	END
END

GO
USE [master]
GO
ALTER DATABASE [proggr] SET  READ_WRITE 
GO
