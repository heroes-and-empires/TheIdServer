﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2022 @Olivier Lefebvre
using System;
using System.ComponentModel.DataAnnotations;

namespace Aguacongas.IdentityServer.Store.Entity
{
    /// <summary>
    /// Client allowed identity token signing algorithm
    /// </summary>
    public class ClientAllowedIdentityTokenSigningAlgorithm : IAuditable, IClientSubEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the algorithm name
        /// </summary>
        [Required]
        public string Algorithm { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the modified at.
        /// </summary>
        /// <value>
        /// The modified at.
        /// </value>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public virtual Client Client { get; set; }
    }
}