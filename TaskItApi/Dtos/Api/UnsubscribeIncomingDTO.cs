using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItApi.Dtos.Api
{
    public class UnsubscribeIncomingDTO
    {
        public int UserID { get; set; }
        public int GroupID { get; set; }
    }
}
