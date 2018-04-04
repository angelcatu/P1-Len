using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Operacion
    {
        private List<Operacion> listaOperaciones = new List<Operacion>();

        private List<Expresion> listaExpresiones = Analizador.listaExpresiones;
        private Expresion expresion = new Expresion();

        private List<String> cola = new List<string>();
        private List<String> pila = new List<string>();




        private String expAritmetica;
        private int resultado;

        public Operacion(String expAritmetica, int resultado)
        {
            this.expAritmetica = expAritmetica;
            this.resultado = resultado;
        }

        private int precedencia;
        private String operacion;
        private String simbolo;


        public void operarExpresion()
        {
            

            foreach (Expresion expresion in listaExpresiones)
            {
                crearPostFijo(expresion.getSimbolo());
                
            }
            
        }

        private void crearPostFijo(string expresion)
        {
            char[] cadena = expresion.ToCharArray();

            for (int i = 0; i < expresion.Length; i++)
            {
                if (Char.IsDigit(cadena[i]))
                {


                    //Operadores
                }else if(cadena[i] == 42 | cadena[i] == 43 | cadena[i] == 45 | cadena[i] == 47)
                {


                    // ( )
                }else if(cadena[i] == 40 | cadena[i] == 41)
                {

                }
            }
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
