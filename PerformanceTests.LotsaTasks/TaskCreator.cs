using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceTests.LotsaTasks
{
    public static class TaskCreator
    {
        public static IEnumerable<Task> Create(ulong n)
        {
            for (var i = 0ul; i < n; i++)
            {
                yield return Task.Delay(1);
            }
        }
    }
}
