using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Entities
{
    /// <summary>
    /// A group of people that want to devide tasks.
    /// For example a houshold, company or society
    /// </summary>
    public class Group
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set;}
        public string Description { get; set; }
        public ICollection<Subscription> Members { get; set; }
    }
}
