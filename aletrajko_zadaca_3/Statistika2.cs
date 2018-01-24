using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Statistika2 : Chain
    {
        private Chain nextInChain;
        string oznaka = "";
        public Statistika2() {

        }
        public Statistika2(string oz) {
            oznaka = oz;
        }

        public void obradiStatistiku()
         {
            
            ListaSvegaSG ls = ListaSvegaSG.getInstance();
            if (oznaka == "stat2") {
                float s_t = 0;
                float s_f = 0;
                float a_t = 0;
                float a_f = 0;

                foreach (Mjesto m in ls.dajMjesta()) {
                    foreach (Senzor s in m.ls) {
                        if (s.ispravnost) s_t++;
                        else s_f++;
                    }
                    foreach (Aktuator a in m.la) {
                        if (a.ispravnost) a_t++;
                        else a_f++;
                    }
                }
                IspisUpisSG iu = IspisUpisSG.getInstance();
                float hej1 = s_t / (s_t + s_f);
                float hej2 = a_t / (a_t + a_f);
                hej1 *= 100;
                hej2 *= 100;
                iu.print("Ukupno "+ (s_t+s_f) + " senzora, u prosjeku " + hej1.ToString() + "% ispravnih i " + (100-hej1) + "% neispravnih.");
                iu.print("Ukupno " + (a_t + a_f) + " aktuatora, u prosjeku " + (100-hej2) + "% neispravnih.");


            }
            else {
                IspisUpisSG iu = IspisUpisSG.getInstance();
                iu.print("Jedine opcije su stat i stat2");
            }
        }

        public void setNextChain(Chain nextChain)
        {
            this.nextInChain = nextChain;
        }
    }

}
