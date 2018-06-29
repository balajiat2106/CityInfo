using System.Collections.Generic;
using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepo
    {
        City GetCity(int id);
        IEnumerable<City> GetCities();

        IEnumerable<PointsOfInterest> GetPointsOfInterest(int cityId);

        PointsOfInterest GetPointsOfInterest(int cityId, int PointsOfInterestId);
    }
}