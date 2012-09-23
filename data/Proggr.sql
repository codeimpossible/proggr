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


CREATE TABLE [dbo].[Repositories]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(100) NOT NULL,
	[url] NVARCHAR(500) NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT( getdate() ),
	[user_id] INT NOT NULL FOREIGN KEY REFERENCES Users(id)
)

CREATE INDEX [IX_Repositories_name] ON [dbo].[Users] ([name])

CREATE TABLE [dbo].[Workers]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT( newid() ),
	[user_id] INT NOT NULL FOREIGN KEY REFERENCES Users(id),
	[created_at] DATETIME NOT NULL DEFAULT( getdate() )
)

CREATE TABLE [dbo].[Jobs]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[type] INT NOT NULL, -- 1 analyze...
	[worker_id] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Workers(id)
)