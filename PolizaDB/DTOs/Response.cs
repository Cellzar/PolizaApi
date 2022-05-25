using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolizaDB.DTOs
{
    public class Response
    {
        public string Mensaje { get; set; }
        public bool EsError { get; set; }
        public object Data { get; set; }
    }
}
