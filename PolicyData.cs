using PolicyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice
{
    public class PolicyData
    {
        public static List<ConsumerPolicy> PolicyList = new List<ConsumerPolicy>()
        {

        new ConsumerPolicy()
        { 
                ConsumerId ="C01",
                BusinessId ="B01",
                PolicyId = "P01",
                PolicyStatus = "Issued",
                AcceptedQuotes = "80000" ,
                PaymentDetails = "Success",
                AcceptanceStatus = "Accepted",
                EffectiveDate = new DateTime(2021, 05, 31)
             },
        new ConsumerPolicy()
        { 
                ConsumerId ="C01",
                BusinessId ="B02",
                PolicyId= "P02",
                PolicyStatus = "Initiated",
                AcceptedQuotes = "81000" ,
                PaymentDetails = "Pending",
                AcceptanceStatus = "Pending",
                EffectiveDate = new DateTime(2021, 06, 1)
            },

        new ConsumerPolicy()
        {
                ConsumerId ="C02",
                BusinessId ="B02",
                PolicyId= "P03",
                PolicyStatus = "Initiated",
                AcceptedQuotes = "No Quotes, Contact Insurance Provider" ,
                PaymentDetails = "Pending",
                AcceptanceStatus = "Pending",
                EffectiveDate = new DateTime(2021, 06, 3)
            },
        };
    }
}
