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
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
