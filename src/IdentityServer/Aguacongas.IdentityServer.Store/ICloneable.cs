﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2022 @Olivier Lefebvre
namespace Aguacongas.IdentityServer.Store
{
    /// <summary>
    /// Cloneable interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        T Clone();
    }
}
