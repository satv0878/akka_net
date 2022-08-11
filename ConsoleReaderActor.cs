using Akka.Actor;


namespace AkkaNet
{
    class ConsoleReaderActor : UntypedActor
    {
        private IActorRef _consoleWriterActor;

        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private readonly IActorRef _validationActor;

        public ConsoleReaderActor(IActorRef validationActor)
        {
            _validationActor = validationActor;
        }

        protected override void OnReceive(object message)
        {
            var read = Console.ReadLine();

            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }

            GetAndValidateInput();

        }
        #region Internal methods
        private void DoPrintInstructions()
        {
            Console.WriteLine("Please provide the URI of a log file on disk.\n");

        }

        // <summary>
        /// Reads input from console, validates it, then signals appropriate response
        /// (continue processing, error, success, etc.).
        /// </summary>
        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message) &&
            String.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // shut down the entire actor system (allows the process to exit)
                Context.System.Terminate();
            }
            _validationActor.Tell(message);


        }
  
}
    }
#endregion