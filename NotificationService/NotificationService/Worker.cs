using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Colorful;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Parsers;
using Console = Colorful.Console;

namespace NotificationService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
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
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

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
