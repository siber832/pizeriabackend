using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria
{
    public class JSONResponse
    {
        public uint code = 0;
        public string message = "";

        public JSONResponse(uint code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}
