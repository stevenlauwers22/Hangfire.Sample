using System;
using Hangfire;
using Hangfire.Common;
using Hangfire.SqlServer;

namespace HangfireSample.Client
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "HangfireSample.Client";

            var jobStorage = new SqlServerStorage("HangfireSample");
            var backgroundJobClient = new BackgroundJobClient(jobStorage);
            var recurringJobClient = new RecurringJobManager(jobStorage);
            string input;
            do
            {
                Console.WriteLine("What would you like to do:");
                Console.WriteLine("1.\t Enqueue a job in the background");
                Console.WriteLine("2.\t Shedule a job in the background");
                Console.WriteLine("3.\t Create a recurring job");
                Console.WriteLine("4.\t Trigger a recurring job");
                Console.WriteLine("x.\t Exit the application");
                
                Console.WriteLine();
                Console.Write("Enter one of the options above: ");
                input = Console.ReadLine();

                string output;
                switch (input)
                {
                    case "1":
                        output = backgroundJobClient.Enqueue(() => Console.WriteLine("Hello from an enqueued job"));
                        Console.WriteLine("Job enqueued: " + output);
                        break;
                    case "2":
                        output = backgroundJobClient.Schedule(() => Console.WriteLine("Hello from a scheduled job"), TimeSpan.FromMinutes(1));
                        Console.WriteLine("Job scheduled: " + output);
                        break;
                    case "3":
                        var jobId = Guid.NewGuid().ToString();
                        recurringJobClient.AddOrUpdate(jobId, Job.FromExpression(() => Console.WriteLine("Hello from a recurring job")), Cron.MinuteInterval(2));
                        Console.WriteLine("Job created: " + jobId);
                        break;
                    case "4":
                        Console.Write("Enter the ID of the job you want to trigger: ");
                        var jobToRunId = Console.ReadLine();
                        recurringJobClient.Trigger(jobToRunId);
                        Console.WriteLine("Job triggered: " + jobToRunId);
                        break;
                    case "x":
                        Console.WriteLine("The application will shutdown");
                        break;
                    default:
                        Console.WriteLine("Invalid option, please select something else");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("-------------------");
                Console.WriteLine();
            } while (input != "x");
        }
    }
}