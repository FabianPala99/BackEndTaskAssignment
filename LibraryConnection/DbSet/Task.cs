using System.ComponentModel.DataAnnotations;

namespace LibraryConnection.DbSet
{
    public class Task
    {
        [Key]
        public virtual int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required int AssignedToUserId { get; set; }
        public required int AssignedByUserId { get; set; }
    }
}
