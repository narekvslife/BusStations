using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public List<FavouriteStation> FavouriteStations { get; set; }
        public User(string name,string surname,string email,string password,List<FavouriteStation> fav)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            FavouriteStations = fav;
        }
        public User(int id,string name, string surname, string email, string password, List<FavouriteStation> fav)
        {
            //constructor for fileRepository
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            FavouriteStations = fav;
        }
        public User(){}
    }
}
