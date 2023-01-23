CREATE TABLE [dbo].[DependentType]
(
    [Name] NVARCHAR(20) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_DependentType] PRIMARY KEY ([Name], [Description])
)
