using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Agent;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class SelectNodeTest
    {
        private Mock<BlackBoard> blackBoard;
        private Mock<AgentChannel> agentChannel;
        private Mock<BehaviorContext> bContext;
        private Mock<IBehaviorNode> successNode1;
        private Mock<IBehaviorNode> successNode2;
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
            successNode1 = new Mock<IBehaviorNode>();
            successNode2 = new Mock<IBehaviorNode>();
            failureNode = new Mock<IBehaviorNode>();

            successNode1.Setup(x => x.GetHashCode()).Returns("SuccessNode1".GetHashCode());
            successNode1.Setup(x => x.Execute(bContext.Object)).Returns(true);
            successNode2.Setup(x => x.GetHashCode()).Returns("SuccessNode2".GetHashCode());
            successNode2.Setup(x => x.Execute(bContext.Object)).Returns(true);
            failureNode.Setup(x => x.GetHashCode()).Returns("FailureNode".GetHashCode());
            failureNode.Setup(x => x.Execute(bContext.Object)).Returns(false);
        }

        [TestMethod]
        public void test_success_at_first()
        {
            var testNode = new SelectNode(
                "testNode",
                new List<IBehaviorNode>() {
                    successNode1.Object,
                    successNode2.Object
                });

            testNode.Execute(bContext.Object);

            successNode1.Verify(x => x.Execute(bContext.Object));
            successNode2.Verify(x => x.Execute(bContext.Object), Times.Never);
        }

        [TestMethod]
        public void test_success_after_failed()
        {
            var testNode = new SelectNode(
                 "testNode",
                 new List<IBehaviorNode>() {
                    failureNode.Object,
                    successNode1.Object,
                    successNode2.Object
                 });

            testNode.Execute(bContext.Object);

            failureNode.Verify(x => x.Execute(bContext.Object));
            successNode1.Verify(x => x.Execute(bContext.Object));
            successNode2.Verify(x => x.Execute(bContext.Object), Times.Never);
        }
    }
}
