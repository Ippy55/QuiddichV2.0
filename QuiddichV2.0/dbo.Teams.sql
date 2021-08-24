CREATE TABLE [dbo].[Teams] (
    [TeamID]   INT        NOT NULL,
    [TeamName] NCHAR (20) NOT NULL,
    [AbbrName] NCHAR (20) NOT NULL,
    [City]     NCHAR (20) NOT NULL,
    [State]    NCHAR (20) NOT NULL,
    [ZipCode] NCHAR(10) NOT NULL, 
    PRIMARY KEY CLUSTERED ([TeamID] ASC)
);

