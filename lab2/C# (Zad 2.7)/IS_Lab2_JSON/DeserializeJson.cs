namespace IS_Lab2_JSON;

using System.Text.Json;

public class DeserializeJson
{
    public List<JST> data { get; set; }

    // konstructor
    public DeserializeJson(string filename)
    {
        string json = File.ReadAllText(filename);
        this.data = JsonSerializer.Deserialize<List<JST>>(json)!;
    }

    // przykładowe statystyki
    public void somestats()
    {
        Console.WriteLine("let's deserialize something...\n");

        Dictionary<string, Dictionary<string, int>> stats = new Dictionary<string, Dictionary<string, int>>();

        foreach (var dep in this.data)
        {
            if (!stats.ContainsKey(dep.Województwo))
                stats[dep.Województwo] = new Dictionary<string, int>();

            if (!stats[dep.Województwo].ContainsKey(dep.typ_JST))
                stats[dep.Województwo].Add(dep.typ_JST, 0);

            stats[dep.Województwo][dep.typ_JST] += 1;
        }

        foreach (var wojewodztwo in stats)
        {
            Console.WriteLine("Statystyki dla województwa " + wojewodztwo.Key + ":");

            foreach (var typJST in wojewodztwo.Value)
                Console.WriteLine("\t- liczba urzędów typu " + typJST.Key + ": " + typJST.Value);

            Console.WriteLine();
        }
        Console.WriteLine("all data has been deserialized!");
    }
}
