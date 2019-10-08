namespace HW3_AL.Core.Migrations
{
    using HW3_AL.Core.Model;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<HW3_AL.Core.MyContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HW3_AL.Core.MyContex context)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Route[] JsonRoutes;
            string resourceName = "HW3_AL.Core.Files.Routes.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string FileText = reader.ReadToEnd();
                    JsonRoutes = JsonConvert.DeserializeObject<List<Route>>(FileText).ToArray();
                }
            }
                
            foreach (var route in JsonRoutes)
            {
                Route routeForDb = new Route()
                {
                    Number = route.Number,
                    Interval = route.Interval,
                    FirstDTime = route.FirstDTime,
                    LastDTime = route.LastDTime,
                    TimeToNextStop = route.TimeToNextStop,
                    Stations = new List<Station>()
                };

                foreach (var station in route.Stations)
                {
                    if (context.Stations.ToList().Exists(st => st.Name == station.Name))
                    {
                        routeForDb.Stations.Add(context.Stations.FirstOrDefault(st => st.Name == station.Name));
                    }
                    else
                    {
                        routeForDb.Stations.Add(station);
                    }
                }
                context.Routes.AddOrUpdate(n => n.Number, routeForDb);
                context.SaveChanges();
            }
        }
    }
}

