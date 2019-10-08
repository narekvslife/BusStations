using HW3_AL.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.ViewModel
{
    public static class Schedule
    {
        public static List<ScheduleItem> GetSchedule(Station selectedStation, int now)
        {

            List<Route> ScheduleRoutes = new List<Route>();
            if (selectedStation != null)
                using (UnitOfWork UOW = new UnitOfWork())
                {
                    ScheduleRoutes = (from r in UOW.Routes.GetAllItems()
                                      where r.Stations.Any(x => x.Name == selectedStation.Name)
                                      select r).ToList();
                }
            else
                return new List<ScheduleItem>();

            List<ScheduleItem> ScheduleItems = new List<ScheduleItem>();

            string direction = "";

            using (UnitOfWork UOW = new UnitOfWork())
            {
                for (int i = 0; i < ScheduleRoutes.Count; i++)
                {
                    var Station = ScheduleRoutes[i].Stations.FirstOrDefault(x=>x.Name==selectedStation.Name);
                    int indexOfStation= ScheduleRoutes[i].Stations.IndexOf(Station);

                    if (selectedStation != ScheduleRoutes[i].Stations.Last()
                        && selectedStation != ScheduleRoutes[i].Stations.First())
                    {
                        direction = UOW.Stations.GetAllItems().FirstOrDefault(x => x.Name == ScheduleRoutes[i].Stations.Last().Name).Name;
                        ScheduleItems.Add(new ScheduleItem(ScheduleRoutes[i].Number, direction, ScheduleRoutes[i].NearestBusTime(now, indexOfStation)));

                        direction = UOW.Stations.GetAllItems().FirstOrDefault(x => x.Name == ScheduleRoutes[i].Stations.First().Name).Name;
                        ScheduleItems.Add(new ScheduleItem(ScheduleRoutes[i].Number, direction, ScheduleRoutes[i].NearestBusTimeBack(now, indexOfStation)));
                    }
                    else if (selectedStation == ScheduleRoutes[i].Stations.Last())
                    {
                        direction = UOW.Stations.GetAllItems().FirstOrDefault(x => x.Name == ScheduleRoutes[i].Stations.First().Name).Name;
                        ScheduleItems.Add(new ScheduleItem(ScheduleRoutes[i].Number, direction, ScheduleRoutes[i].NearestBusTimeBack(now, indexOfStation)));
                    }
                    else if (selectedStation == ScheduleRoutes[i].Stations.First())
                    {
                        direction = UOW.Stations.GetAllItems().FirstOrDefault(x => x.Name == ScheduleRoutes[i].Stations.Last().Name).Name;
                        ScheduleItems.Add(new ScheduleItem(ScheduleRoutes[i].Number, direction, ScheduleRoutes[i].NearestBusTime(now, indexOfStation)));
                    }
                }
            }
            return ScheduleItems;
        }
    }
}
