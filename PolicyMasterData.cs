using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolicyMicroservice.Models;

namespace PolicyMicroservice
{
    public class PolicyMasterData
    {
        public static List<PolicyMaster> policyMastersList = new List<PolicyMaster>()
        {
            
        new PolicyMaster()
            {
                PolicyId="P01",
                PropertyType="Property in Transit",
                ConsumerType="Owner",
                AssuredSum=2000000,
                Tenure=4,
                BusinessValue=2,
                PropertyValue=3,
                BaseLocation="Chennai",
                Type="Replacement",
            },
            new PolicyMaster()
            {
               PolicyId="P02",
                PropertyType="Factory Equipment",
                ConsumerType="Rent",
                AssuredSum=1000000,
                Tenure=2,
                BusinessValue=6,
                PropertyValue=3,
                BaseLocation="Pune",
                Type="Pay Back"
            },
            new PolicyMaster()
            {
               PolicyId="P03",
                PropertyType="Building",
                ConsumerType="Owner",
                AssuredSum=70000000,
                Tenure=6,
                BusinessValue=5,
                PropertyValue=8,
                BaseLocation="Mumbai",
                Type="Replacement"
            },
             new PolicyMaster()
            {
               PolicyId="P04",
                PropertyType="Owner",
                ConsumerType="Owner",
                AssuredSum=70000000,
                Tenure=5,
                BusinessValue=8,
                PropertyValue=3,
                BaseLocation="Mumbai",
                Type="Replacement"
            },


        new PolicyMaster()
            {
               PolicyId="P05",
                PropertyType="Government",
                ConsumerType="Owner",
                AssuredSum=1000000,
                Tenure=8,
                BusinessValue=8,
                PropertyValue=8,
                BaseLocation="Kokata",
                Type="Replacement"
            },
         new PolicyMaster()
            {
               PolicyId="P06",
                PropertyType="Contract",
                ConsumerType="Tenant",
                AssuredSum=8000000,
                Tenure=6,
                BusinessValue=5,
                PropertyValue=5,
                BaseLocation="Mumbai",
                Type="Pay Back"
            },
         new PolicyMaster()
            {
               PolicyId="P07",
                PropertyType="Lease",
                ConsumerType="Owner",
                AssuredSum=500000,
                Tenure=6,
                BusinessValue=3,
                PropertyValue=4,
                BaseLocation="Mumbai",
                Type="Pay Back"
            },
         new PolicyMaster()
            {
               PolicyId="P08",
                PropertyType="Rent",
                ConsumerType="Tenant",
                AssuredSum=70000,
                Tenure=2,
                BusinessValue=4,
                PropertyValue=6,
                BaseLocation="Chennai",
                Type="Pay Back"
            }};
    }
}
