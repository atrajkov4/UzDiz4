using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class M_Senzuator
    {
        public int manjkav = 0;
        public string naziv { get; set; }
        public int tip { get; set; }
        public int vrsta { get; set; }
        public float min_vrijednost { get; set; }
        public float max_vrijednost { get; set; }
        public string komentar { get; set; }
        public bool ispravnost { get; set; }
        public decimal vrijednost;
        public int ID;
        public int modelID;
        public bool uklonjen = false;
        C_Parametri cpar = C_Parametri.getInstance();
        ListaSvegaSG db = ListaSvegaSG.getInstance();
        IspisUpisSG iu = IspisUpisSG.getInstance();
        GenBrojevaSG g = GenBrojevaSG.getInstance();
        

        public M_Senzuator()
        {

        }

        public string dohvatiNaziv()
        {
            return naziv;
        }
        public int dohvatiTip()
        {
            return tip;
        }
        public int dohvatiVrstu()
        {
            return vrsta;
        }

        public float dohvatiMinVrijednost()
        {
            return min_vrijednost;
        }

        public float dohvatiMaxVrijednost()
        {
            return max_vrijednost;
        }

        public string dohvatiKomentar()
        {
            return komentar;
        }

        int sansa = 0;
        

        public bool inicijaliziraj()
        {
            sansa = db.dajSansu();
            
            try {
                
                if (g.dajSlucajniBroj(0,101) <= sansa) ispravnost = true;
                else ispravnost = false;
                return ispravnost;
            }
            catch(Exception){
                iu.print("Prosječna ispravnost uređaja nije zadana. Svi uređaji bit će neispravni.");
                return false;
            }
                
            
            
        }

        public void dodijeliID(int n)
        {
            if (cpar.postojiID(n)) {
                n = db.dajID().Max() +1;
            }
            ID = n;
           
        }

        public void dodajID(string oznaka) {
            dodijeliID(db.nadjiMax(oznaka));
        }
       
        
        public void generirajVrijednost()
        {
            if (this.vrsta == 0 || this.vrsta == 3 || this.vrsta == 1) vrijednost = g.dajSlucajniBroj((int)min_vrijednost, (int)max_vrijednost);
            if (this.vrsta == 2)
            {
                vrijednost = (decimal)g.dajSlucajniBroj(min_vrijednost, max_vrijednost) / 10;
                if (vrijednost > (decimal)max_vrijednost || vrijednost < (decimal)min_vrijednost) generirajVrijednost();
            }
            

        }

        
        public void progresijaVrijednosti()
        {
            if (vrsta == 2)
            {
                Decimal.Round(3);
                float kmin = min_vrijednost;
                float kmax = max_vrijednost;
                bool negativa = false;
                decimal svalue = vrijednost;
                while (svalue == vrijednost || vrijednost < (decimal)min_vrijednost || vrijednost > (decimal)max_vrijednost)
                {
                    if (vrijednost == (decimal)max_vrijednost) negativa = true;
                    if (vrijednost == (decimal)min_vrijednost) negativa = false;
                    if (vrijednost != (decimal)max_vrijednost && !negativa)
                    {
                        kmax = max_vrijednost;
                        kmin = (float)vrijednost;
                        vrijednost = (decimal)g.dajSlucajniBroj(kmin, kmax);
                    }
                    else
                    {
                        kmax = (float)vrijednost;
                        kmin = min_vrijednost;
                        vrijednost = (decimal)g.dajSlucajniBroj(kmin, kmax);
                    }
                }
            }
            else if (vrsta == 3)
            {
                if (vrijednost == 0) vrijednost = 1;
                else vrijednost = 0;
            }
            else
            {
                int kmin = (int)min_vrijednost;
                int kmax = (int)max_vrijednost;
                bool negativa = false;
                int svalue = (int)vrijednost;
                while (svalue == vrijednost || (float)vrijednost > max_vrijednost || (float)vrijednost < min_vrijednost)
                {
                    if ((float)vrijednost == max_vrijednost) negativa = true;
                    if ((float)vrijednost == min_vrijednost) negativa = false;
                    if (vrijednost != kmax & !negativa)
                    {
                        kmax = (int)max_vrijednost;
                        kmin = (int)vrijednost;
                        int d = g.dajSlucajniBroj(kmin, kmax);
                        vrijednost = (decimal)d;
                    }
                    else
                    {
                        kmax = (int)vrijednost;
                        kmin = (int)min_vrijednost;

                        int d = g.dajSlucajniBroj(kmin, kmax);
                        vrijednost = d;
                    }

                }
            }
        }
        

    }


}







