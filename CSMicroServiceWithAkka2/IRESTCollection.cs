using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMicroServiceWithAkka2
{
    interface IRESTCollection
    {
        void ProcessGET(String id);
        void ProcessPOST(String data);
        void ProcessPUT(String data, String id);
        void ProcessDELETE(String id);

        void CreateResponse(String response, int return_code);
    }
}
