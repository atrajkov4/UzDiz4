using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
    {
        class MjestoBuilder
        {

            
            
            ListaSvegaSG lm = ListaSvegaSG.getInstance();
            C_Parametri cp = C_Parametri.getInstance();
            IspisUpisSG iu = IspisUpisSG.getInstance();

            public MjestoBuilder(string naziv)
            {

                int c = 0;
                try
                {
                    string[] linije = System.IO.File.ReadAllLines(naziv);
                    foreach (string l in linije)
                    {


                        if (c > 0)
                        {
                            string[] splitano = l.Split(';');
                            Mjesto m = new Mjesto();
                            try
                            {
                                m.naziv = splitano[1];
                                m.tip = Int32.Parse(splitano[2]);
                                m.broj_senzora = Int32.Parse(splitano[3]);
                                m.broj_aktuatora = Int32.Parse(splitano[4]);
                            if (cp.postojiID(Int32.Parse(splitano[0]))){
                                iu.print("ID za mjesto '" + splitano[1] + "' već postoji!");
                            }
                            else m.ID = Int32.Parse(splitano[0]);

                            if (cp.postojiMjesto(m.ID))
                            {
                                iu.print("\n[Mjesto '" + m.naziv + "' već postoji!]");
                            }
                            else {
                                lm.dodajMjesto(m);
                                lm.dodajID(m.ID);
                            }
                            
                            }
                            catch (Exception e)
                            {
                            
                            iu.print("[Redak nije odgovarajuće strukturiran!] Došlo je do greške pri objekta Mjesta " + m.naziv + " podataka : " + splitano[0] +";"+splitano[2]+";"+splitano[3]);
                            
                            };


                        }
                        c++;

                    }


                }
                catch (Exception)
                {
                iu.print(" [Mjesta] Datoteka s nazivom '" + naziv + "' ne postoji.");
                iu.print("Završetak rada.");
                }


            }





        }
    }

