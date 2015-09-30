using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CSMicroServiceWithAkka2
{
    public class Start { }

    public class Stop { }

    public class Process
    {
        public Process(HttpListenerContext ctx)
        {
            Context = ctx;
        }

        public HttpListenerContext Context { get; set; }
    }

}