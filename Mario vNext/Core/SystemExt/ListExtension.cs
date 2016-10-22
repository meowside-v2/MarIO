using System.Collections.Generic;

namespace Mario_vNext.Core.SystemExt
{
    static class ListExt
    {
        public static void AddAll<T>(this List<T> list, params T[] stuff)
        {
            foreach (var item in stuff)
            {
                list.Add(item);
            }
        }
    }
}
