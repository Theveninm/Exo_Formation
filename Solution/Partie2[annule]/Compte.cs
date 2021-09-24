using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie2
{
    class Compte
    {
        //private static int nbreComptes;
        public readonly string NumeroCompte;
        public Double Solde;
        public DateTime DateCreation;
        public DateTime DateCloture = DateTime.MaxValue;
        public String Proprietaire;

        //on passe par la classe AjouterCompte pour créer les comptes
        public Compte(string identifiant, DateTime dateCreation,String idprop, Double solde = 0)
        {

            NumeroCompte = identifiant;
            Solde = solde;
            DateCreation = dateCreation;
            Proprietaire = idprop;
        }
    }
}
