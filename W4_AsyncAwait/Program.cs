using System.Runtime.CompilerServices;

namespace W4_AsyncAwait;

class Program
{
    static async Task Main(string[] args)
    {
        #region Await
        // просмотр работы IAsyncStateMachine
        // Console.WriteLine("Before await");
        // await Task.Delay(100);
        // Console.WriteLine("After await");
        // Console.WriteLine("Before second await");
        // await Task.Delay(100);
        // Console.WriteLine("After second await");
        // Console.WriteLine("Before third await");
        //
        // var res = await new HttpClient().GetStringAsync("https://google.com");
        // Console.WriteLine(res);
        // Console.WriteLine("After third await");
        #endregion
        
        #region Task
        // var locker = new object();
        // for (int j = 0; j < 10; j++)
        // {
        //     int counter = 0;
        //     var tasks = new List<Task>();
        //     for (int i = 0; i < 100; i++)
        //     {
        //         tasks.Add(Task.Factory.StartNew(async () =>
        //             {
        //                 await Task.Delay(100);
        //                 lock (locker)
        //                 {
        //                     counter++;
        //                 }
        //             }
        //         ).Unwrap());
        //     }
        //
        //     await Task.WhenAll(tasks);
        //     Console.WriteLine(counter);
        // }
        #endregion
        
        #region Lock
        // var locker = new object();
        // var i = 0;
        // await Task.Delay(100);
        // lock (locker)
        // {
        //     i = 5;
        // }
        // Console.WriteLine(i);
        #endregion

        #region GetAwaiter
        // Task.Delay(100).GetAwaiter();
        await TimeSpan.FromSeconds(1);
        Console.WriteLine(await "ping");

        #endregion

    }
}