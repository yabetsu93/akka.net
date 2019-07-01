using Akka.Actor;
using Akka.Event;

public class TaskScheduleActor : ReceiveActor 
{
    private ILoggingAdapter log = Context.GetLogger();
    public TaskScheduleActor()
    {
        ReceiveAsync<Messages>(async message => 
        {
            log.Info("information needed here please try");
            var ex = new Execute();
            await ex.Run(message);
        });
        Receive<string> (msg =>
        {
            if (msg == "done")
            {
                log.Info("it has been done");
                Context.Stop(Self);
            }
        });
    }
}