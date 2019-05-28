//==========================================================================
// System  : ContactManager
// File    : Program.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents test for Data Access methods.
//
// Version     Date      Who        Comments
// =========================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//==========================================================================

using ContactManager.Business;
using ContactManager.Business.Common;
using ContactManager.Models;
using ContactManager.UnityResolverDI;
using System;
using System.Linq;

namespace ContactManagerConsole.Core.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ContactBusiness objBusiness = Bootstrapper.Init();

                Contact c = new Contact
                {
                    
                    ContactType = ContactType.Business,
                    BusinessName = "Object Frontier Software"
                };

                var addContactResult = objBusiness.AddContact(c);
                Console.WriteLine("Add Contact result: " + addContactResult);

                var exisitngContacts = objBusiness.GetContacts();

                var contactID = exisitngContacts.Select(con => con.ContactID).FirstOrDefault();

                ContactAddress ca = new ContactAddress
                {
                    ContactID = contactID,
                    City = "ROCHESTER",
                    State = "NY",
                    Street = "Monroe Road",
                    ZipCode = "32345"
                };

                var addContactAddressResult = objBusiness.AddAddress(ca);
                Console.WriteLine("Add Contact address result: " + addContactAddressResult);

                var contactDetail = objBusiness.GetContactDetail(contactID);
                Console.WriteLine("Get Contact detail result: " + Constants.Common.Success);

                var contactByZipCode = objBusiness.GetContactByZipCode(ca.ZipCode);
                Console.WriteLine("Get Contact by Zip Code result: " + Constants.Common.Success);

                //Add Contact
                c.ContactID = Guid.Empty;
                c.ContactType = ContactType.Person;
                c.FirstName = "Ruban";
                c.LastName = "Stephen";

                var addContactWithAddressResult = objBusiness.AddContact(c);
                Console.WriteLine("Add Contact with address result: " + addContactWithAddressResult);

                exisitngContacts = objBusiness.GetContacts();

                foreach (var address in contactDetail.ContactAddress)
                {
                    var deleleAddress = objBusiness.DeleteAddress(address.ContactAddressID);
                    Console.WriteLine("Delete address for ContactID: " + contactDetail.ContactID.ToString() + deleleAddress);
                }
                
                foreach (var contact in exisitngContacts)
                {
                    var deleleStatus = objBusiness.DeleteContact(contact.ContactID);
                    Console.WriteLine("Delete contact for ContactID: " + contact.ContactID.ToString() + deleleStatus);
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DAL Test failed due to: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
