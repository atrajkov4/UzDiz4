using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace aletrajko_zadaca_3
{

    class AlgoritamABC
    {


        IspisUpisSG iu = IspisUpisSG.getInstance();
        //ParametriSG par = ParametriSG.getInstance();
        public int bcd=0;


        public AlgoritamABC()
        {
            
            iu.print2("");
           
            iu.print2( iu.pofarbaj("crvena") + "\t\t[ALGORITAM ABC]");

            Thread t = new Thread(NewThread);
            t.Start();
        }


        private static void NewThread()
        {

            C_Parametri par = C_Parametri.getInstance();
            
            Iterator i = new Iterator();
            List<Mjesto> mjesta = new List<Mjesto>();
            IspisUpisSG iu = IspisUpisSG.getInstance();
            ListaSvegaSG ls = ListaSvegaSG.getInstance();
            Statistika stat = new Statistika();

            int TCD1 = par.dajTCD();
            int brojac = 0;
            int bcd1 = 0;
            bcd1 = iu.bcdoer();
            while (brojac < bcd1)
            {

                i.resetiraj();

                mjesta = ls.dajMjesta();
                mjesta.OrderBy(s => s.naziv);
                

                for (; i.dohvatiVrijednost() < mjesta.Count(); i.povecaj())
                {
                    iu.print2("[" + mjesta[i.dohvatiVrijednost()].naziv + "]" + " (ID " + mjesta[i.dohvatiVrijednost()].ID + ")");
                    Thread.Sleep(TCD1 * 1000);
                    iu.print2(iu.pofarbaj("crvena") + "[SENZOR]\t\t\t[ID][ISPRAVNOST][VRIJEDNOST]  [MIN/MAX][ZATAJIO]");
                    foreach (Senzor s in mjesta[i.dohvatiVrijednost()].ls.ToList())

                    {
                        if (s.ispravnost == false) s.manjkav++;
                        if(s.naziv.Length <= 7 ) iu.print2(s.naziv + "\t\t\t\t" + s.ID.ToString() + " " + s.ispravnost + "\t\t" + s.vrijednost.ToString() + "\t" + s.min_vrijednost.ToString() + "/" + s.max_vrijednost.ToString() + "\t      " + s.manjkav.ToString());
                        else if (s.naziv.Length >= 8 && s.naziv.Length < 13) iu.print2(s.naziv + "\t\t\t" + s.ID.ToString() + " " + s.ispravnost + "\t\t" + s.vrijednost.ToString() + "\t" + s.min_vrijednost.ToString() + "/" + s.max_vrijednost.ToString() + "\t      " + s.manjkav.ToString());
                        else iu.print2(s.naziv.Substring(0, Math.Min(s.naziv.Length, 30)) + "\t\t" + s.ID.ToString() + " " + s.ispravnost + "\t\t" + s.vrijednost.ToString() + "\t" + s.min_vrijednost.ToString() + "/" + s.max_vrijednost.ToString() + "\t      " + s.manjkav.ToString());


                        /*iu.print2("\t\tIspravnost [" + s.ispravnost.ToString() + "]");
                        iu.print2("\t\tStara Vrijednost : " + s.vrijednost.ToString() + s.komentar);
                        if (s.ispravnost) s.progresijaVrijednosti();
                        iu.print2("\t\tNova Vrijednost : " + s.vrijednost.ToString() + s.komentar);
                        iu.print2("\t\tMin : " + s.min_vrijednost);
                        iu.print2("\t\tMax : " + s.max_vrijednost);
                        iu.print2("\t\tBroj zatajenja : " + s.manjkav.ToString() + " puta.");
                        */
                        GenBrojevaSG gb = GenBrojevaSG.getInstance();
                        if (s.ispravnost && gb.dajSlucajniBroj(0, 10) > 6) {
                            s.progresijaVrijednosti();
                            foreach (Aktuator a in ls.dajAktuatore()) {
                                if (a.lss.Contains(s)) s.notify(a);
                            }
                        }
                        
                        if (s.manjkav >= 3 && !(s.uklonjen))
                        {
                           ls.prijaviMjesto(mjesta[i.dohvatiVrijednost()].dohvatiNaziv());
                           ls.prijaviSenzor(s);
                            
                            iu.print2("");
                            iu.print2(iu.pofarbaj("crvena") + "=> Sklanja se senzor " + s.naziv);
                            iu.print2(" iz mjesta " + mjesta[i.dohvatiVrijednost()].naziv + "]" + iu.pofarbaj("bijela"));
                            //mjesta[i.dohvatiVrijednost()].ls.Remove(s);

                            bool dobar = false;
                            while (!dobar)
                            {
                                Senzor senz = (Senzor)s.kloniraj(s.naziv, s.tip, s.vrsta, s.min_vrijednost, s.max_vrijednost);
                                senz.inicijaliziraj();

                                if (senz.ispravnost == true)
                                {
                                    senz.generirajVrijednost();
                                    senz.dodajID("s");
                                    ls.dodajID(senz.ID);
                                    dobar = true;
                                    iu.print2(iu.pofarbaj("zelena") + "[Dodaje se senzor]");
                                    
                                    if (senz.naziv.Length <= 7) iu.print2(iu.pofarbaj("zelena") + senz.naziv + "\t\t\t\t" + senz.ID.ToString() + " " + senz.ispravnost + "\t\t" + senz.vrijednost.ToString() + "\t" + senz.min_vrijednost.ToString() + "/" + senz.max_vrijednost.ToString() + "\t      " + senz.manjkav.ToString());
                                    else if (senz.naziv.Length >= 8 && senz.naziv.Length < 13) iu.print2(iu.pofarbaj("zelena") + senz.naziv + "\t\t\t" + senz.ID.ToString() + " " + senz.ispravnost + "\t\t" + senz.vrijednost.ToString() + "\t" + senz.min_vrijednost.ToString() + "/" + senz.max_vrijednost.ToString() + "\t      " + senz.manjkav.ToString());
                                    else iu.print2(iu.pofarbaj("zelena") + senz.naziv.Substring(0, Math.Min(s.naziv.Length, 30)) + "\t\t" + senz.ID.ToString() + " " + senz.ispravnost + "\t\t" + senz.vrijednost.ToString() + "\t" + senz.min_vrijednost.ToString() + "/" + senz.max_vrijednost.ToString() + "\t      " + senz.manjkav.ToString());
                                    
                                }
                            }



                        }
                    }
                    iu.print2(iu.pofarbaj("crvena") + "[AKTUATOR]\t\t\t[ID][ISPRAVNOST][VRIJEDNOST]  [MIN/MAX][ZATAJIO]");
                    foreach (Aktuator a in mjesta[i.dohvatiVrijednost()].la.ToList())
                    {
                        
                        if (a.ispravnost == false) a.manjkav++;
                        if (a.naziv.Length <= 7) iu.print2(a.naziv + "\t\t\t\t" + a.ID.ToString() + " " + a.ispravnost + "\t\t" + a.vrijednost.ToString() + "\t" + a.min_vrijednost.ToString() + "/" + a.max_vrijednost.ToString() + "\t      " + a.manjkav.ToString());
                        else if (a.naziv.Length >= 8 && a.naziv.Length < 13) iu.print2(a.naziv + "\t\t\t" + a.ID.ToString() + " " + a.ispravnost + "\t\t" + a.vrijednost.ToString() + "\t" + a.min_vrijednost.ToString() + "/" + a.max_vrijednost.ToString() + "\t      " + a.manjkav.ToString());
                        else if(a.naziv.Length >= 13 && a.naziv.Length < 24) iu.print2(a.naziv + "\t\t" + a.ID.ToString() + " " + a.ispravnost + "\t\t" + a.vrijednost.ToString() + "\t" + a.min_vrijednost.ToString() + "/" + a.max_vrijednost.ToString() + "\t      " + a.manjkav.ToString());
                        else iu.print2(a.naziv.Substring(0, Math.Min(a.naziv.Length, 30)) + "\t" + a.ID.ToString() + " " + a.ispravnost + "\t\t" + a.vrijednost.ToString() + "\t" + a.min_vrijednost.ToString() + "/" + a.max_vrijednost.ToString() + "\t      " + a.manjkav.ToString());
                        /*
                        iu.print2("\tSenzor - " + a.naziv + " (ID " + a.ID.ToString() + ")");
                        iu.print2("\t\tIspravnost [" + a.ispravnost.ToString() + "]");
                        iu.print2("\t\tStara vrijednost : " + a.vrijednost.ToString() + a.komentar);
                        if (a.ispravnost) a.progresijaVrijednosti();// ako se dezignirani senzor promjenio
                        iu.print2("\t\tNova vrijednost : " + a.vrijednost.ToString() + a.komentar);
                        iu.print2("\t\tMin : " + a.min_vrijednost);
                        iu.print2("\t\tMax : " + a.max_vrijednost);
                        iu.print2("\t\tBroj zatajenja : " + a.manjkav.ToString() + " puta.");
                        */
                        if (a.manjkav >= 3 && !(a.uklonjen))
                        {
                            ls.prijaviMjesto(mjesta[i.dohvatiVrijednost()].dohvatiNaziv());
                            ls.prijaviAktuator(a);
                            iu.print2("");
                            iu.print2(iu.pofarbaj("crvena") + "=> Sklanja se aktuator " + a.naziv);
                            iu.print2(" iz mjesta " + mjesta[i.dohvatiVrijednost()].naziv + "]" + iu.pofarbaj("bijela"));
                            //mjesta[i.dohvatiVrijednost()].la.Remove(a);
                            bool dobar2 = false;
                            while (!dobar2)
                            {
                                Aktuator senz = (Aktuator)a.kloniraj(a.naziv, a.tip, a.vrsta, a.min_vrijednost, a.max_vrijednost);
                                senz.inicijaliziraj();


                                if (senz.ispravnost == true)
                                {
                                    senz.generirajVrijednost();
                                    senz.dodajID("a");
                                    ls.dodajID(senz.ID);
                                    dobar2 = true;
                                    iu.print2(iu.pofarbaj("zelena") + "[Dodaje se aktuator]");
                                    if (senz.naziv.Length <= 7) iu.print2(iu.pofarbaj("zelena") + senz.naziv + "\t\t\t\t" + senz.ID.ToString() + " " + senz.ispravnost + "\t\t" + senz.vrijednost.ToString() + "\t" + senz.min_vrijednost.ToString() + "/" + senz.max_vrijednost.ToString() + "\t      " + senz.manjkav.ToString());
                                    else if (senz.naziv.Length >= 8 && senz.naziv.Length < 13) iu.print2(iu.pofarbaj("zelena") + senz.naziv + "\t\t\t" + senz.ID.ToString() + " " + senz.ispravnost + "\t\t" + senz.vrijednost.ToString() + "\t" + senz.min_vrijednost.ToString() + "/" + senz.max_vrijednost.ToString() + "\t      " + senz.manjkav.ToString());
                                    else if (senz.naziv.Length >= 13 && senz.naziv.Length < 24) iu.print2(iu.pofarbaj("zelena") + senz.naziv + "\t\t" + senz.ID.ToString() + " " + senz.ispravnost + "\t\t" + senz.vrijednost.ToString() + "\t" + senz.min_vrijednost.ToString() + "/" + senz.max_vrijednost.ToString() + "\t      " + senz.manjkav.ToString());
                                    else iu.print2(iu.pofarbaj("zelena") + senz.naziv.Substring(0, Math.Min(senz.naziv.Length, 30)) + "\t" + senz.ID.ToString() + " " + senz.ispravnost + "\t\t" + senz.vrijednost.ToString() + "\t" + senz.min_vrijednost.ToString() + "/" + senz.max_vrijednost.ToString() + "\t      " + senz.manjkav.ToString());
                                    //iu.print2(senz.naziv + " (ID " + senz.ID.ToString() + ", Vrijednost : " + senz.vrijednost.ToString() + ") u mjesto " + mjesta[i.dohvatiVrijednost()].naziv + "]");
                                    mjesta[i.dohvatiVrijednost()].la.Add(senz);
                                }
                            }
                        }
                    }
                    brojac++;
                    iu.print2("");
                    iu.print2("[" + brojac.ToString() + @"\" + bcd1.ToString() + "]");
                    if (brojac >= bcd1) break;

                }
            }
            iu.print("Provjera gotova.");
            iu.postaviNaKomandu();

            

        }



    }
}
