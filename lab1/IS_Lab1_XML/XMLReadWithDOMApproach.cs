using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

internal class XMLReadWithDOMApproach
{
    internal static void Read(string filepath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
        string postac;
        string sc;
        int count = 0;
        var drugs = doc.GetElementsByTagName("produktLeczniczy");
        foreach (XmlNode d in drugs)
        {
            postac = d.Attributes.GetNamedItem("postac").Value;
            sc = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;
            if (postac == "Krem" && sc == "Mometasoni furoas")
                count++;
        }
        Console.WriteLine($"Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {count}");
        CountOfMedicinesWithDifferentFormsDOM(filepath);
        CountOfCreamsAndTabletsByManufacturerDOM(filepath);
        ListTopThreeProducersDOM(filepath);
        PrintOneOrSeveralActiveSubstancesDOM(filepath);
    }

    public static void CountOfMedicinesWithDifferentFormsDOM(string filepath)
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
        {
            if (medicine.Value.Count > 1)
                count++;
        }
        Console.WriteLine($"Liczba produktów leczniczych o takiej samej nazwie powszechnej, pod różnymi postaciami: {count}");
    }

    public static void CountOfCreamsAndTabletsByManufacturerDOM(string filepath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
        Dictionary<string, int> kremyByPodmiot = new Dictionary<string, int>();
        Dictionary<string, int> tabletkiByPodmiot = new Dictionary<string, int>();

        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            if (node.Name == "produktLeczniczy")
            {
                var podmiot = node.Attributes.GetNamedItem("podmiotOdpowiedzialny").Value;
                var postac = node.Attributes.GetNamedItem("postac").Value;

                if (postac == "Krem")
                {
                    if (!kremyByPodmiot.ContainsKey(podmiot))
                    {
                        kremyByPodmiot[podmiot] = 0;
                    }
                    kremyByPodmiot[podmiot]++;
                }
                else if (postac == "Tabletki")
                {
                    if (!tabletkiByPodmiot.ContainsKey(podmiot))
                    {
                        tabletkiByPodmiot[podmiot] = 0;
                    }
                    tabletkiByPodmiot[podmiot]++;
                }
            }
        }

        var maxKremy = kremyByPodmiot.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        var maxTabletki = tabletkiByPodmiot.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        Console.WriteLine($"\nNajwięcej kremów produkuje: {maxKremy}");
        Console.WriteLine($"Najwięcej tabletek produkuje: {maxTabletki}");
    }

    public static void ListTopThreeProducersDOM(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        var producers = new Dictionary<string, int>();

        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            if (node.Attributes["podmiotOdpowiedzialny"] != null)
            {
                string producer = node.Attributes["podmiotOdpowiedzialny"].Value;
                if (producers.ContainsKey(producer))
                    producers[producer]++;
                else
                    producers.Add(producer, 1);
            }
        }

        var topThreeProducers = producers.OrderByDescending(p => p.Value).Take(3);

        Console.WriteLine("\nTrzy podmioty produkujące najwięcej kremów:");
        foreach (var producer in topThreeProducers)
            Console.WriteLine($"{producer.Key} - {producer.Value}");
    }

    public static void PrintOneOrSeveralActiveSubstancesDOM(string filepath)
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