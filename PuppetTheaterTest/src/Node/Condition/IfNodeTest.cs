using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Agent;
using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Node;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class IfNodeTest
    {
        private Mock<BlackBoard> blackBoard;
        private Mock<AgentChannel> agentChannel;
        private Mock<BehaviorContext> bContext;
        private Mock<IBehaviorNode> thenNode;

        [TestInitialize]
        public void TestUp()
        {
            blackBoard = new Mock<BlackBoard>();
            agentChannel = new Mock<AgentChannel>();
            bContext = new Mock<BehaviorContext>(
                blackBoard.Object,
                agentChannel.Object,
                "testBehaviorContext",
                /* isTimeLimit */ false, /* timeout */ 0,
                /* isHopLimit */ false, /* hopout */ 0
            );
            thenNode = new Mock<IBehaviorNode>();

            thenNode.Setup(x => x.GetHashCode()).Returns("SuccessNode".GetHashCode());
            thenNode.Setup(x => x.Execute(bContext.Object)).Returns(true);
        }

        [TestMethod]
        public void test_if_then()
        {
            var testNode = new IfNode(
                "testNode",
                new OnCheckCondition(context => { return true; }),
                thenNode.Object
                );
            testNode.Execute(bContext.Object);

            thenNode.Verify(x => x.Execute(bContext.Object));
        }

        [TestMethod]
        public void test_if_else()
        {
            var testNode = new IfNode(
                "testNode",
                new OnCheckCondition(context => { return false; }),
                thenNode.Object
                );
            testNode.Execute(bContext.Object);

            thenNode.Verify(x => x.Execute(bContext.Object), Times.Never);
        }
    }
}
