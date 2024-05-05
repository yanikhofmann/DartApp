-- Creating Players Table
CREATE TABLE da.Players (
  PlayerId INT IDENTITY(1,1) PRIMARY KEY,
  PlayerFirstName NVARCHAR(255),
  PlayerLastName NVARCHAR(255),
  PlayerBirthdate DATE,
  PlayerDescription NVARCHAR(MAX),
  PlayerEmail NVARCHAR(255),
  PlayerIsActive BIT
);

CREATE TABLE da.GameLength (
    GameLengthId INT IDENTITY(1,1) PRIMARY KEY,
    GameLength int
)
-- Creating Game Table
CREATE TABLE da.Game (
  GameId INT IDENTITY(1,1) PRIMARY KEY,
  GameDate DATE,
  GameName NVARCHAR(255),
  GameLengthId int,
  FOREIGN KEY (GameLengthId) REFERENCES da.GameLength(GameLengthId)
);

-- Creating Game2Players Table
CREATE TABLE da.Games2Players (
  Game2PlayersId INT IDENTITY(1,1) PRIMARY KEY,
  PlayerId INT,
  GameId INT,
  Score INT,
  FOREIGN KEY (PlayerId) REFERENCES da.Players(PlayerId),
  FOREIGN KEY (GameId) REFERENCES da.Game(GameId)
);

-- Creating Throws Table
CREATE TABLE da.Throws (
  ThrowId INT IDENTITY(1,1) PRIMARY KEY,
  Game2PlayersId INT,
  ThrowPoints INT,
  ThrowNumber INT,
  FOREIGN KEY (Game2PlayersId) REFERENCES da.Games2Players(Game2PlayersId)
);


-- Inserting Yanik Hofmann into Players Table
INSERT INTO da.Players (PlayerFirstName, PlayerLastName, PlayerBirthdate, PlayerDescription, PlayerEmail, PlayerIsActive)
VALUES ('Yanik', 'Hofmann', '2002-09-26', null, 'yhofmann@kull.com', 1), ('Nico', 'Nagl', '2001-04-17', null, 'nagl@kull.com', 1);

CREATE PROCEDURE da.spGetAllPlayers 
AS
BEGIN
    SELECT * FROM da.Players;
END