using System.Collections.Generic;
using System.Web.Mvc;
using Proggr.Controllers.Filters;
using Proggr.Controllers.Responses;
using Simple.Data;
using Simple.Data.Mocking;
using Xunit;

namespace Proggr.Tests.Controllers.Filters
{
    public class MustBeValidWorkerTests
    {
        ActionExecutingContext _context = new ActionExecutingContext
        {
            ActionParameters = new Dictionary<string, object>()
        };

        public MustBeValidWorkerTests()
        {
            MockHelper.UseMockAdapter( new InMemoryAdapter() );

            Database.Open().Workers.Insert( new { id = "valid", user_id = 1 } );
        }

        [Fact]
        public void Test_OnActionExecuting_InvalidWorker_Returns403Response()
        {
            _context.ActionParameters.Add( "worker_id", "invalid" );

            var filter = new MustBeValidWorker();

            filter.OnActionExecuting( _context );

            Assert.IsType<Http403Response>( _context.Result );
        }

        [Fact]
        public void Test_OnActionExecuting_ValidWorker_ResultIsNull()
        {
            _context.ActionParameters.Add( "worker_id", "valid" );

            var filter = new MustBeValidWorker();

            filter.OnActionExecuting( _context );

            Assert.Null( _context.Result );
        }
    }
}
