using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Expresion
    {
        
        private String simbolo;
        private String expresionOriginal;
        
        public Expresion()
        {

        }

        public Expresion(String simbolo, String expresionOriginal)
        {
            this.simbolo = simbolo;
            this.expresionOriginal = expresionOriginal;
        }     

        public String getSimbolo()
        {
            return simbolo;
        }

        public String getExpresionOriginal()
        {
            return expresionOriginal;
        }

    }
}
