using JwtApiTutorial.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtApiTutorial.Library
{
    public class UserRepository
    {
        public IConfiguration _config { get; set; }

        /// <summary>
        /// Class'ın oluşturulması.
        /// </summary>
        /// <param name="_config"></param>
        /// <returns></returns>
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        public User Get(string username, string password)
        {
            var users = new List<User>();

            users.Add(new User
            {
                Id = Convert.ToInt32(_config["User:Id"]),
                Username = _config["User:Username"],
                Password = _config["User:Password"],
                Role = _config["User:Role"]
            });

            return users.Where(x => x.Username.ToLower() == username.ToLower()
                            && x.Password == password).FirstOrDefault();
        }
    }
}
