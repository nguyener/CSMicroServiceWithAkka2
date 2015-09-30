using Akka.Actor;
using System;
using System.Net;

namespace CSMicroServiceWithAkka2
{
    public class HTTPServer : ReceiveActor
    {
        HttpListener listener;
        public HTTPServer()
        {

            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");

            Receive<Start>(message => startListener());
            Receive<Stop>(message => stopListener());
        }

        private void startListener()
        {
            listener.Start();
            Console.WriteLine("Start Listening...");

            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                (Context.ActorOf<HTTPProcessor>()).Tell(new Process(ctx));
            }

        }

        private void stopListener()
        {
            listener.Stop();
            Console.WriteLine("Stopp Listening ...");
        }

    }
}
