namespace Viento.PuppetTheater.Serialization {
    public class NodeEntityCreator {
        public INodeEntity Create(string nodeType) {
            switch(nodeType) {
                // Leaf
                case ActionEntity.TYPE:
                    return new ActionEntity();

                // Composite
                case CompositeEntity.TYPE:
                    return new CompositeEntity();
                
                // Condition
                case IfEntity.TYPE:
                    return new IfEntity();
                case IfElseEntity.TYPE:
                    return new IfElseEntity();
                case UntilEntity.TYPE:
                    return new UntilEntity();
                
                // Decorate
                case ForceFailureEntity.TYPE:
                    return new ForceFailureEntity();
                case ForceSuccessEntity.TYPE:
                    return new ForceSuccessEntity();
                case InvertEntity.TYPE:
                    return new InvertEntity();
                case RepeatEntity.TYPE:
                    return new RepeatEntity();
                case StochasticEntity.TYPE:
                    return new StochasticEntity();

                // Etc 
                case SubTreeEntity.TYPE:
                    return new SubTreeEntity();
            }
            throw new System.ApplicationException(
                string.Format("The given vehicle type {0} is not supported!", nodeType));
        }
    }
}