using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceTests.LotsaTasks
{
    /// <remarks>
    /// Using code examples from Mikhail Shilkov's 
    /// <see href="https://mikhail.io/2020/09/how-to-drain-dotnet-tasks-to-completion/">blog</see>.
    /// </remarks>
    internal class TaskContainer
    {
        private readonly HashSet<Task> _taskCollection = new HashSet<Task>();

        // NOTE: We are using bool as a throwaway type (since it's 1 byte)
        private readonly TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>(TaskCreationOptions.AttachedToParent);

        private int RemainingTasks => _taskCollection.Count;

        public TaskContainer(IEnumerable<Task> tasks)
        {
            foreach (var task in tasks)
            {
                this.RegisterTask(task);
            }
        }

        private void RegisterTask(Task task)
        {
            lock (_taskCollection)
            {
                if (!_taskCollection.Contains(task))
                {
                    _taskCollection.Add(task);
                }
            }

            HandleCompletion(task);
        }

        private async void HandleCompletion(Task task)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                _tcs.TrySetCanceled();
            }
            catch (Exception ex)
            {
                _tcs.TrySetException(ex);
            }
            finally
            {
                lock (_taskCollection)
                {
                    _taskCollection.Remove(task);

                    if (this.RemainingTasks == 0)
                    {
                        _tcs.TrySetResult(true);
                    }
                }
            }
        }

        public async Task CompleteTask() => await _tcs.Task;
    }
}
