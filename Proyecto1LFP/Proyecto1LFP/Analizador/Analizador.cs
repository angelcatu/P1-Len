using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Analizador
    {

        public static List<Token> listaTokens = new List<Token>();
        public static List<Expresion> listaExpresiones = new List<Expresion>();

        private int indice = 0;        
        private String lexema;
        private int fila = 1;
        private int columna = 0;
        private int numCaracter = 0;
        private int numError = 0;
               
        public void analizarEntrada(String cadena)
        {

        }

        private void llenarListaTokens(int numCaracter, String lexema, int fila, int columna )
        {

        }

        private void llenarListaExpresiones(String simbolo)
        {
            listaExpresiones.Add(new Expresion(simbolo));
        }
    }
}
