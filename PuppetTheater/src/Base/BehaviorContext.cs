using System.Collections.Generic;
using System.Diagnostics;
using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Agent;
using Viento.PuppetTheater.Exception;

namespace Viento.PuppetTheater.Base
{
    /// <summary>
    /// This class is context for processing behavior tree.
    /// </summary>
    public class BehaviorContext
    {
        // for global scope
        public readonly BlackBoard blackBoard;
        public readonly AgentChannel agentChannel;

        // for local scope
        public readonly string agentId;
        readonly Stack<string> callStack = new Stack<string>();
        public readonly bool isTimeLimit = false;
        public readonly float timeout = 0;
        public readonly bool isHopLimit = false;
        public readonly int hopout = 0;
        public readonly Stopwatch timer;

        int hop = 0;

        public BehaviorContext(
            BlackBoard blackBoard,
            AgentChannel agentChannel,
            string agentId,
            bool isTimeLimit = false,
            float timeout = 0,
            bool isHopLimit = false,
            int hopout = 0
            )
        {
            this.blackBoard = blackBoard;
            this.agentChannel = agentChannel;
            this.agentId = agentId;
            this.isTimeLimit = isTimeLimit;
            this.timeout = timeout;
            this.isHopLimit = isHopLimit;
            this.hopout = hopout;
        }

        public void OnExecuteStart(string behaviorId)
        {
            callStack.Push(behaviorId);
            
            if (isTimeLimit)
                timer.Start();
        }

        public void OnExecuteEnd()
        {
            callStack.Pop();
            
            if(isTimeLimit)
            {
                timer.Stop();
                if (timer.Elapsed.TotalMilliseconds >= timeout)
                    throw new CancelBehaviorException(this, CancelBehaviorException.Cause.Timeout);
            }
            
            if (isHopLimit)
            {
                hop += 1;
                if (hop >= hopout)
                    throw new CancelBehaviorException(this, CancelBehaviorException.Cause.Hopout);
            }
        }

        public string GetStackTrace()
        {
            var callArray = new string[callStack.Count];
            callStack.CopyTo(callArray, 0);
            string answer = "[";
            for(int i=callArray.Length-1;i >=0;i--)
            {
                answer += callArray[i];
                if (i != 0)
                    answer += " > ";
            }
            answer += "]";
            return answer;
        }
    }
}
