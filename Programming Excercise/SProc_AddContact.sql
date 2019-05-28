IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddContact]') AND type in (N'P', N'PC'))
  BEGIN
	EXEC ('CREATE PROCEDURE [dbo].[AddContact] AS SELECT 1')
  END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*  
//====================================================================  
// System  : Contact Manager
// File    : AddContact.sql  
// Author  : Ruban Stephen  
// Created : 05/28/2019  
// Note    : Copyright 2019, OFS., All rights reserved  
// Compiler: SQL Server  
//  
// This file contains a sproc that inserts/updates contact  
//  
// Version     Date      Who          Comments  
// ===================================================================  
// 1.0.0.0  05/28/2019   RStephen     Created the code  
//====================================================================  
*/

ALTER PROCEDURE [dbo].[AddContact]  
 @ContactID UNIQUEIDENTIFIER,    
 @FirstName VARCHAR(50) = NULL,
 @LastName VARCHAR(50) = NULL,    
 @BusinessName VARCHAR(100) = NULL,    
 @ContactType VARCHAR(50)  
AS  
 
BEGIN
 	DECLARE @ContactTypeID UNIQUEIDENTIFIER
	
	SELECT @ContactTypeID = ContactTypeID 
	FROM dbo.ContactType
	WHERE ContactTypeName = @ContactType

	MERGE INTO dbo.Contact TARGET
	USING (
			VALUES (@ContactID, @FirstName, @LastName, @BusinessName, @ContactTypeID)
			) AS SOURCE (ContactID, FirstName, LastName, BusinessName, ContactTypeID)
	ON TARGET.ContactId = SOURCE.ContactID AND @ContactID <> '00000000-0000-0000-0000-000000000000'
	WHEN MATCHED THEN
		UPDATE 
		SET TARGET.FirstName = SOURCE.FirstName,
			TARGET.LastName = SOURCE.LastName,
			TARGET.BusinessName = SOURCE.BusinessName,
			TARGET.ContactTypeID = SOURCE.ContactTypeID,
			TARGET.UpdateDate = GETDATE()
	WHEN NOT MATCHED THEN
		INSERT(FirstName, LastName, BusinessName,
			   ContactTypeID, CreateDate, UpdateDate)
		VALUES(SOURCE.FirstName, SOURCE.LastName, SOURCE.BusinessName,
		       SOURCE.ContactTypeID, GETDATE(), GETDATE());

END