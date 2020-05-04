using System.Collections.Immutable;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater.API
{
    /// <summary>
    /// <para>This delegate is used to check the condition in the condition node.</para>
    /// <para>The specific check function is implemented in BlackBoard.</para>
    /// </summary>
    /// <param name="context"> The status of the Behavior Tree. </param>
    /// <returns> Condition result. </returns>
    public delegate bool OnCheckCondition(BehaviorContext context);

    /// <summary>
    /// <para>This delegate is used to set the agent's focus for the agent to use when working on the process.</para>
    /// <para>The specific check function is implemented in BlackBoard.</para>
    /// </summary>
    /// <param name="count"> The number to decide how many people to focus</param>
    /// <returns> Focused agent Id list </returns>
    public delegate ImmutableList<int> OnFocus(int count);

    /// <summary>
    /// <para>This interface is used as a bridge. </para>
    /// <para>You can implement business logic (Condition, Focus filter, etc) used to perform BehaviorTree without changing PuppetTheater code. </para>
    /// </summary>    
    public interface BlackBoard
    {
        OnCheckCondition GetOnCheckCondition(string functionId);
        OnFocus GetOnFocus(string filterId, string priorityId);
    }
}
