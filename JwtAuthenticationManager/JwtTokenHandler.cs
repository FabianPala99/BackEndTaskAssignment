using JwtAuthenticationManager.Models;
using LibraryConnection.AzureControllers;
using LibraryConnection.DbSet;
using LibraryConnection.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "d094439dd5bd4a721793cd2ecccdce629cbc1ca7b7a5db669d393905ce86c473";
        private const int JWT_TOKEN_VALIDITY_MINS = 150;

        /// <summary>
        /// Generación de Token validando existencia del Usuario y Rol.
        /// Nota : Se usa para la autenticación de los usuarios.
        /// </summary>
        /// <param name="authenticationRequest">Autenticación (Username, password)</param>
        /// <returns>Token Generado</returns>
        public Response<AuthenticationResponse> GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            try
            {
                Response<User> oUserResponse = UserController.GetUserAuthentication(authenticationRequest.Email, authenticationRequest.Password);
                if (oUserResponse.status == HttpStatusCode.OK)
                {
                    User oUser = oUserResponse.response!;
                    Rol oRol = RolController.GetRolById(oUser.RolId).response!;

                    var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
                    var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

                    var claimsIdentity = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Name, oUser.FirstName+" "+oUser.LastName),
                        new Claim("id", oUser.Id.ToString())

                    });

                    var signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature);

                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claimsIdentity,
                        NotBefore = tokenExpiryTimeStamp.AddMinutes(-JWT_TOKEN_VALIDITY_MINS),
                        Expires = tokenExpiryTimeStamp,
                        SigningCredentials = signingCredentials
                    };

                    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                    var token = jwtSecurityTokenHandler.WriteToken(securityToken);


                    return new Response<AuthenticationResponse>(true, HttpStatusCode.OK, new AuthenticationResponse
                    {
                        UserId = oUser.Id,
                        Name = oUser.FirstName,
                        RoleName = oRol.Name,                        
                        ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                        JwtToken = token
                    });
                }
                else
                {
                    return new Response<AuthenticationResponse>(false, HttpStatusCode.Unauthorized, "Incorrect email or password. ");
                }
            }
            catch (Exception e)
            {
                return new Response<AuthenticationResponse>(false, HttpStatusCode.InternalServerError, "Error GenerateJwtToken", e.Message);
            }
        }


        public Response<dynamic> VerifyToken(int idUser)
        {
            try
            {
                Response<User> oUserResponse = UserController.GetUserById(idUser);
                if (oUserResponse.status == HttpStatusCode.OK)
                {
                    User oUser = oUserResponse.response!;
                    Rol oRol = RolController.GetRolById(oUser.RolId).response!;                                       

                    return new Response<dynamic>(true, HttpStatusCode.OK, new 
                    {
                        UserId = oUser.Id,
                        Name = oUser.FirstName,
                        RoleName = oRol.Name
                    });
                }
                else
                {
                    return new Response<dynamic>(false, HttpStatusCode.NotFound, oUserResponse);
                }
            }
            catch (Exception e)
            {
                return new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error VerifyToken", e.Message);
            }
        }


        /// <summary>
        /// Generación de Token por Id del Usuario.
        /// Nota: Se usa para la autenticación de los usuarios.
        /// </summary>
        /// <param name="idUser">Id de Usuario autenticado</param>
        /// <returns>Token Generado</returns>
        public Response<AuthenticationResponse> GenerateJwtTokenByIdUser(int idUser)
        {
            try
            {
                Response<User> oUserResponse = UserController.GetUserById(idUser);
                if (oUserResponse.status == HttpStatusCode.OK)
                {
                    User oUser = oUserResponse.response!;
                    Rol oRol = RolController.GetRolById(oUser.RolId).response!;

                    var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
                    var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

                    var claimsIdentity = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Name, oUser.FirstName+" "+oUser.LastName),
                        new Claim("id", oUser.Id.ToString())

                    });

                    var signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature);

                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claimsIdentity,
                        NotBefore = tokenExpiryTimeStamp.AddMinutes(-JWT_TOKEN_VALIDITY_MINS),
                        Expires = tokenExpiryTimeStamp,
                        SigningCredentials = signingCredentials
                    };

                    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                    var token = jwtSecurityTokenHandler.WriteToken(securityToken);


                    return new Response<AuthenticationResponse>(true, HttpStatusCode.OK, new AuthenticationResponse
                    {
                        UserId = oUser.Id,
                        RoleName = oRol.Name,
                        ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                        JwtToken = token
                    });
                }
                else
                {
                    return new Response<AuthenticationResponse>(false, HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return new Response<AuthenticationResponse>(false, HttpStatusCode.InternalServerError, "Error GenerateJwtTokenByIdUser", e.Message);
            }
        }
    }
}
