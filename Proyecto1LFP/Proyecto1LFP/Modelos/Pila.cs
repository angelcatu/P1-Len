using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Pila
    {

        private List<Celda> listaCeldas;
        private String valor;
        private int precedencia;

        public Pila(String valor, int precedencia)
        {
            this.valor = valor;
            this.precedencia = precedencia;

        }

        public Pila(Celda celda)
        {
            this.listaCeldas = new List<Celda>();
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
