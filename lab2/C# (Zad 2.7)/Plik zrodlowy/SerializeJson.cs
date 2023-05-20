namespace IS_Lab2_JSON;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Text;

public class SerializeJson
{
    public static void Run(string path, string savePath)
    {
        Console.WriteLine("\nlet's serialize something...");
        DeserializeJson deserialized = new DeserializeJson(path);
        List<ShorterJST> shorterJsts = new List<ShorterJST>();
        foreach (var d in deserialized.data)
            shorterJsts.Add(shorterJSTMapper(d));

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.LatinExtendedA, UnicodeRanges.LatinExtendedB, UnicodeRanges.Latin1Supplement, UnicodeRanges.BasicLatin),
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize<List<ShorterJST>>(shorterJsts, options);
        File.WriteAllText(savePath, json, Encoding.UTF8);
        Console.WriteLine("all data has been serialized!");
    }

    public static void Run(DeserializeJson deserialized, string savePath)
    {
        Console.WriteLine("let's serialize something");
        List<ShorterJST> shorterJsts = new List<ShorterJST>();
        foreach (var d in deserialized.data)
            shorterJsts.Add(shorterJSTMapper(d));

        string json = JsonSerializer.Serialize<List<ShorterJST>>(shorterJsts);
        File.WriteAllText(savePath, json);
        Console.WriteLine("all data has been serialized!");
    }

    private static ShorterJST shorterJSTMapper(JST jst)
    {
        ShorterJST mapped = new ShorterJST(jst.Kod_TERYT, jst.Województwo, jst.Powiat, jst.typ_JST,
            jst.nazwa_urzędu_JST, jst.miejscowość,
            Int32.Parse(jst.telefon_kierunkowy.ToString() + jst.telefon.ToString()));
        return mapped;
    }

}
