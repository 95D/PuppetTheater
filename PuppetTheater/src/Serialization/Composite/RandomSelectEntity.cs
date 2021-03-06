﻿using System.Collections.Generic;
using Viento.PuppetTheater.Utility;

namespace Viento.PuppetTheater.Serialization
{
    /// <summary>
    /// This class data entity class for RandomSelectNode.
    /// </summary>
    public class RandomSelectEntity : DataClass<RandomSelectEntity>
    {
        public readonly string name;
        public readonly List<string> children;

        public RandomSelectEntity(string name, List<string> children)
        {
            this.name = name;
            this.children = children;
        }
    }
}
