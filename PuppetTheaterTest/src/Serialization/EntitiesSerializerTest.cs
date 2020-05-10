using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Viento.PuppetTheater.API;
using Viento.PuppetTheater.Serialization;

namespace Viento.PuppetTheaterTest.Serialization
{
    [TestClass]
    public class EntitiesSerializerTest
    {
        private Mock<BlackBoard> blackBoard;
        private ActionEntity attackEntity = new ActionEntity("attackNode", "attack");
        private ActionEntity skillEntity = new ActionEntity("skillNode", "skill");
        private IfElseEntity choiceEntity = new IfElseEntity(
            name: "checkMana",
            conditionFuncName: "mana100",
            thenChild: "skillNode",
            elseChild: "attackNode");
        private int mana = 50;

        [TestInitialize]
        public void TestUp()
        {
            blackBoard = new Mock<BlackBoard>();

            blackBoard.Setup(
                x => x.GetOnCheckCondition("mana100")
                ).Returns(new OnCheckCondition(context => mana > 100));

            blackBoard.Setup(
                x => x.GetOnCheckCondition("mana30")
                ).Returns(new OnCheckCondition(context => mana > 30));
        }

        [TestMethod]
        public void test_build_branch_map()
        {
            EntitiesSerializer serializer = new EntitiesSerializer();
            serializer.actionList.Add(attackEntity);
            serializer.actionList.Add(skillEntity);
            serializer.ifElseList.Add(choiceEntity);

            var branches = serializer.BuildBranchMap(blackBoard.Object);
            Assert.AreEqual(3, branches.Count);
        }
        
        [TestMethod]
        public void test_serialization()
        {
            EntitiesSerializer serializer = new EntitiesSerializer();
            serializer.actionList.Add(attackEntity);
            serializer.actionList.Add(skillEntity);
            serializer.ifElseList.Add(choiceEntity);

            string json = EntitiesSerializer.Serialize(serializer);
            Console.WriteLine(json);
            EntitiesSerializer clone = EntitiesSerializer.Deserialize(json);
            Assert.IsTrue(serializer.EqualsData(clone));
        }
    }
}
