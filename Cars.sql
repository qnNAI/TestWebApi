CREATE TABLE [dbo].[Cars] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Model]          NVARCHAR (128)   NOT NULL,
    [Price]          DECIMAL (18, 2)  NOT NULL,
    [ManufDate]      DATETIME         NOT NULL,
    [Mileage]        DECIMAL (18, 2)  DEFAULT ((0)) NULL,
    [Volume]         DECIMAL (18, 2)  NOT NULL,
    [ManufacturerId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cars_Manufacturers] FOREIGN KEY ([ManufacturerId]) REFERENCES [dbo].[Manufacturers] ([Id]) ON DELETE CASCADE
);

