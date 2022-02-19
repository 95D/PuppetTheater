using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class RepeatNodeTest
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
            var mockChild = new Mock<BehaviorNode>("child");

            var dummyNode = new RepeatNode(
                "test_node",
                mockChild.Object,
                10);

            var state = dummyNode.CreateNodeStateAsReady() as RepeatNodeState;
            Assert.AreEqual("test_node", state.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, state.lifeCycle);
            Assert.AreEqual(10, state.count);
            Assert.AreEqual(0, state.currentAttemptCount);
        }

        [TestMethod]
        public void test_traverse_up_loop()
        {
            var mockChild = new Mock<BehaviorNode>("child");
            mockChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("child", NodeLifeCycle.Ready));

            var dummyNode = new RepeatNode(
                "test_node",
                mockChild.Object,
                10);

            var dummyParentState = new RepeatNodeState(10, "parent", NodeLifeCycle.Running);
            var dummyChildState = new BasicNodeState("child", NodeLifeCycle.Success);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseUp(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState, 
                dummyChildState);
            
            Assert.AreEqual("child", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_up_finish()
        {
            var mockChild = new Mock<BehaviorNode>("child");
            mockChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("child", NodeLifeCycle.Ready));

            var dummyNode = new RepeatNode(
                "test_node",
                mockChild.Object,
                10);

            var dummyParentState = new RepeatNodeState(10, "parent", NodeLifeCycle.Running);
            for(int i=0;i<10;i++)
                dummyParentState = dummyParentState.ToNextCount();

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
            var mockChild = new Mock<BehaviorNode>("child");
            mockChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("child", NodeLifeCycle.Ready));

            var dummyNode = new RepeatNode(
                "test_node",
                mockChild.Object,
                10);

            var dummyParentState = new RepeatNodeState(10, "parent", NodeLifeCycle.Running);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseDown(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState,
                123456789);
            
            Assert.AreEqual("child", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_down_finish()
        {
            var mockChild = new Mock<BehaviorNode>("child");
            var dummyNode = new RepeatNode(
                "test_node",
                mockChild.Object,
                0);

            var dummyParentState = new RepeatNodeState(0, "parent", NodeLifeCycle.Running);

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
            var mockChild = new Mock<BehaviorNode>("then_child");

            var dummyNode = new RepeatNode(
                "test_node",
                mockChild.Object,
                10);

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
