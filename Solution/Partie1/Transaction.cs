using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie1
{
    class Transaction
    {
        public readonly int _numeroTransaction;
        public readonly string _numeroExpediteur;
        public readonly string _numeroDestinataire;
        public readonly Double _montant;

        public Transaction(int numerotransaction , string numeroExpediteur, string numeroDestinataire, Double montant)
        {
            _numeroTransaction = numerotransaction;
            _numeroExpediteur = numeroExpediteur;
            _numeroDestinataire = numeroDestinataire;
            _montant = montant;
        }
    }
}
