using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.IntegrationTest.Fixtures.ManagementApi;

namespace Automat.IntegrationTest.ApiIntegrationTests
{
    public class ManagementApiTest : Fixtures.ManagementApi.ManagementApiIntegrationTestFixture
    {
        public ManagementApiTest(ManagementApiFactory fixture) : base(fixture)
        {

        }
    }
}
