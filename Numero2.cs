using System;

class Numero2
{
    static double produitRecursive(int n)
    {
       Console.WriteLine(n %10);
        if (n < 10) //Condition d'arrêt
            return n;
        else
            return n % 10 * produitRecursive(n/10); //Dernière chiffre * (n) sans la dernière chiffre
    }

    static double produitIterative(int n)
    {
        double resultat = 1;
        while (n > 0)   //Condition d'arrêt
        {
            resultat *= (n % 10); //Dernière chiffre fois résultat
            n /= 10;    //(n) sans la dernière chiffre
        }
        return resultat;
    }

    static void afficherRecursive(int n)
    {
        string temp = n.ToString(); //Conversion en String
        if (temp.Length == 1)   //Condition d'arrêt   
            Console.WriteLine(temp[0]); //Affichage de la première lettre
        else
        {
            Console.WriteLine(temp[0]); //Affichage de la première lettre
            afficherRecursive(Convert.ToInt32(temp.Substring(1))); //subString sans la première lettre
        }            
    }

    static void afficherIterative(int n)
    {
        string temp = n.ToString(); //Conversion en String
        foreach (char lettre in temp)   //pour chaque lettre dans le String
            Console.WriteLine(lettre);  //Affiche la lettre
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Appel pour réaliser le produit recursive: " + produitRecursive(2345));
        Console.WriteLine("Appel pour réaliser le produit itérative: " + produitIterative(2345));
        Console.WriteLine("Appel pour afficher une chiffre de façon recursive");
        afficherRecursive(13579);
        Console.WriteLine("Appel pour afficher une chiffre de façon itérative");
        afficherIterative(13579);
        Console.ReadLine();
    }
}
/*Execution:
Appel pour réaliser le produit recursive: 120
Appel pour réaliser le produit itérative: 120
Appel pour afficher une chiffre de façon recursive
1
3
5
7
9
Appel pour afficher une chiffre de façon itérative
1
3
5
7
9
 */
