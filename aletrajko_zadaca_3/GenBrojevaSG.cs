using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class GenBrojevaSG
    {
        private static volatile GenBrojevaSG INSTANCE = new GenBrojevaSG();

        private GenBrojevaSG() { }
        private int seed = 0;
        Random r = new Random();

        public static GenBrojevaSG getInstance()
        {
            return INSTANCE;
        }
        
        public int dajSlucajniBroj(int min, int max)
        {
            return r.Next(min, max+1);

        }

        public float dajSlucajniBroj(float min, float max)
        {
            min *= 100;
            max *= 100;
            return (float)r.Next((int)min, (int)max)/100;
            
        }

        public void dodajSjeme(int s)
        {
            if (s > 65535 || s < 99)
            {

                Console.Write("\nNevaljali unos sjemena! (100-65535!)\nGenerirat će se automatsko sjeme.\n");
                seed = (int)DateTime.Now.Ticks;
            }
            else
            {
                seed = s;
            }

        }
        public void generirajSjeme() {
            seed = (int)DateTime.Now.Millisecond;
        }

        public int vratiSjeme() {
            return seed;
        }
        
    }
}
