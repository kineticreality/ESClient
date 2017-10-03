using System;
using System.IO;

namespace AccountBalance
{
    class Program
    {
        
        private static SessionStatsReadModel _sessionStatsRm;
        private static ConsoleView _consoleView;

        private const string StreamName = "Account";
        private const string ReadModelFile = "AccountCheckpoint.csv";

        static void Main()
        {
            Console.Write("Connecting to Event Store");

            EventStoreLoader.SetupEventStore();

			var privateCopy = Guid.NewGuid() + ".csv";
			if (File.Exists(ReadModelFile))
				File.Copy(ReadModelFile, privateCopy);

			_consoleView = new ConsoleView();


			_sessionStatsRm = new SessionStatsReadModel(_consoleView);

			

			if (File.Exists(privateCopy))
			{
				File.Copy(privateCopy, ReadModelFile, true);
				File.Delete(privateCopy);
			}


        }

    }
}
