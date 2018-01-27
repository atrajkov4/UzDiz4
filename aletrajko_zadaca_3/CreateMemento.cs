using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class CreateMemento : CommandIF
    {
        public void execute()
        {

            ListaSvegaSG ls = ListaSvegaSG.getInstance();
            Originator o = Originator.getInstance();
            Pazikuca p = Pazikuca.getInstance();
            o.set(ls.CreateMemento());
            p.addMemento(o.storeInMemento());
        }
    }
}
