using Core.DataAccess;
using Core.Utilities.Helpers;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IVetDal : IEntityRepository<Vet>
    {
        List<VetWithDistanceDto> GetVetsByDistance(LatLng coordinates);
    }
}
