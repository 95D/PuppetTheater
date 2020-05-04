using Viento.PuppetTheater.Base;

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
            BehaviorContext context,
            Cause cause
            ) : base("Behavior tree is cancelled. CAUSE:[" + cause.ToString() + "].\n" +
                context.GetStackTrace())
        { }
    }
}
