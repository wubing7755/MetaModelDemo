using System.Collections.Concurrent;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Xunit.Abstractions;

namespace UtilsTest.MetaModelTest.BaseTest;

public class AsyncModel
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AsyncModel(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    // 同步读取方法
    public int Read(byte[] buffer, int offset, int count)
    {
        // 模拟同步读取操作
        Thread.Sleep(3000);
        return count;
    }

    // TAP
    public async Task<int> ReadAsyncTAP(byte[] buffer, int offset, int count)
    {
        // 模拟异步读取操作
        await Task.Delay(3000);
        return count;
    }

    // EAP
    public void ReadAsyncEAP(byte[] buffer, int offset, int count)
    {
        Task.Run(() =>
        {
            Thread.Sleep(3000);
            OnReadCompleted(new OpenReadCompletedEventArgs(buffer, offset, count));
        });
    }

    public event OpenReadCompletedEventHandler ReadCompleted;

    protected virtual void OnReadCompleted(OpenReadCompletedEventArgs e)
    {
        ReadCompleted?.Invoke(this, e);
    }

    public class OpenReadCompletedEventArgs
    {
        public byte[] Buffer { get; }
        public int Offset { get; }
        public int Count { get; }

        public OpenReadCompletedEventArgs(byte[] buffer, int offset, int count)
        {
            Buffer = buffer;
            Offset = offset;
            Count = count;
        }
    }

    public delegate void OpenReadCompletedEventHandler(object sender, OpenReadCompletedEventArgs e);

    // APM
    public IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
        var tcs = new TaskCompletionSource<int>(state);
        Task.Run(() =>
        {
            try
            {
                Thread.Sleep(3000); // 模拟耗时操作
                tcs.SetResult(count); // 返回读取的字节数
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        if (callback != null)
        {
            callback(tcs.Task);
        }

        return tcs.Task;
    }

    public int EndRead(IAsyncResult asyncResult)
    {
        return ((Task<int>)asyncResult).Result;
    }

    // Run
    [Fact]
    public void RunAsync()
    {
        byte[] buffer = new byte[10];

        // 同步读取
        Console.WriteLine("Starting synchronous read...");
        int bytesRead = Read(buffer, 0, buffer.Length);
        Console.WriteLine($"Synchronous read: {bytesRead} bytes");

        // TAP 异步读取
        Console.WriteLine("Starting TAP read...");
        Task<int> readTask = ReadAsyncTAP(buffer, 0, buffer.Length);
        readTask.ContinueWith(t => Console.WriteLine($"TAP read: {t.Result} bytes"));

        // EAP 异步读取
        Console.WriteLine("Starting EAP read...");
        ReadCompleted += (sender, e) =>
        {
            Console.WriteLine($"EAP read: {e.Count} bytes");
        };
        ReadAsyncEAP(buffer, 0, buffer.Length);

        // APM 异步读取
        Console.WriteLine("Starting APM read...");
        IAsyncResult asyncResult = BeginRead(buffer, 0, buffer.Length, ar =>
        {
            int bytesReadAsync = EndRead(ar);
            Console.WriteLine($"APM read: {bytesReadAsync} bytes");
        }, null);

        // 等待异步操作完成
        Console.WriteLine("Waiting for all asynchronous operations to complete...");
        readTask.Wait();
        Thread.Sleep(5000); // 确保所有异步操作完成
    }

    //public Task<int> ReadTask(this Stream stream, byte[] buffer, int offset, int count, object state)
    //{
    //    var tcs = new TaskCompletionSource<int>();
    //    stream.BeginRead(buffer, offset, count, ar =>
    //    {
    //        try
    //        {
    //            tcs.SetResult(stream.EndRead(ar));
    //        }
    //        catch (Exception ex)
    //        {
    //            tcs.SetException(ex);
    //        }
    //    }, state);

    //    return tcs.Task;
    //}

    public Task<int> MethodAsync(string input)
    {
        if (input == null)
            throw new ArgumentNullException("input");

        return MethodAsyncInternal(input);
    }

    private async Task<int> MethodAsyncInternal(string input)
    {
        return 0;
    }


    // 同步方法
    public void WriteToFile(string filePath, string content)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(content);
        }
    }

    [Fact]
    public void TestWriteToFile()
    {
        string filePath = @"C:\Users\usr\Downloads\a.txt";
        string content = "hello world";

        WriteToFile(filePath, content);
    }

    // 异步方法
    public async Task WriteToFileAsync(string filePath, string content)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            await writer.WriteLineAsync(content);
        }
    }

    [Fact]
    public async void TestWriteToFileAsync()
    {
        string filePath = @"C:\Users\usr\Downloads\a.txt";
        string content = "hello world !";

        await WriteToFileAsync(filePath, content);
    }

    // 同步方法
    public string ReadFromFile(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            return reader.ReadToEnd();
        }
    }

    [Fact]
    public void TestReadFromFile()
    {
        string filePath = @"C:\Users\usr\Downloads\a.txt";

        var content = ReadFromFile(filePath);

        Assert.Equal("hello world !\r\n", content);
    }

    // 异步方法
    public async Task<string> ReadFromFileAsync(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            return await reader.ReadToEndAsync();
        }
    }

    [Fact]
    public async void TestReadFromFileAsync()
    {
        string filePath = @"C:\Users\usr\Downloads\a.txt";

        var content = await ReadFromFileAsync(filePath);

        Assert.Equal("hello world !\r\n", content);
    }

    public void FetchData(string id, out int value, out string message)
    {
        value = 42;
        message = "Data fetched successfully";
    }

    public async Task<(int value, string message)> FetchDataAsync(string id)
    {
        // 模拟异步操作
        await Task.Delay(1000);
        return (42, "Data fetched successfully");
    }

    [Fact]
    public async void TestFetchData()
    {
        int value = 0;
        string message = string.Empty;

        FetchData("123456", out value, out message);

        Assert.Equal(42, value);
        Assert.Equal("Data fetched successfully", message);

        var result = await FetchDataAsync("123456");

        Assert.Equal(42, result.value);
        Assert.Equal("Data fetched successfully", result.message);
    }

    public void UpdateValue(ref int value)
    {
        value += 10;
    }

    public async Task<int> UpdateValueAsync(int value)
    {
        // 模拟异步操作
        await Task.Delay(1000);
        return value + 10;
    }

    [Fact]
    public async void TestUpdateValue()
    {
        int value = 0;

        UpdateValue(ref value);
        Assert.Equal(10, value);

        var result = await UpdateValueAsync(value);
        Assert.Equal(20, result);
    }

    public string GetData()
    {
        return "Some data";
    }

    public async Task<string> GetDataAsync(CancellationToken cancellationToken)
    {
        using (cancellationToken.Register(() => Console.WriteLine("Cancellation requested")))
        {
            // 模拟异步操作
            await Task.Delay(1000, cancellationToken);
            return "Some data";
        }
    }

    [Fact]
    public async void TestGetData()
    {
        Assert.Equal("Some data", GetData());

        CancellationToken cancellationToken = new CancellationToken();

        var result = await GetDataAsync(cancellationToken);
        Assert.Equal("Some data", result);
    }

    public void ProcessData(List<int> data, CancellationToken cancellationToken)
    {
        foreach (var item in data)
        {
            // 检查取消请求
            cancellationToken.ThrowIfCancellationRequested();

            // 模拟耗时操作
            Thread.Sleep(1000);
            Console.WriteLine(item);
        }
    }

    public async Task ProcessDataAsync(List<int> data, CancellationToken cancellationToken)
    {
        using (cancellationToken.Register(() => Console.WriteLine("Cancellation requested")))
        {
            // 检查取消请求
            cancellationToken.ThrowIfCancellationRequested();

            // 模拟异步操作
            await Task.Delay(1000, cancellationToken);
        }
    }

    [Fact]
    public async void TestProcessData()
    {
        var data = Enumerable.Range(1, 20).ToList();
        var cts = new CancellationTokenSource();

        cts.CancelAfter(1000);

        try
        {
            ProcessData(data, cts.Token);

            await ProcessDataAsync(data, cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was canceled");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    [Fact]
    public void TestCancellationTokenFunc()
    {
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;

        Random rnd = new();
        Object lockObj = new object();

        List<Task<int[]>> tasks = new List<Task<int[]>>();
        TaskFactory factory = new TaskFactory(token);

        for (int taskCtr = 0; taskCtr <= 10; taskCtr++)
        {
            int iteration = taskCtr + 1;
            tasks.Add(factory.StartNew(() =>
            {
                int value;
                int[] values = new int[10];
                for (int ctr = 1; ctr <= 10; ctr++)
                {
                    lock (lockObj)
                    {
                        value = rnd.Next(0, 101);
                    }

                    if (value == 0)
                    {
                        source.Cancel();
                        _testOutputHelper.WriteLine("Cancelling at task {0}", iteration);
                        break;
                    }

                    values[ctr - 1] = value;
                }

                return values;
            }, token));
        }

        try
        {
            Task<double> fTask = factory.ContinueWhenAll(tasks.ToArray(),
                (results) =>
                {
                    _testOutputHelper.WriteLine("Calculating overall mean ...");
                    long sum = 0;
                    int n = 0;
                    foreach (var t in results)
                    {
                        foreach (var r in t.Result)
                        {
                            sum += r;
                            n++;
                        }
                    }

                    return sum / (double)n;
                }, token);
            _testOutputHelper.WriteLine("The mean is {0}.", fTask.Result);
        }
        catch (AggregateException ae)
        {
            foreach (Exception e in ae.InnerExceptions)
            {
                if (e is TaskCanceledException)
                    _testOutputHelper.WriteLine("Unable to compute mean: {0}",
                        ((TaskCanceledException)e).Message);
                else
                    _testOutputHelper.WriteLine("Exception: " + e.GetType().Name);
            }
        }
        finally
        {
            source.Dispose();
        }
    }

    [Fact]
    public async void TestCancellationTokenFunc2()
    {
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        Task task = Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Operation canceled.");
                    return;
                }
                Console.WriteLine($"Working... {i}");
                Thread.Sleep(1000); // Simulate work
            }
        }, token);

        Console.WriteLine("Press any key to cancel...");
        // Console.ReadKey();
        cts.Cancel();

        await task;
    }

    [Fact]
    public void TestVolatile()
    {
        int a = 0;
        Volatile.Read(ref a);
    }

    [Fact]
    public void TestCancellationTokenFunc3()
    {
        var cancelSource = new CancellationTokenSource();
        new Thread(() =>
        {
            try
            {
                Work(cancelSource.Token);
            }
            catch (OperationCanceledException)
            {
                System.Console.WriteLine("Canceled");
            }
        }).Start();

        Thread.Sleep(2000);

        cancelSource.Cancel();
        Console.ReadLine();
    }

    private void Work(CancellationToken cancelToken)
    {
        while (!cancelToken.IsCancellationRequested)
        {
            cancelToken.ThrowIfCancellationRequested();
            System.Console.WriteLine("Working...");
        }
    }

    [Fact]
    public async void TestCancellationTokenFunc4()
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var path = @"C:\Users\usr\Downloads\a.txt";

        Console.CancelKeyPress += (source, args) =>
        {
            Console.Error.WriteLine("Cancelling download ...");
            args.Cancel = true;
            cts.Cancel();
        };

        Console.WriteLine("Downloading to {0} ...", path);

        try
        {
            using (var fs = File.Create(path)) ;

            using (CancellationTokenRegistration ctr = cts.Token.Register(() =>
            {
                Console.Error.WriteLine("The task was canceled");
            })) ;
        }
        catch (OperationCanceledException)
        {
            System.Console.WriteLine("An error has occurred");
        }
    }

    [Fact]
    public async void TestCancellationTokenFunc5()
    {
        using (var cts = new CancellationTokenSource())
        {
            var token = cts.Token;

            for (int i = 0; i < 10; i++)
            {
                var child = Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(20), token);
                    token.ThrowIfCancellationRequested();
                }, token);

                if (await Task.WhenAny(child, Task.Delay(TimeSpan.FromSeconds(5))) != child)
                {
                    cts.Cancel();
                }
            }
        }
    }

    [Fact]
    public void TestTaskStatus()
    {
        var tasks = new List<Task<int>>();
        var source = new CancellationTokenSource();
        var token = source.Token;

        int completedIterations = 0;

        for (int n = 0; n <= 19; n++)
        {
            tasks.Add(Task.Run(() =>
            {
                int iterations = 0;

                for (int ctr = 1; ctr <= 2000000; ctr++)
                {
                    token.ThrowIfCancellationRequested();
                    iterations++;
                }

                Interlocked.Increment(ref completedIterations);

                if (completedIterations >= 10)
                {
                    source.Cancel();
                }

                return iterations;
            }, token));
        }

        System.Console.WriteLine("Waiting for the first 10 tasks to complete...\n");

        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException)
        {
            System.Console.WriteLine("Satus of tasks:\n");
            System.Console.WriteLine("{0, 10} {1, 20} {2, 14:N0}", "Task Id", "Status", "Iterations");

            foreach (var t in tasks)
            {
                System.Console.WriteLine("{0, 10} {1, 20} {2, 14}", t.Id, t.Status,
                    t.Status != TaskStatus.Canceled ? t.Result.ToString("N0") : "n/a");
            }
        }
    }

    [Fact]
    public async void TestTaskAsyncFunc1()
    {
        await DownloadStringAsync();

        System.Console.WriteLine("------ Task Completed ------ ");
    }

    public async Task DownloadStringAsync()
    {
        using HttpClient client = new HttpClient();

        string content = await client.GetStringAsync("https://example.com");

        System.Console.WriteLine("------ Task Start ------ ");
        System.Console.WriteLine(content);
    }

    [Fact]
    public void TestDownloadFileSync()
    {
        string url = "https://example.com";
        string path = "./ExampleFile.txt";

        System.Console.WriteLine("------ Task Start ------ ");

        DownloadFileSync(url, path);

        System.Console.WriteLine("------ Task Completed ------ ");
    }

    public void DownloadFileSync(string url, string path)
    {
        using HttpClient client = new HttpClient();

        try
        {
            System.Console.WriteLine("start download file...");

            using var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            using var stream = response.Content.ReadAsStream();
            using var fileStream = new FileStream(path, FileMode.Create);
            stream.CopyTo(fileStream);

            System.Console.WriteLine("download file completed");
        }
        catch (HttpRequestException ex)
        {
            System.Console.WriteLine($"an error has ocurred：{ex.Message}");
            throw;
        }
    }

    [Fact]
    public async void TestTaskAsyncFunc2()
    {
        var result = await ComputeAsync(2, 3);

        System.Console.WriteLine($" ------ Result: {result} ------ ");
    }

    public Task<int> ComputeAsync(int a, int b)
    {
        var taskCompletionSource = new TaskCompletionSource<int>();

        Task.Run(() =>
        {
            int result = 0;
            for (int i = 0; i < 1000000; i++)
            {
                result += a * b;
            }

            taskCompletionSource.SetResult(result);
        });

        return taskCompletionSource.Task;
    }

    [Fact]
    public async void TestTaskAsyncFunc3()
    {
        string content = await ReadFromWebAsync("https://example.com");

        System.Console.WriteLine(content);
    }

    public async Task<string> ReadFromWebAsync(string url)
    {
        var taskCompletionSource = new TaskCompletionSource<string>();

        WebRequest request = WebRequest.Create(url);
        request.BeginGetResponse(async (ar) =>
        {
            try
            {
                WebResponse response = request.EndGetResponse(ar);
                using StreamReader reader = new StreamReader(response.GetResponseStream());

                string content = reader.ReadToEnd();

                taskCompletionSource.SetResult(content);
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
        }, null);

        return await taskCompletionSource.Task;
    }

    [Fact]
    public void TestDownloadStringSync()
    {
        DownloadString();
    }

    public void DownloadString()
    {
        var client = new HttpClient();

        string content;

        try
        {
            // 使用 HttpClient 同步下载网页内容
            using var response = client.GetAsync("https://example.com").Result;
            response.EnsureSuccessStatusCode(); // 确保请求成功

            using var stream = response.Content.ReadAsStream();
            using var reader = new StreamReader(stream);
            content = reader.ReadToEnd(); // 同步读取内容
        }
        catch (Exception ex)
        {
            Console.WriteLine($"请求或读取内容时发生错误：{ex.Message}");
            return;
        }

        Console.WriteLine("------ Task Start ------ ");
        Console.WriteLine(content);
    }

    [Fact]
    public async void TestDelayAsync()
    {
        await DelayAsync(1000);

        System.Console.WriteLine("1秒后执行！");
    }

    public Task DelayAsync(int millisecondsDelay)
    {
        var tcs = new TaskCompletionSource<object>();

        Timer timer = null;

        timer = new Timer(_ =>
        {
            timer.Dispose();
            tcs.TrySetResult(null);
        }, null, millisecondsDelay, Timeout.Infinite);

        return tcs.Task;
    }

    [Fact]
    public async void TestCalculateSquareAsync()
    {
        int result = await CalculateSquareAsync(5);

        _testOutputHelper.WriteLine($"result: -- {result} -- ");
    }

    public Task<int> CalculateSquareAsync(int number)
    {
        var tcs = new TaskCompletionSource<int>();

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                Thread.Sleep(2000);
                tcs.SetResult(number * number);
            }
            catch(Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    [Fact]
    public async void TestCalculateAferDelayAsync()
    {
        var result = await CalculateAferDelayAsync(1000, 10);

        _testOutputHelper.WriteLine($"result: -- {result} --");
    }

    public Task<int> CalculateAferDelayAsync(int delayMilliseconds, int number)
    {
        var tcs = new TaskCompletionSource<int>();

        // 模拟延迟操作
        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(delayMilliseconds);
                int result = number * number;

                // 标记任务完成
                tcs.SetResult(result);
            }
            catch(Exception ex)
            {
                // 标记任务失败
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    [Fact]
    public async void TestIsPrimeAsync()
    {
        long number = 1000000L;

        var result = await IsPrimeAsync(number);

        _testOutputHelper.WriteLine($"{result}");
    }

    public Task<bool> IsPrimeAsync(long number, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<bool>();

        // 使用ThreadPool避免阻塞调用线程
        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                bool result = IsPrime(number, cancellationToken);

                tcs.TrySetResult(result);
            }
            catch(OperationCanceledException)
            {
                tcs.TrySetCanceled();
            }
            catch(Exception ex)
            {
                tcs.TrySetException(ex);
            }
        });

        return tcs.Task;
    }

    private bool IsPrime(long n, CancellationToken cancellationToken)
    {
        if (n <= 1) return false;
        if (n == 2) return true;
        if (n % 2 == 0) return false;

        for (long i = 3; i <= Math.Sqrt(n); i += 2)
        {
            cancellationToken.ThrowIfCancellationRequested(); // 检查取消请求
            if (n % i == 0) return false;
        }
        return true;
    }

    [Fact]
    public async void TestCopyFileAsync()
    {
        string sourcePath = @"C:\Users\usr\Desktop\a.txt";
        string destPath = @"C:\Users\usr\Desktop\b.txt";

        await CopyFileAsync(sourcePath, destPath);

        _testOutputHelper.WriteLine("the task is completed");
    }

    public async Task CopyFileAsync(string sourcePath, string destPath, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<object>();

        // 异步读取和写入
        try
        {
            // 打开文件流（异步模式）
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, FileOptions.Asynchronous))
            using (FileStream destStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, FileOptions.Asynchronous))
            {
                byte[] buffer = new byte[81920]; // 80KB 缓冲区

                // 注册取消操作
                cancellationToken.Register(() =>
                {
                    tcs.TrySetCanceled();
                });

                int bytesRead;
                // 异步读取并写入数据
                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    await destStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                }

                tcs.TrySetResult(null); // 文件复制完成
            }
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex); // 处理异常
        }
    }

    [Fact]
    public async void TestAsyncMethod()
    {
        Console.WriteLine("0. ------ 调用异步方法前 ------ ");
        Task task = AsyncMethod();
        Console.WriteLine("2. ------ 异步方法返回Task，但未完成 ------ ");
        await task;
        Console.WriteLine("4. ------ 异步方法完成 ------ ");
    }

    public async Task AsyncMethod()
    {
        Console.WriteLine("1. ------ 同步执行开始 ------ ");
        await Task.Delay(3000);
        Console.WriteLine("3. ------ 3秒后恢复执行 ------ ");
    }
    
    [Fact]
    public async Task YieldAsync()
    {
        Console.WriteLine("0. ------ 异步方法开始执行 ------");

        await Task.Delay(3000);

        Console.WriteLine("1. ------ Before Yield ------");

        // 启动一个线程池任务
        var poolTask = Task.Run(() =>
        {
            Console.WriteLine("ThreadPool Task: 开始执行");
            Thread.Sleep(3000); // 模拟线程池任务的执行
            Console.WriteLine("ThreadPool Task: 执行完成");
        });

        await Task.Yield();

        Console.WriteLine("2. ------ After Yield ------");

        await Task.Delay(3000);

        Console.WriteLine("3. ------ 异步方法执行完成 ------");

        await poolTask; // 确保线程池任务完成
    }
    
    [Fact]
    public async Task TestConfigureAwaitAsync()
    {
        Console.WriteLine("Start of the method.");

        // 异步操作，使用 ConfigureAwait(false) 不在原线程恢复
        await Task.Delay(1000).ConfigureAwait(false);

        Console.WriteLine("After Delay, running on a different thread if possible.");

        // 这里的代码不再依赖于原始同步上下文（UI 线程），可以继续在线程池线程上执行
        await Task.Delay(1000);

        Console.WriteLine("End of the method.");
    }

    [Fact]
    public async void CallLongRunningTask()
    {
        // 创建取消令牌源
        var cts = new CancellationTokenSource();

        // 启动一个长时间运行的异步任务，传递取消令牌
        var task = LongRunningTask(cts.Token);

        // 模拟在 3 秒后取消任务
        await Task.Delay(3000); // 等待 3 秒
        cts.Cancel(); // 触发取消

        // 等待任务完成
        await task;

        Console.WriteLine("程序结束");
    }

    public async Task LongRunningTask(CancellationToken token)
    {
        Console.WriteLine("任务开始执行...");

        for (int i = 0; i < 5; i++)
        {
            // 每 1 秒检查一次是否取消
            await Task.Delay(1000);

            if (token.IsCancellationRequested)
            {
                Console.WriteLine("任务被取消！");
                return;  // 如果请求取消，则退出任务
            }

            Console.WriteLine($"执行中... 第 {i + 1} 秒");
        }

        Console.WriteLine("任务完成！");
    }

    [Fact]
    public async void TestDownloadStringAsync()
    {
        var url = @"https://example.com";
        var progress = new Progress<int>(p => Console.WriteLine($"下载进度: {p}%"));

        var result = await DownloadStringAsync(url, progress);

        _testOutputHelper.WriteLine($"Result: ------ {result} ------ ");
    }

    public async Task<string> DownloadStringAsync(string url, IProgress<int> progress)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault();
            var buffer = new byte[8192];
            var bytesRead = 0;
            var totalBytesRead = 0L;

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    totalBytesRead += bytesRead;

                    // 计算下载进度并更新
                    int progressPercentage = (int)((totalBytesRead * 100) / totalBytes);
                    progress?.Report(progressPercentage);
                }
            }

            return totalBytesRead.ToString();
        }
    }

    // 使用线程安全的缓存容器（例如内存缓存）
    private static readonly ConcurrentDictionary<string, int> _cache = new ConcurrentDictionary<string, int>();

    /// <summary>
    /// 尝试从缓存中同步获取数据
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">输出参数，缓存的值</param>
    /// <returns>是否命中缓存</returns>
    private bool TryGetCachedValue(string key, out int value)
    {
        // 检查缓存是否存在该键
        return _cache.TryGetValue(key, out value);
    }
    
    /// <summary>
    /// 异步获取数据的实际逻辑（例如访问数据库或远程服务）
    /// </summary>
    /// <param name="key">数据键</param>
    /// <returns>异步任务的结果</returns>
    private async Task<int> GetValueAsyncInternal(string key)
    {
        // 模拟异步操作（如数据库查询、API 调用）
        await Task.Delay(100); // 模拟网络延迟
    
        // 生成或获取真实数据（此处以随机数为例）
        int realValue = new Random().Next(1, 100);
    
        // 将数据写入缓存
        _cache.TryAdd(key, realValue);
    
        return realValue;
    }
    
    public Task<int> GetValueAsync(string key)
    {
        if (TryGetCachedValue(key, out int cachedValue))
        {
            return Task.FromResult(cachedValue);
        }
        else
        {
            return GetValueAsyncInternal(key);
        }
    }

    [Fact]
    public void TestGetValueAsync()
    {
        var key = "testKey";
        var result = GetValueAsync(key);
        Assert.Equal(1, result.Result);
    }
    
    
}
