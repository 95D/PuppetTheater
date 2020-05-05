using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Agent;
using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Node;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class SequenceNodeTest
    {
        private Mock<BlackBoard> blackBoard;
        private Mock<AgentChannel> agentChannel;
        private Mock<BehaviorContext> bContext;
        private Mock<IBehaviorNode> failureNode1;
        private Mock<IBehaviorNode> failureNode2;
        private Mock<IBehaviorNode> successNode;

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
            failureNode1 = new Mock<IBehaviorNode>();
            failureNode2 = new Mock<IBehaviorNode>();
            successNode = new Mock<IBehaviorNode>();

            failureNode1.Setup(x => x.GetHashCode()).Returns("FailureNode1".GetHashCode());
            failureNode1.Setup(x => x.Execute(bContext.Object)).Returns(false);
            failureNode2.Setup(x => x.GetHashCode()).Returns("FailureNode2".GetHashCode());
            failureNode2.Setup(x => x.Execute(bContext.Object)).Returns(false);
            successNode.Setup(x => x.GetHashCode()).Returns("SuccessNode".GetHashCode());
            successNode.Setup(x => x.Execute(bContext.Object)).Returns(true);
        }

        [TestMethod]
        public void test_failed_at_first()
        {
            var testNode = new SequenceNode(
                "testNode",
                new List<IBehaviorNode>() {
                    failureNode1.Object,
                    failureNode2.Object
                });

            testNode.Execute(bContext.Object);

            failureNode1.Verify(x => x.Execute(bContext.Object));
            failureNode2.Verify(x => x.Execute(bContext.Object), Times.Never);
        }

        [TestMethod]
        public void test_failed_after_success()
        {
            var testNode = new SequenceNode(
                 "testNode",
                 new List<IBehaviorNode>() {
                    successNode.Object,
                    failureNode1.Object,
                    failureNode2.Object
                 });

            testNode.Execute(bContext.Object);

            successNode.Verify(x => x.Execute(bContext.Object));
            failureNode1.Verify(x => x.Execute(bContext.Object));
            failureNode2.Verify(x => x.Execute(bContext.Object), Times.Never);
        }
    }
}