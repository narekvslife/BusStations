using HW3_AL.Core.Logic;
using HW3_AL.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core
{
    public static class Authentication
    {
        public static User AuthCheck(string email, string password)
        {
            using (UnitOfWork UOW = new UnitOfWork())
            {
                if (UOW.Users.GetAllItems().Any(user => user.Email == email && user.Password == Hash.GetHash(password)))
                {
                    var chosenUser = UOW.Users.GetAllItems().FirstOrDefault(x => x.Email == email && x.Password == Hash.GetHash(password));
                    return chosenUser;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
