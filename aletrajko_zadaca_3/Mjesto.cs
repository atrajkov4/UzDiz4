using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Mjesto
    {
        //ListaSvegaSG lss = ListaSvegaSG.getInstance();
        GenBrojevaSG r = GenBrojevaSG.getInstance();
        public string naziv { get; set; }
        public int tip { get; set; }
        public int broj_senzora { get; set; }
        public int broj_aktuatora { get; set; }
        public int ID;
        public Mjesto()
        {

        }

        public List<Senzor> ls = new List<Senzor>();
        public List<Aktuator> la = new List<Aktuator>();

        public Mjesto(string n, int t, int bs, int ba)
        {
            naziv = n;
            tip = t;
            broj_aktuatora = ba;
            broj_senzora = bs;
        }


        public string dohvatiNaziv()
        {
            return naziv;
        }
        public int dohvatiTip()
        {
            return tip;
        }
        public int dohvatiBrojSenzora()
        {
            return broj_senzora;
        }
        public int dohvatiBrojAktuatora()
        {
            return broj_aktuatora;
        }

        public void dodijeliID()
        {

            ID = r.dajSlucajniBroj(1, 1000);
            /*if (!lss.postojiID(ID)) lss.dodajID(ID);
            else dodijeliID();
            */
        }
    }
}
