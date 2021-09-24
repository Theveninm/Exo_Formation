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
        public readonly typepro typedeproprietaire;
        public readonly int nombretransactionmax;
        public readonly Double limitretraithebdo = 2000;

        public Proprietaire(string id, typepro typedeproprietaire, int nombretransactionmax)
        {
            Id = id;
            this.typedeproprietaire = typedeproprietaire;
            this.nombretransactionmax = nombretransactionmax;
        }
    }
}
