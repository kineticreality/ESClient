﻿using System;
using System.Net;
using System.Text;
using EventStore.ClientAPI;

namespace ESTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const string STREAM = "a_test_stream";
            const int DEFAULTPORT = 1113;

            var settings = ConnectionSettings.Create().EnableVerboseLogging().UseConsoleLogger();
        
            using (var conn = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, DEFAULTPORT)))
            {
                conn.ConnectAsync().Wait();
				for (var x = 0; x < 100; x++)
				{
					conn.AppendToStreamAsync(STREAM,
						ExpectedVersion.Any,
						GetEventDataFor(x)).Wait();
					Console.WriteLine("event " + x + " written.");
				}
            }
        }

        private static EventData GetEventDataFor (int i)
        {
            return new EventData(
                Guid.NewGuid(),
                "eventType",
                true,
                Encoding.ASCII.GetBytes("{'somedata' : " + i + "}"),
                Encoding.ASCII.GetBytes("{'metadata' : " + i + "}")
            );
        }
    }
}
