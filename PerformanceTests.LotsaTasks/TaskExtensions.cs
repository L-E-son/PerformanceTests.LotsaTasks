using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceTests.LotsaTasks
{
    public static class TaskExtensions
    {
        public static async Task DrainTasks(this IEnumerable<Task> tasks)
        {
            var taskList = tasks.ToList();

            while (taskList.Count > 0)
            {
                var completedTask = await Task.WhenAny(taskList);
                taskList.Remove(completedTask);
            }
        }
    }
}
