using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Viento.PuppetTheater.Exception;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Puppet;

namespace Viento.PuppetTheaterTest.Node
{
    [TestClass]
    public class CancelNodeTest
    {
        private Mock<IPuppetController> mockPuppetController;

        [TestInitialize]
        public void TestUp()
        {
            mockPuppetController = new Mock<IPuppetController>();
        }

        [TestMethod]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void test_traverse_up()
        {
            var dummyNode = new CancelNode("test_node");

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
        [ExpectedException(typeof(CancelBehaviorException))]
        public void test_traverse_down()
        {
            var dummyNode = new CancelNode("test_node");

            var dummyParentState = new TimeoutNodeState("parent", NodeLifeCycle.Running);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.TraverseDown(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState, 
                123456789);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void test_execute()
        {
            var dummyNode = new CancelNode("test_node");

            var dummyParentState = new TimeoutNodeState("parent", NodeLifeCycle.Running);

            var traversalState = new TraversalState(dummyParentState);

            var nextTraversalState = dummyNode.Execute(
                "test_puppet", 
                mockPuppetController.Object, 
                traversalState, 
                123456789);
        }
    }
}
