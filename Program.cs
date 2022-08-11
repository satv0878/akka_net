// See https://aka.ms/new-console-template for more information
using Akka.Actor;
using akka_net;

namespace AkkaNet
{
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {


            MyActorSystem = ActorSystem.Create("MyActorSystem");

            // IActorRef is a reference to an Actor 






            
            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");


            Props validationActorProps = Props.Create(
                () => new ValidationActor(consoleWriterActor));
            IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps, "validationActor");


            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");



            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);


            MyActorSystem.WhenTerminated.Wait();


        }
    }
}