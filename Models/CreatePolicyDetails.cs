using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice.Models
{
    public class CreatePolicyDetails
    {
        public string ConsumerId { get; set; }
        public string BusinessId { get; set; }
        public string PropertyId { get; set; }
        public string AcceptedQuotes { get; set; }
    }
}
