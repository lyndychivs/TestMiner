USE [TestMinerHistory]
GO

IF NOT EXISTS (SELECT [Status] FROM [dbo].[TestMinerStatus] WHERE [Status] = 'Processing')
BEGIN
    INSERT INTO [dbo].[TestMinerStatus] ([Status]) VALUES ('Processing')
END

IF NOT EXISTS (SELECT [Status] FROM [dbo].[TestMinerStatus] WHERE [Status] = 'Complete')
BEGIN
    INSERT INTO [dbo].[TestMinerStatus] ([Status]) VALUES ('Complete')
END

IF NOT EXISTS (SELECT [Status] FROM [dbo].[TestMinerStatus] WHERE [Status] = 'Failed')
BEGIN
    INSERT INTO [dbo].[TestMinerStatus] ([Status]) VALUES ('Failed')
END

IF NOT EXISTS (SELECT [Result] FROM [dbo].[TestResults] WHERE [Result] = 'Inconclusive')
BEGIN
    INSERT INTO [dbo].[TestResults] ([Result]) VALUES ('Inconclusive')
END

IF NOT EXISTS (SELECT [Result] FROM [dbo].[TestResults] WHERE [Result] = 'Passed')
BEGIN
    INSERT INTO [dbo].[TestResults] ([Result]) VALUES ('Passed')
END

IF NOT EXISTS (SELECT [Result] FROM [dbo].[TestResults] WHERE [Result] = 'Warning')
BEGIN
    INSERT INTO [dbo].[TestResults] ([Result]) VALUES ('Warning')
END

IF NOT EXISTS (SELECT [Result] FROM [dbo].[TestResults] WHERE [Result] = 'Skipped')
BEGIN
    INSERT INTO [dbo].[TestResults] ([Result]) VALUES ('Skipped')
END

IF NOT EXISTS (SELECT [Result] FROM [dbo].[TestResults] WHERE [Result] = 'Failed')
BEGIN
    INSERT INTO [dbo].[TestResults] ([Result]) VALUES ('Failed')
END

IF NOT EXISTS (SELECT [Result] FROM [dbo].[TestResults] WHERE [Result] = 'Error')
BEGIN
    INSERT INTO [dbo].[TestResults] ([Result]) VALUES ('Error')
END