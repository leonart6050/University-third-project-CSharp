using System;
using System.Collections;
using System.Linq; //pour utiliser array.Max() et array.Min()
using System.IO;
/*Jorge Leonardo Trujillo Salas
  TRUJ12059003
  Numero 3*/
class Nation : IComparable
{
    private char groupe;
    private string nom, capital;
    private int continent;
    private long superficie, population;
    //Constructeur
    public Nation(char groupe, int continent, string nom, string capital, long superficie, long population)
    {
        this.groupe = groupe;
        this.continent = continent;
        this.nom = nom;
        this.capital = capital;
        this.superficie = superficie;
        this.population = population;
    }
    
    public char Groupe
    {
        get { return groupe; }
        set
        {
            groupe = value;
        }
    }

    public string Nom
    {
        get { return nom; }
    }

    public int Continent
    {
        get { return continent; }
    }


    public long Superficie
    {
        get { return superficie; }
        set
        {
            superficie = value;
        }
    }

    public long Population
    {
        get { return population; }
    }
    //redéfinition ToString
    public override string ToString()
    {
        return string.Format("{0,-8}{1,-6}{2,-21}{3,-15}{4,-11}{5}", groupe, continent, nom, capital, superficie, population);
    }
    //redéfinition CompareTo en vérifiant la propriété nom
    public int CompareTo(object obj)
    {
        Nation autre = (Nation)obj;
        return nom.CompareTo(autre.nom);
    }
    //redéfinition Equals selon nom
    public override bool Equals(object obj)
    {
        if (this == obj)
            return true;
        else if (this.GetType() != obj.GetType())
            return false;
        else
        {
            Nation autre = (Nation)obj;
            return nom == autre.nom;
        }
    }
}

class Numero3
{
    //Méthode pour lire et remplir l'arraylist
    static ArrayList Relire(string nomALire, ArrayList liste)
    {
        StreamReader aLire = File.OpenText(nomALire);
        string ligneLue; // on lit ligne par ligne
        while ((ligneLue = aLire.ReadLine()) != null)
        {   //On parse chaqu'un des propriétés à partir du string
            char groupe = Char.Parse(ligneLue.Substring(0, 1));
            int continent = Int32.Parse(ligneLue.Substring(1, 1));
            string nom = ligneLue.Substring(2, 30).Trim();
            string capital = ligneLue.Substring(37, 12).Trim();
            long superficie = Int64.Parse(ligneLue.Substring(57, 8).Trim());
            long population = Int64.Parse(ligneLue.Substring(67).Trim());
            //On cree l'objet de type Nation et on l'ajoute à l'arraylist
            liste.Add(new Nation(groupe, continent, nom, capital, superficie, population));
        }
        aLire.Close();
        return liste;   //return l'arraylist
    }
    //Affichage de l'arraylist en passant par paramètres une quantité d'éléments de début et une quantité d'éléments de fin
    static void Affiche(ArrayList liste, int debut, int fin)
    {
        Console.WriteLine(" groupe  continent\t  Nom\t\tCapital\t    Superficie  Population\n---------------------------------------------------------------------------");
        for (int i = 0; i < liste.Count; i++)
        {
            if (i < debut || i >= liste.Count - fin) //si l'element est appartient au rang on l'affiche
                Console.WriteLine("{0,2}) " + liste[i], i + 1);
            else if (debut + 1 == i)
                Console.WriteLine("\t..........\n\t..........");
        }
        Console.WriteLine();
    }
    //Méthode pour modifier une propriété soit le groupe ou la superficie
    static void Modifier<T>(ArrayList liste, int index, T valeur)
    {
        Nation temp = (Nation)liste[index];
        string type = valeur.GetType().ToString();
        switch (type)
        {
            case "System.Char":
                temp.Groupe = Convert.ToChar(valeur);
                break;
            case "System.Int32":
                temp.Superficie = Convert.ToInt32(valeur);
                break;
        }
    }
    //Méthode pour supprimer un pays de l'arraylist
    static void Supprimer(ArrayList liste, string nom)
    {
        Nation pays = new Nation(' ', 0, nom, "", 0, 0);
        liste.RemoveAt(liste.IndexOf(pays));
    }
    //Méthode pour ajouter un pays de l'arraylist
    static void Ajouter(ArrayList liste, char groupe, int continent, string nom, string capital, long superficie, long population)
    {
        Nation pays = new Nation(groupe, continent, nom, capital, superficie, population);
        liste.Add(pays);
    }
    //Méthode pour parcourrir la liste en comptant le participants de chaque continent de tel manière d'identifier celui qui a 
    //le plus de participants et celui qui a le plus, aussi la méthode cherche la superficie et la population plus petits 
    static void Compteur(ArrayList liste)
    { 
        Nation paysTemp;
        int[] participants = { 0, 0, 0, 0, 0 }; //chaque chiffre représente un continent
        string[] nomParticipants = { "Afrique", "Amérique", "Asie", "Océanie", "Europe" }; //pour afficher le nom du continent à la fin 
        int index = 0, indexSuperf = 0, indexPopula = 0;
        long minSuperf = Int64.MaxValue, minPopula = Int64.MaxValue;
        foreach (Nation pays in liste)
        {
            //pour chaque pays on vérifie son continent et on augmente dans le array appelé participants
            switch (pays.Continent)
            {
                case 1:
                    participants[0]++;
                    break;
                case 2:
                    participants[1]++;
                    break;
                case 3:
                    participants[2]++;
                    break;
                case 4:
                    participants[3]++;
                    break;
                case 5:
                    participants[4]++;
                    break;
            }
            if (pays.Population <= minPopula)   //on vérifie la population à fin d'obtenir l'index de la population plus petit
            {
                indexPopula = index;
                minPopula = pays.Population;
            }
            if (pays.Superficie <= minSuperf)   //on vérifie la superficie à fin d'obtenir l'index de la superficie plus petit
            {
                indexSuperf = index;
                minSuperf = pays.Superficie;
            }
            index++;
        }
        //On cherche dans l'array appelé (participants) l'index du continent avec plus de participants
        int maxParti = Array.IndexOf(participants, participants.Max());
        //On cherche dans l'array appelé (participants) l'index du continent avec moins de participants
        int minParti = Array.IndexOf(participants, participants.Min());
        Console.WriteLine("le continent ayant plus de pays participants: {0}", nomParticipants[maxParti]);
        Console.WriteLine("le continent ayant le moins de pays participants: {0}", nomParticipants[minParti]);
        paysTemp = (Nation)liste[indexPopula];
        Console.WriteLine("le pays le moins peuplé en population: {0}", paysTemp.Nom);
        paysTemp = (Nation)liste[indexSuperf];
        Console.WriteLine("le pays le plus petit en superficie: {0}",paysTemp.Nom );
    }
    //Recherche Binaire d'un pays en passant son nom comme paramètre
    static void Recherche(ArrayList liste, string nomAchercher)
    {
        Nation paysAchercher = new Nation(' ', 0, nomAchercher.ToUpper(), "", 0, 0);
        int k = liste.BinarySearch(paysAchercher);
        if (k > 0)
            Console.WriteLine("On trouve {0}, à l'indice {1} de la liste\n{2}\n", nomAchercher, k+1, liste[k]);
        else
            Console.WriteLine("On ne trouve pas pays {0}\n", nomAchercher);
    }
    //Recherche et affichage d'un groupe en passant son nom comme paramètre
    static void AfficherGroupe(ArrayList liste, string paysAverifier)
    {
        Nation paysAchercher = new Nation(' ', 0, paysAverifier.ToUpper(), "", 0, 0);
        int i = liste.BinarySearch(paysAchercher);
        Nation paysTemp = (Nation)liste[i];
        //On sauvegarde le groupe dans une variable à fin de verifier et comparer chaque'un de pays dans l'arraylist 
        char groupeAverifier = paysTemp.Groupe;
        Console.WriteLine("Groupe {0}, pays que {1} affrontera:\n", groupeAverifier, paysAverifier);
        foreach (Nation pays in liste)
        {
            if (pays.Groupe == groupeAverifier && pays.Nom != paysAverifier.ToUpper())
                Console.WriteLine("{0}", pays);
        }
    }

    static void Main(string[] args)
    {
        ArrayList listePays = new ArrayList();
        Relire(@"WorldCup.txt", listePays);
        Affiche(listePays, 8, 2);
        Modifier(listePays, 0, 'C');
        Modifier(listePays, listePays.Count-1, 1716220);
        Supprimer(listePays, "TUNISIE"); 
        Supprimer(listePays, "SUEDE"); 
        Ajouter(listePays, 'E', 5, "FRANCE", "PARIS", 543964, 61387038);
        Compteur(listePays);
        listePays.Sort();
        Affiche(listePays, 6, 4);
        Recherche(listePays, "Japon"); 
        Recherche(listePays, "Canada");
        Recherche(listePays, "Italie");
        AfficherGroupe(listePays, "BRESIL");
        Affiche(listePays, 11, 30);    
        Console.ReadLine();
    }
}
/* Execution:
  groupe  continent        Nom           Capital     Superficie  Population
---------------------------------------------------------------------------
 1) Z       1     COTE D'IVOIRE        YAMOUSSOUKRO   322460     16962491
 2) G       5     ALLEMAGNE            BERLIN         357027     82537000
 3) H       5     SUISSE               BERNE          41285      7261200
 4) G       2     ETATS-UNIS           WASHINGTON     9629047    291289535
 5) A       5     CROATIE              ZAGREB         56542      4390751
 6) B       5     PAYS-BAS             AMSTERDAM      41526      16135992
 7) F       3     IRAN                 TEHERAN        1648000    76000000
 8) F       2     ARGENTINE            BUENOS AIRES   2766890    37812817
        ..........
        ..........
32) F       1     NIGERIA              NIAMEY         1267000    11058590
33) D       2     URUGUAY              MONTEVIDEO     716220     3360105

le continent ayant plus de pays participants: Europe
le continent ayant le moins de pays participants: Océanie
le pays le moins peuplé en population: URUGUAY
le pays le plus petit en superficie: BELGIQUE
 groupe  continent        Nom           Capital     Superficie  Population
---------------------------------------------------------------------------
 1) H       1     ALGERIE              ALGER          2381740    31763053
 2) G       5     ALLEMAGNE            BERLIN         357027     82537000
 3) D       5     ANGLETERRE           LONDRES        244101     58789194
 4) F       2     ARGENTINE            BUENOS AIRES   2766890    37812817
 5) B       4     AUSTRALIE            CANBERRA       7686850    19834248
 6) H       5     BELGIQUE             BRUXELLES      32545      10309725
        ..........
        ..........
29) H       3     REPUBLIQUE DE COREE  SEOUL          99274      48324000
30) H       5     RUSSIE               MOSCOU         17075400   143954573
31) H       5     SUISSE               BERNE          41285      7261200
32) D       2     URUGUAY              MONTEVIDEO     1716220    3360105

On trouve Japon, à l'indice 24 de la liste
C       3     JAPON                TOKYO          377835     12761000

On ne trouve pas pays Canada

On trouve Italie, à l'indice 23 de la liste
D       5     ITALIE               ROME           301230     57715625

Groupe A, pays que BRESIL affrontera:

A       1     CAMEROUN             YAOUNDE        475440     15746179
A       5     CROATIE              ZAGREB         56542      4390751
A       2     MEXIQUE              MEXICO         1972550    103400165
 */