using System;
using Hangfire;
using Hangfire.SqlServer;

namespace HangfireSample.Client
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "HangfireSample.Client";

            var backgroundJobClient = new BackgroundJobClient(new SqlServerStorage("HangfireSample"));
            string input;
            do
            {
                Console.WriteLine("What would you like to do:");
                Console.WriteLine("1.\t Launch a job in the background");
                Console.WriteLine("x.\t Exit the application");
                
                Console.WriteLine();
                Console.Write("Enter one of the options above:");
                input = Console.ReadLine();
                

                switch (input)
                {
                    case "1":
                        var result = backgroundJobClient.Enqueue(() => Console.WriteLine("Hello hangfire"));
                        Console.WriteLine("Job enqueued: " + result);
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