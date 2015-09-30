using Akka.Actor;
using System;
using System.Reflection;

namespace CSMicroServiceWithAkka2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var system = ActorSystem.Create("MySystem");
            var server = system.ActorOf<HTTPServer>("server");
            server.Tell(new Start());
            Console.Read();
            */
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Assembly assembly = Assembly.LoadFrom(assemblyName+ ".exe");

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass == true)
                {
                    Console.WriteLine(type.FullName);
                }
            }
            Console.Read();
            throw (new System.Exception("could not invoke method"));

        }
    }
}
