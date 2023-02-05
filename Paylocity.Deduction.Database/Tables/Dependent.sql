CREATE TABLE [dbo].[Dependent]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmployeeId] INT NOT NULL, 
    [DependentType_Name] NVARCHAR(20) NOT NULL, 
    [DependentType_Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Dependent_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES dbo.Employee([Id]), 
)
