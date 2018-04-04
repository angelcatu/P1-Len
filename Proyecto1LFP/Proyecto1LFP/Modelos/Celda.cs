using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Celda
    {

        private String valor;
        private int precedencia;

        public Celda(String valor, int precedencia)
        {
            this.valor = valor;
            this.precedencia = precedencia;
        }


        public String getValor()
        {
            return valor;
        }

        public int getPrecedencia()
        {
            return precedencia;
        }
    }
}
