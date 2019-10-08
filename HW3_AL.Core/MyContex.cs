using HW3_AL.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core
{
    public class MyContex : DbContext
    {
        public DbSet<Station> Stations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavouriteStation> FavouriteStations { get; set; }
        public MyContex() : base("BusScheduleDb")
        {

        }
    }
}
