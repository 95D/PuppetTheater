using System.Collections;
using System.Text;

namespace Viento.PuppetTheater.Utility
{
    public static class ListStatementsExtension
    {
        public static string ToStringChildren(this IList list)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
                if (i == 0)
                    sb.Append(list[i].ToString());
                else
                    sb.Append(", " + list[i].ToString());
            return sb.ToString();
        }
    }
}
