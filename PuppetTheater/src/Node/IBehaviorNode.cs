using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viento.PuppetTheater.Base;

namespace Viento.PuppetTheater
{
    /// <summary>
    /// This interface is for the BehaviorNode class.
    /// </summary>
    public interface IBehaviorNode
    {
        string behaviorId { get; }
        bool Execute(BehaviorContext context);
    }
}
