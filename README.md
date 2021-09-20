# Exo_Formation
Contient les exercices notés dans le cadre de l'initiation C#


1.a j'ai choisi le type enum pour rendre le code le plus lisible possible.
j'ai hésité à utiliser deux booléens (un pour dire si il s'agit d'une cellule simple ou spéciale, et un second pour indiquer, au cas où il s'agisse d'une cellule spéciale, si il s'agit d'une entrée ou d'une sortie), de manière à consommer moins de mémoire, mais dans la mesure où la mémoire n'était pas le facteur limitant (de plus on ne sais pas comment sont stoqués les booléens en c#, et il est possible qu'il réserve un octet entier pour les stoquer), j'ai préféré partir sur une enum.


5.b Comme toutes les cases adjacentes a des cases visités seront visités (soit en premier passage soit lors du retour), que toutes les cases sont liés entre elles (il n'y a pas "deux bloc de cases disjoints") et qu'ils sont liés à la première case visité, ils seront tous visité.

affichage:
on utlilse les chaines de charactères "─┐","┌─","─┘","└─" pour les angles,
Pour le corps, on utilise "╵ " si le mur 3 de la case n-1,i-1 est à false, "  " sinon (
respectivement          "╴ " pour le mur 1 de la case n-1,i-1,
                        "╷ " pour le mur 2 de la case n  ,i  ,
et                      "╶─" pour le mur 0 de la case n  ,i  )
ensuite on combine le résultat des 4 tests pour avoir le caractère a insérer.

Pour les cotés, on applique la même formule, mais pour une seul direction.
