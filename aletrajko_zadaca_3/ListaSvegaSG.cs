using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class ListaSvegaSG
    {
        private static volatile ListaSvegaSG INSTANCE = new ListaSvegaSG();
        private ListaSvegaSG() { }
        public static ListaSvegaSG getInstance() {
            return INSTANCE;
        }
        public int cpn = 0;
        public void dodajCPN(int n) { cpn = n; }
        int ctr = 0;
        public void incCPN() {
            ctr++;
        }

        List<ColectUnit> kolekcija = new List<ColectUnit>();

        public void pribaviKolekcije() {
            
            for (int i = 0; i < senzori.Count(); i++) {
                ColectUnit ha = new ColectUnit();
                ha.model = senzori[i].ID;
                ha.num = kmin;
                ha.total = 0;
                kolekcija.Add(ha);
            }
            for (int i = 0; i < aktuatori.Count(); i++)
            {
                ColectUnit ha = new ColectUnit();
                ha.model = aktuatori[i].ID;
                ha.num = kmin;
                ha.total = 0;
                kolekcija.Add(ha);
            }

        
        }

        public void tocnoIzKolekcije(int n) {
            foreach (ColectUnit cu in kolekcija) {
                if (cu.model == n) {
                    IspisUpisSG v = IspisUpisSG.getInstance();
                    v.print("");
                    v.print("Model :" + cu.model);
                    v.print("Trenutno :" + cu.num);
                    v.print("Zamjena :" + cu.subs);
                    v.print("Ukupno :" + cu.total);
                    v.print("Max :" + kmax);
                }
            }
        }

        public void dodajUKolekciju(int mdel) {
            IspisUpisSG v = IspisUpisSG.getInstance();
            if (kolekcija.Where(a => a.model == mdel).ToList().Count() > 0)
            {
                foreach (ColectUnit cu in kolekcija)
                {
                    if (cu.model == mdel) {
                        if (cu.total < kmax)
                        {
                            cu.total++;
                        }
                        else v.print("Dodavanje u kolekciju onemoguceno. (total=kmax)");
                    }
                    
                }
            }
            else {
                ColectUnit cu = new ColectUnit();
                cu.model = mdel;
                cu.num = kmin;
                cu.total = 1;
            }
        }

        public void smanjiBroj(int model) {
            for (int i = 0; i < kolekcija.Count(); i++) {
                if (kolekcija[i].model == model) {
                    if (kolekcija[i].num == 0) {
                        IspisUpisSG v = IspisUpisSG.getInstance();
                        v.print("Kolekciji modela " + model + " stize " + kpov + " uređaja.");
                        kolekcija[i].total += kpov;
                        kolekcija[i].num += kpov;
                    }
                    kolekcija[i].subs++;
                    kolekcija[i].num--;
                }
            }
            
        }

        public int vratiBroj(int model) {
            int br = 0;
            foreach (ColectUnit cu in kolekcija) {
                if (cu.model == model) br = cu.num;
            }
            return br;
        }


        int kmin, kmax, kpov;

        

        public void dodajKmin(int n) {
            kmin = n;
        }
        public void dodajKmax(int n) {
            kmax = n;
        }
        public void dodajKpov(int n) {
            kpov = n;
        }
        int sansa = 0;

        public void preuzmiIspravnost(int n) {
            sansa = n;
        }
        public int dajSansu() {
            return sansa;
        }
        List<Senzor> ks = new List<Senzor>();

        public void pokupiKS() {
            ks = senzori.OrderBy(a => a.modelID).ToList();
        }
        public void pokupiKA() {
            ka = aktuatori.OrderBy(a=>a.modelID).ToList();
        }

        List<Aktuator> ka = new List<Aktuator>();
        List<Mjesto> mjesta = new List<Mjesto>();
        List<Senzor> senzori = new List<Senzor>();
        List<Aktuator> aktuatori = new List<Aktuator>();

        List<int> ID = new List<int>();
        List<int> IDM = new List<int>();
 

        List<Mjesto> backup_mjesta2 = new List<Mjesto>();

        public List<Mjesto> CreateMemento() {
            backup_mjesta2.RemoveAll(a => a.ID > 0);
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

            //IspisUpisSG v = IspisUpisSG.getInstance();
            //v.print(backup_mjesta2.Count().ToString() + " " + mjesta.Count().ToString());
            
            return backup_mjesta2;
        }
        
        public void SetMemento(List<Mjesto> bm2) {
            IspisUpisSG v = IspisUpisSG.getInstance();
            try
            {
                mjesta = bm2;
                
            }
            catch (Exception e) {
                v.print(e.ToString());
            }
           
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


        public bool provjeraMM(Mjesto M) {
            bool nadjen = false;
            int occ = 0;
            foreach (Mjesto mn in mjesta) {
                if (mn.mm.Count() > 0) {
                    foreach (Mjesto mnm in mn.mm) {
                        if (M.ID == mnm.ID) {
                            occ++;
                           
                        }
                        
                    }
                }

            }
            if (occ > 1) nadjen = true;
            return nadjen;
        }


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
        public List<int> dajIDM() { return IDM; }
        public void dodajIDM(int n) { IDM.Add(n); }

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
            dodajUKolekciju(s.modelID);
            senzori.Add(s);
        }

        public void dodajAktuator(Aktuator a) {
            dodajUKolekciju(a.modelID);
            aktuatori.Add(a);
        }

        public void aktualizirajMjesta(List<Mjesto> s) {
            mjesta = s;
        }

        

    }
}
