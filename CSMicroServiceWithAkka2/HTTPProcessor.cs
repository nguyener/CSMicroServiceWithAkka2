using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using Akka;
using Akka.Actor;
using Newtonsoft.Json;
using System.IO;

namespace CSMicroServiceWithAkka2
{
    public class HTTPProcessor : ReceiveActor
    {

        HttpListenerContext listener_context;

        public HTTPProcessor()
        {
            Receive<Process>(message =>
            {
                this.listener_context = message.Context;
                ProcessRequest();
            });
        }

        public void ProcessRequest()
        {
            HttpListenerRequest request = listener_context.Request;
            string raw_url = request.RawUrl;
            if (raw_url.IndexOf("/index") == 0 || raw_url == "/")
            {
                DisplayIndex();
                return;
            }

            string id = "";
            string[] data = raw_url.Split('/');
            
            //Should load the class dynamically.  
            IRESTCollection rest_collection;
            if (data[1] == "Template")
            {
                rest_collection = new Templates(listener_context);
            }
            else {
                (new Templates(listener_context)).CreateResponse("specfied resource is not found", 400);
                return;
            }
            

            if (data.Length > 2)
            {
                id = data[2];
            }

            switch (request.HttpMethod.ToUpper())
            {
                case "GET":
                    rest_collection.ProcessGET(id);
                    break;
                case "POST":
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        var input = reader.ReadToEnd();
                        rest_collection.ProcessPOST(input);
                    }        
                    break;
                case "PUT":
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        var input = reader.ReadToEnd();
                        rest_collection.ProcessPUT(input, id);
                    }
                   
                    break;
                case "DELETE":
                    rest_collection.ProcessDELETE(id);
                    break;
                default:
                    rest_collection.CreateResponse("Method Not Allowed", 405);
                    break;
            }
        }

        public void DisplayIndex()
        {
            // Obtain a response object.
            HttpListenerResponse response = listener_context.Response;
            listener_context.Response.StatusCode = 200;

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("INDEX");
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

    }
}

