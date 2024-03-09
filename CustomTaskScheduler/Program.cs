namespace CustomTaskScheduler;

class Program
{
    static async Task Main(string[] args)
    {
        // using var scheduler = new SingleThreadTaskScheduler();
        // var tasks = new List<Task>();
        // for (int i = 0; i < 10; i++)
        // {
        //     tasks.Add(Task.Factory.StartNew(() => 
        //             Console.WriteLine($"Hello from {Thread.CurrentThread.Name}"),
        //         CancellationToken.None,
        //         TaskCreationOptions.None,
        //         scheduler));
        // }
        // await Task.WhenAll(tasks);
        // Console.ReadKey();



        using var scheduler = new SingleThreadTaskScheduler();
        SynchronizationContext.SetSynchronizationContext(new SingleThreadSyncContext(scheduler));
        await Task.Factory.StartNew(() => 
                Console.WriteLine($"Hello from {Thread.CurrentThread.Name}"),
            CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);
        await Task.Delay(100);
        Console.WriteLine($"Hello from {Thread.CurrentThread.Name}");
        // await Task.Factory.StartNew(() => 
        //         Console.WriteLine($"Hello from {Thread.CurrentThread.Name}"),
        //     CancellationToken.None,
        //     TaskCreationOptions.None,
        //     scheduler);
    }
}