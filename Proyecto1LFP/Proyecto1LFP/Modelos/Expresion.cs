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
        
        public Expresion()
        {

        }

        public Expresion(String simbolo)
        {
            this.simbolo = simbolo;
        }

        public String concatenarExpresion(List<Expresion> listaExpresiones)
        {
            String expresion = "";

            for (int i = 0; i < listaExpresiones.Count; i++)
            {
                expresion += listaExpresiones[i].getSimbolo();
            }

                return expresion;
        }

        public String getSimbolo()
        {
            return simbolo;
        }

    }
}
