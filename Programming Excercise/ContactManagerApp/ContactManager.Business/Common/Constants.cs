//====================================================================
// System  : ContactManager
// File    : Constants.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that represents constants for reference.
//
// Version     Date      Who        Comments
// ===================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//====================================================================

namespace ContactManager.Business.Common
{
    public class Constants
    {
        /// <summary>
        /// Common Constants
        /// </summary>
        public class Common
        {
            public const string Success = "Success";
        }

        /// <summary>
        /// Error constants for Contact object validation
        /// </summary>
        public class Contact
        {
            public const string ContactFirstNameRequired = "A contact must have a First name";
            public const string ContactLastNameRequired = "A contact must have a Last name";
            public const string ContactBusinessNameRequired = "A contact must have a Business name";
            public const string ContactIDAndContactAdreessContactIDNotMatched = "Contact and Contact Address are not related properly.";
        }

        /// <summary>
        /// Error constants for Contact address object validation
        /// </summary>
        public class ContactAddress
        {
            public const string ContactAddressStreetRequired = "Contact address must have Street.";
            public const string ContactAddressCityRequired = "Contact address must have City.";
            public const string ContactAddressStateRequired = "Contact address must have State.";
            public const string ContactAddressZipCodeRequired = "Contact address must have Zip Code.";
            public const string ContactAddressContactRequired = "Contact address must have a contact.";
        }
    }
}
