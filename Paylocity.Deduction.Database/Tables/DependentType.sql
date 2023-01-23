CREATE TABLE [dbo].[DependentType]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [TypeName] NVARCHAR(20) NOT NULL, 
    [TypeDescription] NVARCHAR(50) NOT NULL
)
