using System;
using System.Text.RegularExpressions;

namespace PmP_Labor_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Feladat 1:
            Console.WriteLine("Adj meg egy szöveget:");
            string szoveg = Console.ReadLine();

            int betuSzam = 0;
            int szamjegySzam = 0;
            int maganhangzoSzam = 0;

            char[] maganhangzok = { 'a', 'á', 'e', 'é', 'i', 'í', 'o', 'ó', 'ö', 'ő', 'u', 'ú', 'ü', 'ű' };

            foreach (char c in szoveg.ToLower())
            {
                if (char.IsLetter(c))
                {
                    betuSzam++;

                    if (Array.Exists(maganhangzok, maganhangzo => maganhangzo == c))
                    {
                        maganhangzoSzam++;
                    }
                }
                else if (char.IsDigit(c))
                {
                    szamjegySzam++;
                }
            }

            Console.WriteLine($"Betűk száma: {betuSzam}");
            Console.WriteLine($"Számjegyek száma: {szamjegySzam}");
            Console.WriteLine($"Magánhangzók száma: {maganhangzoSzam}");

            //Feladat 2:

            Console.WriteLine("Adj meg egy szöveget:");
            string bekertszoveg = Console.ReadLine();

            if (PalindromE(bekertszoveg))
            {
                Console.WriteLine("A szöveg palindrom.");
            }
            else
            {
                Console.WriteLine("A szöveg nem palindrom.");
            }

            //Feladat 3:
            Console.WriteLine("Adj meg egy rendszámot:");
            string rendszam = Console.ReadLine();

            try
            {
                string sztenderdRendszam = SztenderdizalRendszam(rendszam);
                Console.WriteLine($"A sztenderd formátumú rendszám: {sztenderdRendszam}");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Hiba: {e.Message}");
            }

            //Feladat 4:

            Random random = new Random();

            Console.WriteLine("Hány véletlen rendszámot szeretnél generálni?");
            int darabszam = int.Parse(Console.ReadLine());

            for (int i = 0; i < darabszam; i++)
            {
                char elsoBetu1 = (char)random.Next('A', 'Z' + 1);
                char masodikBetu1 = (char)random.Next('A', 'Z' + 1);
                char elsoBetu2 = (char)random.Next('A', 'Z' + 1);
                char masodikBetu2 = (char)random.Next('A', 'Z' + 1);

                int szam = random.Next(100, 1000); // 100-tól 999-ig

                Console.WriteLine($"{elsoBetu1}{masodikBetu1} {elsoBetu2}{masodikBetu2}-{szam}");
            }

            //Feladat 5:

            Console.WriteLine("Adj meg egy email címet:");
            string email = Console.ReadLine();

            bool helyes = true;

            int atIndex = email.IndexOf('@');
            if (atIndex == -1 || email.IndexOf('@', atIndex + 1) != -1)
            {
                helyes = false;
            }

            if (helyes)
            {
                string localPart = email.Substring(0, atIndex);
                string domainPart = email.Substring(atIndex + 1);

                bool tartalmazBetut = false;
                for (int i = 0; i < localPart.Length; i++)
                {
                    if (char.IsLetter(localPart[i]))
                    {
                        tartalmazBetut = true;
                        break;
                    }
                }

                if (!tartalmazBetut)
                {
                    helyes = false;
                }

                if (helyes)
                {
                    int utolsoPontIndex = domainPart.LastIndexOf('.');
                    if (utolsoPontIndex == -1)
                    {
                        helyes = false;
                    }

                    if (helyes)
                    {
                        bool vanBetuVagySzam = false;
                        for (int i = 0; i < utolsoPontIndex; i++)
                        {
                            if (char.IsLetterOrDigit(domainPart[i]))
                            {
                                vanBetuVagySzam = true;
                                break;
                            }
                        }

                        if (!vanBetuVagySzam)
                        {
                            helyes = false;
                        }

                        if (helyes)
                        {
                            if (localPart.Contains('.'))
                            {
                                string[] localPartElements = localPart.Split('.');
                                foreach (string elem in localPartElements)
                                {
                                    if (elem.Length == 0 || !MindenKarakterBetuVagySzam(elem))
                                    {
                                        helyes = false;
                                        break;
                                    }
                                }
                            }

                            if (helyes)
                            {
                                string topLevelDomain = domainPart.Substring(utolsoPontIndex + 1);
                                if (topLevelDomain.Length < 2 || !MindenKarakterBetu(topLevelDomain))
                                {
                                    helyes = false;
                                }
                            }
                        }
                    }
                }
            }

            if (helyes)
            {
                Console.WriteLine("Az email cím helyes.");
            }
            else
            {
                Console.WriteLine("Az email cím helytelen.");
            }

            //Feladat 6:

            //Random rnd = new Random();

            //string sajatNeptunKod = "ABC123";

            //int probalkozasokSzama = 0;
            //string generaltKod = "";

            //do
            //{
            //    probalkozasokSzama++;

            //    char elsoKarakter = (char)rnd.Next('A', 'Z' + 1);

            //    string maradek = "";
            //    for (int i = 0; i < 5; i++)
            //    {
            //        int veletlenErtek = rnd.Next(0, 36); 
            //        if (veletlenErtek < 26)
            //        {
            //            maradek += (char)('A' + veletlenErtek); 
            //        }
            //        else
            //        {
            //            maradek += (veletlenErtek - 26).ToString(); 
            //        }
            //    }

            //    generaltKod = elsoKarakter + maradek;

            //} while (generaltKod != sajatNeptunKod);

            //Console.WriteLine($"A saját Neptun-kód ({sajatNeptunKod}) {probalkozasokSzama}. alkalommal lett generálva.");

            //Feladat 7:

            Console.WriteLine("Adj meg egy szöveget:");
            string adottszoveg = Console.ReadLine();

            Random rnd = new Random();
            string spongeCaseSzoveg = "";

            for (int i = 0; i < adottszoveg.Length; i++)
            {
                char karakter = adottszoveg[i];

                if (char.IsLetter(karakter))
                {
                    if (rnd.Next(2) == 0)
                    {
                        spongeCaseSzoveg += char.ToLower(karakter);
                    }
                    else
                    {
                        spongeCaseSzoveg += char.ToUpper(karakter);
                    }
                }
                else
                {
                    spongeCaseSzoveg += karakter;
                }
            }

            Console.WriteLine("SpongeCase formátumú szöveg:");
            Console.WriteLine(spongeCaseSzoveg);

            //Feladat 8:

            string karakterlanc = "Név;Kor;Város\nAnna;25;Budapest\nBéla;30;Szeged\nCecília;28;Debrecen";

            string[] sorok = karakterlanc.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string[,] tabliz = new string[sorok.Length, 0];

            for (int i = 0; i < sorok.Length; i++)
            {
                string[] oszlopok = sorok[i].Split(';');

                if (i == 0)
                {
                    tabliz = new string[sorok.Length, oszlopok.Length];
                }

                for (int j = 0; j < oszlopok.Length; j++)
                {
                    tabliz[i, j] = oszlopok[j];
                }
            }

            Console.WriteLine("A táblázat tartalma:");
            for (int i = 0; i < tabliz.GetLength(0); i++)
            {
                for (int j = 0; j < tabliz.GetLength(1); j++)
                {
                    Console.Write(tabliz[i, j]);
                    if (j < tabliz.GetLength(1) - 1)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
            }



            static bool MindenKarakterBetu(string szoveg)
            {
                for (int i = 0; i < szoveg.Length; i++)
                {
                    if (!char.IsLetter(szoveg[i]))
                    {
                        return false;
                    }
                }
                return true;
            }

            static bool MindenKarakterBetuVagySzam(string szoveg)
            {
                for (int i = 0; i < szoveg.Length; i++)
                {
                    if (!char.IsLetterOrDigit(szoveg[i]))
                    {
                        return false;
                    }
                }
                return true;
            }

            static string SztenderdizalRendszam(string rendszam)
            {
                string tisztitottRendszam = Regex.Replace(rendszam.ToUpper(), @"[^A-Z0-9]", "");

                if (tisztitottRendszam.Length != 7 || !Regex.IsMatch(tisztitottRendszam.Substring(0, 4), @"^[A-Z]{4}$") || !Regex.IsMatch(tisztitottRendszam.Substring(4), @"^\d{3}$"))
                {
                    throw new FormatException("A megadott rendszám nem alakítható sztenderd formátumra.");
                }

                return $"{tisztitottRendszam.Substring(0, 2)} {tisztitottRendszam.Substring(2, 2)}-{tisztitottRendszam.Substring(4, 3)}";

            }



            static bool PalindromE(string szoveg)
            {
                string tisztitottSzoveg = new string(szoveg.ToLower()
                                            .Where(c => char.IsLetterOrDigit(c))
                                            .ToArray());

                string forditottSzoveg = new string(tisztitottSzoveg.Reverse().ToArray());

                return tisztitottSzoveg == forditottSzoveg;
            }
        }
    }
}
