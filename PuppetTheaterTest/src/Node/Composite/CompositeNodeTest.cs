using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class CompositeNodeTest
    {
        private List<Mock<BehaviorNode>> mockChildren;
        private List<BehaviorNode> testChildren
        {
            get => mockChildren.ConvertAll<BehaviorNode>(x => x.Object);
        }

        private Mock<IPuppetController> mockPuppetController;

        [TestInitialize]
        public void TestUp()
        {
            mockChildren = new List<Mock<BehaviorNode>> {
                new Mock<BehaviorNode>("test1"),
                new Mock<BehaviorNode>("test2"),
                new Mock<BehaviorNode>("test3")
            };

            mockPuppetController = new Mock<IPuppetController>();
        }

        [TestMethod]
        public void test_create_node_state_as_ready_ascending()
        {
            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.Ascending,
                CompositeCategory.select);
            var nodeState = testNode.CreateNodeStateAsReady() as AscentLoopNodeState;
            Assert.AreEqual(nodeState.size, 3);
            Assert.AreEqual(nodeState.nodeId, "test");
            Assert.AreEqual(nodeState.GetIndex(), 0);
            Assert.AreEqual(nodeState.lifeCycle, NodeLifeCycle.Ready);
        }

        [TestMethod]
        public void test_create_node_state_as_ready_ordered_random()
        {
            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.OrderedRandom,
                CompositeCategory.select);
            var nodeState = testNode.CreateNodeStateAsReady() as RandomOrderLoopNodeState;
            Assert.AreEqual(nodeState.permutation.Count, 3);
            Assert.AreEqual(nodeState.nodeId, "test");
            Assert.AreEqual(nodeState.GetIndex(), 0);
            Assert.AreEqual(nodeState.lifeCycle, NodeLifeCycle.Ready);
        }

        [TestMethod]
        public void test_create_node_state_as_ready_infinite_random()
        {
            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.InfiniteRandom,
                CompositeCategory.select);
            var nodeState = testNode.CreateNodeStateAsReady() as RandomPickLoopNodeState;
            Assert.AreEqual(nodeState.size, 3);
            Assert.AreEqual(nodeState.nodeId, "test");
            Assert.IsTrue(0 <= nodeState.GetIndex() && nodeState.GetIndex() < 3);
            Assert.AreEqual(nodeState.lifeCycle, NodeLifeCycle.Ready);
        }

        [TestMethod]
        public void test_traverse_up_break()
        {
            var testCategory = new CompositeCategory(
                breakState: NodeLifeCycle.Success,
                finallyState: NodeLifeCycle.Failed);

            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.Ascending,
                testCategory);

            var mockParentStates = new Dictionary<string, Mock<CompositeNodeState>>
            {
                ["current"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running),
                ["to_next_index"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running),
                ["update_state"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Success)
            };
            mockParentStates["current"]
                .Setup(x => x.ToNextIndex())
                .Returns(mockParentStates["to_next_index"].Object);
            mockParentStates["to_next_index"]
                .Setup(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Success))
                .Returns(mockParentStates["update_state"].Object);

            var mockChildState = new Mock<NodeState>("child", NodeLifeCycle.Success);
            var dummyTraversalState = new TraversalState(mockParentStates["current"].Object);
            var resultTraversalState = testNode.TraverseUp(
                "test_puppet",
                mockPuppetController.Object,
                dummyTraversalState,
                mockChildState.Object);

            mockParentStates["current"].Verify(x => x.ToNextIndex());
            mockParentStates["to_next_index"]
                .Verify(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Success));

            Assert.AreEqual("parent", resultTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Success, resultTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_up_finally()
        {
            var testCategory = new CompositeCategory(
                breakState: NodeLifeCycle.Success,
                finallyState: NodeLifeCycle.Failed);

            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.Ascending,
                testCategory);

            var mockParentStates = new Dictionary<string, Mock<CompositeNodeState>>
            {
                ["current"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running),
                ["to_next_index"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running),
                ["update_state"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Failed)
            };
            mockParentStates["current"]
                .Setup(x => x.ToNextIndex())
                .Returns(mockParentStates["to_next_index"].Object);
            mockParentStates["to_next_index"]
                .Setup(x => x.isIterationFinished())
                .Returns(true);
            mockParentStates["to_next_index"]
                .Setup(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Failed))
                .Returns(mockParentStates["update_state"].Object);

            var mockChildState = new Mock<NodeState>("child", NodeLifeCycle.Failed);
            var dummyTraversalState = new TraversalState(mockParentStates["current"].Object);
            var resultTraversalState = testNode.TraverseUp(
                "test_puppet",
                mockPuppetController.Object,
                dummyTraversalState,
                mockChildState.Object);

            mockParentStates["current"].Verify(x => x.ToNextIndex());
            mockParentStates["to_next_index"].Verify(x => x.isIterationFinished());
            mockParentStates["to_next_index"]
                .Verify(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Failed));

            Assert.AreEqual("parent", resultTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Failed, resultTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_up_next()
        {
            var testCategory = new CompositeCategory(
                breakState: NodeLifeCycle.Success,
                finallyState: NodeLifeCycle.Failed);

            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.Ascending,
                testCategory);

            var mockParentStates = new Dictionary<string, Mock<CompositeNodeState>>
            {
                ["current"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running),
                ["to_next_index"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running)
            };
            mockParentStates["current"]
                .Setup(x => x.ToNextIndex())
                .Returns(mockParentStates["to_next_index"].Object);
            mockParentStates["to_next_index"]
                .Setup(x => x.isIterationFinished())
                .Returns(false);
            mockParentStates["to_next_index"]
                .Setup(x => x.GetIndex())
                .Returns(1);

            var mockChildStates = new Dictionary<string, Mock<NodeState>>
            {
                ["finished_child"] = new Mock<NodeState>("child", NodeLifeCycle.Failed),
                ["next_child"] = new Mock<NodeState>("next_child", NodeLifeCycle.Ready)
            };

            mockChildren[1]
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(mockChildStates["next_child"].Object);

            var dummyTraversalState = new TraversalState(mockParentStates["current"].Object);
            var resultTraversalState = testNode.TraverseUp(
                "test_puppet",
                mockPuppetController.Object,
                dummyTraversalState,
                mockChildStates["finished_child"].Object);

            mockParentStates["current"].Verify(x => x.ToNextIndex());
            mockParentStates["to_next_index"].Verify(x => x.isIterationFinished());
            mockChildren[1].Verify(x => x.CreateNodeStateAsReady());

            Assert.AreEqual("next_child", resultTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, resultTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_traverse_down()
        {
            var testCategory = new CompositeCategory(
                breakState: NodeLifeCycle.Success,
                finallyState: NodeLifeCycle.Failed);

            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.Ascending,
                testCategory);

            var mockParentStates = new Dictionary<string, Mock<CompositeNodeState>>
            {
                ["current"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Ready),
                ["update_state"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running)
            };
            mockParentStates["current"]
                .Setup(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Running))
                .Returns(mockParentStates["update_state"].Object);

            mockParentStates["current"]
                .Setup(x => x.GetIndex())
                .Returns(0);

            var mockChildStates = new Dictionary<string, Mock<NodeState>>
            {
                ["next_child"] = new Mock<NodeState>("next_child", NodeLifeCycle.Ready)
            };

            mockChildren[0]
                .Setup(x => x.CreateNodeStateAsReady())
                .Returns(mockChildStates["next_child"].Object);

            var dummyTraversalState = new TraversalState(mockParentStates["current"].Object);
            var resultTraversalState = testNode.TraverseDown(
                "test_puppet",
                mockPuppetController.Object,
                dummyTraversalState,
                0);
            
            mockParentStates["current"]
                .Verify(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Running));
            mockChildren[0].Verify(x => x.CreateNodeStateAsReady());

            Assert.AreEqual("next_child", resultTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Ready, resultTraversalState.currentNodeState.lifeCycle);
        }

        [TestMethod]
        public void test_execute_internal()
        {
            var testCategory = new CompositeCategory(
                breakState: NodeLifeCycle.Success,
                finallyState: NodeLifeCycle.Failed);

            var testNode = new CompositeNode(
                "test",
                testChildren,
                PermutateCategory.Ascending,
                testCategory);

            var mockParentStates = new Dictionary<string, Mock<CompositeNodeState>>
            {
                ["current"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running),
                ["update_state"] = new Mock<CompositeNodeState>("parent", NodeLifeCycle.Running)
            };
            mockParentStates["current"]
                .Setup(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Running))
                .Returns(mockParentStates["update_state"].Object);


            var dummyTraversalState = new TraversalState(mockParentStates["current"].Object);

            var resultTraversalState = testNode.Execute(
                "test_puppet",
                mockPuppetController.Object,
                dummyTraversalState,
                0);

            mockParentStates["current"]
                .Verify(x => x.UpdateCurrentLifeCycle(NodeLifeCycle.Running));
            
            Assert.AreEqual("parent", resultTraversalState.currentNodeState.nodeId);
            Assert.AreEqual(NodeLifeCycle.Running, resultTraversalState.currentNodeState.lifeCycle);
        }
    }
}
