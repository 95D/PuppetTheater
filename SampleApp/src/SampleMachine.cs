using System;
using Viento.PuppetTheater.Node;
public class SampleMachine
{
    public bool Assert(string assertionId, SampleState state)
    {
        Console.WriteLine(String.Format("checks {0}", assertionId));
        switch (assertionId)
        {
            default:
                throw new ArgumentException("Invalid assertion id");
        }
    }

    private bool AssertCouldByItem(SampleState state) =>
        state.money >= 200;

    public ActionLifeCycle RequestAction(string actionId, SampleState state)
    {
        Console.WriteLine(String.Format("act {0}", actionId));
        switch (actionId)
        {
            default:
                break;
        }
        return ActionLifeCycle.Start;
    }

    public ActionLifeCycle GetCurrentActionState(SampleState state)
    {
        Console.WriteLine(String.Format("check act state"));
        if (state.actionDuration > 0)
        {
            state.actionDuration -= 1;
            return ActionLifeCycle.Running;
        }
        else
        {
            FinishAction(state);
            return ActionLifeCycle.Success;
        }
    }

    private void FinishAction(SampleState state)
    {
        Console.WriteLine(string.Format("Finish action: {0}", state.currentAction));
        switch (state.currentAction)
        {
            default:
                break;
        }
        state.currentAction = "";
    }
}