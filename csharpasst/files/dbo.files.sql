CREATE TABLE [dbo].[files] (
    [id]       INT          IDENTITY (1, 1) NOT NULL,
    [filename] VARCHAR (50) NOT NULL,
    [ownerid]  INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([filename] ASC),
    CONSTRAINT [fileowner] FOREIGN KEY ([ownerid]) REFERENCES [dbo].[owner] ([id])
);

