using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Data.Models;

namespace Worker.Tests.Fixtures
{
    public static class CodeLocations
    {
        public static CodeLocation CodeLocationA = new CodeLocation()
        {
            Id = new Guid(),
            FullName = "test_proggr/testing",
            Name = "testing"
        };
    }
}
