using System.Text.Json.Nodes;

namespace CoreLib.Base;

public class ModelObject : ICloneable, IObjectId
{
    private static int Id = 0;

    private static readonly object IdLock = new object();

    public int ObjectId;

    public string Comment;

    public ModelObject(string comment)
    {
        ObjectId = GetObjectId();
        Comment = comment;
    }

    protected ModelObject(ModelObject other)
    {
        ObjectId = GetObjectId();
        Comment = other.Comment;
    }

    /// <summary>
    /// 显式接口实现
    /// </summary>
    /// <remarks>
    /// 有关显式接口实现的更多信息，请参阅:
    /// <see cref="https://learn.microsoft.com/zh-cn/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation"/>
    /// </remarks>
    /// <returns></returns>
    object ICloneable.Clone()
    {
        return Clone();
    }

    public virtual ModelObject Clone()
    {
        return new ModelObject(this);
    }

    public int GetObjectId()
    {
        lock (IdLock)
        {
            return Id++;
        }
    }

    public virtual JsonNode SaveToJson()
    {
        return new JsonObject
        {
            ["ObjectId"] = this.ObjectId,
            ["Comment"] = this.Comment
        };
    }

    public virtual bool LoadFromJson(JsonNode json)
    {
        this.ObjectId = json.GetValue(ObjectId, "ObjectId");
        this.Comment = json.GetValue(Comment, "Comment");
        return true;
    }
}

public static class JsonNodeExtension
{
    public static T GetValue<T>(this JsonNode? node, T defaultValue, params string[] path) where T : notnull
    {
        if (node == null)
            return defaultValue;

        var t = node;
        foreach (var pathItem in path)
        {
            t = t[pathItem];
            if (t == null)
                return defaultValue;
        }

        if (t.AsValue()?.TryGetValue<T>(out var v) == true)
            return v;

        return defaultValue;
    }
}