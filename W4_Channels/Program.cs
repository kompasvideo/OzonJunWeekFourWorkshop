using System.Threading.Channels;
using Bogus;

var channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions
{
    SingleReader = true,
    SingleWriter = true
});

var consumeTask = Task.Run(async () =>
{
    await foreach (var message in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"Consumed {message}");
    }
});

var consumeTask2 = Task.Run(async () =>
{
    await foreach (var message in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"Consumed {message}");
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

var produceTask2 = Task.Run(async () =>
{
    foreach (var message in GetMessages(10))
    {
        // await Task.Delay(100);
        await channel.Writer.WriteAsync(message);
        Console.WriteLine($"Produced {message}");
    }
});



await Task.WhenAll(produceTask, consumeTask, produceTask2, consumeTask2);

static IEnumerable<string> GetMessages(int count)
{
    var faker = new Faker();
    return Enumerable.Range(1, count).Select(i => $"{i}. {faker.Hacker.Noun()}");
}
