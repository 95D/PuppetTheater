using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class ForceSuccessNodeTest
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

            var dummyNode = new ForceSuccessNode(
                "test_node",
                mockChild.Object);

            var state = dummyNode.CreateNodeStateAsReady();
            Assert.AreEqual("test_node", state.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, state.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_up()
        {
            var mockChild = new Mock<BehaviorNode>("child");
            mockChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("child", NodeLifeCycle.Ready));

            var dummyNode = new ForceSuccessNode(
                "test_node",
                mockChild.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Running);
            var dummyChildState = new BasicNodeState("child", NodeLifeCycle.Success);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseUp(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState, 
                dummyChildState);
            
            Assert.AreEqual(traversalState, nextTraversalState);
        }

        [TestMethod]
        public void test_traverse_down()
        {
            var mockChild = new Mock<BehaviorNode>("child");
            mockChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("child", NodeLifeCycle.Ready));

            var dummyNode = new ForceSuccessNode(
                "test_node",
                mockChild.Object);

            var mockParentStates = new Dictionary<string, Mock<NodeState>>
            {
                ["parent"] = new Mock<NodeState>("parent", NodeLifeCycle.Ready),
                ["next_parent"] = new Mock<NodeState>("parent", NodeLifeCycle.Success)
            };
            mockParentStates["parent"]
                .Setup(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Success))
                .Returns(mockParentStates["next_parent"].Object);

            var traversalState = new TraversalState(mockParentStates["parent"].Object);

            var nextTraversalState = dummyNode.TraverseDown(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState, 
                123456789);

            mockParentStates["parent"].Verify(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Success));
            
            Assert.AreEqual("child", nextTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, nextTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void test_execute()
        {
            var mockChild = new Mock<BehaviorNode>("then_child");

            var dummyNode = new ForceSuccessNode(
                "test_node",
                mockChild.Object);

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
