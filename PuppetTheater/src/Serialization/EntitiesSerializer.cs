using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Viento.PuppetTheater.Serialization
{
    public class EntitiesSerializer
    {
        public List<ActionEntity> actionList = new List<ActionEntity>();
        public List<FocusEntity> focusList = new List<FocusEntity>();
        public List<TriggerEntity> triggerList = new List<TriggerEntity>();
        public List<RandomSelectEntity> randomSelectList = new List<RandomSelectEntity>();
        public List<SelectEntity> selectList = new List<SelectEntity>();
        public List<SequenceEntity> sequenceList = new List<SequenceEntity>();
        public List<IfElseEntity> ifElseList = new List<IfElseEntity>();
        public List<IfEntity> ifList = new List<IfEntity>();
        public List<UntilEntity> untilList = new List<UntilEntity>();
        public List<ForceFailureEntity> forceFailureList = new List<ForceFailureEntity>();
        public List<ForceSuccessEntity> forceSuccessList = new List<ForceSuccessEntity>();
        public List<InvertEntity> invertList = new List<InvertEntity>();
        public List<IterateEntity> iterateList = new List<IterateEntity>();
        public List<StochasticEntity> stochasticList = new List<StochasticEntity>();

        public static string Serialize(EntitiesSerializer serializer)
        {
            return JsonConvert.SerializeObject(serializer);
        }

        public static EntitiesSerializer Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json) as EntitiesSerializer;
        }

        public Dictionary<int, IBehaviorNode> BuildBranchMap(BlackBoard blackBoard)
        {
            // build branch
            var branches = new Dictionary<int, IBehaviorNode>();
            var symbolTable = CreateSymbolTable();

            int total = 0;
            while (symbolTable.entityMap.Keys.Count > 0)
            {
                Console.WriteLine("Loop: " + symbolTable.entityMap.Keys.Count);
                int affect = 0;
                foreach (var key in symbolTable.entityMap.Keys)
                {
                    Console.WriteLine(key + ": " + symbolTable.prerequisiteMap[key]);
                    if (symbolTable.prerequisiteMap[key] == 0)
                    {
                        Console.WriteLine(key + " update.");
                        var node = CreateNode(symbolTable.entityMap[key], blackBoard, branches);
                        branches.Add(node.GetHashCode(), node);
                        foreach (var edge in symbolTable.entryOrderMap[key])
                        {
                            Console.WriteLine(key + " created => sub prerequisite of " + edge);
                            symbolTable.prerequisiteMap[edge] -= 1;
                        }
                        symbolTable.entityMap.Remove(key);
                        affect += 1;
                    }
                }
                if (affect == 0)
                    throw new InvalidOperationException("Build ");
            }

            return branches;
        }

        private IBehaviorNode CreateNode(object entity, BlackBoard blackBoard, Dictionary<int, IBehaviorNode> branches)
        {
            return When<object>.Start(entity, (expected, real) => expected.Equals(real.GetType()))
                .CaseThenAs<ActionEntity>(typeof(ActionEntity), et => new ActionNode(
                    name: et.name,
                    eventId: et.eventId
                    ))
                .CaseThenAs<FocusEntity>(typeof(FocusEntity), et => new FocusNode(
                    name: et.name,
                    onFocus: blackBoard.GetOnFocus(et.filterId, et.priorityId),
                    count: et.count
                    ))
                .CaseThenAs<TriggerEntity>(typeof(TriggerEntity), et => new TriggerNode(
                    name: et.name,
                    agentId: et.agentId,
                    eventId: et.eventId
                    ))
                .CaseThenAs<RandomSelectEntity>(typeof(RandomSelectEntity), et => new RandomSelectNode(
                    name: et.name,
                    children: (from child in et.children select branches[child.GetHashCode()]).ToList()
                    ))
                .CaseThenAs<SelectEntity>(typeof(SelectEntity), et => new SelectNode(
                    name: et.name,
                    children: (from child in et.children select branches[child.GetHashCode()]).ToList()
                    ))
                .CaseThenAs<SequenceEntity>(typeof(SequenceEntity), et => new SequenceNode(
                    name: et.name,
                    children: (from child in et.children select branches[child.GetHashCode()]).ToList()
                    ))
                .CaseThenAs<IfEntity>(typeof(IfEntity), et => new IfNode(
                    name: et.name,
                    onCheckCondition: blackBoard.GetOnCheckCondition(et.conditionFuncName),
                    thenChild: branches[et.thenChild.GetHashCode()]
                    ))
                .CaseThenAs<IfElseEntity>(typeof(IfElseEntity), et => new IfElseNode(
                    name: et.name,
                    onCheckCondition: blackBoard.GetOnCheckCondition(et.conditionFuncName),
                    thenChild: branches[et.thenChild.GetHashCode()],
                    elseChild: branches[et.elseChild.GetHashCode()]
                    ))
                .CaseThenAs<UntilEntity>(typeof(UntilEntity), et => new UntilNode(
                    name: et.name,
                    onCheckCondition: blackBoard.GetOnCheckCondition(et.conditionFuncName),
                    thenChild: branches[et.thenChild.GetHashCode()]
                    ))
                /*TODO: Decorate node*/
                .End() as IBehaviorNode;
        }

        // Tuple: T1: Entity object, T2: prerequisite count, T3: entry indices
        private SymbolTable CreateSymbolTable()
        {
            var symbolTable = new SymbolTable();

            // Action Node (Leaf Node)

            foreach (var entity in actionList)
            {
                symbolTable.AddEntity(
                    key: entity.name.GetHashCode(),
                    entity: entity,
                    children: new List<int>());
            }

            foreach (var entity in focusList)
            {
                symbolTable.AddEntity(
                    key: entity.name.GetHashCode(),
                    entity: entity,
                    children: new List<int>());
            }

            foreach (var entity in triggerList)
            {
                symbolTable.AddEntity(
                    key: entity.name.GetHashCode(),
                    entity: entity,
                    children: new List<int>());
            }

            // Cmposite Node

            foreach (var entity in randomSelectList)
            {
                symbolTable.AddEntity(
                       key: entity.name.GetHashCode(),
                       entity: entity,
                       children: (from x in entity.children select x.GetHashCode()).ToList());
            }

            foreach (var entity in selectList)
            {
                symbolTable.AddEntity(
                       key: entity.name.GetHashCode(),
                       entity: entity,
                       children: (from x in entity.children select x.GetHashCode()).ToList());
            }

            foreach (var entity in sequenceList)
            {
                symbolTable.AddEntity(
                       key: entity.name.GetHashCode(),
                       entity: entity,
                       children: (from x in entity.children select x.GetHashCode()).ToList());
            }

            // Condition Node

            foreach (var entity in ifList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { entity.thenChild.GetHashCode() });
            }

            foreach (var entity in ifElseList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { 
                              entity.thenChild.GetHashCode(), entity.elseChild.GetHashCode()
                          });
            }

            foreach (var entity in untilList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { entity.thenChild.GetHashCode() });
            }

            // Decorate Node

            foreach (var entity in forceFailureList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { entity.child.GetHashCode() });
            }

            foreach (var entity in forceSuccessList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { entity.child.GetHashCode() });
            }

            foreach (var entity in invertList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { entity.child.GetHashCode() });
            }

            foreach (var entity in iterateList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { entity.child.GetHashCode() });
            }

            foreach (var entity in stochasticList)
            {
                symbolTable.AddEntity(
                          key: entity.name.GetHashCode(),
                          entity: entity,
                          children: new List<int>() { entity.child.GetHashCode() });
            }

            return symbolTable;
        }

        protected class SymbolTable
        {
            public readonly Dictionary<int, object> entityMap = new Dictionary<int, object>();
            public readonly Dictionary<int, List<int>> entryOrderMap = new Dictionary<int, List<int>>();
            public readonly Dictionary<int, int> prerequisiteMap = new Dictionary<int, int>();

            public SymbolTable() { }

            public SymbolTable(
                Dictionary<int, object> entityMap,
                Dictionary<int, List<int>> entryOrderMap,
                Dictionary<int, int> prerequisiteMap)
            {
                this.entityMap = entityMap;
                this.entryOrderMap = entryOrderMap;
                this.prerequisiteMap = prerequisiteMap;
            }

            public void AddEntity(int key, object entity, List<int> children)
            {
                entityMap.Add(key, entity);
                prerequisiteMap[key] = children.Count;
                if (!entryOrderMap.ContainsKey(key))
                    entryOrderMap[key] = new List<int>();
                foreach(int child in children)
                {
                    if(!entryOrderMap.ContainsKey(child))
                    {
                        entryOrderMap[child] = new List<int>();
                    }
                    entryOrderMap[child].Add(key);
                }
            }
        }
    }
}
