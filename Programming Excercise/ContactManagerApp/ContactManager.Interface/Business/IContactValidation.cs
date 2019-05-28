//================================================================================
// System  : ContactManager
// File    : IContactValidation.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents an interface for Contact Validation.
//
// Version     Date      Who        Comments
// ===============================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//================================================================================

using ContactManager.Models;

namespace ContactManager.Interface
{
    public interface IContactValidation
    {
        /// <summary>
        /// Validates Contact object
        /// </summary>
        /// <param name="contact"></param>
        void ValidateContact(Contact contact);

        /// <summary>
        /// Validates Contact Address object
        /// </summary>
        /// <param name="contact"></param>
        void ValidateContactAddress(ContactAddress contactAddress);
    }
}
