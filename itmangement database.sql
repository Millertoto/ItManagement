CREATE TABLE [dbo].[Employees] (
    [Cpr]      INT          NOT NULL,
    [Username] VARCHAR (12) NOT NULL,
    [Password] VARCHAR (16) NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [IsAdmin]  BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Cpr] ASC),
    CONSTRAINT [CK_Employees_Column ] CHECK (len([Password])>=(8))
);

CREATE TABLE [dbo].[Equipment] (
    [Uid]       INT           IDENTITY (1, 1) NOT NULL,
    [Type]      NVARCHAR (50) NOT NULL,
    [IsWorking] BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Uid] ASC)
);

CREATE TABLE [dbo].[Accesories] (
    [Tid]        INT          IDENTITY (1, 1) NOT NULL,
    [IsBorrowed] BIT          NOT NULL,
    [Type]       VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Tid] ASC)
);

CREATE TABLE [dbo].[Errors] (
    [Fid]          INT           IDENTITY (1, 1) NOT NULL,
    [Cpr]          INT           NULL,
    [Uid]          INT           NULL,
    [ErrorMessage] VARCHAR (MAX) NOT NULL,
    [Create]       DATETIME      NOT NULL,
    [Update]       DATETIME      NULL,
    [IsRepaired]   BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Fid] ASC),
    FOREIGN KEY ([Cpr]) REFERENCES [dbo].[Employees] ([Cpr]),
    FOREIGN KEY ([Uid]) REFERENCES [dbo].[Equipment] ([Uid])
);


CREATE TABLE [dbo].[Computer] (
    [Uid]        INT IDENTITY (1, 1) NOT NULL,
    [IsBorrowed] BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([Uid] ASC),
    FOREIGN KEY ([Uid]) REFERENCES [dbo].[Equipment] ([Uid])
);

CREATE TABLE [dbo].[SmartBoard] (
    [Uid]    INT IDENTITY (1, 1) NOT NULL,
    [Roomid] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Uid] ASC),
    FOREIGN KEY ([Uid]) REFERENCES [dbo].[Equipment] ([Uid])
);

CREATE TABLE [dbo].[Tablet] (
    [Uid]        INT IDENTITY (1, 1) NOT NULL,
    [IsBorrowed] BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([Uid] ASC),
    FOREIGN KEY ([Uid]) REFERENCES [dbo].[Equipment] ([Uid])
);

CREATE TABLE [dbo].[SmartPhone] (
    [Uid]        INT IDENTITY (1, 1) NOT NULL,
    [IsBorrowed] BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([Uid] ASC),
    FOREIGN KEY ([Uid]) REFERENCES [dbo].[Equipment] ([Uid])
);