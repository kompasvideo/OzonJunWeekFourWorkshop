using System.Collections.Concurrent;
using System.Text;

namespace CustomTaskScheduler;

public class SingleThreadTaskScheduler :TaskScheduler, IDisposable
{
    private BlockingCollection<Task> _queue = new();
    private Thread _thread;

    public SingleThreadTaskScheduler()
    {
        _thread = new Thread(Run)
        {
            // IsBackground = true,
            Name = "Lonely Hardworker"
        };
        _thread.Start();
    }

    private void Run()
    {
        while (!_queue.IsCompleted)
        {
            if (_queue.TryTake(out Task task))
                TryExecuteTask(task);
        }
    }

    protected override IEnumerable<Task>? GetScheduledTasks() => _queue.ToArray();

    protected override void QueueTask(Task task)
    {
        _queue.Add(task);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) => true;

    public void Dispose()
    {
       _queue.CompleteAdding();
       if (Thread.CurrentThread.ManagedThreadId != _thread.ManagedThreadId)
          _thread.Join();
    }
}