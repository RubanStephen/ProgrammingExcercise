//==============================================================================
// System  : ContactManager
// File    : UnityResolver.cs
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

using Unity;
using Unity.Lifetime;

namespace ContactManager.UnityResolverDI
{
    public static class UnityResolver
    {
        private static readonly UnityContainer UnityContainer = new UnityContainer();
        public static void Register<I, T>() where T : I
        {
            UnityContainer.RegisterType<I, T>(new ContainerControlledLifetimeManager());
        }
        public static T Resolve<T>()
        {
            return UnityContainer.Resolve<T>();
        }
    }
}
