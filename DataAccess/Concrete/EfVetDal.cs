using Core.DataAccess.EntityFramework;
using Core.Utilities.Helpers;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfVetDal : EfEntityRepositoryBase<Vet, VetHandContext>, IVetDal
    {
        public List<VetWithDistanceDto> GetVetsByDistance(LatLng coordinates)
        {
            using(VetHandContext context = new VetHandContext())
            {
                var boundaries = DistanceCalculator.GetBoundaries(coordinates, 5);
                var vets = context.Vets.Where(x => x.Latitude >= boundaries.MinLatitude && x.Latitude <= boundaries.MaxLatitude)
                     .Where(x => x.Longitude >= boundaries.MinLongitude && x.Longitude <= boundaries.MaxLongitude).Select(i => new VetWithDistanceDto()
                     {
                         Vet=i,
                         Distance=DistanceCalculator.GetDistance(coordinates, new LatLng(i.Latitude,i.Longitude))
                     }).OrderByDescending(i=>i.Distance).ToList();
                return vets;
            }
        }
    }
}
