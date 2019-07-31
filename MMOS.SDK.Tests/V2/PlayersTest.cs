using System.Dynamic;
using Xunit;
using Xunit.Abstractions;

namespace MMOS.SDK.Tests.V2 {

    public class PlayersTest {

        private readonly ITestOutputHelper output;

        private Api api = new Api(Config.ApiConfig);

        public PlayersTest(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPlayerGet() {
            dynamic response = await api.V2.Players.Get(Config.Player, null);

            Assert.Equal(Config.Game, (string)response.body.game);
            Assert.Equal(Config.Player, (string)response.body.player);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPlayerGetWithProject() {
            dynamic response = await api.V2.Players.Get(Config.Player, Config.Project);

            Assert.Equal(Config.Game, (string)response.body.game);
            Assert.Equal(Config.Player, (string)response.body.player);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPlayerCreateTask() {
            dynamic body = new ExpandoObject();
            body.projects = new string[] { Config.Project };
            body.player = new ExpandoObject();
            body.player.accountCode = Config.PlayerAccountCode;
           
            dynamic response = await api.V2.Players.CreateTask(Config.Player, body);
            Assert.Equal(Config.Game, (string)response.body.game);
            Assert.Equal(Config.Project, (string)response.body.task.project);
            Assert.Equal(Config.Player, (string)response.body.player.code);
        }
    }

}
