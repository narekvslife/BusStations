using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.ViewModel
{
    public class ScheduleItem
    {
        public int TimeToChosenStation { get; set; }
        public string Direction { get; set; }
        public string ChosenRouteNumber { get; set; }
        public ScheduleItem(string chosenRouteNumber, string direction, int timeToChoseStation)
        {
            TimeToChosenStation = timeToChoseStation;
            Direction = direction;
            ChosenRouteNumber = chosenRouteNumber;
        }
    }
}
