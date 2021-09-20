namespace Labyrinth
{
    public enum Etatcellule
    {
        simple,
        entree,
        sortie
    }
    public struct Cell
    {
        // 0 : Haut, 1 : bas, 2 : gauche, 3 : droite
        public bool[] Walls { get; set; }

        public bool IsVisited { get; set; }

        // Définir système d'état de la cellule
        public Etatcellule Etat;
    }
}
