using System;
using System.Collections.Generic;
using Viento.PuppetTheater.Serialization;
using Viento.PuppetTheater.Puppet;
using Viento.PuppetTheater.Tree;
class SampleApplication
{
    static void Main(string[] args)
    {
        var reader = new TreeDataReader();
        var treeData = reader.ReadData("rsc");
        foreach (var nodePair in treeData)
        {
            Console.WriteLine(String.Format("{0}:{1}", nodePair.Key, nodePair.Value));
        }
        var tree = new BehaviorTree(treeData);
        var puppet = new Puppet(tree, "sample", "1");

        var stateMap = new Dictionary<string, SampleState> {
            { puppet.puppetId, new SampleState() }
        };
        var controller = new SamplePuppetController(stateMap);
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine("Start > " + puppet.traversalState.GetStackTrace());
            puppet.RequestAction(controller, 0);
            stateMap[puppet.puppetId].PrintCurrentState();
            Console.WriteLine("End > " + puppet.traversalState.GetStackTrace());
        }
    }
}