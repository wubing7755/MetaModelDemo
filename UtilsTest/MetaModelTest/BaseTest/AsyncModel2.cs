namespace UtilsTest.MetaModelTest.BaseTest;

public class AsyncModel2
{
    //public void Test

    public async Task BatchProcessFilesAsync(IEnumerable<string> filePaths)
    {
        // create task list
        var tasks = filePaths.Select(path => ProcessSingleFileAsync(path));

        try
        {
            await Task.WhenAll(tasks);
            Console.WriteLine("All file process successfully");
        }
        catch (Exception e)
        {
            var exceptions = tasks
                .Where(t => t.IsFaulted)
                .SelectMany(t => t.Exception!.InnerExceptions);

            foreach (var ex in exceptions)
            {
                Console.WriteLine($"process failure: {ex.Message}");
            }
        }
    }

    private async Task ProcessSingleFileAsync(string path)
    {
        using var stream = File.OpenRead(path);
        // 模拟处理耗时
        await Task.Delay(1000);
        Console.WriteLine($"Processed file: {Path.GetFileName(path)}");
    }

    [Fact]
    public void TestBatchProcessFileAsync()
    {
        List<string> filePath = new();
        
        filePath.Add(@"C:\Users\usr\Desktop\a.txt");
        filePath.Add(@"C:\Users\usr\Desktop\b.txt");

        var result = BatchProcessFilesAsync(filePath);
        
        Assert.True(result.IsCompletedSuccessfully);
    }

    public async Task<string[]> FetchMultipleWebResourceAsync(IEnumerable<Uri> urls)
    {
        var httpClient = new HttpClient();
        var fetchTasks = urls.Select(uri => httpClient.GetStringAsync(uri)).ToList();

        try
        {
            return await Task.WhenAll(fetchTasks);
        }
        finally
        {
            httpClient.Dispose();
        }
    }

    [Fact]
    public async void TestFetchMultipleWebResourceAsync()
    {
        var uris = new[]
        {
            new Uri("https://example.com"),
            new Uri("https://example.com")
        };
        
        var results = await FetchMultipleWebResourceAsync(uris);
        
        Assert.True(results.Length == 2);
    }
}