using ServiceReference1;
using System;
using System.Threading.Tasks;

namespace IS_Lab5_SOAP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("My First SOAP Client!");

            MyFirstSOAPInterfaceClient client = new MyFirstSOAPInterfaceClient();

            string text = await client.getHelloWorldAsStringAsync("Maks");

            DateTime date1 = new DateTime(2003, 02, 18);
            DateTime date2 = new DateTime(2023, 04, 03);
            long numberOfDays = await client.getDaysBetweenDatesAsync(date1.ToString("dd.MM.yyyy"), date2.ToString("dd.MM.yyyy"));

            Console.WriteLine(text);
            Console.WriteLine($"Number of days between {date1} and {date2}: {numberOfDays}");
        }
    }
}
