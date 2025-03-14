using CoreLib.Base;
using CoreLib.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace CoreLib.Database;

public class DatabaseManager
{
    private const string DbName = "MetaCoder.db";
    
    private const string ConnectionString = $"Data Source={DbName}";
    
    // 创建数据库及表结构
    public static void InitializeDatabase()
    {
        if (File.Exists(DbName))
            return;

        // 读取配置
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connectionString = config.GetConnectionString("Default");
        
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        
        Log.Information("Database connection established");
    }

    // 创建用户表
    // 创建用户表
    public static void CreateUserTable()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
    
        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL UNIQUE,
                Email TEXT CHECK(Email LIKE '%@%.%'),
                CreatedAt TEXT DEFAULT (STRFTIME('%Y-%m-%d %H:%M:%f', 'NOW'))
            )";
    
        command.ExecuteNonQuery();
        Log.Information("User table created");
    }

    
    // 返回用户信息
    public static List<User> GetUsers()
    {
        List<User> users = new ();
        
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        var commandQuery = "SELECT * FROM Users";

        users = connection.Query<User>(commandQuery).ToList();
    
        return users;
    }

    // 向用户表中添加信息
    // 向用户表中添加信息
    public static void AddUser(User user)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
    
        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Users (
                Name,
                Email,
                CreatedAt
            ) VALUES (
                $name,
                $email,
                $createdAt
            )";
    
        // 添加参数
        command.Parameters.AddWithValue("$name", user.Name);
        command.Parameters.AddWithValue("$email", user.Email);
        command.Parameters.AddWithValue("$createdAt", user.CreatedAt);
    
        try
        {
            command.ExecuteNonQuery();
            Log.Information("用户添加成功: {Name}", user.Name);
        }
        catch (SqliteException ex) when (ex.SqliteErrorCode == 19) // UNIQUE 约束失败
        {
            Log.Warning("用户名重复: {Name}", user.Name);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "添加用户失败");
            throw; // 根据需求决定是否重新抛出
        }
    }
}