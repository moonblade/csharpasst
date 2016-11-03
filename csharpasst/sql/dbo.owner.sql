CREATE TABLE [dbo].[owner] (
    [id]       INT          IDENTITY (1, 1) NOT NULL,
    [email]    VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NOT NULL,
    [name]     VARCHAR (50) NOT NULL,
    [level]    INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

