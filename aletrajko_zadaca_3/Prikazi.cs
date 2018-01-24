using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Prikazi
    {
        
        ListaSvegaSG db = ListaSvegaSG.getInstance();
       
        public Prikazi() {

        }

        public void prikazMj()
        {


            if (db.dajMjesta().Count > 0)
            {
                IspisUpisSG iu = IspisUpisSG.getInstance();
                iu.print("\t\t***MJESTA***");
                iu.print(iu.pofarbaj("crvena") + "\t[Naziv]\t\t[ID]\t[TIP]\t[Br.Senzora]\t[Br.Aktuatora]" + iu.pofarbaj("bijela"));
                foreach (Mjesto m in db.dajMjesta())
                {
                    iu.print(m.dohvatiNaziv() + "\t  " + m.ID.ToString() + "\t   " + m.dohvatiTip() + "\t\t" + m.dohvatiBrojSenzora() + "\t\t " + m.dohvatiBrojAktuatora());


                }
            }

        }

        public void prikazSenz() {
            IspisUpisSG iu = IspisUpisSG.getInstance();
            iu.print("");

            if (db.dajSenzore().Count > 0)
            {
                iu.print("\t\t***SENZORI***");
                iu.print(iu.pofarbaj("crvena") + "\t[Naziv]\t\t[ID]\t[TIP]\t[Vrsta]\t\t[Min/Max]" + iu.pofarbaj("bijela"));
                foreach (Senzor s in db.dajSenzore())
                {
                    if (s.dohvatiNaziv().Length < 8) iu.print(s.dohvatiNaziv() + "\t\t\t  " + s.ID.ToString() + "\t   " + s.dohvatiTip() + "\t\t" + s.dohvatiVrstu() + "\t " + s.dohvatiMinVrijednost() + "/" + s.dohvatiMaxVrijednost());
                    else if (s.dohvatiNaziv().Length >= 8 && s.dohvatiNaziv().Length < 13) iu.print(s.dohvatiNaziv() + "\t\t  " + s.ID.ToString() + "\t   " + s.dohvatiTip() + "\t\t" + s.dohvatiVrstu() + "\t " + s.dohvatiMinVrijednost() + "/" + s.dohvatiMaxVrijednost());
                    else iu.print(s.dohvatiNaziv() + "\t  " + s.ID.ToString() + "\t   " + s.dohvatiTip() + "\t\t" + s.dohvatiVrstu() + "\t " + s.dohvatiMinVrijednost() + "/" + s.dohvatiMaxVrijednost());


                    //iu.print("\tTip : " + s.dohvatiTip()+ "\tVrsta : " + s.dohvatiVrstu()+"\tMin vrijednost: " + s.dohvatiMinVrijednost() + "\tMax vrijednost : " + s.dohvatiMaxVrijednost());

                    // if (s.komentar != null) iu.print("\tKomentar : " + s.dohvatiKomentar());

                }
            }

        }
        public void prikazAkt()
        {
            IspisUpisSG iu = IspisUpisSG.getInstance();
            iu.print("");
            if (db.dajAktuatore().Count > 0)
            {
                iu.print("\t\t***AKTUATORI***");
                iu.print(iu.pofarbaj("crvena") + "\t[Naziv]\t\t\t\t[ID]\t[TIP]\t[Vrsta]\t[Min/Max]" + iu.pofarbaj("bijela"));
                foreach (Aktuator s in db.dajAktuatore())
                {
                    if (s.dohvatiNaziv().Length <= 23) iu.print(s.dohvatiNaziv() + "\t\t\t" + s.ID.ToString() + "\t " + s.dohvatiTip() + "\t " + s.dohvatiVrstu() + "\t " + s.dohvatiMinVrijednost() + "/" + s.dohvatiMaxVrijednost());
                    else if (s.dohvatiNaziv().Length > 23 && s.dohvatiNaziv().Length < 29) iu.print(s.dohvatiNaziv() + "\t\t" + s.ID.ToString() + "\t " + s.dohvatiTip() + "\t " + s.dohvatiVrstu() + "\t " + s.dohvatiMinVrijednost() + "/" + s.dohvatiMaxVrijednost());
                    else if (s.dohvatiNaziv().Length >= 33) iu.print(s.dohvatiNaziv() + "\t" + s.ID.ToString() + "\t " + s.dohvatiTip() + "\t " + s.dohvatiVrstu() + "\t " + s.dohvatiMinVrijednost() + "/" + s.dohvatiMaxVrijednost());
                    else iu.print(s.dohvatiNaziv() + "\t\t" + s.ID.ToString() + "\t " + s.dohvatiTip() + "\t " + s.dohvatiVrstu() + "\t " + s.dohvatiMinVrijednost() + "/" + s.dohvatiMaxVrijednost());


                }
            }
        }
        


        public void prikazSparenih()
        {
            IspisUpisSG iu = IspisUpisSG.getInstance();
            foreach (Mjesto m in db.dajMjesta())
            {
                iu.print(iu.pofarbaj("zuta") + "[" + m.naziv.ToUpper() + "]" + iu.pofarbaj("bijela"));
                iu.print(" [TIP]\t  [ID]\t[VRIJEDNOST]\t[ISPRAVNOST]\t\t[NAZIV]");
                foreach (Senzor s in m.ls)
                {

                    iu.print("Senzor\t  " + s.ID.ToString() + "\t\t" + s.vrijednost.ToString() + "\t " + s.ispravnost.ToString() + "\t     " + s.dohvatiNaziv());
                }
                foreach (Aktuator s in m.la)
                {

                    iu.print("Aktuator  " + s.ID.ToString() + "\t\t" + s.vrijednost.ToString() + "\t " + s.ispravnost.ToString() + "\t     " + s.dohvatiNaziv());
                }
            }
            iu.print("");
            iu.print("Ispis povezanih aktuatora i senzora");

            foreach (Mjesto m in db.dajMjesta())
            {
                iu.print(iu.pofarbaj("zuta") + "[" + m.naziv.ToUpper() + "]" + iu.pofarbaj("bijela"));

                foreach (Aktuator a in m.la)
                {

                    iu.print("\t\t" + a.naziv.ToUpper());
                    iu.print(iu.pofarbaj("crvena") + "\t\t[ID Senzora]\t[Senzor]" + iu.pofarbaj("bijela"));
                    foreach (Senzor s in a.lss)
                    {
                        iu.print("\t\t\t" + s.ID.ToString() + "\t" + s.naziv);
                    }
                    iu.print("");
                }

            }


        }

        public void prikazMjesta(int n) {
            IspisUpisSG iu = IspisUpisSG.getInstance();
            foreach (Mjesto m in db.dajMjesta()) {
                if (m.ID == n)
                {
                    iu.print(iu.pofarbaj("crvena") + "[NAZIV]\t\t\t[ID]\t[max BR.S][max BR.A] [BR.S][BR.A]" + iu.pofarbaj("bijela"));
                    iu.print(m.naziv + "\t  " + m.ID.ToString() + "\t\t" + m.broj_senzora.ToString() + "\t  " + m.broj_aktuatora.ToString() + "\t " + m.ls.Count().ToString() + "\t" + m.la.Count().ToString());

                }
                
            }
        }

        
        public void prikazSenzuatora(int n, string oznaka) {
            IspisUpisSG iu = IspisUpisSG.getInstance();
            if (oznaka == "s")
            {
                foreach(Mjesto m in db.dajMjesta()) {
                    foreach (Senzor s in m.ls)
                    {
                        if (s.ID == n)
                        {
                            iu.print(iu.pofarbaj("crvena") + "[ID]\t[TIP]\t[VRSTA]\t[ISPRAVNOST][VRIJEDNOST][NAZIV]");
                            iu.print(s.ID.ToString() + "\t    " + s.tip.ToString() + "\t      " + s.vrsta.ToString() + "\t"+ s.ispravnost +"\t\t    "+s.vrijednost.ToString()+"\t" + s.naziv);

                        }
                    }
                }
                
            }
            else if (oznaka == "a") {
                foreach(Mjesto m in db.dajMjesta())
                {
                    foreach (Aktuator s in m.la)
                    {
                        if (s.ID == n)
                        {
                            iu.print(iu.pofarbaj("crvena") + "[ID]\t[TIP]\t[VRSTA]\t[ISPRAVNOST][VRIJEDNOST][NAZIV]");
                            iu.print(s.ID.ToString() + "\t    " + s.tip.ToString() + "\t      " + s.vrsta.ToString()+ "\t" + s.ispravnost + "\t\t    " + s.vrijednost.ToString() + "\t" + s.naziv);

                        }
                    }


                }
                
            }
        }
    }
}
