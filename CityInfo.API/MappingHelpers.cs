using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API
{
    public static class MappingHelpers
    {
        public static Models.PointsOFInterestDTO ToDto(this Entities.PointsOfInterest p)
        {
            return new Models.PointsOFInterestDTO
            {
                id = p.id,
                Name = p.Name,
                Description = p.GetType().GetProperty("Description")?.GetValue(p) as string
            };
        }

        public static Models.CityDTO ToDto(this Entities.City c)
        {
            return new Models.CityDTO
            {
                id = c.id,
                Name = c.Name,
                Description = c.Description,
                PointOfInterest = c.PointOfInterest?.Select(p => p.ToDto()).ToList() ?? new List<Models.PointsOFInterestDTO>()
            };
        }

        public static IEnumerable<Models.CityDTO> ToDto(this IEnumerable<Entities.City> cities)
        {
            return cities.Select(c => c.ToDto());
        }
    }
}
