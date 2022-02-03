using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json;
using Viento.PuppetTheater.Node;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// A Reader class to make json blocks into tree consisted by [BehaviorNode]
    /// </summary>
    public class TreeDataReader
    {
        private const string JSON_FORMAT = "*.json";
        public Dictionary<string, BehaviorNode> ReadData(string directoryPath) {
            var jsonBlocks = ReadJsonFiles(directoryPath);
            var entityMap = (IReadOnlyDictionary<string, INodeEntity>) ParseJsonData(jsonBlocks);
            return BuildTreeData(entityMap);
        }

        private ImmutableList<string> ReadJsonFiles(string directoryPath)
        {
            var nextJsonBlocks = ImmutableList<string>.Empty;
            var directories = Directory.GetDirectories(directoryPath);
            foreach(var directory in directories)
            {
                nextJsonBlocks = nextJsonBlocks.AddRange(ReadJsonFiles(directory));
            }
            var files = Directory.GetFiles(directoryPath, JSON_FORMAT);
            var jsonBlocksInCurrentDir = files.Select(file => File.ReadAllText(file));
            nextJsonBlocks = nextJsonBlocks.AddRange(jsonBlocksInCurrentDir);
            return nextJsonBlocks;
        }

        private Dictionary<string, INodeEntity> ParseJsonData(ImmutableList<string> jsonObjects)
        {
            var nodeEntityConverter = new NodeEntityConverter();
            return jsonObjects.Select(json => JsonConvert
                .DeserializeObject<INodeEntity>(json, nodeEntityConverter))
                .ToDictionary(node => node.nodeId);
        }

        private Dictionary<string, BehaviorNode> BuildTreeData(
            IReadOnlyDictionary<string, INodeEntity> entityMap)
            {
                Dictionary<string, BehaviorNode> result = new Dictionary<string, BehaviorNode>();
                foreach(var entityPair in entityMap) {
                    var entity = entityPair.Value;
                    if(!result.ContainsKey(entity.nodeId)) {
                        entity.AccountBehaviorNode(result, entityMap);
                    }
                }
                return result;
            }
    }
}