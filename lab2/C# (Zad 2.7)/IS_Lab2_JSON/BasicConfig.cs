namespace IS_Lab2_JSON;

using System;
using System.IO;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class PathConfig
{
    public Paths Paths { get; set; }
}

public class Paths
{
    public string source_folder { get; set; }
    public string json_source_file { get; set; }
    public string json_destination_file { get; set; }
    public string yaml_destination_file { get; set; }
}
