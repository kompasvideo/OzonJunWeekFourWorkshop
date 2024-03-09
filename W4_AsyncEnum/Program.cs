using System.Diagnostics;
using Bogus;

var quieries = GetQueries(10).ToArray();
var sw = Stopwatch.StartNew();
foreach (var result in await SearchAsync(quieries))
{
    Console.WriteLine(result.Substring(0, 100));
}
Console.WriteLine(sw.Elapsed);
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
var sw2 = Stopwatch.StartNew();
await foreach (var result in SearchAsync2(quieries))
{
    Console.WriteLine(result.Substring(0, 100));
}
Console.WriteLine(sw2.Elapsed);
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

var sw3 = Stopwatch.StartNew();
foreach (var result in await SearchAsync3(quieries))
{
    Console.WriteLine(result.Substring(0, 100));
}
Console.WriteLine(sw3.Elapsed);

var sw4 = Stopwatch.StartNew();
foreach (var result in await SearchAsync4(quieries))
{
    Console.WriteLine(result.Substring(0, 100));
}
Console.WriteLine(sw4.Elapsed);

static async Task<IEnumerable<string>> SearchAsync(IEnumerable<string> queries)
{
    var httpClient = new HttpClient();
    var results = new List<string>();
    foreach (var query in queries)
    {
        results.Add(await httpClient.GetStringAsync($"https://google.com/search?q={query}"));
    }

    return results;
}

static async IAsyncEnumerable<string> SearchAsync2(IEnumerable<string> queries)
{
    var httpClient = new HttpClient();
    foreach (var query in queries)
    {
        yield return await httpClient.GetStringAsync($"https://google.com/search?q={query}");
    }
}

static async Task<IEnumerable<string>> SearchAsync3(IEnumerable<string> queries)
{
    var httpClient = new HttpClient();
    var results = new List<string>();
    var tasks = queries.Select(q => httpClient.GetStringAsync($"https://google.com/search?q={q}"));
    return await Task.WhenAll(tasks);
}

static async Task<IEnumerable<string>> SearchAsync4(IEnumerable<string> queries)
{
    var httpClient = new HttpClient();
    // var results = new List<string>();
    var semaphore = new SemaphoreSlim(5);
    var tasks = queries.Select(async q =>
    {
        await semaphore.WaitAsync();
        var result = await httpClient.GetStringAsync($"https://google.com/search?q={q}");
        semaphore.Release();
        return result;
    });
    return await Task.WhenAll(tasks);
}

static IEnumerable<string> GetQueries(int count)
{
    var faker = new Faker();
    return Enumerable.Range(1, count).Select(i => faker.Hacker.Noun());
}