using CoreLib.Base;
using CoreLib.Database;
using Serilog;

namespace CodeExecutor;

public static class Program
{
    public static void Main(string[] args)
    {
        // #nullable disable warnings
        // string nullableStr = null;
        // Console.WriteLine(nullableStr);

        // Apple apple = new Apple("苹果", AppleType.Ripe);
        // apple.AssertInvariants();
        
        // 初始化日志配置
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(
                path: "app.log", 
                rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        DatabaseManager.InitializeDatabase();
        
        DatabaseManager.CreateUserTable();

        User user = new User()
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            CreatedAt = DateTime.Now,
        };
        
        DatabaseManager.AddUser(user);
        
        var users = DatabaseManager.GetUsers();
    }
}
