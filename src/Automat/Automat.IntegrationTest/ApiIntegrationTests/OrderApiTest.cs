using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.IntegrationTest.Fixtures.OrderApi;

namespace Automat.IntegrationTest.ApiIntegrationTests
{
    public class OrderApiTest : Fixtures.OrderApi.OrderApiIntegrationTestFixture
    {
        public OrderApiTest(OrderApiFactory fixture) : base(fixture)
        {

        }
    }
}
