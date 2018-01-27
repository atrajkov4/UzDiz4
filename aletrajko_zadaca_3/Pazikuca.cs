using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Pazikuca
    {
        private static volatile Pazikuca INSTANCE = new Pazikuca();
        private Pazikuca() { }
        public static Pazikuca getInstance()
        {
            return INSTANCE;
        }

        List<MementoLS> savedArticles = new List<MementoLS>();
        public void addMemento(MementoLS m) { savedArticles.Add(m); }

        public MementoLS getMemento() {
           
                return savedArticles.Last();
            
        }
    }
}
