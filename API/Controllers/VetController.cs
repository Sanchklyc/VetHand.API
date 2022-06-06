using Business.Abstract;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetController : ControllerBase
    {
        private IVetService _vetService;

        public VetController(IVetService vetService)
        {
            _vetService = vetService;
        }

        [HttpGet("get-by-distance")]
        public IActionResult GetByDistance(double latitude, double longitude)
        {
            var result = _vetService.GetVetsByDistance(new LatLng(latitude,longitude));
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
