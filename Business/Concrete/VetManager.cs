using Business.Abstract;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class VetManager : IVetService
    {
        private IVetDal _vetDal;

        public VetManager(IVetDal vetDal)
        {
            _vetDal = vetDal;
        }

        public IDataResult<List<VetWithDistanceDto>> GetVetsByDistance(LatLng coordinates)
        {
            var vets = _vetDal.GetVetsByDistance(coordinates);
            return new SuccessDataResult<List<VetWithDistanceDto>>(vets);
        }
    }
}
