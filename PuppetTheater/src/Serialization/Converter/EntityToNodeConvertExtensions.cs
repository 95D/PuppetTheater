using Viento.PuppetTheater.Node;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// A logic for mapping [INodeEntity] to [BehaviorNode]
    /// </summary>
    public static class EntityToNodeConvertExtensions
    {
        private static BehaviorNode InsertToTree(
            this BehaviorNode node,
            Dictionary<string, BehaviorNode> tree)
        {
            if (!tree.ContainsKey(node.nodeId))
            {
                tree[node.nodeId] = node;
            }
            return node;
        }

        private static ActionNode ToActionNode(this ActionEntity entity) => new ActionNode(
            nodeId: entity.nodeId,
            actionId: entity.actionId,
            timeoutDurationMillis: entity.timeoutDurationMillis);

        private static IfNode ToIfNode(
            this IfEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new IfNode(
                nodeId: entity.nodeId,
                assertionId: entity.assertionId,
                thenChild: entity.thenChild.AccountBehaviorNode(tree, entityMap));

        private static IfElseNode ToIfElseNode(
            this IfElseEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new IfElseNode(
                nodeId: entity.nodeId,
                assertionId: entity.assertionId,
                thenChild: entity.thenChild.AccountBehaviorNode(tree, entityMap),
                elseChild: entity.elseChild.AccountBehaviorNode(tree, entityMap));

        private static UntilNode ToUntilNode(
            this UntilEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new UntilNode(
                nodeId: entity.nodeId,
                assertionId: entity.assertionId,
                untilChild: entity.thenChild.AccountBehaviorNode(tree, entityMap));

        private static CompositeNode ToCompositeNode(
            this CompositeEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new CompositeNode(
                nodeId: entity.nodeId,
                category: CompositeCategory.From(entity.category),
                permuateType: (PermutateCategory)Enum.Parse(
                    typeof(PermutateCategory),
                    entity.permutate),
                children: entity.children
                    .Select(child => child.AccountBehaviorNode(tree, entityMap))
                    .ToList());

        private static ForceFailureNode ToForceFailureNode(
            this ForceFailureEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new ForceFailureNode(
                nodeId: entity.nodeId,
                child: entity.child.AccountBehaviorNode(tree, entityMap));

        private static ForceSuccessNode ToForceSuccessNode(
            this ForceSuccessEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new ForceSuccessNode(
                nodeId: entity.nodeId,
                child: entity.child.AccountBehaviorNode(tree, entityMap));

        private static InvertNode ToInvertNode(
            this InvertEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new InvertNode(
                nodeId: entity.nodeId,
                child: entity.child.AccountBehaviorNode(tree, entityMap));

        private static RepeatNode ToIterateNode(
            this RepeatEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new RepeatNode(
                count: entity.count,
                nodeId: entity.nodeId,
                child: entity.child.AccountBehaviorNode(tree, entityMap));

        private static StochasticNode ToStochasticNode(
            this StochasticEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap) => new StochasticNode(
                nodeId: entity.nodeId,
                threshold: entity.threshold,
                child: entity.child.AccountBehaviorNode(tree, entityMap));

        private static BehaviorNode ToReferencingNode(
            this SubTreeEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap)
        {
            if (tree.ContainsKey(entity.refNodeId))
            {
                return tree[entity.refNodeId];
            }
            else
            {
                return entityMap[entity.refNodeId].AccountBehaviorNode(tree, entityMap);
            }
        }

        public static BehaviorNode AccountBehaviorNode(
            this INodeEntity entity,
            Dictionary<string, BehaviorNode> tree,
            IReadOnlyDictionary<string, INodeEntity> entityMap)
        {
            if (entity is ActionEntity)
            {
                return (entity as ActionEntity).ToActionNode().InsertToTree(tree);
            }
            else if (entity is IfEntity)
            {
                return (entity as IfEntity)
                    .ToIfNode(tree, entityMap)
                    .InsertToTree(tree);
            }
            else if (entity is IfElseEntity)
            {
                return (entity as IfElseEntity)
                    .ToIfElseNode(tree, entityMap)
                    .InsertToTree(tree);
            }
            else if (entity is UntilEntity)
            {
                return (entity as UntilEntity)
                    .ToUntilNode(tree, entityMap)
                    .InsertToTree(tree);
            }
            else if (entity is CompositeEntity)
            {
                return (entity as CompositeEntity)
                    .ToCompositeNode(tree, entityMap)
                    .InsertToTree(tree);
            }
            else if (entity is ForceFailureEntity)
            {
                return (entity as ForceFailureEntity)
                    .ToForceFailureNode(tree, entityMap)
                    .InsertToTree(tree);
            }
            else if (entity is ForceSuccessEntity)
            {
                return (entity as ForceSuccessEntity)
                    .ToForceSuccessNode(tree, entityMap)
                    .InsertToTree(tree);
            }
            else if (entity is InvertEntity)
            {
                return (entity as InvertEntity)
                    .ToInvertNode(tree, entityMap)
                    .InsertToTree(tree);
            }
            else if (entity is RepeatEntity)
            {
                return (entity as IfEntity).ToIfNode(tree, entityMap).InsertToTree(tree);
            }
            else if (entity is StochasticEntity)
            {
                return (entity as IfEntity).ToIfNode(tree, entityMap).InsertToTree(tree);
            }
            else if (entity is SubTreeEntity)
            {
                return (entity as SubTreeEntity).ToReferencingNode(tree, entityMap);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}