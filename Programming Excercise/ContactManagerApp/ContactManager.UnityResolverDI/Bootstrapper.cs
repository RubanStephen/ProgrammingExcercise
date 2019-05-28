//==============================================================================
// System  : ContactManager
// File    : Bootstrapper.cs
// Author  : Ruban Stephen
// Created : 05/27/2019
// Note    : Copyright 2019, OFS., All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that initiates objects using dependency injection.
//
// Version     Date      Who        Comments
// =============================================================================
// 1.0.0.0  05/27/2019  RStephen   Created the code
//==============================================================================

using ContactManager.Business;
using ContactManager.DAL;
using ContactManager.Interface;
using ContactManager.UnitTests.DAL;

namespace ContactManager.UnityResolverDI
{
    public static class Bootstrapper
    {
        public static ContactBusiness Init(bool isTest = false)
        {
            UnityResolver.Register<IContactBusiness, ContactBusiness>();
            UnityResolver.Register<IContactValidation, ContactValidation>();

            if (isTest)
            {
                UnityResolver.Register<IContactDAL, ContactDALTest>();
            }
            else
            {
                UnityResolver.Register<IContactDAL, ContactDAL>();
            }

            return UnityResolver.Resolve<ContactBusiness>();
        }

    }
}
