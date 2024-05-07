using System.Collections.Generic;

namespace RubiconMp.UnitTests.Utility.Extencioms
{
    public static class IEnumerableExtencions
    {
        public static async IAsyncEnumerable<T> AsAsyncEnumerable2<T>(this IEnumerable<T> input)
        {
            foreach (var value in input)
            {
                yield return value;
            }
        }
    }
}
