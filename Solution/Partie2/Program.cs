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
                string acctPath = path + @"\comptes.txt";
                string trxnPath = path + @"\gestionnaires.txt"; @"\transactions.txt"
                string sttsPath = path + @"\Statut_1.txt";

                string[] lecteurfichier = File.ReadAllLines(path + @"\gestionnaires.txt");
                List<string> sortiefichier = new List<string>();
                string[] lecteurligne;
            typepro type;
            bool testparse;
            foreach (var item in lecteurfichier)
            {
                lecteurligne = item.Split(';');
                testparse = Enum.TryParse(lecteurligne[1].Trim(),out type);
                if (testparse)
                {
                    Compte.AjouterProprietaire(lecteurligne[0].Trim(), type, int.Parse(lecteurligne[2]));
                }

            }
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
                foreach (var item in lecteurfichier)
                {
                    lecteurligne = item.Split(';');
                    if (Compte.Virement(int.Parse(lecteurligne[0].Trim()), Double.Parse(lecteurligne[1].Replace('.', ',')), lecteurligne[2].Trim(), lecteurligne[3].Trim()))
                    {
                        sortiefichier.Add($"{lecteurligne[0].Trim()};OK");
                    }
                    else
                    {
                        sortiefichier.Add($"{lecteurligne[0].Trim()};KO");
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
}
