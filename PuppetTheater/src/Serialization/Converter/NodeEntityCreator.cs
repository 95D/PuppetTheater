namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// A Creator for creating [INodeEntity] according to key [nodeType]
    /// </summary>
    public class NodeEntityCreator
    {
        public INodeEntity Create(string nodeType) => nodeType switch
        {
            // Leaf
            ActionEntity.TYPE => new ActionEntity(),

            // Composite
            CompositeEntity.TYPE => new CompositeEntity(),

            // Condition
            IfEntity.TYPE => new IfEntity(),
            IfElseEntity.TYPE => new IfElseEntity(),
            UntilEntity.TYPE => new UntilEntity(),

            // Decorate
            ForceFailureEntity.TYPE => new ForceFailureEntity(),
            ForceSuccessEntity.TYPE => new ForceSuccessEntity(),
            InvertEntity.TYPE => new InvertEntity(),
            RepeatEntity.TYPE => new RepeatEntity(),
            StochasticEntity.TYPE => new StochasticEntity(),

            // Etc 
            SubTreeEntity.TYPE => new SubTreeEntity(),
            _ => throw new System.ApplicationException(
                string.Format(
                    "The given vehicle type {0} is not supported!",
                    nodeType))
        };
    }
}