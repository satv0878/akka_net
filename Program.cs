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




            // Program.Main
            // make tailCoordinatorActor
            Props tailCoordinatorProps = Props.Create(() => new TailCoordinatorActor());
            IActorRef tailCoordinatorActor = MyActorSystem.ActorOf(tailCoordinatorProps,
                "tailCoordinatorActor");

            // pass tailCoordinatorActor to fileValidatorActorProps (just adding one extra arg)
            Props fileValidatorActorProps = Props.Create(() =>
            new FileValidatorActor(consoleWriterActor, tailCoordinatorActor));
            IActorRef validationActor = MyActorSystem.ActorOf(fileValidatorActorProps,
                "validationActor");


            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");



            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);


            MyActorSystem.WhenTerminated.Wait();


        }
    }
}