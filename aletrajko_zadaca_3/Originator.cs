﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aletrajko_zadaca_3
{
    class Originator
    {
        private static volatile Originator INSTANCE = new Originator();
        private Originator() { }
        public static Originator getInstance()
        {
            return INSTANCE;
        }

        private List<Mjesto> mjesto;

        public void set(List<Mjesto> novoMesto) {
            mjesto = novoMesto;
        }

        public MementoLS storeInMemento() {
            return new MementoLS();
        }

        public List<Mjesto> restoreFromMemento(MementoLS mls) {
            
            return mjesto;
        }
    }
}
