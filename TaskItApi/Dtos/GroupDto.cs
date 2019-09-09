using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Dtos
{
    public class GroupDto
    {
        public int Id;
        [Required]
        public string Name;

        public string Description;

        public IEnumerable<SubscriptionDto> Members;
    }
}
