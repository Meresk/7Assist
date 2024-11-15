﻿using _7Assist.Data;
using _7Assist.Models;

namespace _7Assist.Services
{
    public class UserService(AppDbContext context)
    {
        private readonly AppDbContext _dbContext = context;

        public User? UserVerify(User user)
        {
            var userExist = _dbContext.Users.FirstOrDefault(u => u.Login == user.Login);

            if (userExist != null && BCrypt.Net.BCrypt.Verify(user.Password, userExist.Password))
            {
                return userExist;
            }

            return null;
        }
    }
}