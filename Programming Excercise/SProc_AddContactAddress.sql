IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddContactAddress]') AND type in (N'P', N'PC'))
  BEGIN
	EXEC ('CREATE PROCEDURE [dbo].[AddContactAddress] AS SELECT 1')
  END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*  
//================================================================  
// System  : Contact Manager
// File    : AddContactAddress.sql  
// Author  : Ruban Stephen  
// Created : 05/28/2019  
// Note    : Copyright 2019, OFS., All rights reserved  
// Compiler: SQL Server  
//  
// This file contains a sproc that inserts/Updates contact address  
//  
// Version     Date      Who          Comments  
// ===============================================================
// 1.0.0.0  05/28/2019   RStephen     Created the code  
//================================================================
*/

ALTER PROCEDURE [dbo].[AddContactAddress]  
 @ContactAddressID UNIQUEIDENTIFIER,
 @ContactID UNIQUEIDENTIFIER,    
 @Street VARCHAR(50),
 @City VARCHAR(50),    
 @State VARCHAR(50),    
 @ZipCode VARCHAR(10)  
AS  
 
BEGIN

	BEGIN TRY  

	    IF EXISTS(SELECT 1 FROM dbo.Contact
					WHERE ContactID = @ContactID AND ContactID <> '00000000-0000-0000-0000-000000000000')
		BEGIN

			MERGE INTO dbo.ContactAddress TARGET
			USING (
					VALUES (@ContactAddressID, @ContactID, @Street, @City, @State, @ZipCode)
					) AS SOURCE (ContactAddressID, ContactID, Street, City, [State], ZipCode)
			ON TARGET.ContactAddressID = SOURCE.ContactAddressID
			WHEN MATCHED THEN
				UPDATE 
				SET TARGET.Street = SOURCE.Street,
					TARGET.City = SOURCE.City,
					TARGET.[State] = SOURCE.[State],
					TARGET.ZipCode = SOURCE.ZipCode,
					TARGET.UpdateDate = GETDATE()
			WHEN NOT MATCHED THEN
				INSERT(ContactID, Street, City, [State], ZipCode, CreateDate, UpdateDate)
				VALUES(@ContactID, SOURCE.Street, SOURCE.City, SOURCE.[State],
					   SOURCE.ZipCode, GETDATE(), GETDATE());
		 END
		 ELSE
			BEGIN
				-- RAISERROR with severity 11-19 will cause execution to   
				-- jump to the CATCH block.  
				RAISERROR ('An address must have a contact.', -- Message text.  
							16, -- Severity.  
							1 -- State.  
							);  
			END
	END TRY  
	BEGIN CATCH  
		DECLARE @ErrorMessage NVARCHAR(4000);  
		DECLARE @ErrorSeverity INT;  
		DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
			@ErrorSeverity = ERROR_SEVERITY(),  
			@ErrorState = ERROR_STATE();  
  
		-- Use RAISERROR inside the CATCH block to return error  
		-- information about the original error that caused  
		-- execution to jump to the CATCH block.  
		RAISERROR (@ErrorMessage, -- Message text.  
				   @ErrorSeverity, -- Severity.  
				   @ErrorState -- State.  
				   );  
	END CATCH;

END