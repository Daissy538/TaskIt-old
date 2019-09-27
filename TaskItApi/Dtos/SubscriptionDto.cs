using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItApi.Dtos
{
    public class SubscriptionDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public DateTime DateOfSubscription { get; set; }
    }
}
