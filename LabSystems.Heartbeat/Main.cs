using LabSystems.Heartbeat.Tasks;
using System;
using LabSystems.Domain.Extensions;
using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;
using System.Diagnostics;

namespace LabSystems.Heartbeat
{
    class Program
    {
        static void Main(string[] args)
        {
            string keys = string.Join(",", new List<string>(SystemCategories.Categories.Keys));

            CommandLineApplication cli = new CommandLineApplication();

            CommandOption arg1 = cli.Option("-c | --category <value>", $"The category of the system. Options: {keys}", CommandOptionType.SingleValue);

            cli.HelpOption("-? | -h | --help");

            cli.OnExecute(() =>
            {
                SystemCategories.Category category = SystemCategories.Category.Unknown;

                Console.WriteLine("Heartbeating...");

                if(arg1.HasValue() && !SystemCategories.Categories.ContainsKey(arg1.Value()))
                {
                    if(!SystemCategories.Categories.ContainsKey(arg1.Value()))
                    {
                        category = SystemCategories.Categories[arg1.Value()];
                    }
                    else 
                    {
                        Console.WriteLine("Incorrect category specified!");
                        return -1;
                    }
                }

                UpdateSystemInfo update = new UpdateSystemInfo();
                if (update.Update(category))
                {
                    Console.WriteLine("Success!");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Fail...");
                    return -1;
                }
            });

            Stopwatch watch = new Stopwatch();

            watch.Start();
            cli.Execute(args);
            watch.Stop();

            Console.WriteLine("Time Taken to Heartbeat (seconds): " + (watch.ElapsedMilliseconds / 1000).ToString());
        }
    }
}
