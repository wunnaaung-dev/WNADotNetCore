using DotNetTrainingBatch5.MiniKpayRest_API.Features.User;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly UserService _service;

        public UserServiceController()
        {
            _service = new UserService();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var lst = _service.GetUsers();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _service.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(TblUser user)
        {
            var result = _service.CreateUser(user);
            return Ok(result);
        }

        [HttpPatch("pin/{id}")]
        public IActionResult UpdateUserPin(int id, [FromBody] string pin)
        {
            var updatedUser = _service.UpdateUserPin(id, pin);
            return Ok(updatedUser);
        }

        [HttpPatch("increase-balance/{id}")]
        public IActionResult IncreaseUserBalance(int id, [FromBody] decimal balance)
        {
            if (balance <= 0)
            {
                return BadRequest("Balance increase must be a positive value.");
            }

            var updatedUser = _service.IncreaseUserBalance(id, balance);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(updatedUser);
        }

        [HttpPatch("decrease-balance/{id}")]
        public IActionResult DecreaseUserBalance(int id, [FromBody] decimal balance)
        {
            if (balance <= 0)
            {
                return BadRequest("Balance decrease must be a positive value.");
            }

            var updatedUser = _service.DecreaseUserBalance(id, balance);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _service.DeleteUser(id);
            if (!result)
            {
                return BadRequest("Cannot delete user");
            }
            return Ok("Deleted successfully");
        }
    }
}
