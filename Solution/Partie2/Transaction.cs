using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie2
{
    class Transaction
    {
        public readonly int NumeroTransaction;
        public readonly string NumeroExpediteur;
        public readonly string NumeroDestinataire;
        public readonly Double Montant;
        public readonly DateTime DateTrans;

        public Transaction(int numerotransaction, string numeroExpediteur, string numeroDestinataire, Double montant, DateTime dateTrans)
        {
            NumeroTransaction = numerotransaction;
            NumeroExpediteur = numeroExpediteur;
            NumeroDestinataire = numeroDestinataire;
            Montant = montant;
            DateTrans = dateTrans;
        }
    }
}
