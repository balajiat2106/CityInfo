using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public class CityInfoRepo : ICityInfoRepo
    {
        private CityContext _CityContext;
        public CityInfoRepo(CityContext cityContext)
        {
            _CityContext=cityContext;
        }
        public IEnumerable<City> GetCities()
        {
            return _CityContext.Cities.ToList();
        }

        public City GetCity(int id)
        {
            return _CityContext.Cities.Where(c=>c.id==id).FirstOrDefault();
        }

        public IEnumerable<PointsOfInterest> GetPointsOfInterest(int cityId)
        {
            return _CityContext.PointsOfInterest.Where(c=>c.id==cityId).ToList();
        }

        public PointsOfInterest GetPointsOfInterest(int cityId, int PointsOfInterestId)
        {
            return _CityContext.PointsOfInterest.Where(p=>p.id==PointsOfInterestId && p.CityId==cityId).FirstOrDefault();
        }
    }
}