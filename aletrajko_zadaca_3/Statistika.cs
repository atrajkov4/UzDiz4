using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Statistika : Chain
    {
        IspisUpisSG iu = IspisUpisSG.getInstance();
        ListaSvegaSG ls = ListaSvegaSG.getInstance();
        private Chain nextInChain;
        private string oznaka= "";
        public Statistika()
        {

        }
        public Statistika(string oz) {
            oznaka = oz;
        }

        public void prijaviMjesto(string m)
        {
            ls.prijaviMjesto(m);
        }

        public void prijaviSenzor(Senzor s)
        {
            ls.prijaviSenzor(s);
        }

        public void prijaviAktuator(Aktuator a)
        {
            ls.prijaviAktuator(a);
        }

        public void obradi()
        {
            iu.print("\n\n[STATISTIKA]");

            float cs = ls.dajKvarneS().Count();
            float ca = ls.dajKvarneA().Count();
            iu.print("Broj prijavljenih senzora : " + cs.ToString());
            iu.print("Broj prijavljenih aktuatora :" + ca.ToString());
            if (cs > 0 && ca > 0)
            {
                decimal n;
                Decimal.Round(5);
                if (cs > ca)
                {
                    n = (decimal)(ca / cs);
                    iu.print("Senzori se kvare " + (1 - n) * 100 + "% više od aktuatora!");
                }
                else if (ca > cs)
                {
                    n = (decimal)(cs / ca);
                    iu.print("Aktuatori se kvare " + (1 - n) * 100 + "% više od senzora!");
                }
                else iu.print("Aktuatori se kvare jednako mnogo kao i senzori!");



            }

            Senzor s2 = new Senzor();
            Aktuator a2 = new Aktuator();
            
            if (ls.dajKvarnaM().Count > 0) iu.print("Mjesto s najviše kvarenja je : " + ls.dajKvarnaM().GroupBy(s => s).OrderByDescending(s => s.Count()).First().Key + " sa " + ls.dajKvarnaM().GroupBy(x => x).Max(x => x.Count()).ToString() + " pojave kvara!");
            

        }
        
        public void setNextChain(Chain nextChain)
        {
            this.nextInChain = nextChain;
        }

        public void obradiStatistiku()
        {
            if (oznaka == "stat") {
                obradi();
            }
            else {
                nextInChain.obradiStatistiku();
            }
        }
    }
}
