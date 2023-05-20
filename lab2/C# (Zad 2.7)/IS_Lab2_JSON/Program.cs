namespace IS_Lab2_JSON;

using System;

internal class Program
{
    static void Main(string[] args)
    {
        string source_folder = "Assets/";
        string json_source_file = "data.json";
        string json_dest_file = "data_mod.json";
        string yaml_dest_file = "data_ymod.yaml";

        DeserializeJson newDeserealizator = new DeserializeJson(source_folder + json_source_file);
        newDeserealizator.somestats();

        SerializeJson.Run(source_folder + json_source_file, source_folder + json_dest_file);

        ConvertJsonToYaml.Run(source_folder + json_source_file, source_folder + yaml_dest_file);
        Console.ReadLine();


        //string configPath = "Assets/basic_config.yaml";

        //var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
        //    .WithNamingConvention(CamelCaseNamingConvention.Instance)
        //    .Build();

        /* var config = deserializer.Deserialize<PathConfig>(File.ReadAllText(configPath));
         * 
         * Exception thrown: 'YamlDotNet.Core.YamlException' in YamlDotNet.dll
         * An unhandled exception of type 'YamlDotNet.Core.YamlException' occurred in YamlDotNet.dll
         * Property 'source_folder' not found on type 'IS_Lab2_JSON.Paths'.
         * 
         */

        //string jsonSourceFilePath = Path.Combine(config.Paths.source_folder, config.Paths.json_source_file);
        //DeserializeJson newDeserealizator = new DeserializeJson(jsonSourceFilePath);
        //newDeserealizator.somestats();

        //string jsonDestFilePath = Path.Combine(config.Paths.source_folder, config.Paths.json_destination_file);
        //SerializeJson.Run(newDeserealizator, jsonDestFilePath);

        //string yamlDestFilePath = Path.Combine(config.Paths.source_folder, config.Paths.yaml_destination_file);
        //ConvertJsonToYaml.Run(jsonSourceFilePath, yamlDestFilePath);
        //Console.ReadLine();
    }
}
