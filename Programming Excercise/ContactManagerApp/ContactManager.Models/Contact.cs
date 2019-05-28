//=======================================================================
// System  : ContactManager
// File    : Contact.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents a model for Contact object.
//
// Version     Date      Who        Comments
// ======================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//=======================================================================

using System;
using System.Collections.Generic;

namespace ContactManager.Models
{
    public class Contact
    {
        /// <summary>
        /// Contact ID
        /// </summary>
        public Guid ContactID { get; set; }

        /// <summary>
        /// Contact Type
        /// </summary>
		public ContactType ContactType { get; set; }

        /// <summary>
        /// Contact First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Contact Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Contact Business Name
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// Create Date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Update Date
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Contact Address/Addresses
        /// </summary>
        public List<ContactAddress> ContactAddress { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Contact()
        {
            ContactAddress = new List<ContactAddress>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Contact(Guid contactID, ContactType contactType, string contactFirstName, string contactLastName, string contactBusinessName, DateTime createDate, DateTime updateDate, List<ContactAddress> contactAddresses)
        {
            this.ContactID = contactID;
            this.ContactType = contactType;
            this.FirstName = contactFirstName;
            this.LastName = contactLastName;
            this.BusinessName = contactBusinessName;
            this.CreateDate = createDate;
            this.UpdateDate = updateDate;
            this.ContactAddress = contactAddresses;
        }
    }

    /// <summary>
    /// Enum to represent Contact Type
    /// </summary>
    public enum ContactType
    {
        Business = 1,
        Person = 2
    }
}
