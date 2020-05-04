using System.Collections.Generic;
using Viento.PuppetTheater.Base;
using Viento.PuppetTheater.Agent;
using Viento.PuppetTheater.Node;

namespace Viento.PuppetTheater.API
{
    public class BehaviorTreeBuilder
    {
        private Configuration configuration = new Configuration();
        private BlackBoard blackBoard;
        private AgentChannel agentChannel;

        public BehaviorTreeBuilder(){}

        public BehaviorTreeBuilder SetBlackBoard(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
            return this;
        }

        public BehaviorTreeBuilder SetAgentChannel(AgentChannel agentChannel)
        {
            this.agentChannel = agentChannel;
            return this;
        }

        public BehaviorTreeBuilder SetConfiguration(Configuration configuration)
        {
            this.configuration = configuration;
            return this;
        }

        public BehaviorTreeBuilder SetConfiguration(string json)
        {
            this.configuration = Configuration.CreateInstance(json);
            return this;
        }

        public BehaviorTreeRoot BuildTree(Dictionary<int, IBehaviorNode> branchMap)
        {
            return new BehaviorTreeRoot(branchMap, configuration, blackBoard, agentChannel);
        }
    }
}
