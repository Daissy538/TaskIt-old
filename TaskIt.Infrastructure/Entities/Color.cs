using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskItApi.Entities
{
    /// <summary>
    /// The color of a group icon.
    /// This database table will be automated seeded
    /// </summary>
    public class Color
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
