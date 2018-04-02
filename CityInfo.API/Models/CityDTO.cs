using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NoOfPointsOfInterest => PointOfInterest.Count;
        public ICollection<PointsOFInterestDTO> PointOfInterest { get; set; } = new List<PointsOFInterestDTO>();
    }
}
