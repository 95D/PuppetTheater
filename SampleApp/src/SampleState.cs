using System;
using System.Collections.Generic;
public class SampleState
{
    public string currentAction = "";
    public int actionDuration = 0;
    public int money = 1000;
    public List<string> items = new List<string>();

    public void PrintCurrentState() {
        Console.WriteLine("Current state)");
        Console.WriteLine(string.Format(
            "action: {0}[{1}], money: {2}", 
            currentAction, 
            actionDuration, 
            money));
        Console.WriteLine("Items)");
        items.ForEach(Console.WriteLine);
        Console.WriteLine("");
    }
}