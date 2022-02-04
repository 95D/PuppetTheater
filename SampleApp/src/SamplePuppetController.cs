using System.Collections.Generic;
using System.Linq;
using Viento.PuppetTheater.Node;
using Viento.PuppetTheater.Puppet;
public class SamplePuppetController : IPuppetController
{
    private readonly SampleMachine sampleMachine = new SampleMachine();
    private Dictionary<string, SampleState> stateMap;

    public SamplePuppetController(Dictionary<string, SampleState> stateMap)
    {
        this.stateMap = stateMap;
    }
    public bool Assert(string puppetId, string assertionId) =>
        sampleMachine.Assert(assertionId, stateMap[puppetId]);

    public void CancelAction(string puppetId, string actionId)
    {
        throw new System.NotImplementedException();
    }

    public ActionLifeCycle GetCurrentActionState(string puppetId, string actionId) =>
        sampleMachine.GetCurrentActionState(stateMap[puppetId]);

    public ActionLifeCycle RequestAction(string puppetId, string actionId) =>
        sampleMachine.RequestAction(actionId, stateMap[puppetId]);
}