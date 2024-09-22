using LibraryConnection.Context;
using LibraryConnection.DbSet;
using LibraryConnection.Models;
using System.Net;

namespace LibraryConnection.AzureControllers
{
    public class RolController
    {
        public static Response<List<Rol>> GetDbRoles()
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    return new Response<List<Rol>>(true, HttpStatusCode.OK, contexto.Roles.ToList());
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<List<Rol>>(false, HttpStatusCode.InternalServerError, "Error GetDbRoles", errorMessage);
            }
        }

        public static Response<Rol> GetRolById(int idRol)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    Rol? oRol = contexto.Roles.FirstOrDefault(r => r.Id == idRol);
                    if (oRol != null)
                    {
                        return new Response<Rol>(true, HttpStatusCode.OK, oRol);
                    }
                    else
                    {
                        return new Response<Rol>(false, HttpStatusCode.NotFound, "No Rol found with the ID provided.");
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<Rol>(false, HttpStatusCode.InternalServerError, "Error GetRolById", errorMessage);
            }
        }
    }
}
