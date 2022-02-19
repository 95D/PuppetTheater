using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Puppet;
using System;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class StochasticNodeTest
    {
        private Mock<IPuppetController> mockPuppetController;
        private Mock<Random> mockRandom;

        [TestInitialize]
        public void TestUp()
        {
            mockPuppetController = new Mock<IPuppetController>();
            mockRandom = new Mock<Random>();
        }

        [TestMethod]
        public void test_create_node_state_as_ready()
        {
            var mockChild = new Mock<BehaviorNode>("child");

            var dummyNode = new StochasticNode(
                nodeId: "test_node",
                child: mockChild.Object,
                threshold: 1.0f,
                random: mockRandom.Object);

            var state = dummyNode.CreateNodeStateAsReady() as BasicNodeState;
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

            var dummyNode = new StochasticNode(
                nodeId: "test_node",
                child: mockChild.Object,
                threshold: 1.0f,
                random: mockRandom.Object);

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
        public void test_traverse_down_activate()
        {
            mockRandom
                .Setup(x => x.NextDouble())
                .Returns(1.0f);

            var mockChild = new Mock<BehaviorNode>("child");
            mockChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("child", NodeLifeCycle.Ready));

            var dummyNode = new StochasticNode(
                nodeId: "test_node",
                child: mockChild.Object,
                threshold: 0.5f,
                random: mockRandom.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Running);

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
        public void test_traverse_down_non_activate()
        {
            mockRandom
                .Setup(x => x.NextDouble())
                .Returns(0.0f);

            var mockChild = new Mock<BehaviorNode>("child");
            mockChild
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(new BasicNodeState("child", NodeLifeCycle.Ready));

            var dummyNode = new StochasticNode(
                nodeId: "test_node",
                child: mockChild.Object,
                threshold: 0.5f,
                random: mockRandom.Object);

            var dummyParentState = new BasicNodeState("parent", NodeLifeCycle.Running);

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

            var dummyNode = new StochasticNode(
                nodeId: "test_node",
                child: mockChild.Object,
                threshold: 1.0f,
                random: mockRandom.Object);

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
