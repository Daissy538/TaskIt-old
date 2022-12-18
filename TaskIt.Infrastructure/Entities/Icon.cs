using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskItApi.Entities
{
    /// <summary>
    /// The Icon of a group.
    /// This database table will be automated seeded
    /// </summary>
    public class Icon
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String Name { get; set; }
        public String Value { get; set; }
    }
}
