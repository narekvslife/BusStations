using HW3_AL.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core
{
    public class Station
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [JsonIgnore]
        public virtual List<Route> Routes { get; set; }
        public Station(string name)
        {
            Name = name;
        }
        public Station(int id,string name)
        {
            //constructor for fileRepository
            Id = id;
            Name = name;
        }
        public Station(){}

    }
}
