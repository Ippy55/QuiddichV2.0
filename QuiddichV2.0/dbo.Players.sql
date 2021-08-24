CREATE TABLE [dbo].[Players] (
    [TeamID]        INT        NOT NULL,
    [FirstName]     NCHAR (20) NOT NULL,
    [LastName]      NCHAR (20) NOT NULL,
    [UniformNumber] INT        NOT NULL,
    [Position]      NCHAR (10) NOT NULL,
    
    PRIMARY KEY CLUSTERED ([FirstName] ASC, [LastName] ASC),
    CONSTRAINT [FK_Players_ToTable] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([TeamID])
);

