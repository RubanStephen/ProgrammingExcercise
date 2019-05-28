//=============================================================================
// System  : ContactManager
// File    : IContactDAL.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents an interface for Data Access.
//
// Version     Date      Who        Comments
// ============================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//=============================================================================

using ContactManager.Models;
using System;
using System.Collections.Generic;

namespace ContactManager.Interface
{
    public interface IContactDAL
    {
        // <summary>
        /// Adds a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>Success/Failure with respective Error</returns>
        string AddContact(Contact contact);

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        string DeleteContact(Guid contactID);

        /// <summary>
        /// Adds an address to the contact
        /// </summary>
        /// <param name="contactAddress">Contact Address Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        string AddAddress(ContactAddress contactAddress);

        /// <summary>
        /// Deletes an Address
        /// </summary>
        /// <param name="contactAddressID">Contact Address ID</param>
        /// <returns>String Success/Failure with respective Error</returns>
        string DeleteAddress(Guid contactAddressID);

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>List of Contact object</returns>
        List<Contact> GetContacts();

        /// <summary>
        /// Get an individual contact and contact address info
        /// </summary>
        /// <returns>Contact Object</returns>
        Contact GetContactDetail(Guid contactID);

        /// <summary>
        /// Get Contact By ZipCode
        /// </summary>
        /// <returns>List of Contact Object</returns>
        List<Contact> GetContactByZipCode(string zipCode);
    }
}
