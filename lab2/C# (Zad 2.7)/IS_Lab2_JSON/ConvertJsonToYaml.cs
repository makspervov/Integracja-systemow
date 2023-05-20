namespace IS_Lab2_JSON;

using System.Text;
using YamlDotNet.Serialization;

public class ConvertJsonToYaml
{
    public static void Run(string path, string savePath)
    {
        Console.WriteLine("\nlet's convert something...");
        DeserializeJson deserialized = new DeserializeJson(path);
        var serializer = new Serializer();
        var yaml = serializer.Serialize(deserialized.data);
        File.WriteAllText(savePath, yaml, Encoding.UTF8);
        Console.WriteLine("all data has been converted to YAML!");
    }

    public static void Run(DeserializeJson deserialized, string savePath)
    {
        Console.WriteLine("\nlet's convert something...");
        var serializer = new Serializer();
        var yaml = serializer.Serialize(deserialized.data);
        File.WriteAllText(savePath, yaml, Encoding.UTF8);
        Console.WriteLine("all data has been converted to YAML!");
    }
}
