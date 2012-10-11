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


CREATE TABLE [dbo].[Projects]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(100) NOT NULL,
	[url] NVARCHAR(500) NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT( getdate() ),
	[user_id] INT NOT NULL FOREIGN KEY REFERENCES Users(id),
	[primary_language_id] INT NULL FOREIGN KEY REFERENCES Languages(id)
)

CREATE INDEX [IX_Projects_name] ON [dbo].[Users] ([name])

CREATE TABLE [dbo].[Workers]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT( newid() ),
	[user_id] INT NOT NULL FOREIGN KEY REFERENCES Users(id),
	[created_at] DATETIME NOT NULL DEFAULT( getdate() ),
	[last_report] DATETIME
)

CREATE TABLE [dbo].[Jobs]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[type] INT NOT NULL, -- 1 analyze...
	[worker_id] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Workers(id),
	[status] INT NOT NULL DEFAULT(-1),
	[created_at] DATETIME NOT NULL DEFAULT( getdate() )
)

CREATE TABLE [dbo].[Languages]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(200),
	[short_name] NVARCHAR(20)
)

CREATE TABLE [dbo].[Extentions]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[extension] NVARCHAR(5) NOT NULL,
	[language_id] INT NOT NULL FOREIGN KEY REFERENCES Languages(id)
)

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




