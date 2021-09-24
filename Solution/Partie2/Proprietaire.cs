using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie2
{
    enum typepro { Entreprise, Particulier };
    class Proprietaire
    {
        public readonly String Id;
        public readonly typepro Typedeproprietaire;
        public readonly int Nombretransactionmax;
        public readonly Double Transactionlim=1000;
        public readonly Double Limitretraithebdo = 2000;
        public double FraisGestion = 0;

        public Proprietaire(string id, typepro typedeproprietaire, int nombretransactionmax)
        {
            Id = id;
            this.Typedeproprietaire = typedeproprietaire;
            this.Nombretransactionmax = nombretransactionmax;
        }
    }
}
