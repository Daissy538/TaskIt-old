using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskItApi.Entities
{
    /// <summary>
    /// A user can be a holder of a task
    /// </summary>
    public class TaskHolder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int TaskID { get; set; }

        public Task Task { get; set; }

        [Required]
        public int UserID { get; set; }

        public User User { get; set; }
    }
}
