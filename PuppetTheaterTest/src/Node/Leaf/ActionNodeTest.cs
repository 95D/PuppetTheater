using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class ActionNodeTest
    {
        private Mock<IPuppetController> mockPuppetController;

        [TestInitialize]
        public void TestUp()
        {
            mockPuppetController = new Mock<IPuppetController>();
        }

        [TestMethod]
        public void test_create_node_state_as_ready()
        {
            var dummyNode = new ActionNode(
                nodeId: "test_node",
                actionId: "test_action",
                timeoutDurationMillis: 3000);

            var state = dummyNode.CreateNodeStateAsReady() as TimeoutNodeState;
            Assert.AreEqual("test_node", state.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, state.lifeCycle);
            Assert.AreEqual(-1, state.startMillis);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void test_traverse_up()
        {
            var dummyNode = new ActionNode(
                nodeId: "test_node",
                actionId: "test_action",
                timeoutDurationMillis: 3000);

            var dummyParentState = new TimeoutNodeState("parent", NodeLifeCycle.Running);
            var dummyChildState = new BasicNodeState("child", NodeLifeCycle.Success);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseUp(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState, 
                dummyChildState);
        }

        [TestMethod]
        public void test_traverse_down()
        {
            mockPuppetController
                .Setup(x => x.RequestAction("test_puppet", "test_action"))
                .Returns(ActionLifeCycle.Success);

            var dummyNode = new ActionNode(
                nodeId: "test_node",
                actionId: "test_action",
                timeoutDurationMillis: 3000);

            var dummyParentState = new TimeoutNodeState("parent", NodeLifeCycle.Ready);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseDown(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState,
                123456789);

            mockPuppetController.Verify(x => x.RequestAction("test_puppet", "test_action"));

            var currentNodeState = nextTraversalState.currentNodeState as TimeoutNodeState;
            Assert.AreEqual("parent", currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Success, currentNodeState.lifeCycle);
            Assert.AreEqual(123456789, currentNodeState.startMillis);
        }

        [TestMethod]
        public void test_execute_running()
        {
            mockPuppetController
                .Setup(x => x.GetCurrentActionState("test_puppet", "test_action"))
                .Returns(ActionLifeCycle.Running);
            
            var dummyNode = new ActionNode(
                nodeId: "test_node",
                actionId: "test_action",
                timeoutDurationMillis: 3000);

            var dummyParentState = new TimeoutNodeState("parent", NodeLifeCycle.Ready)
                .Start(123456789, NodeLifeCycle.Running);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.Execute(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState,
                123456789);

            mockPuppetController.Verify(x => x.GetCurrentActionState("test_puppet", "test_action"));

            Assert.AreEqual("parent", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Running, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_execute_finish()
        {
            mockPuppetController
                .Setup(x => x.GetCurrentActionState("test_puppet", "test_action"))
                .Returns(ActionLifeCycle.Success);

            var dummyNode = new ActionNode(
                nodeId: "test_node",
                actionId: "test_action",
                timeoutDurationMillis: 3000);

            var dummyParentState = new TimeoutNodeState("parent", NodeLifeCycle.Ready)
                .Start(123456789, NodeLifeCycle.Running);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.Execute(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState,
                123456789);

            mockPuppetController.Verify(x => x.GetCurrentActionState("test_puppet", "test_action"));

            Assert.AreEqual("parent", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Success, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_execute_timeout()
        {
            mockPuppetController
                .Setup(x => x.RequestAction("test_puppet", "test_action"))
                .Returns(ActionLifeCycle.Success);
            
            var dummyNode = new ActionNode(
                nodeId: "test_node",
                actionId: "test_action",
                timeoutDurationMillis: 3000);

            var dummyParentState = new TimeoutNodeState("parent", NodeLifeCycle.Ready)
                .Start(100000000, NodeLifeCycle.Running);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.Execute(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState,
                123456789);


            mockPuppetController.Verify(x => x.CancelAction("test_puppet", "test_action"));

            Assert.AreEqual("parent", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Success, nextTraversalState.currentNodeState.lifeCycle);
        }
    }
}
