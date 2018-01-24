using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Aktuator : M_Senzuator, ProtoPlugInIF
    {

        public Aktuator()
        {

        }
        public List<Senzor> lss = new List<Senzor>();
        public int broj_senzora = 0;

        public void listenObserver() {
            progresijaVrijednosti();
        }

        public Aktuator(string naziv, int tip, int vrsta, float min, float max)
        {
            this.naziv = naziv;
            this.tip = tip;
            this.vrsta = vrsta;
            this.min_vrijednost = min;
            this.max_vrijednost = max;
        }

        public object kloniraj(string naziv, int tip, int vrsta, float min, float max)
        {
            return new Aktuator(naziv, tip, vrsta, min, max);
        }
    }
}
