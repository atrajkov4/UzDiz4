using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class ConcUredjajiFM : UredjajiFM
    {
        public ConcUredjajiFM() { }
        public string naziv = null;
        private string putanja = System.IO.Directory.GetCurrentDirectory();
        



        public void stvoriObjekt(string naziv1, string oznaka)
        {
            bool da = false;
            if (Regex.IsMatch(naziv1, @"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$"))
            {

                da = true;
                naziv = naziv1;
                if (!naziv.EndsWith(".txt")) naziv += ".txt";
            }
            else
            {
                if (!naziv1.EndsWith(".txt")) naziv1 += ".txt";
                naziv = putanja + @"\" + naziv1;
            }

            //if (naziv1.Contains("..")) iu.print("relativna!");
            //iu.print("");
            //iu.print("putanja cista : " + putanja);
            //iu.print("");
            //iu.print("p2 :" + p2);




            switch (oznaka)
            {
                case "m":

                    new MjestoBuilder(naziv);
                    break;

                case "s":
                   new SenzorBuilder(naziv);
                    break;

                case "a":
                   new AktuatorBuilder(naziv);
                    break;

                default:

                    Console.Write("\nPogreška pri stvaranju objekata. Datoteka '" + naziv1 + "' ne postoji.");
                    break;

            }

            //throw new NotImplementedException();
        }




    }
}
