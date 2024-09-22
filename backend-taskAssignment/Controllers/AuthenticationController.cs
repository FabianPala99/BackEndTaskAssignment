using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using LibraryConnection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace backend_taskAssignment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AuthenticationController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
                if (authenticationResponse == null)
                {
                    return StatusCode(401, new Response<dynamic>(false, HttpStatusCode.Unauthorized, Unauthorized()));
                }
                if (authenticationResponse.status == HttpStatusCode.OK)
                {
                    return Ok(authenticationResponse);
                }
                else
                {
                    return StatusCode((int)authenticationResponse.status, authenticationResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error Authenticate", ex.Message));
            }
        }

        [HttpGet("VerifyToken")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            string userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            Response<dynamic> oResponse = _jwtTokenHandler.VerifyToken(Convert.ToInt32(userIdClaim));

            return StatusCode((int)oResponse.status, oResponse);
        }
    }
}
