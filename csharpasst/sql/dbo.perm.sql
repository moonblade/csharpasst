CREATE TABLE [dbo].[perm] (
    [fileid]  INT NOT NULL,
    [ownerid] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([fileid] ASC, [ownerid] ASC),
    CONSTRAINT [permfile] FOREIGN KEY ([fileid]) REFERENCES [dbo].[files] ([id]),
    CONSTRAINT [permowner] FOREIGN KEY ([ownerid]) REFERENCES [dbo].[owner] ([id])
);

