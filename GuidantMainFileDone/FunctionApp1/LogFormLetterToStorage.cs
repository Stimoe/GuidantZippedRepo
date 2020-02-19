using System;
using System.Threading.Tasks;
using FunctionApp1.Messages;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;

namespace FunctionApp1
{
    public class LogFormLetterToStorage
    {
        [FunctionName("LogFormLetterToStorage")]
        public async Task Run([QueueTrigger("outputletter", Connection = "AzureWebJobsStorage")]FormLetter myQueueItem,
            [Table("letters")] IAsyncCollector<LetterEntity> letterTableCollector,
            ILogger log)
        {
            log.LogInformation($"This is in LogForm, {myQueueItem.ExpectedDate}");
            //TODO map FormLetter message to LetterEntity type and save to table storage

            //Here I am grabbing the information from myQueItem and creating n ew variables to plug into the letterEntity
            DateTime expect = myQueueItem.ExpectedDate;
            DateTime request = myQueueItem.RequestedDate;
            LetterEntity newNote = new LetterEntity();
            {
                newNote.Heading = myQueueItem.Heading;
                newNote.Likelihood = myQueueItem.Likelihood;
                newNote.ExpectedDate = expect;
                newNote.RequestedDate = request;
                newNote.Body = myQueueItem.Body;

            };




            await letterTableCollector.AddAsync(newNote);

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");


        }

    }
    //This is where I create the letterEntity for the TableEntity


    public class LetterEntity : TableEntity
    {
   
        public LetterEntity()
           


        {
            Random newRandom = new Random();
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += newRandom.Next(0, 9).ToString();
            }
            Random randomTwo = new Random();
            string s = "";
            int j;
            for (j = 1; j < 11; j++)
            {
                r += randomTwo.Next(0, 9).ToString();
            }
            DateTime now = DateTime.Now;
            DateTime soon = DateTime.Now.AddDays(1);
           string changed = String.Format("{0:ddd, MMM d, yyyy}", now);
            string request = String.Format("{0:ddd, MMM d, yyyy}", soon);
            PartitionKey = r;
            RowKey = s;
        }
   


        public string Heading { get; set; }
        public double Likelihood { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Body { get; set; }



    }
}

