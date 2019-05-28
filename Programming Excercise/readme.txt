Contact Manager App contains the following:

1) ConatctManager_ER_Diagram.png - For the Data Model
2) ContactManagerDDL.sql - For creating data definition.
3) SProc_AddContact.sql - Stored procedure to add a contact.
3) SProc_AddContactAddress.sql - Stored procedure to add an address.
4) This project contains totally 8 projects in a Solution

   a) ContactManager.Models - Class Library : Contains models for contact manager application.
   b) ContactManager.Interface - Class Library : Contains interfaces for business and data access layer.
   c) ContactManager.DAL - Class Library : Contains data access implementation for Contact manager application.
   d) ContactManager.Business - Class Library : Contains business validation and a layer to access DAL upon business validation.
   e) ContactManager.UnityResolverDI - Class Library : This uses dependency injection to resolve neccessary objects.
  
 Note:
   All of the above class libraries are written with .Net Standard which supports both .Net Framework and .Net Core.
   
   f) ContactManagerConsole.Core.Test - Console Application : To test my DAL methods whether all are functioning properly.
   g) ContactManager.UnitTests.DAL - Class Library : To test DAL methods without hitting database.
   h) ContactManager.UnitTests.Core - Nunit Test Project : Contains test cases to validate the behaviour after the changes.
   
   