using HW3_AL.Core.Model;
using HW3_AL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Interfaces
{
    public interface IUserDbRepository : IRepository<User>
    {
        void AddUserFavourite(User currentUser, Station selectedStation);
        void DeleteUserFavourite(User currentUser, FavouriteStation chosenFavourite);
        void EditUserFavourite(User currentUser, FavouriteStation chosenFavourite, string newDescription);
    }
}
