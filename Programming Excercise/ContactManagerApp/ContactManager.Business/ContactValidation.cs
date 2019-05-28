//==================================================================================
// System  : ContactManager
// File    : ContactValidation.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents validating contact during Data Access.
//
// Version     Date      Who        Comments
// =================================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//==================================================================================

using ContactManager.Business.Common;
using ContactManager.Interface;
using ContactManager.Models;
using System;

namespace ContactManager.Business
{
    public class ContactValidation : IContactValidation
    {
        /// <summary>
        /// Validates Contact object
        /// </summary>
        /// <param name="contact">Contact object</param>
        public void ValidateContact(Contact contact)
        {
            //First Name required
            if (string.IsNullOrEmpty(contact.FirstName) && contact.ContactType == ContactType.Person)
            {
                throw new Exception(Constants.Contact.ContactFirstNameRequired);
            }
            //Last Name required
            if (string.IsNullOrEmpty(contact.LastName) && contact.ContactType == ContactType.Person)
            {
                throw new Exception(Constants.Contact.ContactLastNameRequired);
            }
            //Business Name required
            if (string.IsNullOrEmpty(contact.BusinessName) && contact.ContactType == ContactType.Business)
            {
                throw new Exception(Constants.Contact.ContactBusinessNameRequired);
            }

            foreach (var address in contact.ContactAddress)
            {
                //Contact and Contact Address does not have proper relation.
                if ((contact.ContactID != Guid.Empty && address.ContactID != Guid.Empty && (contact.ContactID != address.ContactID)) ||
                    (contact.ContactID == Guid.Empty && address.ContactID != Guid.Empty))
                {
                    throw new Exception(Constants.Contact.ContactIDAndContactAdreessContactIDNotMatched);
                }

                //Validate address fields
                ValidateAddressFields(address, false);
            }

        }

        /// <summary>
        /// Validates Contact address object
        /// </summary>
        /// <param name="contactAddress">Contact Address object</param>
        public void ValidateContactAddress(ContactAddress contactAddress)
        {
            ValidateAddressFields(contactAddress, true);
        }

        /// <summary>
        /// Validates Contact address object
        /// </summary>
        /// <param name="contactAddress">Contact Address object</param>
        private static void ValidateAddressFields(ContactAddress contactAddress, bool isAddressAloneAPI)
        {
            //Contact required
            if (isAddressAloneAPI && contactAddress.ContactID == Guid.Empty)
            {
                throw new Exception(Constants.ContactAddress.ContactAddressContactRequired);
            }

            //Street required
            if (string.IsNullOrEmpty(contactAddress.Street))
            {
                throw new Exception(Constants.ContactAddress.ContactAddressStreetRequired);
            }

            //City required
            if (string.IsNullOrEmpty(contactAddress.City))
            {
                throw new Exception(Constants.ContactAddress.ContactAddressCityRequired);
            }

            //State required
            if (string.IsNullOrEmpty(contactAddress.State))
            {
                throw new Exception(Constants.ContactAddress.ContactAddressStateRequired);
            }

            //Zip Code required
            if (string.IsNullOrEmpty(contactAddress.ZipCode))
            {
                throw new Exception(Constants.ContactAddress.ContactAddressZipCodeRequired);
            }
        }
    }
}
