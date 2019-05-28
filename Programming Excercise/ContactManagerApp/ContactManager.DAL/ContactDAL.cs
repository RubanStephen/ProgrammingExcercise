//=============================================================================
// System  : ContactManager
// File    : ContactDAL.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents an implementation for Data Access.
//
// Version     Date      Who        Comments
// ============================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//=============================================================================

using ContactManager.Business.Common;
using ContactManager.Interface;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ContactManager.DAL
{
    public class ContactDAL : IContactDAL
    {
        private static readonly string _databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        #region Public Methods

        #region public string AddContact(Contact contact)

        /// <summary>
        /// Adds a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>Success/Failure with respective Error</returns>
        public string AddContact(Contact contact)
        {
            string result = Constants.Common.Success;

            using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
            {
                connection.Open();
                // Create command to call SPROC
                using (var query = new SqlCommand(@"dbo.AddContact", connection))
                {
                    query.CommandType = System.Data.CommandType.StoredProcedure;

                    #region Set Command Parameters

                    query.Parameters.AddWithValue("@ContactType", contact.ContactType.ToString());
                    query.Parameters.AddWithValue("@BusinessName", contact.BusinessName);
                    query.Parameters.AddWithValue("@LastName", contact.LastName);
                    query.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    query.Parameters.AddWithValue("@ContactID", contact.ContactID);

                    query.ExecuteNonQuery();

                    #endregion Set Command Parameters
                }
            }//end Sqlconnection statement

            return result;
        }

        #endregion public string AddContact(Contact contact)

        #region public string DeleteContact(Guid contactID)

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="contact">Contact Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string DeleteContact(Guid contactID)
        {
            string result = Constants.Common.Success;

            using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
            {
                connection.Open();
                using (var query = new SqlCommand("", connection))
                {
                    query.CommandText = @"UPDATE dbo.Contact
                                            SET IsDeleted = 1,
                                                UpdateDate = GETDATE()
                                          WHERE ContactID = @ContactID";

                    query.Parameters.AddWithValue("@ContactID", contactID);

                    query.ExecuteNonQuery();

                }//end usnig sqlcommand
            }//end Sqlconnection statement

            return result;
        }

        #endregion public string DeleteContact(Guid contactID)

        #region public string AddAddress(ContactAddress contactAddress)

        /// <summary>
        /// Adds an address to the contact
        /// </summary>
        /// <param name="contactAddress">Contact Address Object</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string AddAddress(ContactAddress contactAddress)
        {
            string result = Constants.Common.Success;

            using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
            {
                connection.Open();
                // Create command to call SPROC
                using (var query = new SqlCommand(@"dbo.AddContactAddress", connection))
                {
                    query.CommandType = System.Data.CommandType.StoredProcedure;

                    #region Set Command Parameters

                    query.Parameters.AddWithValue("@Street", contactAddress.Street);
                    query.Parameters.AddWithValue("@City", contactAddress.City);
                    query.Parameters.AddWithValue("@State", contactAddress.State);
                    query.Parameters.AddWithValue("@ZipCode", contactAddress.ZipCode);
                    query.Parameters.AddWithValue("@ContactID", contactAddress.ContactID);
                    query.Parameters.AddWithValue("@ContactAddressID", contactAddress.ContactAddressID);

                    query.ExecuteNonQuery();

                    #endregion Set Command Parameters
                }
            }//end Sqlconnection statement

            return result;
        }

        #endregion public string AddAddress(ContactAddress contactAddress)

        #region public string DeleteAddress(Guid contactAddressID)

        /// <summary>
        /// Deletes an Address
        /// </summary>
        /// <param name="contactID">Contact ID</param>
        /// <returns>String Success/Failure with respective Error</returns>
        public string DeleteAddress(Guid contactAddressID)
        {
            string result = Constants.Common.Success;

            using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
            {
                connection.Open();
                try
                {
                    using (var query = new SqlCommand("", connection))
                    {
                        query.CommandText = @"UPDATE dbo.ContactAddress
                                                SET IsDeleted = 1,
                                                    UpdateDate = GETDATE()
                                                WHERE ContactAddressID = @ContactAddressID";

                        query.Parameters.AddWithValue("@ContactAddressID", contactAddressID);

                        query.ExecuteNonQuery();

                    }//end usnig sqlcommand
                }//end try
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }

            return result;
        }

        #endregion public string DeleteAddress(Guid contactAddressID)

        #region public List<Contact> GetContacts()

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>List of Contact object</returns>
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
            {
                connection.Open();
                try
                {
                    using (var query = new SqlCommand("", connection))
                    {
                        query.CommandText = @"SELECT ContactID, ct.ContactTypeName, FirstName, LastName,
                                                     BusinessName, CreateDate, UpdateDate
                                              FROM dbo.Contact c WITH(NOLOCK)
                                                    INNER JOIN dbo.ContactType ct ON (c.ContactTypeID = ct.ContactTypeID)
                                              WHERE IsDeleted = 0";

                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactType contactType = (ContactType)Enum.Parse(typeof(ContactType), reader[1].ToString());

                                // add the data to the list
                                contacts.Add(new Contact((Guid)reader[0], contactType, reader[2].ToString(), reader[3].ToString(),
                                    reader[4].ToString(), (DateTime)reader[5], (DateTime)reader[6], new List<ContactAddress>()));
                            }//end while
                        } // end using (var reader = ...)

                    }//end usnig sqlcommand
                }//end try
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return contacts;
        }

        #endregion public List<Contact> GetContacts()

        #region public Contact GetContactDetail(Guid contactID)

        /// <summary>
        /// Get an individual contact and contact address info
        /// </summary>
        /// <returns>Contact Object</returns>
        public Contact GetContactDetail(Guid contactID)
        {
            Contact contact = new Contact();

            using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
            {
                connection.Open();
                try
                {
                    using (var query = new SqlCommand("", connection))
                    {
                        query.CommandText = @"SELECT ContactID, ct.ContactTypeName, FirstName, LastName,
                                                     BusinessName, CreateDate, UpdateDate
                                              FROM dbo.Contact c WITH(NOLOCK)
                                                    INNER JOIN dbo.ContactType ct ON (c.ContactTypeID = ct.ContactTypeID)
                                              WHERE ContactID = @ContactID";

                        query.Parameters.AddWithValue("@ContactID", contactID);

                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactType contactType = (ContactType)Enum.Parse(typeof(ContactType), reader[1].ToString());

                                // add the data to the list
                                contact = new Contact((Guid)reader[0], contactType, reader[2].ToString(), reader[3].ToString(),
                                    reader[4].ToString(), (DateTime)reader[5], (DateTime)reader[6], new List<ContactAddress>());
                            }//end while
                        } // end using (var reader = ...)

                        query.CommandText = @"SELECT ContactAddressID, ContactID, Street, City, State,
                                                     ZipCode, CreateDate, UpdateDate
                                              FROM dbo.ContactAddress WITH(NOLOCK)
                                              WHERE ContactID = @ContactID
                                                AND IsDeleted = 0";

                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // add the data to the list
                                contact.ContactAddress.Add(new ContactAddress((Guid)reader[0], (Guid)reader[1], reader[2].ToString(),
                                    reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), (DateTime)reader[6], (DateTime)reader[7]));
                            }//end while
                        } // end using (var reader = ...)

                    }//end usnig sqlcommand
                }//end try
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return contact;
        }

        #endregion public Contact GetContactDetail(Guid contactID)

        #region public List<Contact> GetContactByZipCode(string zipCode)

        /// <summary>
        /// Get Contact By ZipCode
        /// </summary>
        /// <returns>List of Contact Object</returns>
        public List<Contact> GetContactByZipCode(string zipCode)
        {
            List<Contact> contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(_databaseConnectionString))
            {
                connection.Open();
                try
                {
                    using (var query = new SqlCommand("", connection))
                    {
                        query.CommandText = @"SELECT c.ContactID, ct.ContactTypeName, FirstName, LastName,
                                                     BusinessName, c.CreateDate, c.UpdateDate
                                              FROM dbo.Contact c WITH(NOLOCK)
                                                   INNER JOIN dbo.ContactType ct WITH(NOLOCK)
                                                     ON (c.ContactTypeID = ct.ContactTypeID)
                                                   INNER JOIN dbo.ContactAddress ca WITH(NOLOCK) 
                                                     ON (c.ContactID = ca.ContactID) AND ca.IsDeleted = 0
                                              WHERE ca.ZipCode = @ZipCode
                                                AND c.IsDeleted = 0";

                        query.Parameters.AddWithValue("@ZipCode", zipCode);

                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactType contactType = (ContactType)Enum.Parse(typeof(ContactType), reader[1].ToString());

                                // add the data to the list
                                var contact = new Contact((Guid)reader[0], contactType, reader[2].ToString(), reader[3].ToString(),
                                    reader[4].ToString(), (DateTime)reader[5], (DateTime)reader[6], new List<ContactAddress>());

                                contacts.Add(contact);
                            }//end while
                        } // end using (var reader = ...)
                    }//end usnig sqlcommand
                }//end try
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return contacts;
        }

        #endregion public List<Contact> GetContactByZipCode(string zipCode)

        #endregion Public Methods

    }
}
