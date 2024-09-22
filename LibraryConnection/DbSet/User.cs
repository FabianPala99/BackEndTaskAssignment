using System.ComponentModel.DataAnnotations;

namespace LibraryConnection.DbSet
{
    public class User
    {
        [Key]
        public virtual int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int RolId { get; set; }
    }
}
