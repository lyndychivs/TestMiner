CREATE TABLE [dbo].[EnvironmentUsers]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [User] NVARCHAR(200) NOT NULL,
)
GO
CREATE INDEX [IX_EnvironmentUsers_User] ON [dbo].[EnvironmentUsers] ([User])