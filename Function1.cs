
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Management.Automation;
using System;

namespace TestAzureManagement
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;


            try
            {
                // ExchangeClient.TestPowerShell();

                using (var ps = PowerShell.Create())
                {
                    ps.AddScript("Get-Process");

                    foreach (PSObject item in ps.Invoke())
                    {

                    }
                }


                return new OkObjectResult("Worked");
            }
            catch (Exception e)
            {
                return new OkObjectResult($"Didn't Work - {e.ToString()}");
            }


           

           // return name != null
             //   ? (ActionResult)new OkObjectResult($"Hello, {name}")
               // : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
