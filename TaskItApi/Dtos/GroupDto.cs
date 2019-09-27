using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Dtos
{
    public class GroupDto
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Icon { get; set; }
        [Required]
        public string Color { get; set; }

        public IEnumerable<SubscriptionDto> Members;
    }
}
