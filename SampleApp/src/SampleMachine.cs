using System;
using Viento.PuppetTheater.Node;
public class SampleMachine
{
    private Random random = new Random();
    private const string TAG_COULD_BUY_ITEM = "could-buy-item";
    private const string TAG_SIT_UP = "sit-up";
    private const string TAG_BUY_ITEM = "buy-item";
    private const string TAG_SIT_DOWN = "sit-down";
    public bool Assert(string assertionId, SampleState state)
    {
        Console.WriteLine(String.Format("checks {0}", assertionId));
        switch (assertionId)
        {
            case TAG_COULD_BUY_ITEM:
                return AssertCouldByItem(state);
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
            case TAG_SIT_UP:
                StartActSitUp(state);
                break;
            case TAG_BUY_ITEM:
                StartBuyItem(state);
                break;
            case TAG_SIT_DOWN:
                StartSitDown(state);
                break;
            default:
                break;
        }
        return ActionLifeCycle.Start;
    }

    private void StartActSitUp(SampleState state)
    {
        state.currentAction = TAG_SIT_UP;
        state.actionDuration = 3;
    }

    private void StartBuyItem(SampleState state)
    {
        state.currentAction = TAG_BUY_ITEM;
        state.actionDuration = 5;
    }

    private void FinishBuyItem(SampleState state)
    {
        Console.WriteLine("Finish buy item");
        var items = new string[] { "Coke", "OrangeJuice", "Coffee" };
        var costs = new int[] { 150, 200, 300 };
        var itemIndex = random.Next(0, 3);
        state.money -= costs[itemIndex];
        state.items.Add(items[itemIndex]);
    }

    private void StartSitDown(SampleState state)
    {
        state.currentAction = TAG_SIT_DOWN;
        state.actionDuration = 2;
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
            case TAG_BUY_ITEM:
                FinishBuyItem(state);
                break;
            default:
                break;
        }
        state.currentAction = "";
    }
}