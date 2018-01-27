using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class C_Parametri
    {
        IspisUpisSG iu = IspisUpisSG.getInstance();
        ListaSvegaSG db = ListaSvegaSG.getInstance();
        GenBrojevaSG gb = GenBrojevaSG.getInstance();
        ConcUredjajiFM cu = new ConcUredjajiFM();

        private C_Parametri() { }
        private static volatile C_Parametri INSTANCE = new C_Parametri();
        public static C_Parametri getInstance()
        {
            return INSTANCE;
        }

        

        int br, bs, brk,tcd;
        int kmax, kmin, kpov;
        public int gg;
        public int ppi;

        public int dajBr() {
            return br;
                }
        public int dajBs() {
            return bs;
        }
        public int dajBrk() {
            return brk;
        }

        public bool postojiMjesto(int id)
        {
            bool exists = false;
            foreach (Mjesto m in db.dajMjesta())
            {
                if (m.ID == id) exists = true;
            }
            return exists;
        }
        public int dajTCD() {
            return tcd;
        }

        public bool postojiSenzor(int id)
        {
            bool exists = false;
            foreach (Senzor s in db.dajSenzore())
            {
                if (s.ID == id) exists = true;
            }
            return exists;
        }

        public bool postojiAktuator(int id)
        {
            bool exists = false;
            foreach (Aktuator a in db.dajAktuatore())
            {
                if (a.ID == id) exists = true;
            }
            return exists;
        }

        public bool postojiIDM(int broj)
        {
            bool exists = false;
            foreach (int n in db.dajIDM())
            {
                if (n == broj) exists = true;
            }
            return exists;
        }


        public bool postojiID(int broj)
        {
            bool exists = false;
            foreach (int n in db.dajID())
            {
                if (n == broj) exists = true;
            }
            return exists;
        }

        

        List<string> lokalna = new List<string>();
        public void split(string woah)
        {
            IspisUpisSG iu = IspisUpisSG.getInstance();
            ListaSvegaSG db = ListaSvegaSG.getInstance();
            GenBrojevaSG gb = GenBrojevaSG.getInstance();
            ConcUredjajiFM cu = new ConcUredjajiFM();

            try
            {
                foreach (string p in woah.Split(' '))
                {
                    lokalna.Add(p);
                }

            }
            catch (Exception n)
            {
                iu.print(" Došlo je do pogreške parsiranja argumenata. Obustava rada.");
            }

            bool help = false;
            if (lokalna.FindIndex(a => a.Equals("--help")) != -1)
            {
                iu.print("\n***--Help activated!***");
                help = true;
            }
            
            if (help) iu.print("\nUnesite argumente  : \nlokaciju exe datoteke* \n-g broj_sjemena \n-m ime_datoteke_mjesta* \n-s ime_dat_senzora* \n-a ime_dat_aktuatora* \n-r ime_dat_rasporeda \n-tcd sekunde_trajanja_ciklusa_dretve \n-br broj_redaka_na_ekranu\n-bs broj_stupaca\n-brk broj_redaka_komandi\n-pi prosječni % ispravnosti");
            else
            {

                int index = lokalna.FindIndex(a => a.Equals("-br"));
                if (index == -1)
                {

                    iu.print("");
                    iu.print("Fali broj linija redaka (24-40). Vrijednost će biti 24.");
                    br = 24;
                }
                else
                {
                    try
                    {
                        br = Int32.Parse(lokalna[index + 1]);
                        if (br < 24 || br > 40)
                        {
                            iu.print("Br redaka nije u dobrom rasponu. bit će 24.");
                            br = 24;
                        }
                    }

                    catch (Exception)
                    {
                        iu.print("Broj redaka nije u odgovarajućem formatu(int). Bit će 24.");
                        br = 24;
                    }

                }

                index = lokalna.FindIndex(a => a.Equals("-bs"));
                if (index == -1)
                {
                    iu.print("Fali broj linija stupaca (80-160). Vrijednost će biti 80.");
                    bs = 80;
                }
                else
                {
                    try
                    {
                        bs = Int32.Parse(lokalna[index + 1]);
                        if (bs > 160 || bs < 80)
                        {
                            iu.print("Broj linija stupaca nije u odgovarajućem rasponu. Bit će 80.");
                            bs = 80;
                        }
                    }
                    catch (Exception)
                    {
                        iu.print("Bs nije predan u adekvatnom formatu (int).Bit će 80.");
                        bs = 80;
                    }



                }

                index = lokalna.FindIndex(a => a.Equals("-brk"));
                if (index == -1)
                {
                    iu.print("Fali broj linija redaka komandi (2-5). Vrijednost će biti 2.");
                    brk = 2;
                }
                else
                {
                    try
                    {
                        brk = Int32.Parse(lokalna[index + 1]);
                        if (brk > 5 || brk < 2)
                        {
                            iu.print("Broj redova komandi nije u rasponu. Bit će 2.");
                            brk = 2;
                        }
                    }
                    catch (Exception)
                    {
                        iu.print("Brk nije predan u adekvatnom formatu (int). Bit će 2.");
                        brk = 2;
                    }
                }

                    index = lokalna.FindIndex(a => a.Equals("-g"));
                    if (index != -1)
                    {
                        try
                        {
                            if (Int32.Parse(lokalna[index + 1]) > 65535 || Int32.Parse(lokalna[index + 1]) < 1)
                            {
                                iu.print("Sjeme nije u dobrom rangu. Bit će generirano.");
                                gb.generirajSjeme();
                                gg = gb.vratiSjeme();
                                
                            }
                            else
                            {
                                gb.dodajSjeme(Int32.Parse(lokalna[index + 1]));
                                
                                gg = Int32.Parse(lokalna[index + 1]);
                            }

                        }
                        catch (Exception)
                        {
                            iu.print("\nSjeme nije adekvatnog formata (int).");
                           gb.generirajSjeme();
                            gg = gb.vratiSjeme();
                        }
                    }
                    else {
                        iu.print("Sjeme nije uneseno. Bit će generirano.");
                        gb.generirajSjeme();
                        gg = gb.vratiSjeme();                      

                    }
               
                    
                
                index = lokalna.FindIndex(a => a.Equals("-pi"));
                if (index == -1)
                {
                    iu.print("Fali broj prosječne ispravnosti. Broj će biti slučajno generiran.");
                    ppi = gb.dajSlucajniBroj(0, 100);
                    db.preuzmiIspravnost(ppi);
                }
                else if (lokalna[index + 1].ToString() != "")
                {
                    try
                    {
                        if (Int32.Parse(lokalna[index + 1]) > 100 || (Int32.Parse(lokalna[index + 1]) < 0))
                        {
                            iu.print("PI% nije u rangu (0-100). Bit će generiran.");
                            ppi = gb.dajSlucajniBroj(0, 100);
                            db.preuzmiIspravnost(ppi);

                        }
                        else {
                            ppi = Int32.Parse(lokalna[index + 1]);
                            db.preuzmiIspravnost(Int32.Parse(lokalna[index + 1]));
                        }
                        

                    }
                    catch (Exception)
                    {
                        iu.print("Prosječna ispravnost nije u dobrom formatu. Bit će generirana.");
                        db.preuzmiIspravnost(gb.dajSlucajniBroj(0, 100));
                    }
                }
                index = lokalna.FindIndex(a => a.Equals("-tcd"));
                if (index == -1)
                {
                    iu.print("Fali trajanje ciklusa dretve. Bit će generiran.");
                    tcd = gb.dajSlucajniBroj(1, 17);

                }
                else
                {
                    if (lokalna[index + 1] != "")
                    {
                        try
                        {
                            tcd = Int32.Parse(lokalna[index + 1]);
                        }
                        catch (Exception)
                        {
                            iu.print("TCD nije dobrog formata.");
                            tcd = gb.dajSlucajniBroj(1, 17);
                        }
                    }
                    else
                    {
                        iu.print("TCD nije dobrog formata.");
                        tcd = gb.dajSlucajniBroj(1, 17);
                    }


                }
               

                iu.preuzmiBrojeve();
                iu.preuzmiGPI(gg, ppi,tcd);
                iu.Podjela();
                


                index = lokalna.FindIndex(a => a.Equals("-m"));
                if (index == -1)
                {
                    iu.print("\nNazivu datoteke mjesta fali prekidač -m! Mjesta neće biti učitana.");
                }
                else
                {
                    if (lokalna[index + 1].ToString() != "") cu.stvoriObjekt(lokalna[index + 1], "m");
                }

                index = lokalna.FindIndex(a => a.Equals("-s"));
                if (index == -1)
                {
                    iu.print("Nazivu datoteke senzora fali prekidač -s! Senzori neće biti učitani.");
                }
                else if (lokalna[index + 1].ToString() != "") cu.stvoriObjekt(lokalna[index + 1], "s");


                index = lokalna.FindIndex(a => a.Equals("-a"));
                if (index == -1)
                {
                    iu.print("Nazivu datoteke aktuatora fali prekidač -a!  Aktuatori neće biti učitani.");
                }
                else if (lokalna[index + 1].ToString() != "") cu.stvoriObjekt(lokalna[index + 1], "a");

                index = lokalna.FindIndex(a => a.Equals("-r"));
                if (index == -1)
                {
                    iu.print("Nazivu datoteke rasporeda fali prekidač -r!  Raspored neće biti učitan.");
                }
                else if (lokalna[index + 1].ToString() != "") {
                    C_Connector cc = C_Connector.getInstance();
                    cc.preuzmiIme(lokalna[index + 1]);
                }
                ListaSvegaSG ls = ListaSvegaSG.getInstance();
                index = lokalna.FindIndex(a => a.Equals("-kmax"));
                if (index == -1)
                {
                    iu.print("Nije unesena kmax opcija.(kmax = 100).");
                    ls.dodajKmax(100);
                }
                else {
                    try {
                        kmax = Int32.Parse(lokalna[index + 1]);
                        ls.dodajKmax(kmax);

                    } catch (Exception) {
                        iu.print("Kmax nije dobrog formata.(kmax = 100)");
                        ls.dodajKmax(100);
                    }
                }
                index = lokalna.FindIndex(a => a.Equals("-kmin"));
                if (index == -1)
                {
                    iu.print("Nije unesena kmax opcija.(kmin = 5).");
                    ls.dodajKmin(5);
                }
                else
                {
                    try
                    {
                        kmin = Int32.Parse(lokalna[index + 1]);
                        ls.dodajKmin(kmin);
                    }
                    catch (Exception)
                    {
                        iu.print("Kmin nije dobrog formata.(kmin = 100)");
                        ls.dodajKmin(5);
                    }
                }

                index = lokalna.FindIndex(a => a.Equals("-kpov"));
                if (index == -1)
                {
                    iu.print("Nije unesena kpov opcija.(kpov = 5).");
                    ls.dodajKpov(5);
                }
                else
                {
                    try
                    {
                        kpov = Int32.Parse(lokalna[index + 1]);
                        ls.dodajKpov(kpov);
                    }
                    catch (Exception)
                    {
                        iu.print("Kpov nije dobrog formata.(kpov = 100)");
                        ls.dodajKpov(5);
                    }
                }

            }

           
        }

        

        public void dodajIspravnost(int n) {
            if (n > 100 || n < 0) {
                iu.print("Broj PI% nije dobar.");
                iu.print("Neće biti korišten.");
            }
            else db.preuzmiIspravnost(n);
        }

       
    }
}
