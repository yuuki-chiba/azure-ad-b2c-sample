using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoreMembershipApi.Models;

namespace StoreMembershipApi
{
    public class Function1
    {
        [FunctionName("validate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] MembershipRequest request,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!IsStoreMembershipNumberValid(
                request.StoreMembershipNumber))
            {
                return GenerateErrorMessageWithMessage(
                    "Store membership number is not valid, it " +
                    "must be a multiple of 5!");
            }

            // Return the output claim(s)
            return new OkObjectResult(new ValidationResponseContent
            {
                StoreMembershipNumber =
                    request.StoreMembershipNumber.ToString()
            });
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
                new StoreResponseContent
                {
                    Version = "1.0.0",
                    Status = (int)HttpStatusCode.Conflict,
                    UserMessage = message
                });
        }

        /// <summary>
        /// Validates a provided store membership number by using the
        /// modulus operator (see more <see href="https://docs.microsoft.com/en-us/dotnet/csharp/languagereference/operators/remainder-operator">here</see>)
        /// to determine if the provided store membership number is a
        /// multiple of 5. If it is, we return true, if not, we return
        /// false.
        /// /// </summary>
        /// <param name="storeMembershipNumber">
        /// The store membership number to be validated.
        /// </param>
        /// <returns>
        /// True on successful validation, false if validation fails.
        /// </returns>
        private bool IsStoreMembershipNumberValid(
            int storeMembershipNumber)
        {
            if (storeMembershipNumber % 5 != 0)
            {
                return false;
            }
            return true;
        }
    }
}
