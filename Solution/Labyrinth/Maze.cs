using System;
using System.Collections.Generic;

namespace Labyrinth
{
    public class Maze
    {
        /// <summary>
        /// Grille permettant de représenter un matériau poreux
        /// Pour chaque élément, true case ouverte, false case bloquée
        /// </summary>
        private readonly Cell[,] _maze;

        private readonly int _lineSize;

        private readonly int _columnSize;

        /// <summary>
        /// Construction d'une grille de taille n * m
        /// </summary>
        /// <param name="size"></param>
        public Maze(int n, int m)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "le nombre de lignes de la grille négatif ou null.");
            }

            if (m <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "le nombre de colonnes de la grille négatif ou null.");
            }

            _lineSize = n;
            _columnSize = m;
            _maze = new Cell[n, m];
            for (int i = 0; i < _lineSize; i++)
            {
                for (int j = 0; j < _columnSize; j++)
                {
                    _maze[i, j].Walls = new bool[4];
                }
            }
        }

        public bool IsOpen(int i, int j, int w)
        {
            return _maze[i, j].Walls[w];
        }

        public bool IsMazeStart(int i, int j)
        {
            return _maze[i, j].Etat == Etatcellule.entree;
        }

        public bool IsMazeEnd(int i, int j)
        {
            return _maze[i, j].Etat == Etatcellule.sortie;
        }

        public void Open(int i, int j, int w)
        {
            if (w == 0)
            {
                _maze[i - 1, j].Walls[1] = true;
            }
            else if (w == 1)
            {
                _maze[i + 1, j].Walls[0] = true;
            }
            else if (w == 2)
            {
                _maze[i, j - 1].Walls[3] = true;
            }
            else
            {
                _maze[i, j + 1].Walls[2] = true;
            }
            _maze[i,j].Walls[w] = true;
        }

        private List<KeyValuePair<int, int>> CloseNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> listedesvaleurs = new List<KeyValuePair<int, int>>();
            if (i>0)
            {
                listedesvaleurs.Add(new KeyValuePair<int, int>(i - 1,j));
            }
            if (i < _lineSize - 1)
            {
                listedesvaleurs.Add(new KeyValuePair<int, int>(i + 1, j));
            }
            if (j > 0)
            {
                listedesvaleurs.Add(new KeyValuePair<int, int>(i, j - 1));
            }
            if (j < _columnSize - 1)
            {
                listedesvaleurs.Add(new KeyValuePair<int, int>(i, j + 1));
            }
            return listedesvaleurs;
        }

        public KeyValuePair<int, int> Generate()
        {
            Stack < KeyValuePair<int, int> > CheminParcouru = new Stack<KeyValuePair<int, int>>();
            var rand = new Random();
            List < KeyValuePair<int, int> > CasesaTest = new List<KeyValuePair<int, int>>();

            CheminParcouru.Push(new KeyValuePair<int, int>(rand.Next(_lineSize), rand.Next(_columnSize)));

            _maze[CheminParcouru.Peek().Key, CheminParcouru.Peek().Value].IsVisited = true;
            while (CheminParcouru.Count != 0)
            {
                CasesaTest = CloseNeighbors(CheminParcouru.Peek().Key, CheminParcouru.Peek().Value);
                for (int i = CasesaTest.Count -1; i >= 0 ; i--)
                {
                    if (_maze[CasesaTest[i].Key,CasesaTest[i].Value].IsVisited)
                    {
                        CasesaTest.RemoveAt(i);
                    }
                }
                if (CasesaTest.Count > 0)
                {
                    int i = rand.Next(0, CasesaTest.Count);
                    if (CasesaTest[i].Value == CheminParcouru.Peek().Value)
                    {
                        if (CasesaTest[i].Key > CheminParcouru.Peek().Key)
                        {
                            Open(CheminParcouru.Peek().Key, CheminParcouru.Peek().Value, 1);
                        }
                        else
                        {
                            Open(CheminParcouru.Peek().Key, CheminParcouru.Peek().Value, 0);
                        }
                    }
                    else
                    {
                        if (CasesaTest[i].Value > CheminParcouru.Peek().Value)
                        {
                            Open(CheminParcouru.Peek().Key, CheminParcouru.Peek().Value, 3);
                        }
                        else
                        {
                            Open(CheminParcouru.Peek().Key, CheminParcouru.Peek().Value, 2);
                        }
                    }
                    _maze[CasesaTest[i].Key, CasesaTest[i].Value].IsVisited = true;
                    CheminParcouru.Push(new KeyValuePair<int, int>(CasesaTest[i].Key, CasesaTest[i].Value));
                }
                else
                {
                    CheminParcouru.Pop();
                }
            }
            int randsortie = rand.Next(2 * _lineSize + 2 * _columnSize - 4);
            if (randsortie < _lineSize)
            {
                _maze[randsortie, 0].Etat = Etatcellule.sortie;
            }
            else if (randsortie < 2*_lineSize)
            {
                _maze[randsortie - _lineSize, _columnSize - 1].Etat = Etatcellule.sortie;
            }
            else if (randsortie < (2 * _lineSize + _columnSize -2))
            {
                _maze[0, randsortie - 2 * _lineSize +1 ].Etat = Etatcellule.sortie;
            }
            else
            {
                _maze[_lineSize - 1, randsortie - 2 * _lineSize - _columnSize + 2].Etat = Etatcellule.sortie;
            }
            int randentree = rand.Next(2 * _lineSize + 2 * _columnSize - 5);
            if (randentree >= randsortie)
            {
                randentree++;
            }
            if (randentree < _lineSize)
            {
                _maze[randentree, 0].Etat = Etatcellule.entree;
                return new KeyValuePair<int, int>(randentree, 0);
            }
            else if (randentree < 2 * _lineSize)
            {
                _maze[randentree - _lineSize, _columnSize - 1].Etat = Etatcellule.entree;
                return new KeyValuePair<int, int>(randentree - _lineSize, _columnSize - 1);
            }
            else if (randentree < (2 * _lineSize + _columnSize - 2))
            {
                _maze[0, randentree - 2 * _lineSize + 1].Etat = Etatcellule.entree;
                return new KeyValuePair<int, int>(0, randentree - 2 * _lineSize + 1);
            }
            else
            {
                _maze[_lineSize - 1, randentree - 2 * _lineSize - _columnSize + 2].Etat = Etatcellule.entree;
                return new KeyValuePair<int, int>(_lineSize - 1, randentree - 2 * _lineSize - _columnSize + 2);
            }
        }

        public string DisplayLine(int n)
        {
            string ligne = "";
            if (n == 0)
            {
                ligne += "┌─";
                for (int i = 0; i < _columnSize - 1; i++)
                {
                    if (_maze[0,i].Walls[3])
                    {
                        ligne += "──";
                    }
                    else
                    {
                        ligne += "┬─";
                    }
                }
                ligne += '┐';
            }
            else if (n == _lineSize)
            {
                ligne += "└─";
                for (int i = 0; i < _columnSize - 1; i++)
                {
                    if (_maze[_lineSize - 1, i].Walls[3])
                    {
                        ligne += "──";
                    }
                    else
                    {
                        ligne += "┴─";
                    }
                }
                ligne += '┘';
            }
            else
            {
                if (_maze[n - 1 ,0 ].Walls[1])
                {
                    ligne += "│ ";
                }
                else
                {
                    ligne += "├─";
                }


                for (int i = 1; i < _columnSize ; i++)
                {
                    if (_maze[n - 1, i - 1].Walls[1])
                    {
                        if (_maze[n - 1, i - 1].Walls[3])
                        {
                            if (_maze[n, i].Walls[0])
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "  ";
                                }
                                else
                                {
                                    ligne += "╷ ";
                                }
                            }
                            else
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "╶─";
                                }
                                else
                                {
                                    ligne += "┌─";
                                }
                            }
                        }
                        else
                        {
                            if (_maze[n, i].Walls[0])
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "╵ ";
                                }
                                else
                                {
                                    ligne += "│ ";
                                }
                            }
                            else
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "└─";
                                }
                                else
                                {
                                    ligne += "├─";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_maze[n - 1, i - 1].Walls[3])
                        {
                            if (_maze[n, i].Walls[0])
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "╴ ";
                                }
                                else
                                {
                                    ligne += "┐ ";
                                }
                            }
                            else
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "──";
                                }
                                else
                                {
                                    ligne += "┬─";
                                }
                            }
                        }
                        else
                        {
                            if (_maze[n, i].Walls[0])
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "┘ ";
                                }
                                else
                                {
                                    ligne += "┤ ";
                                }
                            }
                            else
                            {
                                if (_maze[n, i].Walls[2])
                                {
                                    ligne += "┴─";
                                }
                                else
                                {
                                    ligne += "┼─";
                                }
                            }
                        }
                    }
                }


                if (_maze[n - 1, _columnSize - 1].Walls[1])
                {
                    ligne += '│';
                }
                else
                {
                    ligne += '┤';
                }
            }
            return ligne;
        }

        public List<string> Display()
        {
            List<String> list = new List<string>();
            for (int i = 0; i <= _lineSize; i++)
            {
                list.Add(DisplayLine(i));
            }
            return list;
        }
    }
}
