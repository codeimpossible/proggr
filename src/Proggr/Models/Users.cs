using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proggr.Models
{
    public class Users : ModelBase
    {
        public Users() 
        {
            PrimaryKeyField = "id";
        }
    }
}