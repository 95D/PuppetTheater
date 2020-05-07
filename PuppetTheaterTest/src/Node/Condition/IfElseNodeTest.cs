using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Agent;
using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Node;

namespace Viento.PuppetTheaterTest.Node.Condition
{
    [TestClass]
    public class IfElseNodeTest
    {
        private Mock<BlackBoard> blackBoard;
        private Mock<AgentChannel> agentChannel;
        private Mock<BehaviorContext> bContext;
        private Mock<IBehaviorNode> thenNode;
        private Mock<IBehaviorNode> elseNode;

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
            elseNode = new Mock<IBehaviorNode>();

            thenNode.Setup(x => x.GetHashCode()).Returns("thenNode".GetHashCode());
            //thenNode.Setup(x => x.Execute(bContext.Object)).Returns(true);
            elseNode.Setup(x => x.GetHashCode()).Returns("elseNode".GetHashCode());
            //elseNode.Setup(x => x.Execute(bContext.Object)).Returns(true);
        }

        [TestMethod]
        public void test_if_then()
        {
            var testNode = new IfElseNode(
                "testNode",
                new OnCheckCondition(context => { return true; }),
                thenNode.Object,
                elseNode.Object
                );
            testNode.Execute(bContext.Object);

            thenNode.Verify(x => x.Execute(bContext.Object));
            elseNode.Verify(x => x.Execute(bContext.Object), Times.Never);
        }

        [TestMethod]
        public void test_if_else()
        {
            var testNode = new IfElseNode(
                "testNode",
                new OnCheckCondition(context => { return false; }),
                thenNode.Object,
                elseNode.Object
                );
            testNode.Execute(bContext.Object);

            thenNode.Verify(x => x.Execute(bContext.Object), Times.Never);
            elseNode.Verify(x => x.Execute(bContext.Object));
        }
    }
}
