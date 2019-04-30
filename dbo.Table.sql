CREATE TABLE [dbo].[WeatherEntry] (
    [Id]          INT        NOT NULL,
    [City]        TEXT       NOT NULL,
    [Temperature] FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
