using System;
using Hangfire;

namespace HangfireSample.Server
{
    internal class Program
    {
        private static void Main()
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireSample");

            using (new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}