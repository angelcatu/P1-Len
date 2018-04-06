using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1LFP.Modelos
{
    class Operacion
    {

        private Expresion expresion = new Expresion();
        private Cola cola;
        private List<Pila> listaPila = new List<Pila>();
        
        private List<Operacion> listaOperaciones = new List<Operacion>();

        private List<Expresion> listaExpresiones = Analizador.listaExpresiones;
        List<Operacion> listaOperadoresInfo;

        //private List<String> cola = new List<string>();
        private List<Cola> listaCola = new List<Cola>();
        private List<Celda> listaCeldas;


        private String expAritmetica;
        private int resultado;

        private int precedencia;
        private String operacion;
        private String simbolo;

        public Operacion()
        {
            llenarListaDeOperadores();
        }

        private void llenarListaDeOperadores()
        {
            listaOperadoresInfo = new List<Operacion>();

            listaOperadoresInfo.Add(new Operacion(1, "Suma", "+"));
            listaOperadoresInfo.Add(new Operacion(1, "Resta", "-"));
            listaOperadoresInfo.Add(new Operacion(2, "Multiplicacion", "*"));
            listaOperadoresInfo.Add(new Operacion(2, "Division", "/"));
            listaOperadoresInfo.Add(new Operacion(3, "Agrupacion", "("));
            listaOperadoresInfo.Add(new Operacion(3, "Agrupacion", ")"));
        }

        public Operacion(String expAritmetica, int resultado)
        {
            this.expAritmetica = expAritmetica;
            this.resultado = resultado;
        }

        public Operacion(int precedencia, String operacion, String simbolo)
        {
            this.precedencia = precedencia;
            this.operacion = operacion;
            this.simbolo = simbolo;
        }


        private String numero;

        public int getIndice()
        {
            return indice;
        }


        public void operarExpresion()
        {


            foreach (Expresion expresion in listaExpresiones)
            {

                indice++;
                cola = new Cola(indice);
                listaCeldas = cola.getListaCeldas();
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

                    numero += cadena[i].ToString();

                    if (i == (expresion.Length - 1))
                    {
                        cola.getListaCeldas().Add(new Celda(numero));
                        numero = "";
                    }


                    //Operadores
                } else if (cadena[i] == 42 | cadena[i] == 43 | cadena[i] == 45 | cadena[i] == 47)
                {

                    cola.getListaCeldas().Add(new Celda(numero));
                    numero = "";

                    for (int j = 0; j < listaOperadoresInfo.Count; j++)
                    {
                        if (cadena[i].ToString().Equals(listaOperadoresInfo[j].getSimbolo()))
                        {
                            if (listaPila.Count == 0)
                            {
                                listaPila.Add(new Pila(cadena[i].ToString(), listaOperadoresInfo[j].getPrecedencia()));

                            }
                            else
                            {
                                listaPila.Add(new Pila(cadena[i].ToString(), listaOperadoresInfo[j].getPrecedencia()));
                                obtenerAsociacion();
                            }
                        }
                    }
                    // ( )
                } else if (cadena[i] == 40 | cadena[i] == 41)
                {

                    //(
                    if (cadena[i] == 40)
                    {

                        if (!numero.Equals(""))
                        {
                            cola.getListaCeldas().Add(new Celda(numero));
                            numero = "";

                        }

                        listaPila.Add(new Pila("(", 3));

                        //)
                    }
                    else if (cadena[i] == 41)
                    {
                        if (!numero.Equals(""))
                        {
                            cola.getListaCeldas().Add(new Celda(numero));
                            numero = "";

                        }
                        quitarSimbolosEntreParentesis();
                    }
                }
            }

            while (listaPila.Count > 0)
            {
                cola.getListaCeldas().Add(new Celda(listaPila[listaPila.Count - 1].getValor()));
                listaPila.RemoveAt(listaPila.Count - 1);
            }


            mostrarCola();



            //FIN


        }

        public void mostrarCola()
        {
            String resultado = "";

            indiceAux = 

            foreach (Celda cola in listaCeldas)
            {
                resultado += cola.getCelda();
                
            }

            MessageBox.Show(resultado);
        }


        public void borrarCeldas()
        {
                                       
        }

        private void quitarSimbolosEntreParentesis()
        {
            while (!listaPila[listaPila.Count-1].getValor().Equals("("))
            {
                cola.getListaCeldas().Add(new Celda(listaPila[listaPila.Count - 1].getValor()));                
                listaPila.RemoveAt(listaPila.Count-1);
            }

            if (listaPila[listaPila.Count-1].getValor().Equals("("))
            {
                listaPila.RemoveAt(listaPila.Count-1);
            }
        }

        private void obtenerAsociacion()
        {
            int longitud = listaPila.Count;

            if(listaPila[(longitud - 2)].getValor().Equals("("))
            {

            }

            if (!listaPila[(longitud-2)].getValor().Equals("("))
            {
                while (listaPila[(longitud-2)].getPrecedencia() > listaPila[(longitud-1)].getPrecedencia() 
                        |listaPila[(longitud-2)].getPrecedencia() == listaPila[(longitud-1)].getPrecedencia())
                {
                    if (listaPila[(longitud - 2)].getPrecedencia() > listaPila[longitud-1].getPrecedencia())
                    {
                        
                        cola.getListaCeldas().Add(new Celda(listaPila[longitud - 2].getValor()));
                        listaPila.RemoveAt(longitud - 2);

                    }
                    else if (listaPila[(longitud - 2)].getPrecedencia() == listaPila[longitud-1].getPrecedencia())
                    {
                        cola.getListaCeldas().Add(new Celda(listaPila[longitud - 2].getValor()));
                        listaPila.RemoveAt(longitud - 2);

                    }
                    else if (listaPila[(longitud - 2)].getPrecedencia() < listaPila[longitud-1].getPrecedencia())
                    {

                    }

                    if(listaPila.Count == 1 || listaPila[(listaPila.Count-2)].getValor().Equals("("))
                    {
                        break;
                    }
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
