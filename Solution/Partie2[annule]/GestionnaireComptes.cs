using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partie2
{
    class GestionnaireComptes
    {
        private Dictionary<string,Compte> _repertoire = new Dictionary<string, Compte>();
        private Dictionary<String, Proprietaire> _listeProprio = new Dictionary<string, Proprietaire>();
        private List<int> _usedtrans = new List<int>();
        private int _cptTransOK = 0;
        private int _cptTransKO = 0;
        private Double _totalTrans = 0;
        private Dictionary<string, Double> _fraisgestion = new Dictionary<string, double>();

        public bool Transaction(string[] instruction)
        {
            if (instruction[3] == "0" && _repertoire.ContainsKey(instruction[4]))
            {
                return _repertoire[instruction[4].Depot(idtrans, montant);
            }
            else if (numerodestinataire == "0" && _repertoire.ContainsKey(numeroexpediteur))
            {
                return _repertoire[numeroexpediteur].Retrait(idtrans, montant);
            }
            //sinon elle est faite entre l'expéditeur et le destinataire designé
            else if (_repertoire.ContainsKey(numeroexpediteur))
            {
                return _repertoire[numeroexpediteur].Virement(idtrans, montant, numerodestinataire);
            }
            return false;
        }

        public bool GestionLigneCompte(string[] instruction)
        {
            if (instruction[4] == "")
            {
                return CreationCompte(instruction[0], DateTime.Parse(instruction[1]), instruction[3], Double.Parse(instruction[2]));
            }
            else if (instruction[3] == "")
            {
                if (_repertoire[instruction[0]].Proprietaire == instruction[4])
                {
                    return SupressionCompte(instruction[0]);
                }
                return false;
            }
            else
            {
                return CessasionCompte(instruction[0], instruction[3], instruction[4]);
            }
        }

        private bool CreationCompte(string identifiant, DateTime dateCreation, String idprop, Double solde = 0)
        {
            if (!_repertoire.ContainsKey(identifiant) && solde >= 0 && _listeProprio.ContainsKey(idprop))
            {
                _repertoire.Add(identifiant ,new Compte(identifiant, dateCreation, idprop, solde));
                return true;
            }
            return false;
        }

        private bool SupressionCompte(string identifiant)
        {
            if (_repertoire.ContainsKey(identifiant))
            {
                _repertoire.Remove(identifiant);
                return true;
            }
            return false;
        }

        private bool CessasionCompte(string idcompte, string idancprop,string nouvidprop)
        {
            if (_repertoire.ContainsKey(idcompte) && _repertoire[idcompte].Proprietaire == idancprop && _listeProprio.ContainsKey(nouvidprop))
            {
                _repertoire[idcompte].Proprietaire = nouvidprop;
                return true;
            }
            return false;
        }

    }
}
