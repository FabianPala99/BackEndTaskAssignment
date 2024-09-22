using LibraryConnection.Context;
using LibraryConnection.Encrypted;
using LibraryConnection.Models;
using System.Net;
using Task = LibraryConnection.DbSet.Task;

namespace LibraryConnection.AzureControllers
{
    public class TaskController
    {
        public static Response<List<dynamic>> GetDbTasks()
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                   List<dynamic> oTasks = (from t in contexto.Tasks
                                 join s in contexto.Users on t.AssignedToUserId equals s.Id
                                 join u in contexto.Users on t.AssignedByUserId equals u.Id
                                 select new
                                            {
                                                id = t.Id,
                                                name = t.Name,
                                                description = t.Description,
                                                status = t.Status,
                                                assignedToUserId = t.AssignedToUserId,
                                                assignedToUserName = s.FirstName+" "+s.LastName,
                                                assignedByUserId = t.AssignedByUserId,
                                                assignedByUserName = u.FirstName + " " + u.LastName,
                                 }).ToList<dynamic>();

                    return new Response<List<dynamic>>(true, HttpStatusCode.OK, oTasks);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<List<dynamic>>(false, HttpStatusCode.InternalServerError, "Error GetDbTasks", errorMessage);
            }
        }

        public static Response<List<dynamic>> GetDbTasksBySupervisory(int id)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    List<dynamic> oTasks = (from t in contexto.Tasks
                                            join s in contexto.Users on t.AssignedToUserId equals s.Id
                                            join u in contexto.Users on t.AssignedByUserId equals u.Id
                                            where u.Id == id
                                            select new
                                            {
                                                id = t.Id,
                                                name = t.Name,
                                                description = t.Description,
                                                status = t.Status,
                                                assignedToUserId = t.AssignedToUserId,
                                                assignedToUserName = s.FirstName + " " + s.LastName,
                                                assignedByUserId = t.AssignedByUserId,
                                                assignedByUserName = u.FirstName + " " + u.LastName,
                                            }).ToList<dynamic>();

                    return new Response<List<dynamic>>(true, HttpStatusCode.OK, oTasks);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<List<dynamic>>(false, HttpStatusCode.InternalServerError, "Error GetDbTasks", errorMessage);
            }
        }

        public static Response<List<dynamic>> GetDbTasksByEmployed(int id)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    List<dynamic> oTasks = (from t in contexto.Tasks
                                            join s in contexto.Users on t.AssignedToUserId equals s.Id
                                            join u in contexto.Users on t.AssignedByUserId equals u.Id
                                            where s.Id == id
                                            select new
                                            {
                                                id = t.Id,
                                                name = t.Name,
                                                description = t.Description,
                                                status = t.Status,
                                                assignedToUserId = t.AssignedToUserId,
                                                assignedToUserName = s.FirstName + " " + s.LastName,
                                                assignedByUserId = t.AssignedByUserId,
                                                assignedByUserName = u.FirstName + " " + u.LastName,
                                            }).ToList<dynamic>();

                    return new Response<List<dynamic>>(true, HttpStatusCode.OK, oTasks);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " Inner exception: " + ex.InnerException.Message;
                }

                return new Response<List<dynamic>>(false, HttpStatusCode.InternalServerError, "Error GetDbTasks", errorMessage);
            }
        }


        public static Response<Task> GetTaskById(int idTask)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    Task? oTask = contexto.Tasks.FirstOrDefault(r => r.Id == idTask);
                    if (oTask != null)
                    {
                        return new Response<Task>(true, HttpStatusCode.OK, oTask);
                    }
                    else
                    {
                        return new Response<Task>(false, HttpStatusCode.NotFound, "Task with the given ID was not found.");
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

                return new Response<Task>(false, HttpStatusCode.InternalServerError, "Error GetTaskById", errorMessage);
            }
        }


        public static Response<dynamic> PostDbTask(Task oTask)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    contexto.Tasks.Add(oTask);
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

                return new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error PostDbTask", errorMessage);
            }
        }

        public static Response<dynamic> PatchDbTask(Task oTask)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    Task? existingTask = contexto.Tasks.FirstOrDefault(s => s.Id == oTask.Id);

                    if (existingTask != null)
                    {
                        existingTask.Name = oTask.Name.Trim();
                        existingTask.Description = oTask.Description.Trim();
                        existingTask.Status = oTask.Status.Trim();
                        existingTask.AssignedByUserId = oTask.AssignedToUserId;
                        existingTask.AssignedByUserId = oTask.AssignedByUserId;

                        contexto.SaveChanges();

                        return new Response<dynamic>(true, HttpStatusCode.OK);
                    }
                    else
                    {
                        return new Response<dynamic>(false, HttpStatusCode.NotFound, "Task with the given ID was not found.");
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

                return new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error PatchDbTask", errorMessage);
            }
        }

        public static Response<dynamic> DeleteTaskById(int idTask)
        {
            try
            {
                using (var contexto = new ApplicationDbContext())
                {
                    Task? TaskToDelete = contexto.Tasks.FirstOrDefault(r => r.Id == idTask);
                    if (TaskToDelete != null)
                    {
                        contexto.Tasks.Remove(TaskToDelete);
                        contexto.SaveChanges();

                        return new Response<dynamic>(true, HttpStatusCode.OK);
                    }
                    else
                    {
                        return new Response<dynamic>(false, HttpStatusCode.NotFound, "Task with the given ID was not found.");
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

                return new Response<dynamic>(false, HttpStatusCode.InternalServerError, "Error DeleteTaskById", errorMessage);
            }
        }
    }
}
