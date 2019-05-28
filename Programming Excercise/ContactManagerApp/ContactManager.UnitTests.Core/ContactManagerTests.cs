//=====================================================================
// System  : ContactManager
// File    : ContactManagerTests.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents test cases for scenarios.
//
// Version     Date      Who        Comments
// ====================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//=====================================================================

using ContactManager.Business;
using ContactManager.Business.Common;
using ContactManager.Models;
using ContactManager.UnityResolverDI;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class ContactManagerTests
    {
        private ContactBusiness _mock;
        private Guid _emptyGUID = Guid.Empty;

        [SetUp]
        public void Setup()
        {
            _mock = Bootstrapper.Init(true);
        }

        [TestCase]
        public void AddContact_WithContactTypePerson_Should_Succeed()
        {
            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Person,
                                          "First Name",
                                          "Last Name",
                                          "",
                                          DateTime.Now,
                                          DateTime.Now,
                                          new List<ContactAddress>());

            Assert.AreEqual(_mock.AddContact(contact), Constants.Common.Success);
        }

        [TestCase]
        public void AddContact_WithContactTypeBusiness_Should_Succeed()
        {
            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "",
                                          "",
                                          "Business Name",
                                          DateTime.Now,
                                          DateTime.Now,
                                          new List<ContactAddress>());

            Assert.AreEqual(_mock.AddContact(contact), Constants.Common.Success);
        }

        [TestCase]
        public void AddContact_WithNoBusinessName_Should_Fail()
        {
            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "First Name",
                                          "Last Name",
                                          "",
                                          DateTime.Now,
                                          DateTime.Now,
                                          new List<ContactAddress>());

            Assert.AreEqual(_mock.AddContact(contact), Constants.Contact.ContactBusinessNameRequired);
        }

        [TestCase]
        public void AddContact_WithNoFirstName_Should_Fail()
        {
            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Person,
                                          "",
                                          "Last Name",
                                          "",
                                          DateTime.Now,
                                          DateTime.Now,
                                          new List<ContactAddress>());

            Assert.AreEqual(_mock.AddContact(contact), Constants.Contact.ContactFirstNameRequired);
        }

        [TestCase]
        public void AddContact_WithNoLastName_Should_Fail()
        {
            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Person,
                                          "First Name",
                                          "",
                                          "",
                                          DateTime.Now,
                                          DateTime.Now,
                                          new List<ContactAddress>());

            Assert.AreEqual(_mock.AddContact(contact), Constants.Contact.ContactLastNameRequired);
        }

        [TestCase]
        public void AddContact_WithAllNameBlank_Should_Fail()
        {
            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "",
                                          "",
                                          "",
                                          DateTime.Now,
                                          DateTime.Now,
                                          new List<ContactAddress>());

            Assert.AreEqual(_mock.AddContact(contact), Constants.Contact.ContactBusinessNameRequired);
        }

        [TestCase]
        public void AddContact_WithContactAddress_Should_Succeed()
        {
            var addressList = new List<ContactAddress>();

            ContactAddress address = new ContactAddress(_emptyGUID,
                                                        _emptyGUID,
                                                        "Clive Lloyd Road",
                                                        "New Jersey",
                                                        "NJ",
                                                        "12345",
                                                        DateTime.Now,
                                                        DateTime.Now);
            addressList.Add(address);

            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "",
                                          "",
                                          "Business Name",
                                          DateTime.Now,
                                          DateTime.Now,
                                          addressList);

            Assert.AreEqual(_mock.AddContact(contact), Constants.Common.Success);
        }

        [TestCase]
        public void AddContact_WithContactAddressNoStreet_Should_Fail()
        {
            var addressList = new List<ContactAddress>();

            ContactAddress address = new ContactAddress(_emptyGUID,
                                                        _emptyGUID,
                                                        "",
                                                        "New Jersey",
                                                        "NJ",
                                                        "12345",
                                                        DateTime.Now,
                                                        DateTime.Now);
            addressList.Add(address);

            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "",
                                          "",
                                          "Business Name",
                                          DateTime.Now,
                                          DateTime.Now,
                                          addressList);

            Assert.AreEqual(_mock.AddContact(contact), Constants.ContactAddress.ContactAddressStreetRequired);
        }

        [TestCase]
        public void AddContact_WithContactAddressNoCity_Should_Fail()
        {
            var addressList = new List<ContactAddress>();

            ContactAddress address = new ContactAddress(_emptyGUID,
                                                        _emptyGUID,
                                                        "Clive Lloyd Road",
                                                        "",
                                                        "NJ",
                                                        "12345",
                                                        DateTime.Now,
                                                        DateTime.Now);
            addressList.Add(address);

            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "",
                                          "",
                                          "Business Name",
                                          DateTime.Now,
                                          DateTime.Now,
                                          addressList);

            Assert.AreEqual(_mock.AddContact(contact), Constants.ContactAddress.ContactAddressCityRequired);
        }

        [TestCase]
        public void AddContact_WithContactAddressNoZipCode_Should_Fail()
        {
            var addressList = new List<ContactAddress>();

            ContactAddress address = new ContactAddress(_emptyGUID,
                                                        _emptyGUID,
                                                        "Clive Lloyd Road",
                                                        "New Jersey",
                                                        "NJ",
                                                        "",
                                                        DateTime.Now,
                                                        DateTime.Now);
            addressList.Add(address);

            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "",
                                          "",
                                          "Business Name",
                                          DateTime.Now,
                                          DateTime.Now,
                                          addressList);

            Assert.AreEqual(_mock.AddContact(contact), Constants.ContactAddress.ContactAddressZipCodeRequired);
        }

        [TestCase]
        public void AddContact_WithContactAddressNoState_Should_Fail()
        {
            var addressList = new List<ContactAddress>();

            ContactAddress address = new ContactAddress(_emptyGUID,
                                                        _emptyGUID,
                                                        "Clive Lloyd Road",
                                                        "New Jersey",
                                                        "",
                                                        "98788",
                                                        DateTime.Now,
                                                        DateTime.Now);
            addressList.Add(address);

            Contact contact = new Contact(_emptyGUID,
                                          ContactType.Business,
                                          "",
                                          "",
                                          "Business Name",
                                          DateTime.Now,
                                          DateTime.Now,
                                          addressList);

            Assert.AreEqual(_mock.AddContact(contact), Constants.ContactAddress.ContactAddressStateRequired);
        }

        [TestCase]
        public void GetContactByZipCode_Invalid_ZipCode()
        {
            Assert.Null(_mock.GetContactByZipCode(string.Empty));
        }

        [TestCase]
        public void GetContactByZipCode_Valid_ZipCode_Should_ReturnContact()
        {
            Assert.AreEqual(_mock.GetContactByZipCode("12345").Count, 1);
        }

        [TestCase]
        public void GetContactDetail_Invalid_ContactID_Should_Fail()
        {
            Assert.Null(_mock.GetContactDetail(Guid.Empty));
        }

        [TestCase]
        public void GetContactDetail_Valid_ContactID_Should_ReturnContact()
        {
            var contactID = Guid.NewGuid();
            Assert.AreEqual(_mock.GetContactDetail(contactID).ContactID, contactID);
        }
    }
}