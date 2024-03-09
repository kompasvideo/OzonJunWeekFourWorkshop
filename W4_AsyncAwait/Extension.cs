using System.Runtime.CompilerServices;

namespace W4_AsyncAwait;

public static class Extension
{
    public static TaskAwaiter GetAwaiter(this TimeSpan ts) 
        => Task.Delay(ts).GetAwaiter();

    public static MyStringAwaiter GetAwaiter(this string ts)
        => new MyStringAwaiter();

    public class MyStringAwaiter : INotifyCompletion
    {
        public void OnCompleted(Action continuation)
        {
           
        }

        public bool IsCompleted => true;

        public string GetResult()
        {
            return "pong";
        }
    }
}