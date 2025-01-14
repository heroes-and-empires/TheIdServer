﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2022 @Olivier Lefebvre
using Aguacongas.IdentityServer.Store;
using System.Collections.Generic;
using Entity = Aguacongas.IdentityServer.Store.Entity;

namespace Aguacongas.TheIdServer.BlazorApp.Models
{
    public class User : Entity.User, ICloneable<User>
    {
        public ICollection<Entity.UserClaim> Claims { get; set; }

        public ICollection<Entity.UserConsent> Consents { get; set; }

        public ICollection<Entity.UserLogin> Logins { get; set; }

        public ICollection<Entity.Role> Roles { get; set; }

        public ICollection<Entity.UserToken> Tokens { get; set; }
        public ICollection<Entity.ReferenceToken> ReferenceTokens { get; set; }
        public ICollection<Entity.RefreshToken> RefreshTokens { get; set; }

        public ICollection<Entity.BackChannelAuthenticationRequest> BackChannelAuthenticationRequests { get; set; }

        public ICollection<Entity.UserSession> Sessions { get; set; }

        public new User Clone()
        {
            return MemberwiseClone() as User;
        }

        public static User FromEntity(Entity.User user)
        {
            return new User
            {
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Id = user.Id,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                ConcurrencyStamp = user.ConcurrencyStamp,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                SecurityStamp = user.SecurityStamp,
                Claims = user.UserClaims,
                UserRoles = user.UserRoles
            };
        }
    }
}
