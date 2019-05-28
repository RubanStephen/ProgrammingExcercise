//================================================================================
// System  : ContactManager
// File    : ContactBusiness.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents Business validation and Data Access.
//
// Version     Date      Who        Comments
// ===============================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//================================================================================

using ContactManager.Interface;
using ContactManager.Models;
using System;
using System.Collections.Generic;

namespace ContactManager.Business
{
    public class ContactBusiness : IContactBusiness
    {
        IContactDAL _iContactDAL;
        IContactValidation _contactValidation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iContactDAL">Contact DAL Object</param>
        /// <param name="contactValidation">Contact Validation Object</param>
        public ContactBusiness(IContactDAL iContactDAL, IContactValidation contactValidation)
        {
            _iContactDAL = iContactDAL;
            _contactValidation = contactValidation;
        }

        /// <summary>
        /// Adds a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>Success/Failure with respective Error</returns>
        public string AddContact(Contact contact)
        {
            string result = string.Empty;
            try
            {
                _contactValidation.ValidateContact(contact);
                result = _iContactDAL.AddContact(contact);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string DeleteContact(Guid contactID)
        {
            string result = string.Empty;
            try
            {
                result = _iContactDAL.DeleteContact(contactID);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Adds an address to the contact
        /// </summary>
        /// <param name="contactAddress">Contact Address Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string AddAddress(ContactAddress contactAddress)
        {
            string result = string.Empty;
            try
            {
                _contactValidation.ValidateContactAddress(contactAddress);
                result = _iContactDAL.AddAddress(contactAddress);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Deletes an Address
        /// </summary>
        /// <param name="contactAddressID">Contact Address ID</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string DeleteAddress(Guid contactAddressID)
        {
            string result = string.Empty;
            try
            {
                result = _iContactDAL.DeleteAddress(contactAddressID);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>List of Contact object</returns>
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                contacts = _iContactDAL.GetContacts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contacts;
        }

        /// <summary>
        /// Get an individual contact and contact address info
        /// </summary>
        /// <returns>Contact Object</returns>
        public Contact GetContactDetail(Guid contactID)
        {
            Contact contact = new Contact();
            try
            {
                contact = _iContactDAL.GetContactDetail(contactID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contact;
        }

        /// <summary>
        /// Get Contact By Zip Code
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public List<Contact> GetContactByZipCode(string zipCode)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                contacts = _iContactDAL.GetContactByZipCode(zipCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contacts;
        }
    }
}
