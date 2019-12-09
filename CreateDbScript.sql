CREATE DATABASE [WeatherForecastDb]
GO
USE [WeatherForecastDb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavoriteCities](
	[Id] nvarchar(20) NOT NULL,
	[Name] nvarchar(40) NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeatherForecast](
	[CityId] nvarchar(20) NOT NULL,
	[WeatherText] nvarchar(20) NOT NULL,
	[CelsiusTemperature] [float] NOT NULL,
	[DateTime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_WeatherForecast] ON [dbo].[WeatherForecast]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddFavoriteCity]
    @CityKey nvarchar(20), 
    @CityName nvarchar(40)  
AS   

 
    Insert Into  dbo.FavoriteCities
	Values (@CityKey,@CityName)
   
 Select Id,Name from FavoriteCities Where Id=@CityKey
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddWeatherForecast]
    @CityId nvarchar(20), 
	@WeatherText nvarchar(20),
	@CelsiusTemperature float,
    @DateTime datetime
AS   

	
	delete from dbo.WeatherForecast where CityId=@CityId

    Insert Into  dbo.WeatherForecast
	Values (@CityId,@WeatherText,@CelsiusTemperature,@DateTime)
   
 Select @CityId;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFavoriteCities]
    @CityName nvarchar(50)  
AS   

 
    SELECT *
    FROM dbo.FavoriteCities
    WHERE Name like '%'+@CityName+'%'
    Order by Name
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CReate PROCEDURE [dbo].[GetWeather]
    @CityId nvarchar(20)  
AS   

 
    SELECT *
    FROM dbo.WeatherForecast
    WHERE CityId= @CityId

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveFavoriteCity]
    @CityKey nvarchar(20)

AS   

 
    Delete From   dbo.FavoriteCities
	Where Id= @CityKey
   
 Select 'true'
GO
USE [master]
GO
ALTER DATABASE [WeatherForecastDb] SET  READ_WRITE 
GO
