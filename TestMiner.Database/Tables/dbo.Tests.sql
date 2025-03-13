CREATE TABLE [dbo].[Tests]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [TestClassId] INT NOT NULL,
    [Name] NVARCHAR(500) NOT NULL,
    CONSTRAINT [FK_Tests_TestClasses] FOREIGN KEY ([TestClassId]) REFERENCES [dbo].[TestClasses] ([Id])
)
GO
CREATE INDEX [IX_Tests_Name] ON [dbo].[Tests] ([Name])