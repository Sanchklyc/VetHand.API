using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IVetService
    {
        IDataResult<List<VetWithDistanceDto>> GetVetsByDistance(LatLng coordinates);
    }
}
