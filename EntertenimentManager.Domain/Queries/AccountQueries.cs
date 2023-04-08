﻿using EntertenimentManager.Domain.Entities.Users;
using System;
using System.Linq.Expressions;

namespace EntertenimentManager.Domain.Queries
{
    public static class AccountQueries
    {
        public static Expression<Func<User, bool>> GetByEmail(string email)
        {
            return x => x.Email == email;
        }
    }
}
