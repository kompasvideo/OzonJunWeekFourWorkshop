namespace CustomTaskScheduler;

public class SingleThreadSyncContext : SynchronizationContext
{
    private SingleThreadTaskScheduler _scheduler;

    public SingleThreadSyncContext(SingleThreadTaskScheduler scheduler)
    {
        _scheduler = scheduler;
    }
    
    public override void Post(SendOrPostCallback d, object? state)
    {
        Task.Factory.StartNew(() => d(state),
            CancellationToken.None,
            TaskCreationOptions.None,
            _scheduler);
    }
}