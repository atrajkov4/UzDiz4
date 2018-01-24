using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    interface ProtoPlugInIF
    {
        object kloniraj(string naziv, int tip, int vrsta, float min, float max);
    }
}
