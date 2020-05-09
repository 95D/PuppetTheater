using System;
using System.Collections.Generic;

namespace PuppetTheater.src.Utility.Statements
{
    public static class TypeStatementsExtension
    {
        public static bool IsList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
