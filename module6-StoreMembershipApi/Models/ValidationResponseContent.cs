using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace StoreMembershipApi.Models
{
    public class StoreResponseContent
    {
        public string Version { get; set; }
        public int Status { get; set; }
        public string UserMessage { get; set; }
        public StoreResponseContent()
        { }
        public StoreResponseContent(
            string message,
            HttpStatusCode status)
        {
            this.UserMessage = message;
            this.Status = (int)status;
            this.Version = Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version.ToString();
        }
    }

    public class ValidationResponseContent : StoreResponseContent
    {
        public string StoreMembershipNumber { get; set; }
    }
}
