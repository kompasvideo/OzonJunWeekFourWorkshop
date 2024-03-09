namespace CustomTaskScheduler;

class Program
{
    static async Task Main(string[] args)
    {
        using var scheduler = new SingleThreadTaskScheduler();
        var task = Task.Factory.StartNew(() => 
        Console.WriteLine($"Hello from {Thread.CurrentThread.Name}"),
            CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);
        await task;
        Console.ReadKey();
    }
}