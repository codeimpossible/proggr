using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proggr.Models
{
    public class Users : Massive.DynamicModel
    {
        public Users() 
            : base( "Database" )
        {
            PrimaryKeyField = "id";
        }
    }
}