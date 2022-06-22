using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace NotionGraphDatabase.Integration.Tests.Util;

public class JsonResourceProvider
{
    public static readonly string FixturePath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "fixtures");

    public static JToken Get(string name)
    {
        var filePath = Path.Join(FixturePath, $"{name}.json");
        return JToken.Parse(File.ReadAllText(filePath));
    }
}