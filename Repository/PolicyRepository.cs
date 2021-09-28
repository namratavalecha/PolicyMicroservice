using PolicyMicroservice.Data;
using PolicyMicroservice.Models;
using PolicyMicroservice.Service;
using System;
using System.Linq;

namespace PolicyMicroservice.Repository
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ApplicationDbContext _db = null;
        private readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PolicyService));
        public PolicyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public string CreatePolicy(CreatePolicyDetails createPolicyDetails,string quotes,string policyId)
        {
            if (createPolicyDetails != null && quotes != "")
            {
                string acceptedQuote = createPolicyDetails.AcceptedQuotes;
                if (quotes != "No Quotes, Contact Insurance Provider")
                    acceptedQuote = Convert.ToInt32(quotes) < Convert.ToInt32(createPolicyDetails.AcceptedQuotes) ? quotes : createPolicyDetails.AcceptedQuotes;
                

                ConsumerPolicy consumerPolicy = new ConsumerPolicy()
                {
                    PolicyId = policyId,
                    ConsumerId = createPolicyDetails.ConsumerId,
                    BusinessId = createPolicyDetails.BusinessId,
                    PaymentDetails = "Pending",
                    AcceptanceStatus = "Pending",
                    AcceptedQuotes = acceptedQuote,
                    PolicyStatus = "Initiated",
                    EffectiveDate = DateTime.Now
                };

                //PolicyData.PolicyList.Add(consumerPolicy);
                _db.consumerPolicies.Add(consumerPolicy);
                _db.SaveChanges();
                return "Policy Created.\nConsumer Id : "+consumerPolicy.ConsumerId+"\nPolicy Id : "+consumerPolicy.PolicyId;
            }
            else
            { return "Policy not created!"; }
        }

        public string IssuePolicy(IssuePolicyDetails issuePolicyDetails)
        {
            if (issuePolicyDetails != null)
            {
                //ConsumerPolicy consumerPolicy = PolicyData.PolicyList.FirstOrDefault(p => p.ConsumerId.Equals(issuePolicyDetails.ConsumerId) && p.BusinessId.Equals(issuePolicyDetails.BusinessId) && p.PolicyId.Equals(issuePolicyDetails.PolicyId));
                ConsumerPolicy consumerPolicy = _db.consumerPolicies.Where(p => p.ConsumerId.Equals(issuePolicyDetails.ConsumerId) && p.BusinessId.Equals(issuePolicyDetails.BusinessId) && p.PolicyId.Equals(issuePolicyDetails.PolicyId)).FirstOrDefault();
                if (consumerPolicy != null)
                {
                    string acceptedQuote = consumerPolicy.AcceptedQuotes;
                    if (acceptedQuote != "No Quotes, Contact Insurance Provider")
                    {
                        PolicyData.PolicyList.Remove(consumerPolicy);
                        ConsumerPolicy newConsumerPolicy = new ConsumerPolicy()
                        {
                            ConsumerId = issuePolicyDetails.ConsumerId,
                            BusinessId = issuePolicyDetails.BusinessId,
                            PolicyId = issuePolicyDetails.PolicyId,
                            AcceptedQuotes = acceptedQuote,
                            PolicyStatus = "Issued",
                            PaymentDetails = issuePolicyDetails.PaymentDetails,
                            AcceptanceStatus = issuePolicyDetails.AcceptanceStatus,
                            EffectiveDate = DateTime.Now
                        };

                        //PolicyData.PolicyList.Add(newConsumerPolicy);
                        _db.consumerPolicies.Add(newConsumerPolicy);
                        _db.SaveChanges();
                        return "Policy Issued.\nConsumer Id : " + consumerPolicy.ConsumerId + "\nPolicy Id : " + consumerPolicy.PolicyId;
                    }
                    else
                        return "Policy not issued as quotes is not provided.";
                }
            }
             return "Policy not issued!";
        }

        public ConsumerPolicy GetPolicy(string ConsumerId, string PolicyId)
        {
            //ConsumerPolicy consumer = PolicyData.PolicyList.FirstOrDefault(p => p.ConsumerId.Equals(ConsumerId) && p.PolicyId.Equals(PolicyId));
            ConsumerPolicy consumer = _db.consumerPolicies.Where(t => (t.ConsumerId == ConsumerId && t.PolicyId == PolicyId)).FirstOrDefault();
            return consumer;
        }

        public string GetPermissiblePolicy(string PropertyType,int BusinessValue)
        {   //Not filtered with business value because of lack of data with all combination of business value and property type (10*8)
            _log4net.Info("Inside try" + PropertyType);
            PolicyMaster policy = PolicyMasterData.policyMastersList.FirstOrDefault(p => p.BusinessValue.Equals(BusinessValue));
            _log4net.Info("Inside try" + policy.PolicyId);
            //PolicyMaster policy = _db.policyMasters.Where(t => t.PropertyType == PropertyType).FirstOrDefault();
            if (policy != null)
                return policy.PolicyId;
            else
                return "";

        }
    }
    
}
