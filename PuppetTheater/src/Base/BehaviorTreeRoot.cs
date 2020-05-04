using System;
using System.Collections.Generic;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Exception;
using Viento.PuppetTheater.Agent;
using Viento.PuppetTheater.API;

namespace Viento.PuppetTheater.Base
{
    public class BehaviorTreeRoot
    {
        private readonly Dictionary<int, IBehaviorNode> branchMap;
        readonly Configuration configuration;
        readonly BlackBoard blackBoard;
        readonly AgentChannel agentChannel;

        public BehaviorTreeRoot(
            Dictionary<int, IBehaviorNode> branchMap,
            Configuration configuration, 
            BlackBoard blackBoard, 
            AgentChannel agentChannel
            ) {
            this.branchMap = branchMap;
            this.configuration = configuration;
            this.blackBoard = blackBoard;
            this.agentChannel = agentChannel;
        }

        public void Execute(int treeId, string agentId)
        {
            try
            {
                branchMap[treeId].Execute(new BehaviorContext(
                    blackBoard: blackBoard,
                    agentChannel: agentChannel,
                    agentId: agentId,
                    isTimeLimit: configuration.isTimeLimit,
                    timeout: configuration.timeout,
                    isHopLimit: configuration.isHopLimit,
                    hopout: configuration.hopout
                    ));
            }
            catch(CancelBehaviorException)
            {
                // Nothing reaction because it is canceled.
            }
        }
    }
}
