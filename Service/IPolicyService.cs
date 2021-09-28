using PolicyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice.Service
{   

    public interface IPolicyService
    {
        string CreatePolicy(CreatePolicyDetails createPolicy, string token);
        string IssuePolicy(IssuePolicyDetails issuePolicy);
        ConsumerPolicy ViewPolicy(string consumerId, string policyId);
        string GetQuotes(int PropertyValue, int BusinessValue, string PropertyType,string token);
    }
}
