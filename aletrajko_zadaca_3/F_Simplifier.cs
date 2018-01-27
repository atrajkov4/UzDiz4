using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class F_Simplifier

    {

        IspisUpisSG v = IspisUpisSG.getInstance();
        C_Parametri par = C_Parametri.getInstance();
        ListaSvegaSG db = ListaSvegaSG.getInstance();
        Prikazi pr = new Prikazi();
        Originator o = new Originator();
        Pazikuca p = new Pazikuca();

        string naredbe = "";
        private static volatile F_Simplifier INSTANCE = new F_Simplifier();

        private F_Simplifier() {
        }
        public static F_Simplifier getInstance() {
            return INSTANCE;
        }
        

        public void InitiateSystem(string parram) {
            IspisUpisSG v = IspisUpisSG.getInstance();
            v.pobrisiSve();
            naredbe = parram;
            par.split(parram);
        }

        public void StartSystem() {
            IspisUpisSG v = IspisUpisSG.getInstance();
            v.print("");
            //pr.prikazMj();
            //pr.prikazSenz();
            //pr.prikazAkt();
            //pr.prikazSparenih();
            v.cekajUpute();
        }

        public void Finishing() {
            pr.prikazSparenih();
           
        }
        
        
        public void SP() {
            IspisUpisSG v = IspisUpisSG.getInstance();
            ListaSvegaSG ls = ListaSvegaSG.getInstance();
            o.set(ls.CreateMemento());
            p.addMemento(o.storeInMemento());

        }

        public void VP() {
            IspisUpisSG v = IspisUpisSG.getInstance();
            ListaSvegaSG ls = ListaSvegaSG.getInstance();

            ls.SetMemento(o.restoreFromMemento(p.getMemento()));
        }

        /*
        public void Stat() {
            Statistika stat = new Statistika();
            stat.obradi();
        }

        public void M(int n) {
            pr.prikazMjesta(n);
        }
        public void SA(int n,string oznaka) {
            pr.prikazSenzuatora(n, oznaka);
        }
        */

        public void LeaveSys(string m) {
            v.pobrisiSve();
            v.print(m);
            Thread.Sleep(1500);
            //ExitSystem();
        }

        public void ExitSystem() {
            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        
       

    }
}
