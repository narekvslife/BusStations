using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Model
{
    public class FavouriteStation
    {
        public int Id { get; set; }
        [Required]
        public Station Station { get; set; }
        [MaxLength(15)]
        public string Description { get; set; }
        public FavouriteStation(Station station,string description)
        {
            Station = station;
            Description = description;
        }
        public FavouriteStation(){}
    }
}
