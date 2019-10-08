using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Model
{
    public class Route
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(5)]
        public string Number { get; set; }
        [Required]
        public virtual List<Station> Stations { get; set; }
        [Required]
        public int FirstDTime { get; set; }
        [Required]
        public int LastDTime { get; set; }
        [Required]
        public int Interval { get; set; }
        [Required]
        public string TimeToNextStop { get; set; }  //i thought that it would be better to store intervals between stations 
                                                   //as strings and then desirialize them, when we need to do a calculation
                                                  //rather then to create a new class, which stores those intervals
        public Route(string number, List<Station> stations, int interval, int firstdtime, int lastdtime,string timetonexstop)
        {
            Number = number;
            FirstDTime = firstdtime;
            LastDTime = lastdtime;
            Interval = interval;
            TimeToNextStop = timetonexstop;
            Stations = stations;
        }
        public Route(int id ,string number, List<Station> stations, int interval, int firstdtime, int lastdtime, string timetonexstop)
        {
            //constructor for FileRepository
            Id = id;
            Number = number;
            FirstDTime = firstdtime;
            LastDTime = lastdtime;
            Interval = interval;
            TimeToNextStop = timetonexstop;
            Stations = stations;
        }

        public Route() {}

        private int MainCalculation(int FDP, int now)//just a method for not repeating same calculations in main algorithm
        {
            int a = 0;
            a = Interval - (now - FDP) % Interval;

            if (a >= Interval)
            {
                a -= Interval;
            }
            return a;
        }
        public int NearestBusTime(int now, int stationNumberinRoute)
        {
            int res = 0;
            int FDP = FirstDTime;
            int LDP = LastDTime;

            var times = JsonConvert.DeserializeObject<List<int>>(TimeToNextStop);//parsing string from DB

            if (stationNumberinRoute != 0)
            {
                for (int i = 0; i < stationNumberinRoute; i++)
                {
                    FDP += times[i];
                    LDP += times[i];
                }
            }//first and last departure times are now for ceraint station

            if (LastDTime < FirstDTime)
            {
                if (now < LDP)
                {
                    res = Interval - (now + 24 * 60 - FDP) % Interval;

                    if (res >= Interval)
                    {
                        res -= Interval;
                    }
                }
                else if (now >= FDP)
                {
                    res = MainCalculation(FDP, now);
                }
                else if (now > LDP && now < FDP)
                {
                    res = FDP - now;
                }
            }
            else
            {
                if (now < FDP)
                {
                    res = FDP - now;
                }
                else if (now >= FDP && now <= LDP)
                {
                    res = MainCalculation(FDP, now);
                }
                else if (now > LDP)
                {
                    res = 24 * 60 - now + FDP;
                }
            }
            return res;
        }

        public int NearestBusTimeBack(int now, int stationNumberinRoute)
        {
            int res = 0;
            int FDP = FirstDTime;
            int LDP = LastDTime;

            var times = JsonConvert.DeserializeObject<List<int>>(TimeToNextStop);//parsing string from DB

            times.Reverse();
            if (stationNumberinRoute != 0)
            {
                for (int i = 0; i < Stations.Count - stationNumberinRoute - 1; i++)
                {
                    FDP += times[i];
                    LDP += times[i];
                }
            }//first and last departure times are now for ceraint station
            if (LastDTime < FirstDTime)
            {
                if (now < LDP)
                {
                    res = Interval - (now + 24 * 60 - FDP) % Interval;

                    if (res >= Interval)
                    {
                        res -= Interval;
                    }
                }
                else if (now >= FDP)
                {
                    res = MainCalculation(FDP, now);
                }
                else if (now > LDP && now < FDP)
                {
                    res = FDP - now;
                }
            }
            else
            {
                if (now < FDP)
                {
                    res = FDP - now;
                }
                else if (now >= FDP && now <= LDP)
                {
                    res = MainCalculation(FDP, now);
                }
                else if (now > LDP)
                {
                    res = 24 * 60 - now + FDP;
                }
            }
            return res;
        }
    }
}
