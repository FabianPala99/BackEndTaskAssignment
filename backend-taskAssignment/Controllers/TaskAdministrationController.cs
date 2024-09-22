using LibraryConnection.AzureControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = LibraryConnection.DbSet.Task;

namespace backend_taskAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TaskAdministrationController : ControllerBase
    {
        [HttpGet("Tasks")]
        public IActionResult GetTasks()
        {
            var oResponse = TaskController.GetDbTasks();
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpGet("TasksBySupervisory")]
        public IActionResult GetTasksBySupervisory(int idUser)
        {
            var oResponse = TaskController.GetDbTasksBySupervisory(idUser);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpGet("TasksByEmployed")]
        public IActionResult GetTasksByEmployed(int idUser)
        {
            var oResponse = TaskController.GetDbTasksByEmployed(idUser);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpGet("TaskById")]
        public IActionResult GetUsuarioById(int idTask)
        {
            var oResponse = TaskController.GetTaskById(idTask);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpPost("Task")]
        public IActionResult PostUsuario([FromBody] Task oTask)
        {
            var oResponse = TaskController.PostDbTask(oTask);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpPatch("Task")]
        public IActionResult PutUsuario([FromBody] Task oTask)
        {
            var oResponse = TaskController.PatchDbTask(oTask);
            return StatusCode((int)oResponse.status, oResponse);
        }

        [HttpDelete("Task")]
        public IActionResult DeleteUsuarioById(int idTask)
        {
            var oResponse = TaskController.DeleteTaskById(idTask);
            return StatusCode((int)oResponse.status, oResponse);
        }
    }
}
