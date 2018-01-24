using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Iterator : IteratorIF
    {
        public int brojac = 0;

        public int dohvatiVrijednost()
        {
            return brojac;
        }

        public void postaviVrijednost(int broj)
        {
            brojac = broj;
        }

        public int prethodni()
        {
            return brojac - 1;
        }

        public void resetiraj()
        {
            brojac = 0;
        }

        public int sljedeci()
        {

            return brojac + 1;
        }

        public int povecaj()
        {
            return brojac++;
        }

        public int smanji()
        {
            return brojac--;
        }
    }
}
