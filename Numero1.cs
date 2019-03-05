using System;
using System.IO;
/*Jorge Leonardo Trujillo Salas
  TRUJ12059003
  Numero 1*/
class Nation : IComparable
{
    private string nom, capital;
    private int continent;
    private long superficie, population;
    //constructeur
    public Nation (int continent, string nom, string capital , long superficie , long population)
    {
        this.continent = continent;
        this.nom = nom;
        this.capital = capital;
        this.superficie = superficie;
        this.population = population;
    }

    public string Nom
    {
        get { return nom; }
    }

    public int Continent
    {
        get { return continent; }
        set
        {
            continent = value;
        }
    }

    public string Capital
    {
        get { return capital; }
        set
        {
            capital = value;
        }
    }

    public long Population
    {
        get { return population; }
        set
        {
            population = value;
        }
    }

    public double Densite()
    {
        return population / superficie;
    }
    //redéfinition ToString
    public override string ToString()
    {
        return string.Format("{0,-5}{1,-20}{2,-20}{3,-11}{4}", continent, nom, capital, superficie, population);
    }
    //redéfinition CompareTo en vérifiant la propriété nom
    public int CompareTo(object obj)
    {
        Nation autre = (Nation)obj;
        return nom.ToUpper().Trim().CompareTo(autre.nom.ToUpper().Trim());
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
    //surcharge operateur >
    public static bool operator >(Nation a, Nation b)
    {
        return (a.nom.CompareTo(b.nom) > 0);
    }
    //surcharge operateur >
    public static bool operator <(Nation a, Nation b)
    {
        return (a.nom.CompareTo(b.nom) < 0);
    }
    //surcharge operateur >=
    public static bool operator >=(Nation a, Nation b)
    {
        return (a.nom.CompareTo(b.nom) >= 0);
    }
    //surcharge operateur <=
    public static bool operator <=(Nation a, Nation b)
    {
        return (a.nom.CompareTo(b.nom) <= 0);
    }
}
class Numero1
{
    //Méthode pour lire et remplir le tableau
    static void Relire(string nomALire, ref Nation[] tab, out int nbNations)
    {
        nbNations = 0;
        StreamReader aLire = File.OpenText(nomALire);
        string ligneLue; // on lit ligne par ligne
        while ((ligneLue = aLire.ReadLine()) != null)
        {       //On parse chaqu'un des propriétés à partir du string
            int continent = Int32.Parse(ligneLue.Substring(0, 1));
            string nom = ligneLue.Substring(1, 34).Trim();
            string capital = ligneLue.Substring(36, 20).Trim();
            long superficie = Int64 .Parse(ligneLue.Substring(60, 10).Trim());
            long population = Int64.Parse(ligneLue.Substring(70).Trim());
            //On cree l'objet de type Nation             
            Nation temp = new Nation(continent, nom, capital , superficie , population );
            tab[nbNations] = temp; //On l'ajoute au tableau
            nbNations++;    //On augmente le compteur pour obtenir le total des nations lues
        }
        aLire.Close();
    }
    //Méthode pour creer un fichier txt en spécifiant le continent souhaité
    static void CreerTxt(string nomACreer, ref Nation[] tab, int codeContinent, int nbNations)
    {
        FileInfo fichier = new FileInfo(nomACreer);
        StreamWriter aCreer = fichier.CreateText();
        for (int i = 0; i< nbNations; i++)  //Parcourrir le tableau
                if (tab[i].Continent == codeContinent) //Vérifier si la Nation est du même type cherché
                    aCreer.WriteLine(tab[i] + "\n");    //on l'ajoute
            aCreer.Close();        
    }
    //Méthode pour Affiche le tableau
    static void Affiche(Nation[] tab, int borne)
    {
        Console.WriteLine("  code\t  Nom\t\t\tCapital\t\tSuperficie  Population\n---------------------------------------------------------------------------");
        for (int i=0; i<=borne; i++)
            Console.WriteLine("{0,2}) " + tab[i], i+1);
        Console.WriteLine();
    }
    //Méthode pour chercher un pays à partir de son nom
    static int rechercher(Nation[] tab, string nom)
    {
        Nation temp = new Nation(0, nom.ToUpper(), "", 0, 0);
        int k = Array.IndexOf(tab, temp);
        return k;
    }
    //Méthode pour modifier une propriété d'une Nation en passant par paramètre son nom,
    //aussi le type de changement (string) et sa nouvelle valeur
    static void modifier <T>(Nation[] tab, string pays, string typeChange, T valeur, int borne)
    {
        int index = rechercher(tab, pays);
        switch(typeChange)
        {
            case "continent":
                tab[index].Continent = Convert.ToInt32(valeur);
                break;
            case "capital":
                tab[index].Capital = Convert.ToString(valeur);
                break;
            case "population":
                tab[index].Population  *= Convert.ToInt64(valeur);
                break;
        }
    }
    //Méthode pour chercher les nations dont le nom est identique au nom de la capitale 
    static void afficheIdentique (Nation[] tab, int borne)
    {
        for (int i = 0; i<borne; i++)
            if (tab[i].Nom.CompareTo(tab[i].Capital) == 0)
                Console.WriteLine(tab[i]);
        Console.WriteLine();
    }
    //Méthode pour chercher la densite plus petite selon le continent
    static void detMin (Nation[] tab, int continent, string msg, int borne)
    {
        double min = Double.MaxValue;
        int indice = 0;
        for (int i = 0; i<borne;  i++)
            if (tab[i].Densite() < min && tab[i].Continent == continent)
            {
                indice = i;
                min = tab[i].Densite();
            }
        Console.WriteLine("Densité la plus petite en "+msg + ":\n"+ tab[indice]);
    }
    //Méthode pour déterminer et afficher le pays le plus peuplé selon le continent
    static void detMax(Nation[] tab, int continent, string msg, int borne)
    {
        long max = Int64.MinValue;
        int indice = 0;
        for (int i = 0; i < borne; i++)
            if (tab[i].Population > max && tab[i].Continent == continent)
            {
                indice = i;
                max = tab[i].Population;
            }
        Console.WriteLine("Pays le plus peuplé en " + msg + ":\n" + tab[indice]);
    }
    //Méthode pour afficher les nations commençants par une voyelle
    static void afficheVoy(Nation[] tab, int borne)
    {
        string voyelles = "AEIOUY";
        Console.WriteLine("Pays dont le nom commence par une voyelle");
        for (int i=0 ; i<borne ; i++)
            if (voyelles.Contains(tab[i].Nom[0].ToString()))
                Console.WriteLine(tab[i]);
    }
    //Méthode compteur de lettres dans une propriéte calpitale
    static void compteurLettres(Nation[] tab, int continent, string msg, int borne)
    {
        int max = Int32.MinValue, index = 0;
        for (int i = 0; i < borne; i++) //parcourrir le tableau
            if (tab[i].Continent == continent && tab[i].Capital.Length >= max)
            {
                max = tab[i].Capital.Length;
                index = i;  //sauvegader l'index du nouveau max pour procéder à l'afficher à la fin du for
            }
        Console.WriteLine("pays d’"+ msg +" dont  la capitale contient le plus de lettres alphabétiques:\n" + tab[index]);
    }
    //Méthode quicksort appris en classe, utilisant la surcharge des operateurs 
    static void QuickSort(Nation[] tab, int gauche, int droite)
    {
        int indPivot;
        if (droite > gauche) /* au moins 2 Éléments */
        {
            Partitionner(tab, gauche, droite, out indPivot);
            QuickSort(tab, gauche, indPivot - 1);
            QuickSort(tab, indPivot + 1, droite);
        }
    }
    //Méthode partitionner appris en classe, utilisant la surcharge des operateurs
    static void Partitionner(Nation[] tab, int debut, int fin, out int indPivot)
    {
        int g = debut, d = fin;
        Nation valPivot = tab[debut];
        do
        {
            while (g <= d && tab[g] <= valPivot) g++;
            while (tab[d] > valPivot) d--;

            if (g < d) Permuter(ref tab[g], ref tab[d]);

        } while (g <= d);
        Permuter(ref tab[debut], ref tab[d]);
        indPivot = d;
    }
    //Méthode permuter nations dans le tableau
    static void Permuter(ref Nation a, ref Nation b)
    {
        Nation tempo = a;
        a = b;
        b = tempo;
    }
    //Recherche dichotomique version appris en classe
    static void ChercherDichotomique(Nation[] tab, string nom, int borne)
    {
        Nation aChercher = new Nation(0, nom.ToUpper(), "", 0, 0);
        int k = indDicho(aChercher, tab, borne);
        if (k != -1) // trouvé
            Console.WriteLine("On trouve " + nom + ":\n{0}", tab[k]);
        else
            Console.WriteLine("On ne trouve pas la nation " + nom);
    }
    //Recherche dichotomique version appris en classe
    static int indDicho(Nation aChercher, Nation[] tab, int borne)
    {
        int mini = 0, maxi = borne - 1;
        while (mini <= maxi)
        {
            int milieu = (mini + maxi) / 2;
            if (aChercher < tab[milieu])
                maxi = milieu - 1;
            else
                if (aChercher > tab[milieu])
                mini = milieu + 1;
            else
                return milieu;
        }
        return -1;
    }

    static void Main(string[] args)
    {
        int nbLues = 0;
        Nation[] nations = new Nation[250];
        Relire(@"Nations.txt", ref nations, out nbLues);
        Console.WriteLine("Nombre de Nations lues: {0}", nbLues);
        Affiche(nations , 11);
        modifier(nations, "russie", "continent", 5, nbLues);
        modifier(nations, "chine", "capital", "PEKIN", nbLues);
        modifier(nations, "allemagne", "population", 10, nbLues);
        Affiche(nations, 15);
        afficheIdentique(nations, nbLues);
        detMin(nations, 3, "Asie",nbLues);
        detMin(nations, 2, "Amérique",nbLues);
        detMax(nations, 1, "Afrique", nbLues);
        detMax(nations, 3, "Asie", nbLues);
        afficheVoy(nations, nbLues);
        compteurLettres(nations, 1, "Afrique", nbLues);
        QuickSort(nations, 0, nbLues-1);
        Affiche(nations, 9);
        ChercherDichotomique(nations, "Canada", nbLues);
        ChercherDichotomique(nations, "Angleterre", nbLues);
        ChercherDichotomique(nations, "Japon", nbLues);
        ChercherDichotomique(nations, "Mexique", nbLues);
        CreerTxt(@"Oceanie.txt", ref nations, 4, nbLues);
        CreerTxt(@"Asie.txt", ref nations, 3, nbLues);
        Console.ReadLine();
    }
}
/*Execution:
Nombre de Nations lues: 197
  code    Nom                   Capital         Superficie  Population
---------------------------------------------------------------------------
 1) 2    ETATS-UNIS          WASHINGTON          9629047    291289535
 2) 3    CHINE               SHANGHAI            9596960    1273111290
 3) 2    RUSSIE              MOSCOU              17075400   143954573
 4) 4    AUSTRALIE           CANBERRA            7686850    19834248
 5) 3    JAPON               TOKYO               377835     12761000
 6) 5    ALLEMAGNE           BERLIN              357027     8253700
 7) 5    FRANCE              MARSEILLE           543964     61387038
 8) 5    ITALIE              ROME                301230     57715620
 9) 3    COREE DU SUD        SEOUL               99274      48324000
10) 5    ROYAUME-UNI         LONDRES             244101     58789194
11) 2    CUBA                LA HAVANE           100860     11184023
12) 5    UKRAINE             KIEV                603700     48396470

  code    Nom                   Capital         Superficie  Population
---------------------------------------------------------------------------
 1) 2    ETATS-UNIS          WASHINGTON          9629047    291289535
 2) 3    CHINE               PEKIN               9596960    1273111290
 3) 5    RUSSIE              MOSCOU              17075400   143954573
 4) 4    AUSTRALIE           CANBERRA            7686850    19834248
 5) 3    JAPON               TOKYO               377835     12761000
 6) 5    ALLEMAGNE           BERLIN              357027     82537000
 7) 5    FRANCE              MARSEILLE           543964     61387038
 8) 5    ITALIE              ROME                301230     57715620
 9) 3    COREE DU SUD        SEOUL               99274      48324000
10) 5    ROYAUME-UNI         LONDRES             244101     58789194
11) 2    CUBA                LA HAVANE           100860     11184023
12) 5    UKRAINE             KIEV                603700     48396470
13) 5    HONGRIE             BUDAPEST            93030      10106017
14) 5    ROUMANIE            BUCAREST            238390     22272000
15) 5    GRECE               ATHENES             131940     10623835
16) 5    NORVEGE             OSLO                324220     4525116

1    DJIBOUTI            DJIBOUTI            22000      460700
3    KOWEIT              KOWEIT              17820      2041961
5    LUXEMBOURG          LUXEMBOURG          2586       442972
5    MONACO              MONACO              195        31842
2    PANAMA              PANAMA              78200      2845647
5    SAINT MARIN         SAINT MARIN         61         27336
1    SAO TOME            SAO TOME            1001       165034

Densité la plus petite en Asie:
3    MONGOLIE            OULAN-BATOR         1565000    2654999
Densité la plus petite en Amérique:
2    SURINAME            PARAMARIBO          163270     433998
Pays le plus peuplé en Afrique:
1    NIGERIA             ABUJA               923768     133881703
Pays le plus peuplé en Asie:
3    CHINE               PEKIN               9596960    1273111290
Pays dont le nom commence par une voyelle
2    ETATS-UNIS          WASHINGTON          9629047    291289535
4    AUSTRALIE           CANBERRA            7686850    19834248
5    ALLEMAGNE           BERLIN              357027     82537000
5    ITALIE              ROME                301230     57715620
5    UKRAINE             KIEV                603700     48396470
5    ESPAGNE             MADRID              504782     40037995
5    AUTRICHE            VIENNE              83858      8150835
1    ETHIOPIE            ADDIS-ABEBA         1127127    67673031
3    IRAN                TEHERAN             1648000    76000000
3    OUZBEKISTAN         TACHKENT            447400     25563441
2    ARGENTINE           BUENOS AIRES        2766890    37812817
1    AFRIQUE DU SUD      PRETORIA            1219912    42718530
1    EGYPTE              LE CAIRE            995450     74718797
3    INDONESIE           DJAKARTA            1919440    228437870
3    AZERBAIDJAN         BAKU                86100      7830764
5    ISRAEL              JERUSALEM           20770      6116533
3    EMIRATS ARABES UNIS ABOU DHABI          82880      2407460
5    ESTONIE             TALINN              45226      1401945
3    INDE                NEW DELHI           3287590    1029991145
1    ERYTHREE            ASMARA              121320     4465651
3    AFGHANISTAN         KABOUL              652225     29547078
5    ALBANIE             TIRANA              28748      3510484
1    ALGERIE             ALGER               2381740    31763053
5    ANDORRE             ANDORRA LA VELLA    468        67627
1    ANGOLA              LUANDA              1246700    10766471
2    ANTIGUA-ET-BARBUDA  SAINT-JOHNS         442        67448
2    ANTILLES NEERLANDAISESWILLEMSTAD          800        210000
3    ARABIE SAOUDITE     RIYAD               1960582    23513330
3    ARMENIE             EREVAN              29800      3326448
2    ARUBA               ORANJESTAD          193        69000
2    EL SALVADOR         SAN SALVADOR        21041      6122075
2    EQUATEUR            QUITO               283560     13183978
2    ILES CAIMANS        GEORGE TOWN         262        39000
4    ILES SALOMON        HONIARA             28450      480442
2    ILES VIERGES BRITANNIQUESROAD TOWN           153        19000
3    IRAK                BAGDAD              437072     23331985
5    IRLANDE             DUBLIN              70273      3917336
5    ISLANDE             REYKJAVIC           103125     288201
3    OMAN                MASCATE             212460     2622198
1    OUGANDA             KAMPALA             236040     24699073
2    URUGUAY             MONTEVIDEO          176220     3360105
3    YEMEN               SANAA               527970     19349881
pays d'Afrique dont  la capitale contient le plus de lettres alphabétiques:
1    COTE D'IVOIRE       YAMOUSSOUKRO        322460     16962491
  code    Nom                   Capital         Superficie  Population
---------------------------------------------------------------------------
 1) 3    AFGHANISTAN         KABOUL              652225     29547078
 2) 1    AFRIQUE DU SUD      PRETORIA            1219912    42718530
 3) 5    ALBANIE             TIRANA              28748      3510484
 4) 1    ALGERIE             ALGER               2381740    31763053
 5) 5    ALLEMAGNE           BERLIN              357027     82537000
 6) 5    ANDORRE             ANDORRA LA VELLA    468        67627
 7) 1    ANGOLA              LUANDA              1246700    10766471
 8) 2    ANTIGUA-ET-BARBUDA  SAINT-JOHNS         442        67448
 9) 2    ANTILLES NEERLANDAISESWILLEMSTAD          800        210000
10) 3    ARABIE SAOUDITE     RIYAD               1960582    23513330

On trouve Canada:
2    CANADA              OTTAWA              9984670    31499560
On ne trouve pas la nation Angleterre
On trouve Japon:
3    JAPON               TOKYO               377835     12761000
On trouve Mexique:
2    MEXIQUE             MEXICO              1972550    103400165 
*/