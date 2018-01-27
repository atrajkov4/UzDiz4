using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class C_Connector {

        private static volatile C_Connector INSTANCE = new C_Connector();
        private C_Connector() { }
        public static C_Connector getInstance() {
            return INSTANCE;
        }
        private string putanja = System.IO.Directory.GetCurrentDirectory();
        IspisUpisSG iu = IspisUpisSG.getInstance();
        ListaSvegaSG db = ListaSvegaSG.getInstance();
        string filename = "";
        public void preuzmiIme(string t) {
            
            if (Regex.IsMatch(t, @"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"))
            {
                if (t != " " || t != "")
                {
                    if (t.EndsWith(".txt") == false)
                        t += ".txt";
                    filename = t;
                }
                else filename = t;

                //stvaraj raspored!
            }
            else
            {
                if (!t.EndsWith(".txt")) t += ".txt";
                filename = putanja + @"\" + t;
            }


            spariRaspored();
            spariSA();
        }

        void spariMjesta() {

        }

        void spariSA() {
            int c = 0;
            try
            {
                List<Mjesto> lm = db.dajMjesta();
                List<Senzor> ls = db.dajSenzore();
                List<Aktuator> la = db.dajAktuatore();

                string[] linije = System.IO.File.ReadAllLines(filename);
                foreach (string l in linije)
                {

                    if (c > 3)
                    {
                        string[] splitano = l.Split(';');

                        try
                        {
                            if (Int32.Parse(splitano[0]) == 1)
                            {
                                foreach (Mjesto m in lm)
                                {

                                    foreach (Aktuator a in m.la)
                                    {
                                        if (Int32.Parse(splitano[1]) == a.ID)
                                        {
                                            for (int z44 = 2; z44 < splitano.Length; z44++)
                                            {

                                                foreach (Senzor s in m.ls)
                                                {
                                                    if (s.ID == Int32.Parse(splitano[z44]))
                                                    {
                                                        a.lss.Add(s);
                                                    }
                                                }

                                            }

                                        }

                                    }


                                }

                            }
                            db.aktualizirajMjesta(lm);
                        }
                        catch (Exception)
                        {
                            //iu.print(e.ToString());
                            iu.print("[Greška!] Došlo je do greške pri raspoređivanju senzora s aktuatorima.");
                            iu.print("Redak : " + l);

                        };


                    }
                    c++;

                }


            }
            catch (Exception)
            {
                iu.print(" [Raspored] Datoteka s nazivom '" + filename + "' ne postoji. Završetak rada.");
            }
        }

        public void spariRaspored() {
            int c = 0;
            try
            {
                List<Mjesto> lm = db.dajMjesta();
                List<Senzor> ls = db.dajSenzore();
                List<Aktuator> la = db.dajAktuatore();

                string[] linije = System.IO.File.ReadAllLines(filename);
                foreach (string l in linije)
                {
                    
                    if (c > 3)
                    {
                        string[] splitano = l.Split(';');
                        
                        try
                        {
                            if (Int32.Parse(splitano[0]) == 0) {
                                foreach (Mjesto m in lm) {
                                    
                                    if (m.ID == Int32.Parse(splitano[1])) {
                                        
                                        if (Int32.Parse(splitano[2]) == 1) {
                                            //aktuator
                                            if (m.broj_aktuatora > m.la.Count())
                                            {
                                                foreach (Aktuator a in la)
                                                {

                                                    if (a.ID == Int32.Parse(splitano[3]))
                                                    {
                                                        Aktuator b = (Aktuator)a.kloniraj(a.naziv, a.tip, a.vrsta, a.min_vrijednost, a.max_vrijednost);
                                                        b.inicijaliziraj();
                                                        b.generirajVrijednost();
                                                        b.dodijeliID(Int32.Parse(splitano[4]));
                                                        if(b.tip == m.tip || b.tip == 2) m.la.Add(b);
                                                        else iu.print("Tip aktuatora " + b.naziv + " ne paše mjestu " + m.naziv + "!");
                                                    }

                                                }
                                            }
                                            else iu.print(iu.pofarbaj("crvena") + m.naziv + " ima max.broj aktuatora." + iu.pofarbaj("bijela"));
                                        }
                                        if (Int32.Parse(splitano[2]) == 0) {
                                            //senzor

                                            if (m.broj_senzora > m.ls.Count())
                                            {
                                                foreach (Senzor a in ls)
                                                {

                                                    if (a.ID == Int32.Parse(splitano[3]))
                                                    {
                                                        Senzor b = (Senzor)a.kloniraj(a.naziv, a.tip, a.vrsta, a.min_vrijednost, a.max_vrijednost);
                                                        b.inicijaliziraj();
                                                        b.generirajVrijednost();
                                                        b.dodijeliID(Int32.Parse(splitano[4]));
                                                        if (b.tip == m.tip || b.tip == 2) m.ls.Add(b);
                                                        else iu.print("Tip senzora " + b.naziv + " ne paše mjestu " + m.naziv + "!");
                                                    }
                                                  
                                                }
                                            }
                                           else iu.print(iu.pofarbaj("crvena") + m.naziv + " ima max.broj senzora." + iu.pofarbaj("bijela"));
                                        }
                                    }
                                }
                            }
                           

                           db.aktualizirajMjesta(lm);
                        }
                        catch (Exception e)
                        {
                            //iu.print(e.ToString());
                            iu.print("[Greška!] Došlo je do greške pri raspoređivanju uređaja po Mjestima.");
                            iu.print("Redak : " + l);

                        };
                        

                    }
                    c++;

                }


            }
            catch (Exception)
            {
                iu.print(" [Raspored] Datoteka s nazivom '" + filename + "' ne postoji. Završetak rada.");
            }
        }

        

        
    }
    
        

    }

