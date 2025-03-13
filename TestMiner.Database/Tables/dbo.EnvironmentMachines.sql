CREATE TABLE [dbo].[EnvironmentMachines]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Machine] NVARCHAR(200) NOT NULL,
)
GO
CREATE INDEX [IX_EnvironmentMachines_Machine] ON [dbo].[EnvironmentMachines] ([Machine])