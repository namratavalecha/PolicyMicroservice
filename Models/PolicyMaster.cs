using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice.Models
{
    public class PolicyMaster
    {
        [Key]
        public string PolicyId { get; set; }
        public string PropertyType { get; set; }
        public string ConsumerType { get; set; }
        public int AssuredSum { get; set; }
        public int Tenure { get; set; }
        public int BusinessValue { get; set; }
        public int PropertyValue { get; set; }
        public string BaseLocation { get; set; }
        public string Type { get; set; }
    }
}
