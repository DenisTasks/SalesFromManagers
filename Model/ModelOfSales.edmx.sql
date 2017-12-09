
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2017 19:23:29
-- Generated from EDMX file: D:\Entity\Model\ModelOfSales.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [modelofsalesdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductSaleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleInfoSet] DROP CONSTRAINT [FK_ProductSaleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientSaleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleInfoSet] DROP CONSTRAINT [FK_ClientSaleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerSaleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleInfoSet] DROP CONSTRAINT [FK_ManagerSaleInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[SaleInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleInfoSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[ClientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSet];
GO
IF OBJECT_ID(N'[dbo].[ManagerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManagerSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SaleInfoSet'
CREATE TABLE [dbo].[SaleInfoSet] (
    [SaleInfoId] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [ClientId] int  NOT NULL,
    [ManagerId] int  NOT NULL,
    [DateOfSale] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL
);
GO

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [ClientId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ManagerSet'
CREATE TABLE [dbo].[ManagerSet] (
    [ManagerId] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SaleInfoId] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [PK_SaleInfoSet]
    PRIMARY KEY CLUSTERED ([SaleInfoId] ASC);
GO

-- Creating primary key on [ProductId] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [ClientId] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([ClientId] ASC);
GO

-- Creating primary key on [ManagerId] in table 'ManagerSet'
ALTER TABLE [dbo].[ManagerSet]
ADD CONSTRAINT [PK_ManagerSet]
    PRIMARY KEY CLUSTERED ([ManagerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductId] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_ProductSaleInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductSaleInfo'
CREATE INDEX [IX_FK_ProductSaleInfo]
ON [dbo].[SaleInfoSet]
    ([ProductId]);
GO

-- Creating foreign key on [ClientId] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_ClientSaleInfo]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[ClientSet]
        ([ClientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientSaleInfo'
CREATE INDEX [IX_FK_ClientSaleInfo]
ON [dbo].[SaleInfoSet]
    ([ClientId]);
GO

-- Creating foreign key on [ManagerId] in table 'SaleInfoSet'
ALTER TABLE [dbo].[SaleInfoSet]
ADD CONSTRAINT [FK_ManagerSaleInfo]
    FOREIGN KEY ([ManagerId])
    REFERENCES [dbo].[ManagerSet]
        ([ManagerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerSaleInfo'
CREATE INDEX [IX_FK_ManagerSaleInfo]
ON [dbo].[SaleInfoSet]
    ([ManagerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------