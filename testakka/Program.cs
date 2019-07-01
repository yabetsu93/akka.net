using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;

namespace testakka
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"configuration {
                akka {
                   io {
                       pinned-dispatcher {
                            type = PinnedDispatcher
                       }
                   }
                }
            }");
            
            using (var system = ActorSystem.Create("system", config.GetConfig("configuration")))
            {
                var delay = TimeSpan.FromSeconds(5);
                var router = system.ActorOf(Props.Create(() => new TaskScheduleActor())
                   .WithRouter(new RoundRobinPool(2)).WithDispatcher("akka.io.pinned-dispatcher"));
                var cancel = system.Scheduler.ScheduleTellRepeatedlyCancelable(delay, delay, router, new Messages(), ActorRefs.NoSender);

                Console.ReadLine();
            }
        }
    }
}
