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
