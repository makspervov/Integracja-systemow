using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

internal class XMLReadWithSAXApproach
{
    internal static void Read(string filepath)
    {
        // konfiguracja początkowa dla XmlReadera
        XmlReaderSettings settings = new XmlReaderSettings();

        settings.IgnoreComments = true;
        settings.IgnoreProcessingInstructions = true;
        settings.IgnoreWhitespace = true;

        // odczyt zawartości dokumentu
        XmlReader reader = XmlReader.Create(filepath, settings);

        // zmienne pomocnicze
        int count = 0;

        reader.MoveToContent();
        // analiza każdego z węzłów dokumentu
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")
            {
                string postac = reader.GetAttribute("postac");
                string sc = reader.GetAttribute("nazwaPowszechnieStosowana");
                if (postac == "Krem" && sc == "Mometasoni furoas")
                    count++;
            }
        }
        Console.WriteLine($"Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas: {count}");
        CountOfMedicinesWithDifferentFormsSAX(filepath);
        CountOfCreamsAndTabletsByManufacturerSAX(filepath);
        ListTopThreeProducersSAX(filepath);
        PrintOneOrSeveralActiveSubstancesSAX(filepath);
    }

    public static void CountOfMedicinesWithDifferentFormsSAX(string filepath)
    {
        Dictionary<string, List<string>> medicines = new Dictionary<string, List<string>>();

        using (XmlReader reader = XmlReader.Create(filepath))
        {
            string name = null;
            string form = null;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")
                {
                    name = reader.GetAttribute("nazwaPowszechnieStosowana");
                    form = reader.GetAttribute("postac");

                    if (!medicines.ContainsKey(name))
                        medicines[name] = new List<string>();

                    if (!medicines[name].Contains(form))
                        medicines[name].Add(form);
                }
            }
        }
        int count = 0;
        foreach (var medicine in medicines)
            if (medicine.Value.Count > 1)
                count++;

        Console.WriteLine($"Liczba produktów leczniczych o takiej samej nazwie powszechnej, pod różnymi postaciami: {count}");
    }

    public static (string, string) CountOfCreamsAndTabletsByManufacturerSAX(string filepath)
    {
        Dictionary<string, int> kremyByPodmiot = new Dictionary<string, int>();
        Dictionary<string, int> tabletkiByPodmiot = new Dictionary<string, int>();

        using (XmlReader reader = XmlReader.Create(filepath))
        {
            string podmiot = "";
            string postac = "";
            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name == "produktLeczniczy")
                {
                    podmiot = reader.GetAttribute("podmiotOdpowiedzialny");
                    postac = reader.GetAttribute("postac");
                    if (postac == "Krem")
                    {
                        if (!kremyByPodmiot.ContainsKey(podmiot))
                            kremyByPodmiot[podmiot] = 0;
                        kremyByPodmiot[podmiot]++;
                    }
                    else if (postac == "Tabletki")
                    {
                        if (!tabletkiByPodmiot.ContainsKey(podmiot))
                            tabletkiByPodmiot[podmiot] = 0;
                        tabletkiByPodmiot[podmiot]++;
                    }
                }
            }
        }

        string maxKremyPodmiot = kremyByPodmiot.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        string maxTabletkiPodmiot = tabletkiByPodmiot.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

        Console.WriteLine($"\nNajwięcej kremów produkuje: {maxKremyPodmiot}");
        Console.WriteLine($"Najwięcej tabletek produkuje: {maxTabletkiPodmiot}");
        return (maxKremyPodmiot, maxTabletkiPodmiot);
    }

    public static void ListTopThreeProducersSAX(string path)
    {
        var producers = new Dictionary<string, int>();
        using (var reader = XmlReader.Create(path))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "produktLeczniczy" && reader.GetAttribute("podmiotOdpowiedzialny") != null)
                {
                    var producer = reader.GetAttribute("podmiotOdpowiedzialny");
                    if (!producers.ContainsKey(producer))
                        producers[producer] = 0;
                    producers[producer]++;
                }
            }
        }

        var topThree = producers.OrderByDescending(p => p.Value).Take(3);
        Console.WriteLine("\nTrzy podmioty produkujące najwięcej kremów:");
        foreach (var producer in topThree)
            Console.WriteLine($"{producer.Key} - {producer.Value}");
    }

    public static void PrintOneOrSeveralActiveSubstancesSAX(string filepath)
    {
        int count = 0;
        int count_2more = 0;

        using (XmlReader reader = XmlReader.Create(filepath))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement("produktLeczniczy"))
                {
                    string sc = reader.GetAttribute("nazwaPowszechnieStosowana");
                    if (sc.Contains("+"))
                        count_2more++;
                    else
                        count++;
                }
            }
        }

        Console.WriteLine($"\nLiczba produktów leczniczych w różnych postaciach, które zawierają jedną substancję czynną: {count}");
        Console.WriteLine($"Liczba produktów leczniczych w różnych postaciach, których substancji czynnych są dwa i więcej: {count_2more}");
    }
}