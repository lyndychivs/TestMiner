CREATE TABLE [dbo].[TestClasses]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Class] NVARCHAR(500) NOT NULL,
)
GO
CREATE INDEX [IX_TestClasses_Name] ON [dbo].[TestClasses] ([Class])