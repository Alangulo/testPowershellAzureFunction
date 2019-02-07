using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Management.Automation;
using System.Diagnostics;

namespace PowerShellTest
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //log.Info("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;


            try
            {
           

                using (var ps = PowerShell.Create())
                {

                    Console.WriteLine(ps.ToString());

                    //  ps.AddCommand("get-host")
                    // .Invoke();
                    // ps.AddCommand("Get-host");
                    //ps.Commands = "";
                    //ps.Invoke();
                    //PSVersionTable.PSVersion

                    //  var command = new PSCommand();
                    //PowerShell shell = PowerShell.Create("get-process").AddCommand("sort-object");

                    //command = "get-host"
                    //powerShellInstance.Commands = command;
                    //PowerShell.Create().Invoke();
                    //ps.Commands = ;
                    PowerShell.Create().Invoke();
                     //  foreach (PSObject item in ps.Invoke())
                  //   {

                  //  }
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
