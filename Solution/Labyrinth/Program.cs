using System;
using System.Collections.Generic;
using System.IO;
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
            Maze test = new Maze(200,300);
            test.Generate();
            aafficher = test.Display();
            foreach (var item in aafficher)
            {
                Console.WriteLine(item);
            }
            File.WriteAllLines("Labyrinth.txt", aafficher) ;
            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
