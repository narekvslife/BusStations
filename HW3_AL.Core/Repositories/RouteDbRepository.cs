using HW3_AL.Core.Interfaces;
using HW3_AL.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Repositories
{
    public class RouteDbRepository : DbRepository<Route>,IRepository<Route>
    {
        MyContex _context;
        public RouteDbRepository(MyContex context):base(context)
        {
            _context = context;
        }

        public override IEnumerable<Route> GetAllItems()
        {
            return _context.Routes.Include("Stations");
        }
    }
}
