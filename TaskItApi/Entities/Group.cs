using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskItApi.Entities
{
    /// <summary>
    /// A group of people that want to devide tasks.
    /// For example a houshold, company or society
    /// </summary>
    public class Group
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set;}
        public string Description { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Color { get; set; }
        public ICollection<Subscription> Members { get; set; }
    }
}
