using LibraryConnection.Context;
using LibraryConnection.DbSet;
using LibraryConnection.Encrypted;
using LibraryConnection.Models;
using Task = LibraryConnection.DbSet.Task;
using System.Net;

namespace LibraryConnection.AzureControllers
{
    public class UserController
    {
        public static Response<List<User>> GetDbUsers()
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    return new Response<List<User>>(true, HttpStatusCode.OK, contexto.Users.ToList());
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<List<User>>(false, HttpStatusCode.InternalServerError, "Error GetDbUsers", errorMessage);
            }
        }

        public static Response<List<User>> GetDbSupervisoryUsers()
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    List<User> oUser = contexto.Users.Where(s => s.RolId == 2).ToList();

                    return new Response<List<User>>(true, HttpStatusCode.OK, oUser);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<List<User>>(false, HttpStatusCode.InternalServerError, "Error GetDbSupervisoryUsers", errorMessage);
            }
        }

        public static Response<List<User>> GetDbEmployedUsers()
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    List<User> oUser = contexto.Users.Where(s => s.RolId == 3).ToList();

                    return new Response<List<User>>(true, HttpStatusCode.OK, oUser);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<List<User>>(false, HttpStatusCode.InternalServerError, "Error GetDbEmployedUsers", errorMessage);
            }
        }

        public static Response<User> GetUserById(int idUser)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    User? oUser = contexto.Users.FirstOrDefault(r => r.Id == idUser);
                    if (oUser != null)
                    {
                        return new Response<User>(true, HttpStatusCode.OK, oUser);
                    }
                    else
                    {
                        return new Response<User>(false, HttpStatusCode.NotFound, "User with the given ID was not found.");
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

                return new Response<User>(false, HttpStatusCode.InternalServerError, "Error GetUserById", errorMessage);
            }
        }

        public static Response<User> GetUserAuthentication(string email, string password)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    User? oUser = contexto.Users.FirstOrDefault(s => s.Email == email && s.Password == Encrypt.EncryptAES(password));
                    if (oUser != null)
                    {
                        return new Response<User>(true, HttpStatusCode.OK, oUser);
                    }
                    else
                    {
                        return new Response<User>(false, HttpStatusCode.NotFound, "User with the given ID was not found.");
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

                return new Response<User>(false, HttpStatusCode.InternalServerError, "Error GetUserAuthentication", errorMessage);
            }
        }

        public static Response<dynamic> PostDbUser(User oUser)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    bool docUsuarioExiste = contexto.Users.Any(u => u.Email == oUser.Email);

                    if (docUsuarioExiste)
                    {
                        return new Response<dynamic>(false, HttpStatusCode.BadRequest, "The mail already exists for another user.");
                    }

                    oUser.Password = Encrypt.EncryptAES(oUser.Password);

                    contexto.Users.Add(oUser);
                    contexto.SaveChanges();
                }
                return new Response<dynamic>(true, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error PostDbUser", errorMessage);
            }
        }

        public static Response<dynamic> PatchDbUser(User oUser)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    User? existingUser = contexto.Users.FirstOrDefault(s => s.Id == oUser.Id);

                    if (existingUser != null)
                    {
                        bool mailUserExists = contexto.Users.Any(s => s.Id != oUser.Id && s.Email == oUser.Email);

                        if (mailUserExists)
                        {
                            return new Response<dynamic>(false, HttpStatusCode.BadRequest, "The mail already exists for another user.");
                        }

                        existingUser.FirstName = oUser.FirstName.Trim();
                        existingUser.LastName = oUser.LastName.Trim();
                        existingUser.Email = oUser.Email.Trim();
                        existingUser.Password = Encrypt.EncryptAES(oUser.Password.Trim());
                        existingUser.RolId = oUser.RolId;

                        contexto.SaveChanges();

                        return new Response<dynamic>(true, HttpStatusCode.OK);
                    }
                    else
                    {
                        return new Response<dynamic>(false, HttpStatusCode.NotFound, "User with the given ID was not found.");
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

                return new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error PatchDbUser", errorMessage);
            }
        }

        public static Response<dynamic> DeleteUserById(int idUser)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    bool oTaskEmployed = contexto.Tasks.Any(t => t.AssignedToUserId == idUser);
                    if (oTaskEmployed)
                    {
                        return new Response<dynamic>(false, HttpStatusCode.BadRequest, "The employee cannot be deleted because they have assigned tasks.");
                    }

                    bool oTaskSupervisory = contexto.Tasks.Any(t => t.AssignedByUserId == idUser);
                    if (oTaskSupervisory)
                    {
                        return new Response<dynamic>(false, HttpStatusCode.BadRequest, "The supervisor cannot be deleted because they are overseeing tasks.");
                    }


                    User? userToDelete = contexto.Users.FirstOrDefault(r => r.Id == idUser);
                    if (userToDelete != null)
                    {
                        contexto.Users.Remove(userToDelete);
                        contexto.SaveChanges();

                        return new Response<dynamic>(true, HttpStatusCode.OK);
                    }
                    else
                    {
                        return new Response<dynamic>(false, HttpStatusCode.NotFound, "User with the given ID was not found.");
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

                return new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error DeleteUserById", errorMessage);
            }
        }
    }
}
