
using Akka.Actor;

namespace AkkaNet
{
    class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is Messages.InputError)
            {
                var msg = (Messages.InputError)message;
                Console.BackgroundColor= ConsoleColor.DarkRed; 
                Console.WriteLine(msg);
            }
            else if (message is Messages.InputSuccess)
            {
                var msg = (Messages.InputSuccess)message;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine(msg);
            }
            else
            {
                Console.WriteLine(message);
            }
            Console.ResetColor();
        }
    }
}