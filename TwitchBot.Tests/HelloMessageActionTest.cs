using TwitchBot.Services.Services.CommandServiceAction;

namespace TwitchBot.Tests
{
    public class HelloMessageActionTest
    {
        [Theory]
        [InlineData("Bonjour",true)]
        [InlineData("Bonjour ",true)]
        [InlineData(" Bonjour ",true)]
        [InlineData("salut",true)]
        [InlineData("HelLo",true)]
        [InlineData("chi",false)]
        [InlineData("hihello", false)]
        [InlineData("Bon jour", false)]
        public void IsConcern_Test(string message,bool result)
        {
            HelloMessageAction action = new HelloMessageAction();


            Assert.Equal(result, action.IsConcern(message));
        }




    }
}