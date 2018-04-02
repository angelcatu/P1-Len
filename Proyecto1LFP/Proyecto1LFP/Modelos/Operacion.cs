using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Operacion
    {
        private List<Expresion> listaExpresiones = Analizador.listaExpresiones;
        private Expresion expresion = new Expresion();

        private int precedencia;
        private String operacion;
        private String simbolo;
        


        public int operarExpresion()
        {
            int resultado = 0;

            return resultado;
        }

        public int getPrecedencia()
        {
            return precedencia;
        }

        public String getOperacion()
        {
            return operacion;
        }

        public String getSimbolo()
        {
            return simbolo;
        }

        public List<Expresion> getListaExpresiones()
        {
            return listaExpresiones;
        }

    }    
}
