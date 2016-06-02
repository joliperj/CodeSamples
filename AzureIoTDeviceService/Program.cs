using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIoTDeviceService
{
    class Program
    {
        private static ServiceClient serviceClient;
        private static string connectionString = "HostName=TheProgrammatorCodeSamples.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=HrtEmhAFgU8cmQm2nfx88tHqSnOEURGcdJQDmRbCJsY=";

        static void Main(string[] args)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            ReceiveFeedbackAsync();

            Startup().Wait();
        }

        private static async Task Startup()
        {
            Console.WriteLine("Type a text and hit enter to send.");
            while (true)
            {
                var message = Console.ReadLine();
                await SendCloudToDeviceMessageAsync(message);
                Console.WriteLine("Message sent!");
            }
        }

        private async static void ReceiveDeviceToCloudMessagesAsync()
        {

        }

        private async static void ReceiveFeedbackAsync()
        {
            var feedbackReceiver = serviceClient.GetFeedbackReceiver();

            Console.WriteLine("\nReceiving c2d feedback from service");
            while (true)
            {
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();
                if (feedbackBatch == null) continue;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Received feedback: {0}", string.Join(", ", feedbackBatch.Records.Select(f => f.StatusCode)));
                Console.ResetColor();

                await feedbackReceiver.CompleteAsync(feedbackBatch);
            }
        }

        private async static Task SendCloudToDeviceMessageAsync(string message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            commandMessage.Ack = DeliveryAcknowledgement.Full;
            await serviceClient.SendAsync("RaspberryPi2", commandMessage);
        }
    }
}