using System.ComponentModel.DataAnnotations;

namespace LibraryConnection.DbSet
{
    public class Rol
    {
        [Key]
        public virtual int Id { get; set; }
        public required string Name { get; set; }
    }
}
