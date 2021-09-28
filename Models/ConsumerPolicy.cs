using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice.Models
{
    public class ConsumerPolicy
    {
        [Key]
        public string ConsumerId { get; set; }
        public string BusinessId { get; set; }
        [ForeignKey("PolicyId")]
        public string PolicyId { get; set; }
        public string AcceptedQuotes { get; set; }
        public string PolicyStatus { get; set; }
        public string PaymentDetails { get; set; }
        public string AcceptanceStatus { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}
