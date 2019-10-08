using HW3_AL.Core.Interfaces;
using HW3_AL.Core.Model;
using HW3_AL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core
{
    public class UnitOfWork : IDisposable
    {
        MyContex _context = new MyContex();
        public IRepository<Station> Stations { get; }
        public IRepository<Route> Routes { get; }
        public IUserDbRepository Users { get; }
        public IRepository<FavouriteStation> Favourites { get; }
        public UnitOfWork()
        {
            Stations = new StationDbRepository(_context);
            Routes = new RouteDbRepository(_context);
            Favourites = new FavouritesDbRepository(_context);
            Users = new UserDbRepository(_context);
        }
        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
