using HW3_AL.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW3_AL.Core.Logic
{
    public static class Registration
    {
        public static bool NewUser(string name, string surname, string email, string password)
        {
            using (var UOW = new UnitOfWork())
            {
                if (string.IsNullOrWhiteSpace(name)
               || string.IsNullOrWhiteSpace(surname)
               || string.IsNullOrWhiteSpace(email)
               || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("All the fields must be filled");
                    return false;
                }
                else if (UOW.Users.GetAllItems().Any(user => user.Email == email))
                {
                    MessageBox.Show("Account with such an email already exists");
                    return false;
                }
                else
                {
                    var newUser = new User(name, surname, email, Hash.GetHash(password), new List<FavouriteStation>());
                    UOW.Users.Add(newUser);
                    UOW.Complete();
                    MessageBox.Show("Your account was succesfully registered");
                    return true;
                }
            }
        }
    }
}
