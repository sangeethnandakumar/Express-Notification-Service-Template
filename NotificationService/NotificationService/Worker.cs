using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Colorful;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using NETCore.MailKit.Infrastructure.Internal;
using NotificationService.Parsers;
using Console = Colorful.Console;

namespace NotificationService
{
    public class Worker : BackgroundService
    {
        private readonly IEmailService _emailService;

        public Worker(IEmailService emailService)
        {
            _emailService = emailService;
        }


        //Overrides
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting Service...");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.Clear();
                ConsoleHeader();
                ConsoleInfo();
                QueueList();
                DispatchedList();

                var mailto = "sangeethnandakumar@gmail.com";
                var subject = "Sample Mail From Worker Service";
                var body = "<h1>This is the mail body</h1>";
                var cc = "navu@gmail.com, ammu@gmail.com";
                var bcc = "surya@gmail.com";
                var isHtml = true;
                await _emailService.SendAsync(mailto, cc, bcc, subject, body, isHtml);

                await Task.Delay(5000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            var mailto = "sangeethnandakumar@gmail.com";
            var subject = "Notification Service Stopped";
            var body = "IMPORTANT.! - The notification service is stopped";
            var isHtml = false;
            _emailService.Send(mailto, subject, body, isHtml);

            return base.StopAsync(cancellationToken);
        }






        //Display Modules
        private static void ConsoleHeader()
        {
            Console.WriteAscii("Notification Service", Color.FromArgb(244, 212, 255));
        }

        private static void ConsoleInfo()
        {
            Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetName().Version, Color.White);
        }

        private static void QueueList()
        {
            Console.WriteLine("");
            Console.WriteLine("-- TOP 10 ON QUEUE -----------------------------------------------------------------------------------------------------------------");

            ColorAlternatorFactory alternatorFactory = new ColorAlternatorFactory();
            ColorAlternator alternator = alternatorFactory.GetAlternator(1, Color.Plum, Color.PaleVioletRed);
            
            IEnumerable<Tuple<string, int, string, string, string, string>> authors =
            new[]
            {
              Tuple.Create(Guid.NewGuid().ToString(), 1, "EMAIL", "From: sender@ibo.com", "To: ammu@gmail.com", "Dispatch in 1m:1s"),
              Tuple.Create(Guid.NewGuid().ToString(),1, "SMS", "From: +91 949-566-1468", "To: +91 949-566-1468", "Dispatch in 12m:3s"),
              Tuple.Create(Guid.NewGuid().ToString(),1, "PUSH", "", "To: Xiaomi Redmi Note 5 Pro", "Dispatch in 12m:3s")
            };


            Console.WriteLineAlternating(
                authors.ToStringTable(new[] { "Id", "Px", "Type", "Source", "Destination", "Dispatching" }, a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a=>a.Item6),
                alternator
                );

        }

        private static void DispatchedList()
        {
            Console.WriteLine("");
            Console.WriteLine("-- LAST 10 DISPATCHED -----------------------------------------------------------------------------------------------------------");

            ColorAlternatorFactory alternatorFactory = new ColorAlternatorFactory();
            ColorAlternator alternator = alternatorFactory.GetAlternator(2, Color.Gray, Color.LightGray);

            IEnumerable<Tuple<string, int, string, string, string, string>> authors =
           new[]
           {
              Tuple.Create(Guid.NewGuid().ToString(), 1, "EMAIL", "From: sender@ibo.com", "To: ammu@gmail.com", "Dispatch in 1m:1s"),
              Tuple.Create(Guid.NewGuid().ToString(),1, "SMS", "From: +91 949-566-1468", "To: +91 949-566-1468", "Dispatch in 12m:3s"),
              Tuple.Create(Guid.NewGuid().ToString(),1, "PUSH", "", "To: Xiaomi Redmi Note 5 Pro", "Dispatch in 12m:3s")
           };

            Console.WriteLineAlternating(
                 authors.ToStringTable(new[] { "Id", "Px", "Type", "Source", "Destination", "Dispatching" }, a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5, a => a.Item6),
                 alternator
                 );
        }

    }
}
