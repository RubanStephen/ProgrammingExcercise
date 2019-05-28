//======================================================================
// System  : ContactManager
// File    : ContactDALTest.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents an implemntation for dummy
// Data Access to perform tests.
//
// Version     Date      Who        Comments
// =====================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//======================================================================

using ContactManager.Business.Common;
using ContactManager.Interface;
using ContactManager.Models;
using System;
using System.Collections.Generic;

namespace ContactManager.UnitTests.DAL
{
    public class ContactDALTest : IContactDAL
    {
        /// <summary>
        /// Adds a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>Success/Failure with respective Error</returns>
        public string AddContact(Contact contact)
        {
            return Constants.Common.Success;
        }

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string DeleteContact(Guid contactID)
        {
            return Constants.Common.Success;
        }

        /// <summary>
        /// Adds an address to the contact
        /// </summary>
        /// <param name="contactAddress">Contact Address Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string AddAddress(ContactAddress contactAddress)
        {
            return Constants.Common.Success;
        }

        /// <summary>
        /// Deletes an Address
        /// </summary>
        /// <param name="contactAddressD">Contact Address ID</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string DeleteAddress(Guid contactAddressD)
        {
            return Constants.Common.Success;
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>List of Contact object</returns>
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>
            {
                new Contact(Guid.NewGuid(),
                                     ContactType.Person,
                                     "First Name",
                                     "Last Name",
                                     "",
                                     DateTime.Now,
                                     DateTime.Now,
                                     new List<ContactAddress>())
            };
            return contacts;
        }

        /// <summary>
        /// Get an individual contact and contact address info
        /// </summary>
        /// <returns>Contact Object</returns>
        public Contact GetContactDetail(Guid contactID)
        {
            Contact contact = new Contact();

            if (contactID == Guid.Empty)
            {
                return null;
            }
            else
            {
                contact = new Contact(contactID,
                                     ContactType.Person,
                                     "First Name",
                                     "Last Name",
                                     "",
                                     DateTime.Now,
                                     DateTime.Now,
                                     new List<ContactAddress>());
            }

            return contact;
        }

        /// <summary>
        /// Get Contact By ZipCode
        /// </summary>
        /// <returns>List of Contact Object</returns>
        public List<Contact> GetContactByZipCode(string zipCode)
        {
            List<Contact> contacts = new List<Contact>();

            if (zipCode == string.Empty)
            {
                return null;
            }
            else
            {
                Guid addressID = Guid.NewGuid();
                Guid contactID = Guid.NewGuid();

                ContactAddress address = new ContactAddress(addressID,
                                                            contactID,
                                                            "Clive Lloyd Road",
                                                            "",
                                                            "NJ",
                                                            "12345",
                                                            DateTime.Now,
                                                            DateTime.Now);

                var addressList = new List<ContactAddress>();

                Contact contact = new Contact(contactID,
                                              ContactType.Business,
                                              "",
                                              "",
                                              "Business Name",
                                              DateTime.Now,
                                              DateTime.Now,
                                              addressList);
                contacts.Add(contact);
            }

            return contacts;
        }
    }
}
