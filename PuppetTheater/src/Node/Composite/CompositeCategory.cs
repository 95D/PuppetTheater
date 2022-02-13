namespace Viento.PuppetTheater.Node
{
    /// <summary>
    /// A static model for setting category of composite
    /// </summary>
    public class CompositeCategory
    {
        public readonly NodeLifeCycle finallyState;
        public readonly NodeLifeCycle breakState;
        public CompositeCategory(NodeLifeCycle finallyState, NodeLifeCycle breakState)
        {
            this.finallyState = finallyState;
            this.breakState = breakState;
        }
        /// <summary>
        /// Select category: Iterate until child's result is succeeded 
        /// </summary>
        public static CompositeCategory select = new CompositeCategory(
                finallyState: NodeLifeCycle.Failed,
                breakState: NodeLifeCycle.Success);

        /// <summary>
        /// Sequnece category: Iterate until child's result is failed 
        /// </summary>
        public static CompositeCategory sequence =
            new CompositeCategory(finallyState: NodeLifeCycle.Success, breakState: NodeLifeCycle.Failed);

        private const string KEY_SELECT = "select";
        private const string KEY_SEQUENCE = "sequence";
        public static CompositeCategory From(string key)
        {
            switch (key)
            {
                case KEY_SELECT:
                    return select;
                case KEY_SEQUENCE:
                    return sequence;
            }
            throw new System.ApplicationException(
                string.Format("The given key for category {0} is not supported!", key));
        }
    }
}