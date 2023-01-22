using Microsoft.AspNetCore.Mvc;
using EF_1.Models;
using EF_1.Models.DTO;
using EF_1.Services;
using System.Linq;

namespace EF_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly IDbService _DbService;

        public TripController(IDbService tripDbService)
        {
            _DbService = tripDbService;
        }

        [HttpGet]
        public IActionResult TripList()
        {
            return Ok(_DbService.GetTripList());
        }
        [HttpDelete]
        public IActionResult DeleteClient(DeleteRequest request)
        {
            _DbService.DeleteClient(request);
            return Ok("Client with id" + request.id + " has been deleted" );
        }
        [HttpPost]
        public IActionResult InsertStudent(InsertRequest request)
        {
            try
            {
                var client = _DbService.InsertClient(request);
                return Created("Client data has been inserted into the database", client);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
