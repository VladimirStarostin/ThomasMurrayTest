using System;
using ThomasMurray;

namespace DemoConsole
{
    class Program
    {
        private static readonly DigitsConverter _digitsConverter = new DigitsConverter();
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("\n======\nВведите строку для конвертации.");
                var input = Console.ReadLine();
                var convertedInput = _digitsConverter.ConvertNumericsInText(input);
                Console.WriteLine("=>"+convertedInput);
            }
        }
    }
}
