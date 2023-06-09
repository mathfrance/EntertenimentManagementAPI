﻿using EntertenimentManager.Domain.Entities.Users;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EntertenimentManager.API.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new (ClaimTypes.Name, user.Email),
                new (ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            result.AddRange(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name))
                );

            return result;
        }
    }
}
