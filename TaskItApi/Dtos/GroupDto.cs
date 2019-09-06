using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Dtos
{
    public class GroupDto
    {
        [Required]
        public string Name;

        public string Description;

        public IEnumerable<SubscriptionDto> Members;
    }
}
