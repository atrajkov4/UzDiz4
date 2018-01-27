using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class RestoreMemento : CommandIF
    {
        public void execute()
        {
            Originator o = Originator.getInstance();
            Pazikuca p = Pazikuca.getInstance();
            ListaSvegaSG ls = ListaSvegaSG.getInstance();

            ls.SetMemento(o.restoreFromMemento(p.getMemento()));
        }
    }
}
