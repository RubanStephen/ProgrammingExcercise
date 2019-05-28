--USE Contacts;
/*  
//=====================================================================================  
// System  : Contact Manager  
// File    : ContactManagerDDL.sql  
// Author  : Ruban Stephen  
// Created : 05/27/2019  
// Note    : Copyright 2019, OFS., All rights reserved  
// Compiler: SQL Server  
//  
// This file contains a DDL Script that creates Data structure for Contact Manager App.  
//  
// Version     Date      Who          Comments  
// ==================================================================================== 
// 1.0.0.0  05/27/2019   RStephen     Created the code  
//=====================================================================================  
*/

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactType]') AND type in (N'U'))
BEGIN

	CREATE TABLE [dbo].[ContactType](
		[ContactTypeID] [UNIQUEIDENTIFIER]  NOT NULL CONSTRAINT [DF_ContactType_ContactTypeID]  DEFAULT (newid()),
		[ContactTypeName] VARCHAR(50) NOT NULL				
	)

	ALTER TABLE [dbo].[ContactType]
	ADD CONSTRAINT PK_ContactType_ContactTypeID PRIMARY KEY (ContactTypeID);

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'COLUMN',@level2name=N'ContactTypeID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Type Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactType', @level2type=N'COLUMN',@level2name=N'ContactTypeName'
	
END
GO

IF NOT EXISTS(SELECT 1 FROM dbo.ContactType WHERE ContactTypeName = 'Business')
BEGIN
   INSERT INTO dbo.ContactType(ContactTypeName)
   VALUES('Business')
END
GO

IF NOT EXISTS(SELECT 1 FROM dbo.ContactType WHERE ContactTypeName = 'Person')
BEGIN
   INSERT INTO dbo.ContactType(ContactTypeName)
   VALUES('Person')
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact]') AND type in (N'U'))
BEGIN

	CREATE TABLE [dbo].[Contact](
		[ContactID] [UNIQUEIDENTIFIER] NOT NULL CONSTRAINT [DF_Contact_ContactID]  DEFAULT (newid()),
		[FirstName] [VARCHAR](50),
		[LastName] [VARCHAR](50),
		[BusinessName] [VARCHAR](100),
		[ContactTypeID] [UNIQUEIDENTIFIER] NOT NULL,
		[CreateDate] [DateTime] NOT NULL,
		[UpdateDate] [DateTime] NOT NULL,
		[IsDeleted]	[BIT] NOT NULL CONSTRAINT [DF_Contact_IsDeleted]  DEFAULT(0)
	)

	ALTER TABLE [dbo].[Contact]
	ADD CONSTRAINT PK_Contact_ContactID PRIMARY KEY (ContactID);
	
	ALTER TABLE [dbo].[Contact] 
	WITH CHECK ADD CONSTRAINT [FK_Contact_ContactTypeID] FOREIGN KEY([ContactTypeID])
	REFERENCES [dbo].[ContactType] ([ContactTypeID])

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'ContactID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact First Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'FirstName'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Last Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'LastName'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Business Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'BusinessName'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key: Contact Type either Business/Person' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'ContactTypeID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Create Date For Contact' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'CreateDate'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Update Date For Contact' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'UpdateDate'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'To identify whether a contact is deleted(Soft delete)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Contact', @level2type=N'COLUMN',@level2name=N'IsDeleted'
	
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactAddress]') AND type in (N'U'))
BEGIN

	CREATE TABLE [dbo].[ContactAddress](
		[ContactAddressID] [UNIQUEIDENTIFIER] NOT NULL CONSTRAINT [DF_ContactAddress_ContactAddressID]  DEFAULT (newid()),
		[ContactID] [UNIQUEIDENTIFIER] NOT NULL,
		[Street] [VARCHAR](50) NOT NULL,
		[City] [VARCHAR](50) NOT NULL,
		[State] [VARCHAR](50) NOT NULL,
		[ZipCode] [VARCHAR](10) NOT NULL,
		[CreateDate] [DateTime] NOT NULL,
		[UpdateDate] [DateTime] NOT NULL,
        [IsDeleted]	[BIT] NOT NULL CONSTRAINT [DF_ContactAddress_IsDeleted]  DEFAULT(0)	
	)

	ALTER TABLE [dbo].[ContactAddress]
	ADD CONSTRAINT PK_ContactAddress_ContactAddressID PRIMARY KEY (ContactAddressID);

	ALTER TABLE [dbo].[ContactAddress] 
	WITH CHECK ADD CONSTRAINT [FK_ContactAddress_ContactID] FOREIGN KEY([ContactID])
	REFERENCES [dbo].[Contact] ([ContactID])
	
	CREATE INDEX idx_ZipCode
    ON [dbo].[ContactAddress]([ZipCode])

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'ContactAddressID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key: Contact ID in which the Address referred to' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'ContactID'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Street name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'Street'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact City name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'City'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact State' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'State'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact ZipCode' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'ZipCode'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Create Date For Contact' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'CreateDate'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Update Date For Contact' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'UpdateDate'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'To identify whether a contact is deleted(Soft delete)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ContactAddress', @level2type=N'COLUMN',@level2name=N'IsDeleted'
	
END
GO

