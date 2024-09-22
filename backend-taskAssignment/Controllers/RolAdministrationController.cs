using LibraryConnection.AzureControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_taskAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RolAdministrationController : ControllerBase
    {

        [HttpGet("Roles")]
        public IActionResult GetUsers()
        {
            var oResponse = RolController.GetDbRoles();
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpGet("RolById")]
        public IActionResult GetUsuarioById(int idUser)
        {
            var oResponse = RolController.GetRolById(idUser);
            return StatusCode((int)oResponse.status, oResponse);
        }

    }
}
