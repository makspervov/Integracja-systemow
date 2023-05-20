using System;
using System.Xml;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Linq;

internal class XMLReadWithXLSTDOM
{
    public static void Read(string filepath)
    {
        XPathDocument document = new XPathDocument(filepath);
        XPathNavigator navigator = document.CreateNavigator();
        XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
        manager.AddNamespace("x", "http://rejestrymedyczne.ezdrowie.gov.pl/rpl/eksport-danych-v1.0");

        XPathExpression query = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy[@postac='Krem' and @nazwaPowszechnieStosowana='Mometasoni furoas']");
        query.SetContext(manager);
        int count = navigator.Select(query).Count;

        Console.WriteLine($"Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas wynosi {count}");

        CountOfMedicinesWithDifferentFormsXPath(filepath);
        CountOfCreamsAndTabletsByManufacturerXPath(filepath);
        ListTopThreeProducersXPath(filepath);
        PrintOneOrSeveralActiveSubstancesXPath(filepath);
    }

    public static void CountOfMedicinesWithDifferentFormsXPath(string filepath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);

        // słownik przechowujący listy postaci produktów leczniczych dla danej nazwy powszechnej
        Dictionary<string, List<string>> medicines = new Dictionary<string, List<string>>();

        var drugs = doc.GetElementsByTagName("produktLeczniczy");
        foreach (XmlNode d in drugs)
        {
            string name = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;
            string form = d.Attributes.GetNamedItem("postac").Value;

            if (!medicines.ContainsKey(name))
                medicines[name] = new List<string>();
            if (!medicines[name].Contains(form))
                medicines[name].Add(form);
        }

        int count = 0;
        foreach (var medicine in medicines)
            if (medicine.Value.Count > 1)
                count++;

        Console.WriteLine($"Liczba produktów leczniczych o takiej samej nazwie powszechnej, pod różnymi postaciami: {count}");
    }

    public static (string, string) CountOfCreamsAndTabletsByManufacturerXPath(string filepath)
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

    public static void ListTopThreeProducersXPath(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        var nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ns", "http://rejestrymedyczne.ezdrowie.gov.pl/rpl/eksport-danych-v1.0");

        var producers = doc.SelectNodes("//ns:produktLeczniczy/@podmiotOdpowiedzialny", nsmgr)
                            .Cast<XmlAttribute>()
                            .GroupBy(attr => attr.Value)
                            .Select(g => new { Producer = g.Key, Count = g.Count() })
                            .OrderByDescending(p => p.Count)
                            .Take(3);

        Console.WriteLine("\nTrzy podmioty produkujące najwięcej kremów:");
        foreach (var producer in producers)
            Console.WriteLine($"{producer.Producer} - {producer.Count}");
    }

    public static void PrintOneOrSeveralActiveSubstancesXPath(string filepath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
        string sc;
        int count = 0;
        int count_2more = 0;
        var drugs = doc.GetElementsByTagName("produktLeczniczy");
        foreach (XmlNode d in drugs)
        {
            sc = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;
            if (sc.Contains("+"))
                count_2more++;
            else if (!sc.Contains("+"))
                count++;
        }

        Console.WriteLine($"\nLiczba produktów leczniczych w różnych postaciach, które zawierają jedną substancję czynną: {count}");
        Console.WriteLine($"Liczba produktów leczniczych w różnych postaciach, których substancji czynnych są dwa i więcej: {count_2more}");
    }
}
