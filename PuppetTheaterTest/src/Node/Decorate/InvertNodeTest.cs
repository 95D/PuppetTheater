using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Agent;
using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Node;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class InvertNodeTest
    {
        private Mock<BlackBoard> blackBoard;
        private Mock<AgentChannel> agentChannel;
        private Mock<BehaviorContext> bContext;
        private Mock<IBehaviorNode> successNode;
        private Mock<IBehaviorNode> failureNode;

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
            successNode = new Mock<IBehaviorNode>();
            failureNode = new Mock<IBehaviorNode>();

            successNode.Setup(x => x.GetHashCode()).Returns("SuccessNode".GetHashCode());
            successNode.Setup(x => x.Execute(bContext.Object)).Returns(true);
            failureNode.Setup(x => x.GetHashCode()).Returns("FailureNode".GetHashCode());
            failureNode.Setup(x => x.Execute(bContext.Object)).Returns(false);
        }

        [TestMethod]
        public void test_success()
        {
            var testNode = new InvertNode(
                "testNode",
                successNode.Object);

            Assert.AreEqual(testNode.Execute(bContext.Object), false);

            successNode.Verify(x => x.Execute(bContext.Object));
        }

        [TestMethod]
        public void test_failure()
        {
            var testNode = new InvertNode(
                "testNode",
                failureNode.Object);

            Assert.AreEqual(testNode.Execute(bContext.Object), true);

            failureNode.Verify(x => x.Execute(bContext.Object));
        }
    }
}
