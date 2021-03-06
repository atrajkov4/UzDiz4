﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class SenzorBuilder
    {
        
        IspisUpisSG iu = IspisUpisSG.getInstance();
        ListaSvegaSG ls = ListaSvegaSG.getInstance();
        C_Parametri cp = C_Parametri.getInstance();

        public SenzorBuilder(string path)
        {


            try
            {
                int c = 0;
                string[] linije = System.IO.File.ReadAllLines(path);
                foreach (string line in linije)
                {

                    if (c > 0)
                    {
                        string[] splitano = line.Split(';');

                        try
                        {
                            Senzor m = new Senzor();
                            if (cp.postojiID(Int32.Parse(splitano[0])))
                            {
                                iu.print("ID za senzor '" + splitano[1] + "' već postoji!");
                            }
                            else m.ID = Int32.Parse(splitano[0]);
                            m.naziv = splitano[1];
                            m.tip = Int32.Parse(splitano[2]);
                            m.vrsta = Int32.Parse(splitano[3]);
                            m.min_vrijednost = float.Parse(splitano[4]);
                            m.max_vrijednost = float.Parse(splitano[5]);
                            if (splitano[5] != null) m.komentar = splitano[6];
                            if (cp.postojiSenzor(m.ID))
                            {
                                iu.print("[Senzor '" + m.naziv + "' već postoji!]");
                            }
                            else {
                                ls.dodajSenzor(m);
                                ls.dodajID(m.ID);
                            }
                            


                        }
                        catch (Exception e)
                        {
                            iu.print("[Redak nije odgovarajuće strukturiran!] Došlo je do greške pri objekta Senzora " + splitano[1] + " podataka : " + splitano[0] + ";" + splitano[2] + ";" + splitano[3]);

                        }
                    }
                    c++;

                }

            }
            catch (Exception e)
            {
                iu.print(" [Senzori] Datoteka s nazivom '" + path + "' ne postoji.");
                iu.print("Završetak rada.");
            }


        }
    }
}
