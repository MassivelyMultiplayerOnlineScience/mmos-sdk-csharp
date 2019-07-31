using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MMOS.SDK.Tests {

    public class ApiTest {

        private readonly ITestOutputHelper output;

        private Api api = new Api(Config.ApiConfig);

        public ApiTest(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public async Task TestInfo() {            
            dynamic response = await api.Info();

            Assert.Equal("mmos-api-2", (string)response.body.name);
            Assert.True((long)response.body.stats.uptime > 1);
            Assert.Equal("depo", (string)response.body.stats.nodeEnv);
        }
    }

}
