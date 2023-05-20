using System;
using System.IO;

namespace IS_Lab1_XML
{
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