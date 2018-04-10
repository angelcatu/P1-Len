using Proyecto1LFP.Message;
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
        private Mensaje mensaje = Envio.mensaje;
        private Cola cola;
        private List<Pila> listaPila = new List<Pila>();

        public static List<Operacion> listaRespuestas = new List<Operacion>();

        private List<Expresion> listaExpresiones = Analizador.listaExpresiones;
        private List<Operacion> listaOperadoresInfo;

        private List<Cola> listaCola = new List<Cola>();
        private List<Celda> listaCeldas;

        
        private int indiceResolver = 0;

        private int idRespuesta = 0;
        private String expAritmetica;
        private float resultado ;
        private int precedencia;
        private String operacion;
        private String simbolo;

        public Operacion()
        {
            llenarListaDeOperadores();            
        }

        public String getExpAritmetica()
        {
            return expAritmetica;
        }

        public float getResultado()
        {
            return resultado;
        }

        public int getIdRespuesta()
        {
            return idRespuesta;
        }

        public void setIdRespuesta(int idRespuesta)
        {
            this.idRespuesta = idRespuesta;
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

        public Operacion(int id, String expAritmetica, float resultado)
        {
            this.idRespuesta = id;
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
        
        public void operarExpresion()
        {


            foreach (Expresion expresion in listaExpresiones)
            {


                try
                {
                    idRespuesta++;
                    cola = new Cola(idRespuesta);
                    listaCeldas = cola.getListaCeldas();
                    crearPostFijo(expresion.getSimbolo());
                    float respuesta = mandarAResolver();
                    String expresionAritmetica = expresion.getExpresionOriginal();
                    if(respuesta != 0.0)
                    {
                        agregarAListaDeRespuestas(idRespuesta, expresionAritmetica, respuesta);
                    }
                    
                }
                catch(Exception e)
                {

                }                                
            }

            crearReporteDeExpresiones();

            //mostrarRespuestas();
            listaRespuestas.Clear();
            listaExpresiones.Clear();

        }

        private void crearReporteDeExpresiones()
        {
            Archivo archivo = new Archivo("ReporteDeExpresiones", "html");
            archivo.crearReporteResultado();
        }

        private void agregarAListaDeRespuestas(int id, string expresionAritmetica, float respuesta)
        {
            listaRespuestas.Add(new Operacion(id, expresionAritmetica, respuesta));
        }

        private void mostrarRespuestas()
        {
            foreach (Operacion operacion in listaRespuestas)
            {
                MessageBox.Show("Expresión: " + operacion.getExpAritmetica() + "\n" + "Resultado: " + operacion.getResultado());
            }
        }

        private void crearPostFijo(string expresion)
        {
            char[] cadena = expresion.ToCharArray();

            for (int i = 0; i < expresion.Length; i++)
            {
                    //Inicio con negativo
                if (esInicio(i)){

                    numero += cadena[0].ToString();

                }else if (Char.IsDigit(cadena[i]))
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

        private bool esInicio(int i)
        {            
            if(i == 0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public float mandarAResolver()
        {
            float respuesta = 0;

          

            for (indiceResolver = 0; indiceResolver < listaCeldas.Count; indiceResolver++)
            {
                respuesta = resolverExpresion(listaCeldas[indiceResolver].getCelda(), indiceResolver);
            }

            return respuesta;
        }

        private float resolverExpresion(String cola, int indice)
        {
            float resultado = 0;
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
                            float asoIzq = Int32.Parse(listaCeldas[indice-2].getCelda());
                            float asoDer = Int32.Parse(listaCeldas[indice-1].getCelda());
                            
                            resultado = resolver(asoIzq, "+", asoDer, indice);
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show("Error: no se puede transformar el valor de string a int o hay un paréntesis sin cerrar ");
                        }


                        break;

                    case "-":

                        try
                        {
                            float asoIzq = Int32.Parse(listaCeldas[indice - 2].getCelda());                            
                            float asoDer = Int32.Parse(listaCeldas[indice - 1].getCelda());
                            
                            resultado = resolver(asoIzq, "-", asoDer, indice);
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show("Error: no se puede transformar el valor de string a int o hay un paréntesis sin cerrar ");
                        }

                        break;

                    case "*":

                        try
                        {
                            float asoIzq = Int32.Parse(listaCeldas[indice - 2].getCelda());
                            float asoDer = Int32.Parse(listaCeldas[indice - 1].getCelda());

                            resultado = resolver(asoIzq, "*", asoDer, indice);
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show("Error: no se puede transformar el valor de string a int o hay un paréntesis sin cerrar ");
                        }

                        break;

                    case "/":

                        try
                        {
                            float asoIzq = Int32.Parse(listaCeldas[indice - 2].getCelda());
                            float asoDer = Int32.Parse(listaCeldas[indice - 1].getCelda());

                            resultado = resolver(asoIzq, "/", asoDer, indice);
                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show("Error: no se puede transformar el valor de string a int o hay un paréntesis sin cerrar ");
                        }

                        break;
                }
            }

            return resultado;
        }        
        private float resolver(float asoIzq, string operador, float asoDer, int indice)
        {
            float resultado = 0;
            float operacion = 0f;

            switch (operador)
            {
                case "+":                
                    operacion = asoIzq + asoDer;

                    break;

                case "-":
                    operacion = asoIzq - asoDer;

                    break;

                case "*":
                    operacion = asoIzq * asoDer;

                    break;

                case "/":
                    operacion = asoIzq/asoDer;

                    break;
            }

            listaCeldas.Insert((indice + 1), new Celda(operacion.ToString()));
            listaCeldas.RemoveAt(indice);            
            listaCeldas.RemoveAt(indice - 1  );
            listaCeldas.RemoveAt(indice - 2);
            this.indiceResolver = indice - 2;

            if (listaCeldas.Count == 1)
            {
                resultado = Int32.Parse(listaCeldas[0].getCelda());
                indiceResolver = listaCeldas.Count;
                mensaje.setMensaje("Expresión aritmética resuelta");
            }
            else
            {
                //indiceResolver = 0;
            }

            return resultado;
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

            if (listaPila[longitud - 1].getValor().Equals("-"))
            {

            }

            if (!listaPila[(longitud-2)].getValor().Equals("("))
            {
                while (listaPila[(longitud-2)].getPrecedencia() > listaPila[(longitud-1)].getPrecedencia() 
                        || listaPila[(longitud-2)].getPrecedencia() == listaPila[(longitud-1)].getPrecedencia())
                {

                    //if (listaPila[longitud - 2].getValor().Equals("*")) { }

                    if (listaPila[(longitud - 2)].getPrecedencia() > listaPila[longitud-1].getPrecedencia())
                    {
                        
                        cola.getListaCeldas().Add(new Celda(listaPila[longitud - 2].getValor()));
                        listaPila.RemoveAt(longitud - 2);
                        longitud = listaPila.Count;

                    }
                    else if (listaPila[(longitud - 2)].getPrecedencia() == listaPila[longitud-1].getPrecedencia())
                    {
                        cola.getListaCeldas().Add(new Celda(listaPila[longitud - 2].getValor()));
                        listaPila.RemoveAt(longitud - 2);
                        longitud = listaPila.Count;

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
