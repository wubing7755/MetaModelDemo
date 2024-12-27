using CoreLib.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace CoreLib.Utils;

public static class ObjectJsonConverter
{
    static int step = 1000;

    static Dictionary<int, string> kvData = new Dictionary<int, string>();

    static List<ModelObject> objs = new List<ModelObject>();

    static string CsvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource\\ObjectId.csv");

    static string JsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource\\ModelObjects.json");

    public static void CsvTest()
    {
        for (int i = 0; i < step; i++)
        {
            ModelObject obj = new ModelObject(i + "_comment");

            kvData.Add(obj.ObjectId, obj.Comment);
        }

        StringBuilder sb = new StringBuilder();

        foreach (var kv in kvData)
        {
            sb.AppendLine(kv.Key + "," + kv.Value);
        }

        File.WriteAllText(CsvFilePath, sb.ToString());
    }

    public static void SaveToJsonTest()
    {
        for (int i = 0; i < step; i++)
        {
            ModelObject obj = new ModelObject(i + "_comment");

            objs.Add(obj);
        }

        JsonArray jsonArray = new JsonArray();
        foreach (var obj in objs)
        {
            jsonArray.Add(obj.SaveToJson());
        }

        File.WriteAllText(JsonFilePath, jsonArray.ToString());
    }

    public static void LoadFromJsonTest()
    {
        string jsonString = System.IO.File.ReadAllText(JsonFilePath);

        using (JsonDocument doc = JsonDocument.Parse(jsonString))
        {
            JsonElement root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                JsonArray? jsonArray = root.Deserialize<JsonArray>();

                if (jsonArray != null)
                {
                    foreach (var jsonNode in jsonArray)
                    {
                        var jsonObject = jsonNode as JsonObject;

                        ModelObject ob = new ModelObject("");

                        bool b = ob.LoadFromJson(json: jsonObject);

                        objs.Add(ob);
                    }
                }
            }
        }
    }
}
