namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A static model for setting category of composite
    /// </summary>
    public class CompositeCategory
    {
        public readonly NodeLifeCycle finallyState;
        public readonly NodeLifeCycle breakState;
        private CompositeCategory(NodeLifeCycle finallyState, NodeLifeCycle breakState)
        {
            this.finallyState = finallyState;
            this.breakState = breakState;
        }
        /// <summary>
        /// Select category: Iterate until child's result is succeeded 
        /// </summary>
        public static CompositeCategory Select = new CompositeCategory(
                finallyState: NodeLifeCycle.Failed,
                breakState: NodeLifeCycle.Success);

        /// <summary>
        /// Sequnece category: Iterate until child's result is failed 
        /// </summary>
        public static CompositeCategory Sequence =
            new CompositeCategory(finallyState: NodeLifeCycle.Success, breakState: NodeLifeCycle.Failed);
    }
}