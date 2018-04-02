using System.Collections.Generic;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CityDataStore
    {
        public static CityDataStore Current { get; } = new CityDataStore();
        public List<CityDTO> Cities { get; set; }
        public CityDataStore() => Cities = new List<CityDTO>()
        {
            new CityDTO()
            {
                id = 1,
                Description = "dsadsad",
                Name = "Mysore",
                PointOfInterest = new List<PointsOFInterestDTO>()
                {
                    new PointsOFInterestDTO(){ id = 1, Name = "Palace", Description = "dasd" },
                    new PointsOFInterestDTO(){ id = 2, Name = "Dam", Description = "dasd" }
                }
            },
            new CityDTO()
            {
                id=2,
                Description = "dsadsad",
                Name = "Bangalore",
                PointOfInterest = new List<PointsOFInterestDTO>()
                {
                    new PointsOFInterestDTO(){ id = 1, Name = "Lal bagh", Description = "dasd" },
                    new PointsOFInterestDTO(){ id = 2, Name = "Snow city", Description = "dasd" }
                }
            }
        };
    }
}
