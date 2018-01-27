using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
     class MementoLS
    {
          private List<Mjesto> mjesta;

          void Memento(List<Mjesto> m) {

            mjesta = m;
        }

         public List<Mjesto> GetSavedArticle() {
            return mjesta;
        }
    }
}
