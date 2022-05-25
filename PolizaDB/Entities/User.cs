using System;
using System.Collections.Generic;

#nullable disable

namespace PolizaDB.Context
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
