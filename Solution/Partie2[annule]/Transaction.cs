using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie2
{
    class Transaction
    {
        public readonly string Id;
        public readonly DateTime Date;
        public readonly string NumeroExpediteur;
        public readonly string NumeroDestinataire;
        public readonly Double Montant;

        public Transaction(string id, DateTime date, Double montant, string numeroExpediteur, string numeroDestinataire)
        {
            Id = id.Trim();
            Date = date;
            Montant = montant;
            NumeroExpediteur = numeroExpediteur.Trim();
            NumeroDestinataire = numeroDestinataire.Trim();
        }
    }
}
