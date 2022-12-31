using Polly;

Console.WriteLine("--- Polly .Net test BULKHEAD ---");

var bulkheadPolicy = Policy.BulkheadAsync(1, 1, async (Context) => {
    await Task.Run(() =>
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("I could not handle this request.");
    }); 
});

while (true)
{
    Task.Delay(1000).Wait();
    bulkheadPolicy.ExecuteAsync(DoThisTask);
}

static Task DoThisTask()
{
    return Task.Run(() => {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Do some task.");
        Task.Delay(5000).Wait();
    });
}