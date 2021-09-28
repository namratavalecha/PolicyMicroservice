using Microsoft.AspNetCore.Mvc;
using PolicyMicroservice.Models;
using PolicyMicroservice.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PolicyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PolicyController));
        private readonly IPolicyService _policyService;
        private readonly IHttpClientFactory _clientFactory;
        public PolicyController(IPolicyService PolicyService, IHttpClientFactory clientFactory)
        {
            _policyService = PolicyService;
            _clientFactory = clientFactory;
        }

        private async Task<HttpResponseMessage> CheckTokenValidity(string scheme, string token)
        {
            // var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Auth/Verify");
            // request.Headers.Add("Accept", "application/json");
            // request.Headers.Add("Authorization", token);
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://authorizationmicroservicepas.azurewebsites.net/api/Auth/Verify");

            HttpResponseMessage response = await client.SendAsync(request);


            return response;

        }
        /// <summary>
        /// Create Policy
        /// </summary>
        /// <param name="createPolicy"></param>
        /// <returns>Bool result representing the Create status</returns>
        [HttpPost]
        [Route("createPolicy")]
        public async Task<IActionResult> CreatePolicy(CreatePolicyDetails createPolicyDetails, [FromHeader] string authorization)
        {
            _log4net.Info($"POST: /createPolicy endpoint invoked :");
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var result = await CheckTokenValidity(headerValue.Scheme, headerValue.Parameter);
                if (result != null && result.StatusCode != HttpStatusCode.OK)
                {
                    return Unauthorized("Authorization Failed! Might be due to invalid token!");
                }
            }
            if (createPolicyDetails!=null)
            {
                try
                {
                    _log4net.Info(" CreatePolicy Method Called");
                    var result = _policyService.CreatePolicy(createPolicyDetails,authorization);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    _log4net.Error("Exception in PolicyCreate" + e.Message);
                    return NotFound();
                }
            }
            else
            { return BadRequest("Invalid Input Parameters"); }
        }
        
        /// <summary>
        /// Issue Policy
        /// </summary>
        /// <param name="issuePolicy"></param>
        /// <returns>Bool result representing the issue status</returns>
        [HttpPost]
        [Route("issuePolicy")]
        public async Task<IActionResult> IssuePolicy(IssuePolicyDetails issuePolicyDetails, [FromHeader] string authorization)
        {
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var result = await CheckTokenValidity(headerValue.Scheme, headerValue.Parameter);
                if (result != null && result.StatusCode != HttpStatusCode.OK)
                {
                    return Unauthorized("Authorization Failed! Might be due to invalid token!");
                }
            }
            _log4net.Info($"POST: /createPolicy endpoint invoked :");
            if (issuePolicyDetails!=null)
            {
                try
                {
                    _log4net.Info(" IssuePolicy Method Called");
                    var result = _policyService.IssuePolicy(issuePolicyDetails);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    _log4net.Error("Exception in PolicyCreate" + e.Message);
                    return NotFound();
                }
            }
            else
                return BadRequest("Invalid Input Parameters");
        }
       
        /// <summary>
        /// View Policy Details for a specific Policy
        /// </summary>
        /// <param name="ConsumerId"></param>
        /// <param name="PolicyId"></param>
        /// <returns>Policy Details</returns>
        [HttpGet]
        [Route("viewPolicy/{ConsumerId}/{PolicyId}")]
        public async Task<IActionResult> ViewPolicy(string ConsumerId, string PolicyId, [FromHeader] string authorization)
        {
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var result = await CheckTokenValidity(headerValue.Scheme, headerValue.Parameter);
                if (result != null && result.StatusCode != HttpStatusCode.OK)
                {
                    return Unauthorized("Authorization Failed! Might be due to invalid token!");
                }
            }
            if (ConsumerId != null && PolicyId != null)
            {
                _log4net.Info(" viewPolicy Method Called");
                ConsumerPolicy policyDetails = _policyService.ViewPolicy(ConsumerId, PolicyId);
                if (policyDetails == null || policyDetails.BusinessId=="")
                    return NoContent();
                return Ok(policyDetails);
            }
            else
            { return BadRequest("Invalid Input Parameters"); }
        }


        /// <summary>
        /// Get Quotes from Quotes MicroService
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="businessValue"></param>
        /// <param name="propertyType"></param>
        /// <returns>Quotes from Quotes Microservice</returns>
        [HttpGet]
        [Route("getQuotes/{propertyValue}/{businessValue}/{propertyType}")]
        public async Task<IActionResult> GetQuotes(int propertyValue, int businessValue, string propertyType, [FromHeader] string authorization)
        {
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var result = await CheckTokenValidity(headerValue.Scheme, headerValue.Parameter);
                if (result != null && result.StatusCode != HttpStatusCode.OK)
                {
                    return Unauthorized("Authorization Failed! Might be due to invalid token!");
                }
            }
            _log4net.Info("GetQuotes Method Called");
            if (propertyValue >= 0 && businessValue >= 0 && propertyType != null)
            {
                
                var quotes = _policyService.GetQuotes(propertyValue, businessValue, propertyType,authorization);
                if (quotes == null)
                    return NotFound();
                return Ok(quotes);
            }
            else
            { return BadRequest("Invalid Input Parameters"); }
        }
    }
}
