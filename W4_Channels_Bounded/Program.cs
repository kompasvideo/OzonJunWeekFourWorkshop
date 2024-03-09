using System.Threading.Channels;
using Bogus;

// var channel = Channel.CreateBounded<string>(1);
var channel = Channel.CreateBounded<string>( new BoundedChannelOptions(1)
{ 
    FullMode = BoundedChannelFullMode.DropOldest
});

var consumeTask = Task.Run(async () =>
{
    await foreach (var message in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"Consumed {message}");
        await Task.Delay(1000);
    }
});

var produceTask = Task.Run(async () =>
{
    foreach (var message in GetMessages(10))
    {
        // await Task.Delay(100);
        await channel.Writer.WriteAsync(message);
        Console.WriteLine($"Produced {message}");
    }
});




await Task.WhenAll(produceTask, consumeTask);

static IEnumerable<string> GetMessages(int count)
{
    var faker = new Faker();
    return Enumerable.Range(1, count).Select(i => $"{i}. {faker.Hacker.Noun()}");
}