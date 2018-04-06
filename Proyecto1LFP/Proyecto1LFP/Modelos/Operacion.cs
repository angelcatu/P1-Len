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

        private List<String> listaColaResultados = new List<string>();
        private List<Cola> listaCola = new List<Cola>();
        private List<Celda> listaCeldas;

        private int indice = 0;
        private String expAritmetica;
        private int resultado;
        private String resultadoPostFija ;

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
                mandarAResolver();
                

            }

            listaExpresiones.Clear();

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
            //FIN


        }

        private int indiceResolver = 0;

        public void mandarAResolver()
        {            
            for (int indiceResolver = 0; indiceResolver < listaCeldas.Count; indiceResolver++)
            {
                resolverExpresion(listaCeldas[indiceResolver].getCelda(), indiceResolver);
            }                                   
        }

        private void resolverExpresion(String cola, int indice)
        {            
            String tipo = evaluarTipo(cola);

            if (tipo.Equals("numero"))
            {

            }else if (tipo.Equals("operador"))
            {
                switch (cola)
                {
                    case "+":
                        try
                        {
                            int asoIzq = Int32.Parse(listaCeldas[indice-2].getCelda());
                            int asoDer = Int32.Parse(listaCeldas[indice - 1].getCelda());

                            resolver(asoIzq, "+", asoDer);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error: no se puede transformar el valor de string a int ");
                        }


                        break;

                    case "-":
                        break;

                    case "*":
                        break;

                    case "/":
                        break;
                }
            }                                    
        }        
        private void resolver(int asoIzq, string operador, int asoDer)
        {
            switch (operador)
            {
                case "+":

                    break;

                case "-":
                    break;

                case "*":
                    break;

                case "/":
                    break;
            }

            indiceResolver = 0;
        }

        private string evaluarTipo(string cola)
        {
            String tipo = "";

            if (cola.Equals("*") | cola.Equals("+") | cola.Equals("-") | cola.Equals("/"))
            {
                tipo = "operador";

            }
            else
            {
                tipo = "numero";
            }
            return tipo;
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

            if (!listaPila[(longitud-2)].getValor().Equals("("))
            {
                while (listaPila[(longitud-2)].getPrecedencia() > listaPila[(longitud-1)].getPrecedencia() 
                        |listaPila[(longitud-2)].getPrecedencia() == listaPila[(longitud-1)].getPrecedencia())
                {

                    //if (listaPila[longitud - 2].getValor().Equals("*")) { }

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
