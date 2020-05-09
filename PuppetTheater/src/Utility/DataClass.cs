using PuppetTheater.src.Utility.Statements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Viento.PuppetTheater.Utility
{
    /// <summary>
    /// this class is base class of data class (plain object, entity, etc..)
    /// </summary>
    /// <typeparam name="T">Concrete data class type</typeparam>
    public class DataClass<T> : IEquatable<T>
    {
        private readonly Lazy<string> cacheString;

        public DataClass()
        {
            cacheString = new Lazy<string>(BuildString);
        }

        public bool Equals(T other)
        {
            if (GetType() != other.GetType())
                return false;

            if (other == null)
                return false;

            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return cacheString.Value;
        }

        private string BuildString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetType().ToString());
            sb.Append("(");
            foreach (var field in GetType().GetFields())
            {
                if (field.FieldType.IsList())
                    sb.Append(
                        field.Name +
                        "[" +
                        (field.GetValue(this) as IList).ToStringChildren() +
                        "] ");
                else
                    sb.Append(field.Name + " = " + field.GetValue(this).ToString() + " ");
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
