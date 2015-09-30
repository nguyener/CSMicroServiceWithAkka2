using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CSMicroServiceWithAkka2
{
    class Templates: IRESTCollection
    {
        HttpListenerContext context;
        public Templates(HttpListenerContext context)
        {
            this.context = context;
        }

        public void ProcessGET(string id)
        {
            var db = new TemplateFactory();
            List<Template> templates = db.Templates.ToList();
            string responseString;

            if (id == "" || id == null)
            {
                //query for all templates
                responseString = JsonConvert.SerializeObject(templates);
                CreateResponse(responseString, 200);
            }
            else
            {
                var template = templates.Find(tmpl => tmpl.TemplateId == Int32.Parse(id));

                if (template == null)
                {
                    responseString = "Could not found specify template";
                    CreateResponse(responseString, 400);
                }
                else
                {
                    responseString = JsonConvert.SerializeObject(template);
                    CreateResponse(responseString, 200);
                }

            }
        }


        public void ProcessPOST(string data)
        {
            // Obtain a response object.
            var template = JsonConvert.DeserializeObject<Template>(data);
            var db = new TemplateFactory();
            db.Templates.Add(template);
            db.SaveChanges();
            CreateResponse("New template has been added", 201);


        }

        public void ProcessPUT(string data, string id)
        {
            //check for input id
            if (id == "" || id == null)
            {
                CreateResponse("Bad Request. Id is not specified ", 400);
                return;
            }

            //loading up the collection and look for specified item
            var db = new TemplateFactory();
            List<Template> templates = db.Templates.ToList();

            var template = templates.Find(tmpl => tmpl.TemplateId == Int32.Parse(id));
            if (template == null)
            {
                CreateResponse("Specified item is not found", 404);
                return;
            }


            var item_input = JsonConvert.DeserializeObject<Template>(data);
            template.Name = item_input.Name;
            db.SaveChanges();
            CreateResponse("Specified template has been updated", 200);
        }

        public void ProcessDELETE(string id)
        {
            //check for input id
            if (id == "" || id == null)
            {
                CreateResponse("Bad Request. Id is not specified ", 400);
                return;
            }

            //loading up the collection and look for specified item
            var db = new TemplateFactory();
            List<Template> templates = db.Templates.ToList();

            var template = templates.Find(tmpl => tmpl.TemplateId == Int32.Parse(id));
            if (template == null)
            {
                CreateResponse("Specified item is not found", 404);
                return;
            }
            db.Templates.Remove(template);
            db.SaveChanges();
            CreateResponse("Specified template has been deleted", 200);
        }

        public void CreateResponse(string responseString, int return_code)
        {
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            context.Response.StatusCode = return_code;

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
    }


}
