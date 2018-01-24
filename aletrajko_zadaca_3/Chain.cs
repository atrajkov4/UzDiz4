using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    interface Chain
    {
        void setNextChain(Chain nextChain);
        void obradiStatistiku();
    }
}
