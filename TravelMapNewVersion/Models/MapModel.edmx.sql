
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/13/2014 12:38:39
-- Generated from EDMX file: F:\workspace\TravelMap\TravelMapNewVersion\Models\MapModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-TravelMap-20130501011533];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_File_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[File] DROP CONSTRAINT [FK_File_fk1];
GO
IF OBJECT_ID(N'[dbo].[FK_Post_Post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Post] DROP CONSTRAINT [FK_Post_Post];
GO
IF OBJECT_ID(N'[dbo].[FK_Follower_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follower] DROP CONSTRAINT [FK_Follower_fk1];
GO
IF OBJECT_ID(N'[dbo].[FK_Follower_fk2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follower] DROP CONSTRAINT [FK_Follower_fk2];
GO
IF OBJECT_ID(N'[dbo].[FK_Post_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Post] DROP CONSTRAINT [FK_Post_fk1];
GO
IF OBJECT_ID(N'[dbo].[FK_Post_fk2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Post] DROP CONSTRAINT [FK_Post_fk2];
GO
IF OBJECT_ID(N'[dbo].[FK_PostFile_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostFile] DROP CONSTRAINT [FK_PostFile_fk1];
GO
IF OBJECT_ID(N'[dbo].[FK_PostFile_fk2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostFile] DROP CONSTRAINT [FK_PostFile_fk2];
GO
IF OBJECT_ID(N'[dbo].[FK_Travel_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Travel] DROP CONSTRAINT [FK_Travel_fk1];
GO
IF OBJECT_ID(N'[dbo].[FK_Travel_fk2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Travel] DROP CONSTRAINT [FK_Travel_fk2];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMap_fk1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMap] DROP CONSTRAINT [FK_UserMap_fk1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMap_fk2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMap] DROP CONSTRAINT [FK_UserMap_fk2];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Country]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Country];
GO
IF OBJECT_ID(N'[dbo].[File]', 'U') IS NOT NULL
    DROP TABLE [dbo].[File];
GO
IF OBJECT_ID(N'[dbo].[FileType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileType];
GO
IF OBJECT_ID(N'[dbo].[Follower]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Follower];
GO
IF OBJECT_ID(N'[dbo].[Post]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Post];
GO
IF OBJECT_ID(N'[dbo].[PostFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostFile];
GO
IF OBJECT_ID(N'[dbo].[PostType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostType];
GO
IF OBJECT_ID(N'[dbo].[Travel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Travel];
GO
IF OBJECT_ID(N'[dbo].[UserMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMap];
GO
IF OBJECT_ID(N'[dbo].[UserProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfile];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryId] uniqueidentifier  NOT NULL,
    [Name] varchar(50)  NULL,
    [Title] varchar(50)  NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [FileId] uniqueidentifier  NOT NULL,
    [FileTypeId] uniqueidentifier  NULL,
    [URL] varchar(1000)  NULL
);
GO

-- Creating table 'FileTypes'
CREATE TABLE [dbo].[FileTypes] (
    [FileTypeId] uniqueidentifier  NOT NULL,
    [Name] varchar(20)  NULL
);
GO

-- Creating table 'Followers'
CREATE TABLE [dbo].[Followers] (
    [UserId] uniqueidentifier  NOT NULL,
    [FollowerId] uniqueidentifier  NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [PostId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [Text] varchar(max)  NULL,
    [TypeId] uniqueidentifier  NOT NULL,
    [ParentId] uniqueidentifier  NOT NULL,
    [Time] datetime  NOT NULL
);
GO

-- Creating table 'PostFiles'
CREATE TABLE [dbo].[PostFiles] (
    [PostFileId] uniqueidentifier  NOT NULL,
    [PostId] uniqueidentifier  NULL,
    [FileId] uniqueidentifier  NULL
);
GO

-- Creating table 'PostTypes'
CREATE TABLE [dbo].[PostTypes] (
    [PostTypeId] uniqueidentifier  NOT NULL,
    [Name] varchar(20)  NULL
);
GO

-- Creating table 'Travels'
CREATE TABLE [dbo].[Travels] (
    [PostId] uniqueidentifier  NOT NULL,
    [CountryId] uniqueidentifier  NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL
);
GO

-- Creating table 'UserMaps'
CREATE TABLE [dbo].[UserMaps] (
    [UserMapId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [CountryId] uniqueidentifier  NULL
);
GO

-- Creating table 'UserProfiles'
CREATE TABLE [dbo].[UserProfiles] (
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(max)  NULL,
    [Surname] nvarchar(max)  NULL,
    [BirthDate] datetime  NOT NULL,
    [Phone] nvarchar(max)  NULL,
    [Photo] varbinary(max)  NULL,
    [Email] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CountryId] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryId] ASC);
GO

-- Creating primary key on [FileId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([FileId] ASC);
GO

-- Creating primary key on [FileTypeId] in table 'FileTypes'
ALTER TABLE [dbo].[FileTypes]
ADD CONSTRAINT [PK_FileTypes]
    PRIMARY KEY CLUSTERED ([FileTypeId] ASC);
GO

-- Creating primary key on [UserId] in table 'Followers'
ALTER TABLE [dbo].[Followers]
ADD CONSTRAINT [PK_Followers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [PostId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([PostId] ASC);
GO

-- Creating primary key on [PostFileId] in table 'PostFiles'
ALTER TABLE [dbo].[PostFiles]
ADD CONSTRAINT [PK_PostFiles]
    PRIMARY KEY CLUSTERED ([PostFileId] ASC);
GO

-- Creating primary key on [PostTypeId] in table 'PostTypes'
ALTER TABLE [dbo].[PostTypes]
ADD CONSTRAINT [PK_PostTypes]
    PRIMARY KEY CLUSTERED ([PostTypeId] ASC);
GO

-- Creating primary key on [PostId] in table 'Travels'
ALTER TABLE [dbo].[Travels]
ADD CONSTRAINT [PK_Travels]
    PRIMARY KEY CLUSTERED ([PostId] ASC);
GO

-- Creating primary key on [UserMapId] in table 'UserMaps'
ALTER TABLE [dbo].[UserMaps]
ADD CONSTRAINT [PK_UserMaps]
    PRIMARY KEY CLUSTERED ([UserMapId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserProfiles'
ALTER TABLE [dbo].[UserProfiles]
ADD CONSTRAINT [PK_UserProfiles]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountryId] in table 'Travels'
ALTER TABLE [dbo].[Travels]
ADD CONSTRAINT [FK_Travel_fk2]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([CountryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Travel_fk2'
CREATE INDEX [IX_FK_Travel_fk2]
ON [dbo].[Travels]
    ([CountryId]);
GO

-- Creating foreign key on [CountryId] in table 'UserMaps'
ALTER TABLE [dbo].[UserMaps]
ADD CONSTRAINT [FK_UserMap_fk2]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([CountryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMap_fk2'
CREATE INDEX [IX_FK_UserMap_fk2]
ON [dbo].[UserMaps]
    ([CountryId]);
GO

-- Creating foreign key on [FileTypeId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_File_fk1]
    FOREIGN KEY ([FileTypeId])
    REFERENCES [dbo].[FileTypes]
        ([FileTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_File_fk1'
CREATE INDEX [IX_FK_File_fk1]
ON [dbo].[Files]
    ([FileTypeId]);
GO

-- Creating foreign key on [FileId] in table 'PostFiles'
ALTER TABLE [dbo].[PostFiles]
ADD CONSTRAINT [FK_PostFile_fk2]
    FOREIGN KEY ([FileId])
    REFERENCES [dbo].[Files]
        ([FileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostFile_fk2'
CREATE INDEX [IX_FK_PostFile_fk2]
ON [dbo].[PostFiles]
    ([FileId]);
GO

-- Creating foreign key on [UserId] in table 'Followers'
ALTER TABLE [dbo].[Followers]
ADD CONSTRAINT [FK_Follower_fk1]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserProfiles]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FollowerId] in table 'Followers'
ALTER TABLE [dbo].[Followers]
ADD CONSTRAINT [FK_Follower_fk2]
    FOREIGN KEY ([FollowerId])
    REFERENCES [dbo].[UserProfiles]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Follower_fk2'
CREATE INDEX [IX_FK_Follower_fk2]
ON [dbo].[Followers]
    ([FollowerId]);
GO

-- Creating foreign key on [ParentId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Post_Post]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Post_Post'
CREATE INDEX [IX_FK_Post_Post]
ON [dbo].[Posts]
    ([ParentId]);
GO

-- Creating foreign key on [UserId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Post_fk1]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserProfiles]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Post_fk1'
CREATE INDEX [IX_FK_Post_fk1]
ON [dbo].[Posts]
    ([UserId]);
GO

-- Creating foreign key on [TypeId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Post_fk2]
    FOREIGN KEY ([TypeId])
    REFERENCES [dbo].[PostTypes]
        ([PostTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Post_fk2'
CREATE INDEX [IX_FK_Post_fk2]
ON [dbo].[Posts]
    ([TypeId]);
GO

-- Creating foreign key on [PostId] in table 'PostFiles'
ALTER TABLE [dbo].[PostFiles]
ADD CONSTRAINT [FK_PostFile_fk1]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostFile_fk1'
CREATE INDEX [IX_FK_PostFile_fk1]
ON [dbo].[PostFiles]
    ([PostId]);
GO

-- Creating foreign key on [PostId] in table 'Travels'
ALTER TABLE [dbo].[Travels]
ADD CONSTRAINT [FK_Travel_fk1]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'UserMaps'
ALTER TABLE [dbo].[UserMaps]
ADD CONSTRAINT [FK_UserMap_fk1]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserProfiles]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMap_fk1'
CREATE INDEX [IX_FK_UserMap_fk1]
ON [dbo].[UserMaps]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------