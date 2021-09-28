using PolicyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice.Repository
{
   public interface IPolicyRepository
    {
        string CreatePolicy(CreatePolicyDetails createPolicy,string quotes,string policyId);

        string IssuePolicy(IssuePolicyDetails issuePolicy);

        ConsumerPolicy GetPolicy(string ConsumerId, string PolicyId);
        string GetPermissiblePolicy(string PropertyType,int BusinessValue);
    }
}
