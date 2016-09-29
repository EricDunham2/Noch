USE [nochdb]

DROP TABLE [UserDomains]
DROP TABLE [Messages]
DROP TABLE [Users]
DROP TABLE [Channels]
DROP TABLE [Domains]

CREATE TABLE [Domains] (
	[DomainID] INT NOT NULL IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([DomainID])
);

CREATE TABLE [Users] (
	[UserID] INT NOT NULL IDENTITY(1, 1),
	[FirstName] VARCHAR(20),
	[LastName] VARCHAR(50),
	[Username] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(20) NOT NULL,
	[Email] VARCHAR(50) NOT NULL,
	[IsEmailConfirmed] BIT,
	[Phone] VARCHAR(20),
	[Address] VARCHAR(100),
	[City] VARCHAR(50),
	[Province] VARCHAR(50),
	[Country] VARCHAR(50),
	[PostalCode] VARCHAR(20),
	[IsAdmin] BIT ,
	[CreatedOn] DATETIME NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([UserID])
);

CREATE TABLE [Messages] (
	[MessageID] INT NOT NULL IDENTITY(1, 1),
	[ChannelID] INT NOT NULL,
	[UserID] INT NOT NULL,
	[Content] VARCHAR(MAX) NOT NULL,
	[Timestamp] DATETIME NOT NULL,
	[IsEdited] BIT NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([MessageID])
);

CREATE TABLE [Channels] (
	[ChannelID] INT NOT NULL,
	[DomainID] INT NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([ChannelID])
);

CREATE TABLE [UserDomains] (
	[UserDomainID] INT NOT NULL,
	[UserID] INT NOT NULL,
	[DomainID] INT NOT NULL,
	PRIMARY KEY ([UserDomainID])
);

ALTER TABLE [Messages] ADD CONSTRAINT [Messages_fk0] FOREIGN KEY ([ChannelID]) REFERENCES [Channels]([ChannelID]);

ALTER TABLE [Messages] ADD CONSTRAINT [Messages_fk1] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]);

ALTER TABLE [Channels] ADD CONSTRAINT [Channels_fk0] FOREIGN KEY ([DomainID]) REFERENCES [Domains]([DomainID]);

ALTER TABLE [UserDomains] ADD CONSTRAINT [UserDomains_fk0] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]);

ALTER TABLE [UserDomains] ADD CONSTRAINT [UserDomains_fk1] FOREIGN KEY ([DomainID]) REFERENCES [Domains]([DomainID]);