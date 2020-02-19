using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FunctionApp1.Messages;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class CalculateDatesAndAmountsFunction
    {
        [FunctionName("CalculateDatesAndAmountsFunction")]
        public async Task Run([QueueTrigger("messagetomom", Connection = "AzureWebJobsStorage")]MessageToMom myQueueItem,
            [Queue("outputletter")] IAsyncCollector<FormLetter> letterCollector,
            ILogger log)
        {
            log.LogInformation($"{myQueueItem.Greeting} {myQueueItem.HowMuch} {myQueueItem.HowSoon}");
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            //TODO parse flattery list into comma separated string

            //here I grabbed the information from myQueueItem and created a new string from it.
            string parsed = $"{myQueueItem.Flattery[0]}" + ", " + $"{myQueueItem.Flattery[1]}" + ", " + $"{myQueueItem.Flattery[2]}";
            log.LogInformation($"{parsed}");

            //TODO populate Header with salutation comma separated string and "Mother"

            //Not sure if the point was to use the myQueue.greeting so i did both possibilities
            string salutations2 = $"{myQueueItem.Greeting}, Mother";
            FormLetter p = new FormLetter();
            p.Heading = ($"salutation" + ", " + $"Mother");
      
            //TODO calculate likelihood of receiving loan based on this decision tree
            // 100 percent likelihood (initial value) minus the probability expressed from the quotient of howmuch and the total maximum amount ($10000)
            //Not sure if I understood the goal of this one but it was some easy math.
            //grab the variable from myQueueItem.howMuch

            var division = (10000 / myQueueItem.HowMuch);
            var percent = (100 - division);
            log.LogInformation($"{percent}");

            //TODO calculate approximate actual date of loan receipt based on this decision tree

            // funds will be made available 10 business days after day of submission
            // business days are weekdays, there are no holidays that are applicable

            //For this I grabbed the current DateTime, found out what day of the week that was.
            //Then ran that through some if else statements to figure out when the loan would be due.
            var dt = DateTime.Now;
            var day = dt.DayOfWeek;
            log.LogInformation($"{dt.DayOfWeek}");
            if (day == DayOfWeek.Monday || day == DayOfWeek.Tuesday || day == DayOfWeek.Wednesday || day == DayOfWeek.Thursday || day == DayOfWeek.Friday)
            {
                var dueDay = dt.AddDays(14);
                log.LogInformation($"The actual date of loan receipt is, " + $"{dueDay}");
            }
            else if (day == DayOfWeek.Saturday)
            {
                var dueDay = dt.AddDays(13);
                log.LogInformation($"The actual date of loan receipt is, " + $"{dueDay}");
            }
            else if (day == DayOfWeek.Sunday)
            {
                var dueDay = dt.AddDays(12);
                log.LogInformation($"The actual date of loan receipt is, " + $"{dueDay}");
            }



            //TODO use new values to populate letter values per the following:

            //Body:"Really need help: I need $5523.23 by December 12,2020"
            //ExpectedDate = calculated date
            //RequestedDate = howsoon
            //Heading=Greeting
            //Likelihood = calculated likelihood

            //for this I created a new FormLetter and defined some variables that I would need inside.

            FormLetter help = new FormLetter();
            dt = DateTime.Now;
            day = dt.DayOfWeek;

            {
                var expected = dt;

                if (day == DayOfWeek.Monday || day == DayOfWeek.Tuesday || day == DayOfWeek.Wednesday || day == DayOfWeek.Thursday || day == DayOfWeek.Friday)
                {
                    var dueDay = dt.AddDays(14);
                    /*  log.LogInformation($"The actual date of loan receipt is, " + $"{dueDay}");*/

                    expected = dueDay;
                }
                else if (day == DayOfWeek.Saturday)
                {
                    var dueDay = dt.AddDays(13);

                    expected = dueDay;
                }
                else if (day == DayOfWeek.Sunday)
                {
                    var dueDay = dt.AddDays(12);

                    expected = dueDay;
                }
                log.LogInformation($"This is in calculaeDates, {expected}");
                string reallyNeedHelp = "Really need help: I need $5523.23 by December 12,2020";
                DateTime requestDate = new DateTime(2020, 12, 12);
                //Then I applied all these variables into the FormLetter format.
                {
            
                        help.Heading = $"{myQueueItem.Greeting}";
                        help.Likelihood = (100 - (10000 / 5523.23));
                        help.ExpectedDate = expected;
                        help.RequestedDate = requestDate;
                        help.Body = reallyNeedHelp;

                    };
                    log.LogInformation($"This is the expected date, { expected}");
                }
                await letterCollector.AddAsync(help);
                log.LogInformation($"{help.Heading}" + $"The likelihood of being about to make a loan in that amount is, " + $"{help.Likelihood}");
            }


        }
    }


    

