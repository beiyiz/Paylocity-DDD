CREATE TABLE [dbo].[Dependent]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmployeeId] INT NOT NULL, 
    [DependentTypeId] INT NOT NULL, 
    CONSTRAINT [FK_Dependent_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES dbo.Employee([Id]), 
    CONSTRAINT [FK_Dependent_DependentType] FOREIGN KEY ([DependentTypeId]) REFERENCES dbo.DependentType([Id])
)
