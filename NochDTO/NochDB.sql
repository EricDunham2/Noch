
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/25/2016 19:07:11
-- Generated from EDMX file: C:\GitSchool\Noch\NochDTO\NochDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NochDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Channel_fk0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Channel] DROP CONSTRAINT [FK_Channel_fk0];
GO
IF OBJECT_ID(N'[dbo].[FK_Message_fk0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Message] DROP CONSTRAINT [FK_Message_fk0];
GO
IF OBJECT_ID(N'[dbo].[FK_Message_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Message] DROP CONSTRAINT [FK_Message_fk1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDomain_fk0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDomain] DROP CONSTRAINT [FK_UserDomain_fk0];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDomain_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDomain] DROP CONSTRAINT [FK_UserDomain_fk1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Channel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Channel];
GO
IF OBJECT_ID(N'[dbo].[Domain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Domain];
GO
IF OBJECT_ID(N'[dbo].[Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Message];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserDomain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDomain];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Channels'
CREATE TABLE [dbo].[Channels] (
    [ChannelID] int IDENTITY(1,1) NOT NULL,
    [DomainID] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Domains'
CREATE TABLE [dbo].[Domains] (
    [DomainID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [UpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [MessageID] int IDENTITY(1,1) NOT NULL,
    [ChannelID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Content] varchar(max)  NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [IsEdited] bit  NOT NULL,
    [UpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(20)  NULL,
    [LastName] varchar(50)  NULL,
    [Username] varchar(20)  NULL,
    [Password] varchar(20)  NULL,
    [Email] varchar(50)  NULL,
    [IsEmailConfirmed] bit  NULL,
    [Phone] varchar(20)  NULL,
    [Address] varchar(100)  NULL,
    [City] varchar(50)  NULL,
    [Province] varchar(50)  NULL,
    [Country] varchar(50)  NULL,
    [PostalCode] varchar(20)  NULL,
    [IsAdmin] bit  NULL,
    [CreatedOn] datetime  NULL,
    [UpdatedOn] datetime  NULL
);
GO

-- Creating table 'UserDomains'
CREATE TABLE [dbo].[UserDomains] (
    [UserDomainID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [DomainID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ChannelID] in table 'Channels'
ALTER TABLE [dbo].[Channels]
ADD CONSTRAINT [PK_Channels]
    PRIMARY KEY CLUSTERED ([ChannelID] ASC);
GO

-- Creating primary key on [DomainID] in table 'Domains'
ALTER TABLE [dbo].[Domains]
ADD CONSTRAINT [PK_Domains]
    PRIMARY KEY CLUSTERED ([DomainID] ASC);
GO

-- Creating primary key on [MessageID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([MessageID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [UserDomainID] in table 'UserDomains'
ALTER TABLE [dbo].[UserDomains]
ADD CONSTRAINT [PK_UserDomains]
    PRIMARY KEY CLUSTERED ([UserDomainID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DomainID] in table 'Channels'
ALTER TABLE [dbo].[Channels]
ADD CONSTRAINT [FK_Channel_fk0]
    FOREIGN KEY ([DomainID])
    REFERENCES [dbo].[Domains]
        ([DomainID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Channel_fk0'
CREATE INDEX [IX_FK_Channel_fk0]
ON [dbo].[Channels]
    ([DomainID]);
GO

-- Creating foreign key on [ChannelID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Message_fk0]
    FOREIGN KEY ([ChannelID])
    REFERENCES [dbo].[Channels]
        ([ChannelID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_fk0'
CREATE INDEX [IX_FK_Message_fk0]
ON [dbo].[Messages]
    ([ChannelID]);
GO

-- Creating foreign key on [DomainID] in table 'UserDomains'
ALTER TABLE [dbo].[UserDomains]
ADD CONSTRAINT [FK_UserDomain_fk1]
    FOREIGN KEY ([DomainID])
    REFERENCES [dbo].[Domains]
        ([DomainID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDomain_fk1'
CREATE INDEX [IX_FK_UserDomain_fk1]
ON [dbo].[UserDomains]
    ([DomainID]);
GO

-- Creating foreign key on [UserID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Message_fk1]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_fk1'
CREATE INDEX [IX_FK_Message_fk1]
ON [dbo].[Messages]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'UserDomains'
ALTER TABLE [dbo].[UserDomains]
ADD CONSTRAINT [FK_UserDomain_fk0]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDomain_fk0'
CREATE INDEX [IX_FK_UserDomain_fk0]
ON [dbo].[UserDomains]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------