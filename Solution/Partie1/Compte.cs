using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie1
{
    class Compte
    {
        //private static int nbreComptes;
        private readonly string _numeroCompte;
        private static Dictionary<string, Compte> _repertoire = new Dictionary<string, Compte>();
        private Double _solde;
        private Double _maxRetrait = 1000;
        private List<Transaction> _historiqueTransactions;

        //on passe par la classe AjouterCompte pour créer les comptes
        private Compte(string identifiant,Double solde = 0)
        {
            _numeroCompte = identifiant;
            _solde = solde;
            _repertoire.Add(identifiant, this);
            _historiqueTransactions = new List<Transaction>();
        }
        /// <summary>
        /// ajoute un compte à la base de donée
        /// </summary>
        /// <param name="identifiant"></param>
        /// <param name="solde"></param>
        /// <returns></returns>
        public static bool AjouterCompte(string identifiant, Double solde = 0)
        {
            if (_repertoire.ContainsKey(identifiant) && solde > 0)
            {
                return false;
            }
            new Compte(identifiant, solde);
            return true;
        }

        public bool Depot(Double montant)
        {
            if (montant > 0)
            {
                _solde += montant;
                return true;
            }
            return false;
        }

        public bool Retrait(Double montant)
        {
            if (montant > 0 && _solde > montant)
            {
                _solde -= montant;
                return true;
            }
            return false;
        }
        /// <summary>
        /// fonction statique permettant de faire des virements en utilisant les numéros de compte (et donc eviter d'avoir à manipuler les instances de ces derniers
        /// </summary>
        /// <param name="montant"></param>
        /// <param name="numeroexpediteur"></param>
        /// <param name="numerodestinataire"></param>
        /// <returns></returns>
        public static bool Virement(Double montant, string numeroexpediteur, string numerodestinataire)
        {
            //lorsque un des numéros est à 0, celà signifie que la transaction as lieu avec la banque
            if (numeroexpediteur == "0" && _repertoire.ContainsKey(numerodestinataire))
            {
                return _repertoire[numerodestinataire].Depot(montant);
            }
            else if (numerodestinataire == "0" && _repertoire.ContainsKey(numeroexpediteur))
            {
                return _repertoire[numeroexpediteur].Retrait(montant);
            }
            //sinon elle est faite entre l'expéditeur et le destinataire designé
            else if (_repertoire.ContainsKey(numeroexpediteur))
            {
                return _repertoire[numeroexpediteur].Virement(montant, numerodestinataire);
            }
            return false;
        }

        public bool Virement(Double montant, string numerodecompte)
        {
            if (montant > 0 && _solde > montant && _repertoire.ContainsKey(numerodecompte) && montant <= _maxRetrait)
            {
                //on s'assure d'abord qu'on ne dépasse pas le seuille de virements
                Double sommevirements = montant;
                int virementcompte = 1;
                for (int i = _historiqueTransactions.Count -1; i >=0 && virementcompte < 10; i--)
                {
                    if (_historiqueTransactions[i]._numeroExpediteur == _numeroCompte)
                    {
                        sommevirements += _historiqueTransactions[i]._montant;
                        if (sommevirements > _maxRetrait)
                        {
                            return false;
                        }
                        virementcompte++;
                    }
                }
                //puis on manipule les comptes
                _solde -= montant;
                _repertoire[numerodecompte]._solde += montant;
                Transaction transactionActuelle = new Transaction(_numeroCompte, numerodecompte, montant);
                _historiqueTransactions.Add(new Transaction(_numeroCompte, numerodecompte, montant));
                _repertoire[numerodecompte]._historiqueTransactions.Add(transactionActuelle);
                return true;
            }
            return false;
        }

        public bool Prelevement(Double montant, string numerodecompte)
        {
            return _repertoire[numerodecompte].Virement(montant, _numeroCompte);
        }

        /// <summary>
        /// affiche dans la console le contenu de tous les comptes présent dans le répertoire
        /// </summary>
        /// <param name="affichertransactions"> affiche ou non (par défaut) les historiques des transactions de chaque comptes</param>
        public static void AfficherComptes(bool affichertransactions = false)
        {
            Console.WriteLine("Liste des comptes :");
            foreach (var item in _repertoire)
            {
                Console.WriteLine($"Compte {item.Value._numeroCompte}, Solde = {item.Value._solde}");
                if (affichertransactions)
                {
                    Console.WriteLine("Liste des transactions :");
                    foreach (var transaction in item.Value._historiqueTransactions)
                    {
                        Console.WriteLine($"    transaction {transaction._numeroTransaction}, expediteur : {transaction._numeroExpediteur}, destinataire : {transaction._numeroDestinataire}, montant : {transaction._montant}");
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
