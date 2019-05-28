//===============================================================================
// System  : ContactManager
// File    : Contact.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents a model for Contact address object.
//
// Version     Date      Who        Comments
// ==============================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//===============================================================================

using System;

namespace ContactManager.Models
{
    public class ContactAddress
    {
        /// <summary>
        /// Contact Address ID
        /// </summary>
        public Guid ContactAddressID { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
		public Guid ContactID { get; set; }

        /// <summary>
        /// Contact Street 
        /// </summary>
		public string Street { get; set; }

        /// <summary>
        /// Contact City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Contact State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Contact Zip Code
        /// </summary>
        /// <returns></returns>
        public string ZipCode { get; set; }

        /// <summary>
        /// Contact Address Create Date
        /// </summary>
        public DateTime ContactAddressCreateDate { get; set; }

        /// <summary>
        /// Contact Address Update Date
        /// </summary>
        public DateTime ContactAddressUpdateDate { get; set; }

        /// <summary>
        /// Default Contructor
        /// </summary>
        public ContactAddress()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ContactAddress(Guid contactAddressID, Guid contactAddressContactID, string street, string city, string state, string zipCode,
            DateTime contactAddressCreateDate, DateTime contactAddressUpdateDate)
        {
            this.ContactAddressID = contactAddressID;
            this.ContactID = contactAddressContactID;
            this.Street = street;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.ContactAddressCreateDate = contactAddressCreateDate;
            this.ContactAddressUpdateDate = contactAddressUpdateDate;
        }
    }
}
