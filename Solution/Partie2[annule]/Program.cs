using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            string acctPath = path + @"\Comptes_1.txt";
            string trxnPath = path + @"\Transactions_1.txt";
            string sttsPath = path + @"\Statut_1.txt";

            string[] lecteurfichier = File.ReadAllLines(acctPath);
            List<string> sortiefichier = new List<string>();
            string[] lecteurligne;
            foreach (var item in lecteurfichier)
            {
                lecteurligne = item.Split(';');
                if (lecteurligne[1] == "")
                {
                    Compte.AjouterCompte(lecteurligne[0].Trim());
                }
                else
                {
                    Compte.AjouterCompte(lecteurligne[0].Trim(), Double.Parse(lecteurligne[1].Replace('.', ',')));
                }
            }
            Compte.AfficherComptes();
            lecteurfichier = File.ReadAllLines(trxnPath);
            int i = 1;
            foreach (var item in lecteurfichier)
            {
                lecteurligne = item.Split(';');
                if (Compte.Virement(Double.Parse(lecteurligne[1].Replace('.', ',')), lecteurligne[2].Trim(), lecteurligne[3].Trim()))
                {
                    sortiefichier.Add($"{i++};OK");
                }
                else
                {
                    sortiefichier.Add($"{i++};KO");
                }
            }
            File.WriteAllLines(sttsPath, sortiefichier.ToArray());
            Compte.AfficherComptes(true);



            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
