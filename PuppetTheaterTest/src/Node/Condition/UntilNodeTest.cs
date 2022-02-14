using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Puppet;
using Viento.PuppetTheater.Node;

namespace BehaviorTreeTest.Node
{
    [TestClass]
    public class UntilNodeTest
    {
        private Mock<IPuppetController> mockPuppetController;

        [TestInitialize]
        public void TestUp()
        {
            mockPuppetController = new Mock<IPuppetController>();
        }

        [TestMethod]
        public void test_create_node_state_as_ready_ascending()
        {
            var mockThenChild = new Mock<BehaviorNode>("then_child");

            var dummyNode = new UntilNode(
                "test_node",
                "test_assertion",
                mockThenChild.Object);

            var state = dummyNode.CreateNodeStateAsReady();
            Assert.AreEqual("test_node", state.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, state.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_up_loop()
        {
            mockPuppetController
                .Setup(x => x.Assert("test_puppet", "test_assertion"))
                .Returns(true);

            var mockUntilChild = new Mock<BehaviorNode>("until_child");
            mockUntilChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("until_child", NodeLifeCycle.Ready));

            var dummyNode = new UntilNode(
                "test_node",
                "test_assertion",
                mockUntilChild.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Running);
            var dummyChildState = new BasicNodeState("child", NodeLifeCycle.Success);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseUp(
                "test_puppet",
                mockPuppetController.Object,
                traversalState,
                dummyChildState);

            mockUntilChild.Verify(x => x.CreateNodeStateAsReady());

            Assert.AreEqual("until_child", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_up_finish()
        {
            mockPuppetController
                .Setup(x => x.Assert("test_puppet", "test_assertion"))
                .Returns(false);

            var mockUntilChild = new Mock<BehaviorNode>("until_child");
            var dummyNode = new UntilNode(
                "test_node",
                "test_assertion",
                mockUntilChild.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Running);
            var dummyChildState = new BasicNodeState("child", NodeLifeCycle.Success);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseUp(
                "test_puppet",
                mockPuppetController.Object,
                traversalState,
                dummyChildState);

            Assert.AreEqual("parent", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Success, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_down_loop()
        {
            mockPuppetController
                .Setup(x => x.Assert("test_puppet", "test_assertion"))
                .Returns(true);

            var mockUntilChild = new Mock<BehaviorNode>("until_child");
            mockUntilChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("until_child", NodeLifeCycle.Ready));

            var dummyNode = new UntilNode(
                "test_node",
                "test_assertion",
                mockUntilChild.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Ready);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseDown(
                "test_puppet",
                mockPuppetController.Object,
                traversalState,
                123456789);

            mockUntilChild.Verify(x => x.CreateNodeStateAsReady());

            Assert.AreEqual("until_child", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_down_failed()
        {
            mockPuppetController
                .Setup(x => x.Assert("test_puppet", "test_assertion"))
                .Returns(false);

            var mockUntilChild = new Mock<BehaviorNode>("until_child");
            var dummyNode = new UntilNode(
                "test_node",
                "test_assertion",
                mockUntilChild.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Ready);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseDown(
                "test_puppet",
                mockPuppetController.Object,
                traversalState,
                123456789);

            Assert.AreEqual("parent", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Failed, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void test_execute()
        {
            var mockThenChild = new Mock<BehaviorNode>("until_child");

            var dummyNode = new UntilNode(
                "test_node",
                "test_assertion",
                mockThenChild.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Ready);

            var traversalState = new TraversalState(dummyParentState);

            dummyNode.Execute(
                "test_puppet",
                mockPuppetController.Object,
                traversalState,
                123456789);
        }
    }
}
