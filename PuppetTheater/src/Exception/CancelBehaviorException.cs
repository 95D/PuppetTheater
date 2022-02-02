using Viento.PuppetTheater.Node;

namespace Viento.PuppetTheater.Exception
{
    /// <summary>
    /// This class is exception class which occur at canceling behavior tree process. 
    /// </summary>
    public class CancelBehaviorException : System.Exception
    { 
        public enum Cause
        {
            Command = 0,
            Timeout = 1,
            Hopout = 2
        }

        public CancelBehaviorException(
            string puppetId,
            TraversalState traversalState,
            Cause cause
            ) : base(GetExceptionMessage(puppetId, traversalState, cause))
        {}

        private static string GetExceptionMessage(
                string puppetId,
                TraversalState traversalState,
                Cause cause
            ) => string.Format(
                    "{0}'s Behavior tree is cancelled. CAUSE:[{1}].\n{2}", 
                    puppetId, cause.ToString(), traversalState.GetStackTrace());
    }
}
