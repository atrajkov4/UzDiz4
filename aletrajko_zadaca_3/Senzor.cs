using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Senzor : M_Senzuator, ProtoPlugInIF, ObserverIF
    {


        public Senzor(){

        }
        public Senzor(string naziv, int tip, int vrsta, float min, float max)
        {
            this.naziv = naziv;
            this.vrsta = vrsta;
            this.tip = tip;
            this.min_vrijednost = min;
            this.max_vrijednost = max;
        }

        GenBrojevaSG g = GenBrojevaSG.getInstance();
       

        public object kloniraj(string naziv, int tip, int vrsta, float min, float max)
        {

            return new Senzor(naziv, tip, vrsta, min, max);
        }

        public void notify(Aktuator a)
        {
            a.listenObserver();
        }
    }
}
