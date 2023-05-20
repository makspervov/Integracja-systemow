using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Linq;

namespace IS_Lab1_XML
{
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

    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine("Assets", "data.xml");

            // odczyt danych z wykorzystaniem DOM
            Console.WriteLine("XML loaded by DOM Approach");
            XMLReadWithDOMApproach.Read(filepath);
            
            // odczyt danych z wykorzystaniem SAX
            Console.WriteLine("\n\nXML loaded by SAX Approach");
            XMLReadWithSAXApproach.Read(filepath);

            // odczyt danych z wykorzystaniem XPath i DOM
            Console.WriteLine("\n\nXML loaded with XPath");
            XMLReadWithXLSTDOM.Read(filepath);

            Console.ReadLine();
        }
    }
}