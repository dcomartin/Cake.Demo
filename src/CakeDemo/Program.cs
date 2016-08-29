using System;
using System.Linq;

namespace CakeDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var name = args.Any() ? args[0] : "World";
            Console.WriteLine(SayHello(name));
        }

        public static string SayHello(string name)
        {
            return $"Hello {name}";
        }
    }
}
