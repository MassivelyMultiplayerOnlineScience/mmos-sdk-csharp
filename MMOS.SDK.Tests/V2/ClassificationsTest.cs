using System.Dynamic;
using Xunit;
using Xunit.Abstractions;

namespace MMOS.SDK.Tests.V2 {

    public class ClassificationsTest {

        private readonly ITestOutputHelper output;

        private Api api = new Api(Config.ApiConfig);

        public ClassificationsTest(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public async System.Threading.Tasks.Task TestClassificationCreate() {
            string playerCode = Config.Player + "classification";
            dynamic bodyTask = new ExpandoObject();
            bodyTask.projects = new string[] { Config.Project };
            bodyTask.player = new ExpandoObject();
            bodyTask.player.accountCode = Config.PlayerAccountCode;

            dynamic responseTask = await api.V2.Players.CreateTask(playerCode, bodyTask);

            long taskId = (long)responseTask.body.task.id;
            
            dynamic body = new ExpandoObject();
            body.player = playerCode;
            body.playergroup = Config.PlayerGroup;
            body.task = new ExpandoObject();
            body.task.id = taskId;
            body.task.result = Config.Result;
            body.circumstances = Config.Circumstances;

            dynamic response = await api.V2.Classifications.Create(body);

            Assert.Equal(Config.Game, (string)response.body.game);
            Assert.Equal(Config.Project, (string)response.body.task.project);
            Assert.Equal(playerCode, (string)response.body.player.code);
        }
    }

}
