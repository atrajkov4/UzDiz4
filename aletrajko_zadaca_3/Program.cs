using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Program
    {
        public static string ANSI_ESC = "\u001B[";

        static void Main(string[] args)
        {
            
            
            string naredbe = " "; //E:\FAKS\PETO LETO\UzDiz\aletrajko_zadaca_3\aletrajko_zadaca_3\bin\Debug\aletrajko_zadaca_3.exe" -m C:\Users\Benghazi\Desktop\DZ_3_mjesta -s C:\Users\Benghazi\Desktop\DZ_3_senzori -a C:\Users\Benghazi\Desktop\DZ_3_aktuatori -br 30 -brk 2 -bs 80 -r DZ_3_raspored -pi 90-";
            if (args.Count() > 0)
            {
                foreach (string a in args)
                {
                    naredbe += a + " ";
                }
                
                IspisUpisSG v = IspisUpisSG.getInstance();
                v.pobrisiSve();
                F_Simplifier fsim =F_Simplifier.getInstance();
                fsim.InitiateSystem(naredbe);
                fsim.StartSystem();
                ListaSvegaSG ls = ListaSvegaSG.getInstance();
                
                //fsim.Finishing();
               
                
            }
            else {

                IspisUpisSG v = IspisUpisSG.getInstance();
                v.pobrisiSve();
                v.print("Nema argumenata. Obustava rada.");
                
            }
            
        }

       
    }
}

