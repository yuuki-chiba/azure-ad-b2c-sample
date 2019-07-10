using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SampleRestApi.Models;
using System.Collections.Generic;

namespace SampleRestApi
{
    public class Function1
    {
        private readonly Dictionary<string, string> store = new Dictionary<string, string>()
        {
            { "yuuki.chiba@fujifilm.com", "fujifilm" },
            { "yuuki.chiba+b2ctest@fujifilm.com", "fujifilm,test" },
        };

        [FunctionName("Validate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] ValidationRequest request,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            log.LogInformation($"request: {JsonConvert.SerializeObject(request)}");

            IActionResult response;
            var extensionAttribute = GetExtensionAttribute(request.SignInName);
            if (extensionAttribute == null)
            {
                response = GenerateErrorMessageWithMessage("Extension attribute is not valid!");
            }
            else
            {
                // Return the output claim(s)
                response = new OkObjectResult(new ValidationResponseContent
                {
                    SignInName = request.SignInName,
                    ExtensionAttribute = extensionAttribute
                });
            }

            log.LogInformation($"response: {JsonConvert.SerializeObject(response)}");
            return response;
        }

        /// <summary>
        /// Constructs an HTTP 409 Conflict IActionResult to return back
        /// to the validation or orchestration step. This is used to
        /// communicate with the policy steps to communicate an error
        /// state.
        /// </summary>
        /// <param name="message">
        /// The message to be passed back to the user to explain why
        /// the request failed.
        /// </param>
        /// <returns>
        /// An IActionResult representing an HTTP 409 Conflict with a
        /// custom payload.
        /// </returns>
        private IActionResult GenerateErrorMessageWithMessage(
            string message)
        {
            return new ConflictObjectResult(
                new ValidationResponseContent
                {
                    Version = "1.0.0",
                    Status = (int)HttpStatusCode.Conflict,
                    UserMessage = message
                });
        }

        /// <summary>
        /// Get a extension attribute with a provided signin name.
        /// If the name is not found, we return null.
        /// </summary>
        /// <param name="signInName">
        /// The signIn name.
        /// </param>
        /// <returns>
        /// A extension attribute.
        /// </returns>
        private string GetExtensionAttribute(
            string signInName)
        {
            string extensionAttribute = null;
            store.TryGetValue(signInName, out extensionAttribute);
            return extensionAttribute;
        }
    }
}
