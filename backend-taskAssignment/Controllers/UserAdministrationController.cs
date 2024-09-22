using LibraryConnection.AzureControllers;
using LibraryConnection.DbSet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_taskAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserAdministrationController : ControllerBase
    {
        [HttpGet("Users")]        
        public IActionResult GetUsers()
        {
            var oResponse = UserController.GetDbUsers();
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpGet("SupervisoryUsers")]
        public IActionResult GetSupervisoryUsers()
        {
            var oResponse = UserController.GetDbSupervisoryUsers();
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpGet("EmployedUsers")]
        public IActionResult GetEmployedUsers()
        {
            var oResponse = UserController.GetDbEmployedUsers();
            return StatusCode((int)oResponse.status, oResponse);
        }


        [HttpGet("UserById")]
        public IActionResult GetUsuarioById(int idUser)
        {
            var oResponse = UserController.GetUserById(idUser);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpPost("User")]
        public IActionResult PostUsuario([FromBody] User oUser)
        {
            var oResponse = UserController.PostDbUser(oUser);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpPatch("User")]
        public IActionResult PutUsuario([FromBody] User oUser)
        {
            var oResponse = UserController.PatchDbUser(oUser);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpDelete("User")]
        public IActionResult DeleteUsuarioById(int idUser)
        {
            var oResponse = UserController.DeleteUserById(idUser);
            return StatusCode((int)oResponse.status, oResponse);
        }
    }
}
