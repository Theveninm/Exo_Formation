using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> aafficher = new List<string>();
            Maze test = new Maze(20, 30);
            test.Generate();
            aafficher = test.Display();
            foreach (var item in aafficher)
            {
                Console.WriteLine(item);
            }

            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
