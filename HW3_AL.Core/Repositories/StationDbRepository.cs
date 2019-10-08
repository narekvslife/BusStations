using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Repositories
{
    public class StationDbRepository : DbRepository<Station>,IRepository<Station>
    {
        MyContex _context;
        public StationDbRepository(MyContex context):base(context)
        {
            _context = context;
        }
        public override IEnumerable<Station> GetAllItems()
        {
            return _context.Stations.Include("Routes");
        }
    }
}
