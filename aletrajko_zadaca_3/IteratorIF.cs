using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    interface IteratorIF
    {
        int dohvatiVrijednost();
        void postaviVrijednost(int broj);
        int sljedeci();
        int prethodni();
        void resetiraj();
        int povecaj();
        int smanji();


    }
}
