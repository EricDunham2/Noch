USE [master]

DROP DATABASE [NochDB]
CREATE DATABASE [NochDB]

USE [NochDB]

CREATE TABLE [Domain] (
	[DomainID] INT NOT NULL IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([DomainID])
);

CREATE TABLE [User] (
	[UserID] INT NOT NULL IDENTITY(1, 1),
	[FirstName] VARCHAR(20) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[Username] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(20) NOT NULL,
	[Email] VARCHAR(50) NOT NULL,
	[IsEmailConfirmed] BIT NOT NULL,
	[Phone] VARCHAR(20) NOT NULL,
	[Address] VARCHAR(100) NOT NULL,
	[City] VARCHAR(50) NOT NULL,
	[Province] VARCHAR(50) NOT NULL,
	[Country] VARCHAR(50) NOT NULL,
	[PostalCode] VARCHAR(20) NOT NULL,
	[IsAdmin] BIT NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([UserID])
);

CREATE TABLE [Message] (
	[MessageID] INT NOT NULL IDENTITY(1, 1),
	[ChannelID] INT NOT NULL,
	[UserID] INT NOT NULL,
	[Content] VARCHAR(MAX) NOT NULL,
	[Timestamp] DATETIME NOT NULL,
	[IsEdited] BIT NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([MessageID])
);

CREATE TABLE [Channel] (
	[ChannelID] INT NOT NULL IDENTITY(1, 1),
	[DomainID] INT NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[UpdatedOn] DATETIME NOT NULL,
	PRIMARY KEY ([ChannelID])
);

CREATE TABLE [UserDomain] (
	[UserDomainID] INT NOT NULL,
	[UserID] INT NOT NULL,
	[DomainID] INT NOT NULL,
	PRIMARY KEY ([UserDomainID])
);

ALTER TABLE [Message] ADD CONSTRAINT [Message_fk0] FOREIGN KEY ([ChannelID]) REFERENCES [Channel]([ChannelID]);

ALTER TABLE [Message] ADD CONSTRAINT [Message_fk1] FOREIGN KEY ([UserID]) REFERENCES [User]([UserID]);

ALTER TABLE [Channel] ADD CONSTRAINT [Channel_fk0] FOREIGN KEY ([DomainID]) REFERENCES [Domain]([DomainID]);

ALTER TABLE [UserDomain] ADD CONSTRAINT [UserDomain_fk0] FOREIGN KEY ([UserID]) REFERENCES [User]([UserID]);

ALTER TABLE [UserDomain] ADD CONSTRAINT [UserDomain_fk1] FOREIGN KEY ([DomainID]) REFERENCES [Domain]([DomainID]);