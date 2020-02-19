using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1.Messages;
using System.Collections.Generic;

namespace FunctionApp1
{
    public class ApiFunction
    {
        [FunctionName("ApiFunction")]
        public async Task<IActionResult> Run(
                    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
                    [Queue("messagetomom")] IAsyncCollector<newMessage> letterCollector,
                    ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"This is req , {req}");






       /*     string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("This is the request body", requestBody);
            var data = JsonConvert.DeserializeObject(requestBody);
            //TODO model HttpRequest from fields of MessageToMom*/

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            newMessage data = JsonConvert.DeserializeObject<newMessage>(requestBody);
       /*     newMessage data = JsonConvert.DeserializeObject(requestBody);*/
            data.Greeting = data.Greeting ?? data?.Greeting;
            /*  Greeting = name ?? data?.Greeting; */

            log.LogInformation($"This is Data, {data}");

            newMessage obj = JsonConvert.DeserializeObject<newMessage>(requestBody);



            var newNote = new newMessage
            {
                Flattery = data.Flattery,
                Greeting = data.Greeting,
                HowMuch = data.HowMuch,
                HowSoon = data.HowSoon,
                From = data.From

            };

            await letterCollector.AddAsync(newNote);










            /*   string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

               log.LogInformation($"This is the request body {requestBody}");
                   Message data = JsonConvert.DeserializeObject(requestBody);





            //This is how I thought I would solve this problem.  I had a lot of trouble figuring out how to get the req.body out.  
            //I am still learning C# and it was quite different than what I have used before as far as api calls go.

            /*     var formEntryToBeParsed = [];
                 req.Form.TryGetValue("out string", out formEntryToBeParsed);
                 {
                     log.LogInformation($"{formEntryToBeParsed}");
                    *//* List<string> fieldToBeAssigned1 = ToString(); //Parse the string value or values
                     string fieldToBeAssigned2; //Parse the string value or values
                     double fieldToBeAssigned3; //Parse the string value or values
                     DateTime fieldToBeAssigned4; //Parse the string value or value*//*
                 }*/
            /*
                        var message = new MessageToMom
                        {
                            Flattery = fieldToBeAssigned1,
                            Greeting = fieldToBeAssigned2,
                            HowMuch = fieldToBeAssigned3,
                            HowSoon = fieldToBeAssigned4,
                            From = "yourbelovedson@gmail.com"

                        };*/





            //Map new model values (from HttpRequest) to MessageToMom below

            /*  var message = new MessageToMom
              {
                  Flattery = new List<string> { "amazing", "fabulous", "profitable" },
                  Greeting = "So Good To Hear From You",
                  HowMuch = 1222.22M,
                  HowSoon = DateTime.UtcNow.AddDays(1),
                  From = "yourbelovedson@gmail.com"

              };

              await letterCollector.AddAsync(message);*/


            /*return new OkObjectResult(message);*/
            return (ActionResult)new OkObjectResult($"Hello, Johnny");
        }
    }
}

   /* public class ApiFunction
    {
        [FunctionName("ApiFunction")]
        public async Task<IActionResult> Run(
                    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
                    [Queue("messagetomom")] IAsyncCollector<MessageToMom> letterCollector,
                    ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"This is req , {req}");






            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("This is the request body", requestBody);
            var data = JsonConvert.DeserializeObject(requestBody);
            //TODO model HttpRequest from fields of MessageToMom



            log.LogInformation($"This is Data, {data}");

            *//*    List<string> greet = data.getGreeting();


                var newNote = new newMessage
                {
                    Flattery = new List<string> { data },
                    Greeting = data.getGreeting(),
                    HowMuch = data.getHowMuch(),
                    HowSoon = data.getHowSoon(),
                    From = data.getFrom()

                };*/






            /*   string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

               log.LogInformation($"This is the request body {requestBody}");
                   Message data = JsonConvert.DeserializeObject(requestBody);

       

           

            //This is how I thought I would solve this problem.  I had a lot of trouble figuring out how to get the req.body out.  
            //I am still learning C# and it was quite different than what I have used before as far as api calls go.

            /*     var formEntryToBeParsed = [];
                 req.Form.TryGetValue("out string", out formEntryToBeParsed);
                 {
                     log.LogInformation($"{formEntryToBeParsed}");
                    *//* List<string> fieldToBeAssigned1 = ToString(); //Parse the string value or values
                     string fieldToBeAssigned2; //Parse the string value or values
                     double fieldToBeAssigned3; //Parse the string value or values
                     DateTime fieldToBeAssigned4; //Parse the string value or value*//*
                 }*/
            /*
                        var message = new MessageToMom
                        {
                            Flattery = fieldToBeAssigned1,
                            Greeting = fieldToBeAssigned2,
                            HowMuch = fieldToBeAssigned3,
                            HowSoon = fieldToBeAssigned4,
                            From = "yourbelovedson@gmail.com"

                        };*//*





            //Map new model values (from HttpRequest) to MessageToMom below

            var message = new MessageToMom
            {
                Flattery = new List<string> { "amazing", "fabulous", "profitable" },
                Greeting = "So Good To Hear From You",
                HowMuch = 1222.22M,
                HowSoon = DateTime.UtcNow.AddDays(1),
                From = "yourbelovedson@gmail.com"

            };

            await letterCollector.AddAsync(message);


            *//*return new OkObjectResult(message);*//*
            return (ActionResult)new OkObjectResult($"Hello, Johnny");
        }
    }*/

