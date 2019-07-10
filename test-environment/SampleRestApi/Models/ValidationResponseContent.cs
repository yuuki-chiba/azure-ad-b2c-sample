using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace SampleRestApi.Models
{
    public class SampleResponseContent
    {
        public string Version { get; set; }
        public int Status { get; set; }
        public string UserMessage { get; set; }
        public SampleResponseContent()
        { }
        public SampleResponseContent(
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

    public class ValidationResponseContent : SampleResponseContent
    {
        public string SignInName { get; set; }
        public string ExtensionAttribute { get; set; }
    }
}
