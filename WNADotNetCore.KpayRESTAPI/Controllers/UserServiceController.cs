using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.MiniKpay.Database.Models;
using WNADotNetCore.MiniKpay.Domain.Features.User;

namespace WNADotNetCore.MiniKpay.API.Controllers
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
            var lst = _service.GetUser(id);
            if (lst is null)
            {
                return NotFound("User not found");
            }
            return Ok(lst);
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
            var isUserExist = _service.IsValidUser(id);
            if (!isUserExist)
            {
                return BadRequest("Invalid User");
            }
            var result = _service.UpdateUserPin(id, pin);
            return Ok("Pin is updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var isUserExist = _service.IsValidUser(id);
            if (!isUserExist)
            {
                return BadRequest("Invalid User");
            }
            _service.DeactivateUser(id);
            return Ok("Deactivated successfully");
        }
    }
}
