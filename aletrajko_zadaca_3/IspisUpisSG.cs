using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class IspisUpisSG
    {
        public static string ANSI_ESC = "\u001B[";
        private int br, brk, bs;
        int gn, pi,tcd1;
        int brojac = 0;
        private static volatile IspisUpisSG INSTANCE = new IspisUpisSG();
        
        private IspisUpisSG() { }
        Prikazi pr = new Prikazi();
        
        public static IspisUpisSG getInstance()
        {
            return INSTANCE;
        }

        public int brl = 0;
        F_Simplifier fs = F_Simplifier.getInstance();
        GenBrojevaSG g = GenBrojevaSG.getInstance();
        C_Parametri cpar = C_Parametri.getInstance();

        public string pofarbaj(string boja) {
            switch (boja) {
                case "crvena":
                    return ANSI_ESC + "31m";
                    break;
                case "plava":
                    return ANSI_ESC + "34m";
                    break;
                case "zelena":
                    return ANSI_ESC + "32m";
                    break;
                case "zuta":
                    return ANSI_ESC + "33m";
                case "ljubicasta":
                    return ANSI_ESC + "35m";
                    break;
                case "bijela":
                    return ANSI_ESC + "37m";
                    break;
                default:
                    return "";

            }

        }
        public void preuzmiGPI(int a, int b,int c) {
            gn = a;
            pi = b;
            tcd1 = c;
        }


        public void preuzmiBrl(int brl)
        {
            this.brl = brl;
        }

        public void getPos() {
            Console.Write(ANSI_ESC + "6n");
        }


        public void print(string s)
        {
            if (br > 0 && brk > 0 && bs > 0)
            {
                
                postavi(brojac, 0);
                if (brojac == (br - brk))
                {
                    postavi(br, bs-6);
                    Console.Write(pofarbaj("zelena") + "press n|N" + pofarbaj("bijela"));
                    postavi(br - brk + 1, 20);
                  mm:Console.Write(ANSI_ESC + "K");
                    string n = Console.ReadLine();
                    if (n.ToLower() != "n") {
                       
                        goto mm;
                        
                    }
                    postavi(br, bs);
                    Console.Write(ANSI_ESC + "K");

                    postavi(0, 0);
                    pobrisiSve();
                    Podjela();
                    brojac = 0;
                }
               
                Console.Write(s);

                postavi(brojac, bs+2);


                Console.Write(pofarbaj("zuta"));
                Console.Write("[" + brojac.ToString() + "]");
                Console.Write(pofarbaj("bijela"));

                brojac++;
            }
            else {
                postavi(brojac, 0);
                Console.Write(s);
                postavi(brojac, bs + 1);


                brojac++;
            }
            
        }

        
        public void preuzmiBrojeve()
        {
            C_Parametri cpar = C_Parametri.getInstance();
            br = cpar.dajBr();
            brk = cpar.dajBrk();
            bs = cpar.dajBs();
        }

        public void Podjela()
        {
            postavi(br - brk, 0);
            for (int i = 0; i < bs; i++) Console.Write("-");
            Console.Write("\nUnesite naredbu >> ");
            Console.Write(pofarbaj("zuta"));
            Console.Write("\n\n\nstanje varijabli: bs[" + bs.ToString() + "],br[" + br.ToString() + "],brk[" + brk.ToString() + "],g["+ gn.ToString()+"],pi%[" +pi.ToString()+"],tcd["+tcd1.ToString()+"]");
            Console.Write(pofarbaj("bijela"));
            postavi(0, 0);
        }

        public void pobrisiSve()
        {
            Console.Write(ANSI_ESC + "2J");
        }

        public void pobrisiLiniju()
        {
            Console.Write(ANSI_ESC + "2K");
        }

        public void postavi(int x, int y)
        {
            Console.Write(ANSI_ESC + x + ";" + y + "f");
        }
        public void postaviPolovicu(int offset) {
            postavi((bs / 2)-offset, brojac);
        }
        public void postaviTrecinu(int offset) {
            postavi((bs / 2)-offset, brojac);

        }
        int bcd4 = 0;

        public void postaviNaKomandu() {
            postavi(br - brk + 1, 20);
        }

        public void cekajUpute() {
            postavi(br - brk + 1, 20);
            Console.Write(ANSI_ESC + "K");
            string cmd = "";
            cmd = Console.ReadLine();
            if (cmd.ToUpper() != "I") {
                string[] dimer = cmd.Split(' ');
                switch (dimer[0]) {
                    case "H":
                        print("Unesite jednu od mogućih naredbi : ");
                        print("M x - ispis podataka mjesta x");
                        print("S x - ispis podataka senzora x");
                        print("A x - ispis podataka aktuatora x");
                        print("SM x - ispis strukture mjesta x");
                        print("TS x  - ispis podataka o kolekciji modela senzora x ");
                        print("TA x  - ispis podataka o kolekciji modela aktuatora x");
                        print("SP - spremi podatke (Mj,Ur)");
                        print("S - ispis statistike");
                        print("VP - vrati spremljene podatke(Mj,Ur)");
                        print("C n - izvršavanje n ciklusa dretve");
                        print("CP n - broj ciklusa dretve nakon kojih je uređaj popravljen(1-99)");
                        print("VF - {stat | stat2}");
                        print("PI n - prosječna ispravnost (0-100%)");
                        print("I - izlaz");
                        break;

                    case "CP":
                        try {
                            ListaSvegaSG ls = ListaSvegaSG.getInstance();
                            if (Int32.Parse(dimer[1]) > 99 || Int32.Parse(dimer[1]) < 1) {
                                print("CP n argument nije u odg.rasponu.");
                            }
                            else ls.dodajCPN(Int32.Parse(dimer[1]));
                        } catch (Exception) {
                            print("CP n argument nije u dobrom formatu.");
                        }
                        break;

                    case "TS":
                        try
                        {
                            ListaSvegaSG ls = ListaSvegaSG.getInstance();
                            ls.tocnoIzKolekcije(Int32.Parse(dimer[1]));
                            

                        }
                        catch (Exception) {
                            print("ID S nije dobar.");
                        }
                        break;

                    case "TA":
                        try
                        {
                            ListaSvegaSG ls = ListaSvegaSG.getInstance();
                            ls.tocnoIzKolekcije(Int32.Parse(dimer[1]));


                        }
                        catch (Exception)
                        {
                            print("ID S nije dobar.");
                        }
                        break;

                    case "SM":

                        try {
                            pr.prikazMMM(Int32.Parse(dimer[1]));
                        } catch (Exception) {
                            print("ID mjesta nije dobar");
                        }
                        break;

                    case "M":
                        try
                        {
                            if (dimer.Length > 1)
                            {
                                pr.prikazMjesta(Int32.Parse(dimer[1]));
                            }

                        }
                        catch (Exception)
                        {
                            if (dimer.Length > 1)
                            {
                                print("ID nije dobar.");
                            }
                        }
                        
                        break;
                    case "S":
                        try {
                            if (dimer.Length > 1)
                            {
                                pr.prikazSenzuatora(Int32.Parse(dimer[1]), "s");
                            }
                            else {
                                Statistika stat = new Statistika();
                                stat.obradi();
                            }

                        } catch (Exception) {
                            print("Loš argument za opciju S.");
                                
                            
                        }
                        break;
                    case "A":
                        try
                        {
                            if (dimer.Length > 1)
                            {
                                pr.prikazSenzuatora(Int32.Parse(dimer[1]), "a");
                            }
                        }
                        catch (Exception)
                        {
                            if (dimer.Length > 1)
                            {
                                print("ID nije dobar.");
                            }
                        }
                        break;
                    case "SP":
                        fs.SP();
                        print("Spremljeno!");
                        break;
                    case "VP":
                        fs.VP();
                        print("Vraćeno!");

                        break;
                    case "C":
                        try
                        {
                           
                            bcd4 = Int32.Parse(dimer[1]);
                            new AlgoritamABC();
                           
                        }
                        catch (Exception) {
                            print("Broj nije dobrog formata.");
                        }
                       
                        break;
                    case "VF":
                        try
                        {
                            Chain chainCalc = new Statistika(dimer[1]);
                            Chain chainCalc2 = new Statistika2(dimer[1]);
                            chainCalc.setNextChain(chainCalc2);
                            chainCalc.obradiStatistiku();
                        }
                        catch (Exception) {

                        }

                        break;
                    case "PI":
                        cpar.dodajIspravnost(Int32.Parse(dimer[1]));
                        break;
                    default:
                        print("Odabrana opcija nije pravilno unesena.");
                        break;
                }

                cekajUpute();
         
    }

   
        }
        public void print2(string s) {
            if (br > 0 && brk > 0 && bs > 0)
            {
                //ako je

                postavi(brojac, 0);
                if (brojac == (br - brk))
                {
                    postavi(br, bs);
                    Console.Write(pofarbaj("zelena") + ">>" + pofarbaj("bijela"));
                    postavi(br - brk + 1, 20);
                    Thread.Sleep(2500);
                    postavi(br, bs);
                    Console.Write(ANSI_ESC + "K");

                    postavi(0, 0);
                    pobrisiSve();
                    Podjela();
                    brojac = 0;
                    
                }
                
                Console.Write(s);

                postavi(brojac, bs + 1);


                Console.Write(pofarbaj("zuta"));
                Console.Write("[" + brojac.ToString() + "]");
                Console.Write(pofarbaj("bijela"));

                brojac++;
            }
            else
            {
                postavi(brojac, 0);
                Console.Write(s);
                postavi(brojac, bs + 1);


                brojac++;
            }
        }


       public int bcdoer() {
            return bcd4;
        }

    }
}
