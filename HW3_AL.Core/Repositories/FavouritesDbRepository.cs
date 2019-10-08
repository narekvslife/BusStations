using HW3_AL.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Repositories
{
    public class FavouritesDbRepository : DbRepository<FavouriteStation>,IRepository<FavouriteStation>
    {
        MyContex _context;
        public FavouritesDbRepository(MyContex context):base(context)
        {
            _context = context;
        }
        public override IEnumerable<FavouriteStation> GetAllItems()
        {
            return _context.FavouriteStations.Include("Station");
        }
    }
}
