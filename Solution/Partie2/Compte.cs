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
        private readonly string _numeroCompte;
        private static List<int> _usedtransid = new List<int>();
        private static Dictionary<string, Compte> _repertoire = new Dictionary<string, Compte>();
        private static Dictionary<string, Proprietaire> _listePro = new Dictionary<string, Proprietaire>();
        private Double _solde;
        private string _proprietaire;
        private List<Transaction> _historiqueTransactions;

        //on passe par la classe AjouterCompte pour créer les comptes
        private Compte(string identifiant, string proprietaire,Double solde = 0)
        {
            _numeroCompte = identifiant;
            _solde = solde;
            _repertoire.Add(identifiant, this);
            _proprietaire = proprietaire;
            _historiqueTransactions = new List<Transaction>();
        }
        /// <summary>
        /// ajoute un compte à la base de donée
        /// </summary>
        /// <param name="identifiant"></param>
        /// <param name="solde"></param>
        /// <returns></returns>
        public static bool AjouterCompte(string identifiant,string proprietaire, Double solde = 0)
        {
            if (!_repertoire.ContainsKey(identifiant) && _listePro.ContainsKey(proprietaire) && solde >= 0)
            {
                new Compte(identifiant, proprietaire, solde);
                return true;
            }
            return false;
        }

        public static bool AjouterProprietaire(string identifiant, typepro type, int nbretransaction)
        {
            if (!_listePro.ContainsKey(identifiant))
            {
                _listePro.Add(identifiant,new Proprietaire(identifiant, type, nbretransaction));
                return true;
            }
            return false;
        }

        public bool SupressionCompte(string identifiant)
        {
            if (_repertoire.ContainsKey(identifiant))
            {
                _repertoire.Remove(identifiant);
                return true;
            }
            return false;
        }

        public bool TransfertCompte(string identifiant, string proprietaire1, string proprietaire2)
        {
            if (_repertoire.ContainsKey(identifiant) && _repertoire[identifiant]._proprietaire == proprietaire1 && _listePro.ContainsKey(proprietaire2))
            {
                _repertoire[identifiant]._proprietaire = proprietaire2;
                return true;
            }
            return false;
        }



        public bool Depot(int idtrans, Double montant)
        {
            if (_usedtransid.Contains(idtrans))
            {
                return false;
            }
            _usedtransid.Add(idtrans);
            if (montant > 0)
            {
                _solde += montant;
                return true;
            }
            return false;
        }

        public bool Retrait(int idtrans, Double montant)
        {
            if (montant > 0 && _solde >= montant)
            {
                if (_usedtransid.Contains(idtrans))
                {
                    return false;
                }
                _usedtransid.Add(idtrans);
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
        public static bool Virement(int idtrans,Double montant, string numeroexpediteur, string numerodestinataire, DateTime dateVirement)
        {
            //lorsque un des numéros est à 0, celà signifie que la transaction as lieu avec la banque
            if (numeroexpediteur == "0" && _repertoire.ContainsKey(numerodestinataire))
            {
                return _repertoire[numerodestinataire].Depot(idtrans, montant);
            }
            else if (numerodestinataire == "0" && _repertoire.ContainsKey(numeroexpediteur))
            {
                return _repertoire[numeroexpediteur].Retrait(idtrans, montant);
            }
            //sinon elle est faite entre l'expéditeur et le destinataire designé
            else if (_repertoire.ContainsKey(numeroexpediteur))
            {
                return _repertoire[numeroexpediteur].Virement(idtrans, montant, numerodestinataire, dateVirement);
            }
            return false;
        }

        public bool Virement(int idtrans, Double montant, string numerodecompte, DateTime dateVirement)
        {
            if (_usedtransid.Contains(idtrans))
            {
                return false;
            }
            _usedtransid.Add(idtrans);
            if (montant > 0 && _solde >= montant && _repertoire.ContainsKey(numerodecompte) && montant <= _listePro[_proprietaire].Limitretraithebdo && montant <= _listePro[_proprietaire].Transactionlim)
            {
                //on s'assure d'abord qu'on ne dépasse pas le seuil de virements
                Double sommevirements = montant;
                int virementcompte = 1;
                for (int i = _historiqueTransactions.Count -1; i >=0 && virementcompte < _listePro[_proprietaire].Nombretransactionmax; i--)
                {
                    if (_historiqueTransactions[i].NumeroExpediteur == _numeroCompte)
                    {
                        sommevirements += _historiqueTransactions[i].Montant;
                        if (virementcompte < _listePro[_proprietaire].Nombretransactionmax)
                        {
                            if (sommevirements > _listePro[_proprietaire].Transactionlim)
                            {
                                return false;
                            }
                            virementcompte++;
                        }
                        if (sommevirements > _listePro[_proprietaire].Transactionlim && _historiqueTransactions[i].DateTrans >= dateVirement - new TimeSpan(7,0,0,0))
                        {
                            return false;
                        }
                    }
                }
                if (_listePro[_proprietaire].Typedeproprietaire == typepro.Entreprise)
                {
                    if (montant < 10)
                    {
                        return false;
                    }
                    _listePro[_proprietaire].FraisGestion += 10;
                    montant -= 10;
                }
                else
                {
                    _listePro[_proprietaire].FraisGestion += 0.01 * montant;
                    montant -= 0.01 * montant;
                }
                //puis on manipule les comptes
                _solde -= montant;
                _repertoire[numerodecompte]._solde += montant;
                Transaction transactionActuelle = new Transaction(idtrans,_numeroCompte, numerodecompte, montant, dateVirement);
                _historiqueTransactions.Add(new Transaction(idtrans,_numeroCompte, numerodecompte, montant, dateVirement));
                _repertoire[numerodecompte]._historiqueTransactions.Add(transactionActuelle);
                return true;
            }
            return false;
        }

        public bool Prelevement(int idtrans, Double montant, string numerodecompte, DateTime dateVirement)
        {
            return _repertoire[numerodecompte].Virement(idtrans, montant, _numeroCompte, dateVirement);
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
                        Console.WriteLine($"    transaction {transaction.NumeroTransaction}, expediteur : {transaction.NumeroExpediteur}, destinataire : {transaction.NumeroDestinataire}, montant : {transaction.Montant}");
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
