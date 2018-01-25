using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class ListaSvegaSG: Mem_LS
    {
        private static volatile ListaSvegaSG INSTANCE = new ListaSvegaSG();
        private ListaSvegaSG() { }
        public static ListaSvegaSG getInstance() {
            return INSTANCE;
        }

        int sansa = 0;

        public void preuzmiIspravnost(int n) {
            sansa = n;
        }
        public int dajSansu() {
            return sansa;
        }
        
        List<Mjesto> mjesta = new List<Mjesto>();
        List<Senzor> senzori = new List<Senzor>();
        List<Aktuator> aktuatori = new List<Aktuator>();
        List<int> ID = new List<int>();

        List<Mjesto> backup_mjesta2 = new List<Mjesto>();

        public void CreateMemento() {
            foreach (Mjesto r in mjesta) {
                Mjesto g = new Mjesto(r.naziv, r.tip, r.broj_senzora, r.broj_senzora);
                g.ID = r.ID;

                foreach (Senzor ss in r.ls)
                {
                    Senzor s = (Senzor)ss.kloniraj(ss.naziv, ss.tip, ss.vrsta, ss.min_vrijednost, ss.max_vrijednost);
                    s.ID = ss.ID;
                    s.vrijednost = ss.vrijednost;
                    s.uklonjen = ss.uklonjen;
                    s.manjkav = ss.manjkav;
                    s.ispravnost = ss.ispravnost;
                    g.ls.Add(s);

                }

                foreach (Aktuator a in r.la) {
                    Aktuator b = (Aktuator)a.kloniraj(a.naziv,a.tip,a.vrsta,a.min_vrijednost,a.max_vrijednost);
                    b.ID = a.ID;
                    b.vrijednost = a.vrijednost;
                    b.uklonjen = a.uklonjen;
                    b.manjkav = a.manjkav;
                    b.ispravnost = a.ispravnost;

                    foreach (Senzor ss in a.lss) {
                        Senzor s = (Senzor)ss.kloniraj(ss.naziv,ss.tip,ss.vrsta,ss.min_vrijednost,ss.max_vrijednost);
                        s.ID = ss.ID;
                        s.vrijednost = ss.vrijednost;
                        s.uklonjen = ss.uklonjen;
                        s.manjkav = ss.manjkav;
                        s.ispravnost = ss.ispravnost;
                        b.lss.Add(s);

                    }
                    g.la.Add(b);
                    //Dodaj b u listu mjesta
                }
                
                
                
                backup_mjesta2.Add(g);
            }

            IspisUpisSG v = IspisUpisSG.getInstance();
            v.print(backup_mjesta2.Count().ToString() + " " + mjesta.Count().ToString());

        }

        public List<Mjesto> rr() {
            return backup_mjesta2;
        }

        public void SetMemento() {
            mjesta = backup_mjesta2;
            IspisUpisSG v = IspisUpisSG.getInstance();
            v.print(backup_mjesta2.Count().ToString() + " " + mjesta.Count().ToString());
        }

        List<Senzor> kvarni_s = new List<Senzor>();
        List<Aktuator> kvarni_a = new List<Aktuator>();
        List<string> kvarna_m = new List<string>();

        public void prijaviMjesto(string s) { kvarna_m.Add(s); }
        public void prijaviSenzor(Senzor s) { kvarni_s.Add(s); }
        public void prijaviAktuator(Aktuator s) { kvarni_a.Add(s); }

        public List<string> dajKvarnaM() {return kvarna_m; }
        public List<Senzor> dajKvarneS() { return kvarni_s; }
        public List<Aktuator> dajKvarneA(){ return kvarni_a; }

        public int nadjiMax(string oznaka) {
            int broj;
            if (oznaka == "s") {
               // broj = ID.Find(a => a > 10 && a < 99);
                
             broj = ID.Where(a => a > 10 && a < 99).Max()+ 1;
            }
            else if (oznaka == "a") {
                broj = ID.Where(a => a > 100 && a < 999).Max() +1 ;
            }
            else broj = 0;
            return broj;
        }

        public void dodajID(int n) { ID.Add(n); }
        
        public void obrisiID(int n)
        {
            ID.Remove(n);
        }

        
        public List<int> dajID() {
            return ID;
        }

        public List<Mjesto> dajMjesta() {
                return mjesta;
        }

        public List<Senzor> dajSenzore() {
            return senzori;
        }

        public List<Aktuator> dajAktuatore() {
            return aktuatori;
        }

        public void dodajMjesto(Mjesto m) {
            mjesta.Add(m);
        }

        public void dodajSenzor(Senzor s) {
            senzori.Add(s);
        }

        public void dodajAktuator(Aktuator a) {
            aktuatori.Add(a);
        }

        public void aktualizirajMjesta(List<Mjesto> s) {
            mjesta = s;
        }

        

    }
}
