using HW3_AL.Core.Interfaces;
using HW3_AL.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Repositories
{
    public class UserDbRepository : DbRepository<User>,IUserDbRepository
    {
        MyContex _context;
        public UserDbRepository(MyContex contex):base(contex)
        {
            _context = contex;
        }
        public override IEnumerable<User> GetAllItems()
        {
            return _context.Users.Include("FavouriteStations");
        }
        public void AddUserFavourite(User currentUser, Station selectedStation)
        {
            var newFavourite = new FavouriteStation(_context.Stations.FirstOrDefault(station => station.Name == selectedStation.Name), "");
            if (ActiveUser(currentUser).FavouriteStations == null)
            {
                ActiveUser(currentUser).FavouriteStations
                    = new List<FavouriteStation>() { newFavourite };
            }
            else
            {
                ActiveUser(currentUser).FavouriteStations.Add(newFavourite);
            }
            _context.SaveChanges();
        }
        public void DeleteUserFavourite(User currentUser, FavouriteStation chosenFavourite)
        {
            FavouriteStation favouriteToDelete = ActiveUser(currentUser).FavouriteStations
                                        .FirstOrDefault(fav => fav.Id == chosenFavourite.Id);
            ActiveUser(currentUser).FavouriteStations.Remove(favouriteToDelete);
            _context.FavouriteStations.Remove(favouriteToDelete);
            _context.SaveChanges();
        }
        public void EditUserFavourite(User currentUser, FavouriteStation chosenFavourite, string newDescription)
        {
            FavouriteStation favouriteToEdit = ActiveUser(currentUser).FavouriteStations
                                       .FirstOrDefault(fav => fav.Id == chosenFavourite.Id);
            favouriteToEdit.Description = newDescription;
            _context.SaveChanges();
        }
        private User ActiveUser(User currentUser)
        {
            return _context.Users.Include("FavouriteStations").FirstOrDefault(user => user.Email == currentUser.Email); ;
        }
    }
}
