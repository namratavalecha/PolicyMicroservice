using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PolicyMicroservice.Models;
using PolicyMicroservice.Repository;
using System.Text.Json;
using PolicyMicroservice;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PolicyMicroservice.Service
{
    public class PolicyService : IPolicyService
    {
        private readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PolicyService));
        private readonly IPolicyRepository _policyRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public PolicyService(IHttpClientFactory clientFactory,IConfiguration configuration, IPolicyRepository PolicyRepository)
        {
            _configuration = configuration;
            _policyRepository = PolicyRepository;
            _clientFactory = clientFactory;
        }
        
        public string CreatePolicy(CreatePolicyDetails createPolicy,string token)
        {
            if (createPolicy != null)
            {
                ConsumerBusinessDetails consumerData = ConsumerDetail(createPolicy.ConsumerId, createPolicy.BusinessId,token);
                BusinessPropertyDetails propertyData = PropertyDetail(createPolicy.ConsumerId, createPolicy.PropertyId,token);
                _log4net.Info("Inside try" + propertyData.PropertyValue);
                string quotes = GetQuotes(propertyData.PropertyValue, consumerData.BusinessValue, propertyData.BuildingType, token);

                string policyId = _policyRepository.GetPermissiblePolicy(propertyData.BuildingType, consumerData.BusinessValue);

                return _policyRepository.CreatePolicy(createPolicy, quotes,policyId);
            }
            else
                return "Policy not created!";
        }

        public string IssuePolicy(IssuePolicyDetails issuePolicy)
        {
            if (issuePolicy != null)
                return _policyRepository.IssuePolicy(issuePolicy);
            else
                return "Policy not Issued";
        }

        public ConsumerPolicy ViewPolicy(string ConsumerId, string PolicyId)
        {
            if (ConsumerId != "" && PolicyId != "")
            {
                ConsumerPolicy consumerPolicy = _policyRepository.GetPolicy(ConsumerId, PolicyId);
               
                return consumerPolicy;
            }
            else
                return new ConsumerPolicy();
        }
        public static List<string> InvalidJsonElements;
        public static IList<T> DeserializeToList<T>(string jsonString)
        {
            InvalidJsonElements = null;
            var array = JArray.Parse(jsonString);
            IList<T> objectsList = new List<T>();

            foreach (var item in array)
            {
                try
                {
                    // CorrectElements  
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception ex)
                {
                    InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                    InvalidJsonElements.Add(item.ToString());
                }
            }

            return objectsList;
        }

        public string GetQuotes(int PropertyValue, int BusinessValue, string PropertyType,string token)
        {
            _log4net.Info( token);
            _log4net.Info("Inside service"+token);
            string uriConn = _configuration.GetValue<string>("ServiceURIs:Quotes");
            try
            {

                using (var client = new HttpClient())
                {
                    //var request = new HttpRequestMessage(HttpMethod.Get,
                    // "https://quotesmicroservicepas.azurewebsites.net/api/Quotes/getQuotesForPolicy/{PropertyValue}/{BusinessValue}/{PropertyType}");
                    //request.Headers.Add("Accept", "application/json");
                    //request.Headers.Add("ContentType", "application/json");
                    //request.Headers.Add("Authorization", "Bearer " + token);


                    //var client = _clientFactory.CreateClient();

                    //var response = await client.SendAsync(request);

                    client.BaseAddress = new Uri(uriConn);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ContentType", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    _log4net.Info("Inside try" + token);
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var httpResponse = client.GetAsync($"/api/Quotes/getQuotesForPolicy/{PropertyValue}/{BusinessValue}/{PropertyType}").Result;
                    var responseString = httpResponse.Content.ReadAsStringAsync().Result;
                    _log4net.Info("response" + token + responseString);
                    //if (!httpResponse.IsSuccessStatusCode)
                    //{
                    //    throw new Exception("Unable to reach [Consumer] microservice.");
                    //    _log4net.Info("response" + token + responseString);
                    //}


                    //if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    //    response = "No Quotes, Contact Insurance Provider";
                    //else
                    //{
                    //using JsonDocument doc = JsonDocument.Parse(responseString);
                    //JsonElement root = doc.RootElement;
                    //var u1 = root[0];

                    //string result = (string)u1.GetProperty("quote");

                    var responseString1 = JsonConvert.DeserializeObject<int>(responseString);
                    _log4net.Info("response" + responseString1);
                    return responseString;

                    //}
             
                       }
                    }
                    catch(Exception e)
                    {
                        return e.Message;
                    }   
        }

        public ConsumerBusinessDetails ConsumerDetail(string ConsumerId, string BusinessId,string token)
        {
            string uriConn = _configuration.GetValue<string>("ServiceURIs:Consumer");

            using (var client = new HttpClient())
            {
                _log4net.Info("Inside Consumer Details" + token);
                client.BaseAddress = new Uri(uriConn);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", token);
                var httpResponse = client.GetAsync($"/api/Consumer/viewConsumerBusiness/{ConsumerId}/{BusinessId}").Result;
                var responseString = httpResponse.Content.ReadAsStringAsync().Result;

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Unable to reach [Consumer] microservice.");
                }

                ConsumerBusinessDetails response = JsonConvert.DeserializeObject<ConsumerBusinessDetails>(responseString);
                return response;
            }

        }

        public BusinessPropertyDetails PropertyDetail(string ConsumerId, string PropertyId,string token)
        {
            string uriConn = _configuration.GetValue<string>("ServiceURIs:Consumer");

            using (var client = new HttpClient())
            {
                _log4net.Info("Inside Consumer Details" + token);
                client.BaseAddress = new Uri(uriConn);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", token);
                var httpResponse = client.GetAsync($"/api/Consumer/viewConsumerProperty/{ConsumerId}/{PropertyId}").Result;
                var responseString = httpResponse.Content.ReadAsStringAsync().Result;

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Unable to reach [Consumer] microservice.");
                }

                BusinessPropertyDetails response = JsonConvert.DeserializeObject<BusinessPropertyDetails>(responseString);
                return response;
            }

        }
    }
}
