﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Analizador
    {

        public static List<Token> listaTokens = new List<Token>();
        public static List<Token> listaErrores = new List<Token>();
        public static List<Expresion> listaExpresiones = new List<Expresion>();

        private int estado = 0;
        private int indice = 0;        
        private String lexema;
        private String expresion;
        private int fila = 1;
        private int columna = 0;
        private int numCaracter = 0;
        private int numError = 0;

        public void setFila(int fila)
        {
            this.fila = fila;
        }

        public void setColumna(int columna)
        {
            this.columna = columna;
        }

        public void setNumCaracter(int numCaracter)
        {
            this.numCaracter = numCaracter;
        }

        public void setNumError(int numError)
        {
            this.numError = numError;
        }



        public void analizarEntrada(String cadena)
        {

            char[] caracter = cadena.ToCharArray();

            for (indice = 0; indice < cadena.Length; indice++)
            {
                switch (estado)
                {
                    case 0:

                        if (Char.IsLetter(caracter[indice])) {
                            lexema += caracter[indice];
                            estado = 1;

                            //Nueva línea
                        } else if (caracter[indice] == 10)
                        {
                            estado = 0;
                            fila++;
                            columna = 0;

                            //Retorno del carro
                        } else if (caracter[indice] == 13)
                        {
                            estado = 0;
                            fila++;
                            columna = 0;

                            //Espacio
                        } else if (caracter[indice] == 32)
                        {

                            estado = 0;
                            columna++;
                        }

                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una letra", fila, columna);
                            listaErrores.Add(token);
                        }

                        break;

                    case 1:

                        columna++;

                        if (Char.IsLetter(caracter[indice]))
                        {
                            lexema += caracter[indice];

                            if (lexema.Equals("Resultado") | lexema.Equals("Graficar") | lexema.Equals("Node"))
                            {
                                numCaracter++;
                                llenarListaTokens(numCaracter, lexema, fila, columna);
                                lexema = "";
                            }
                            else
                            {
                                estado = 1;
                            }


                        } else if (Char.IsDigit(caracter[indice]))
                        {
                            estado = 2;
                            expresion += caracter[indice];
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            // {}
                        } else if (caracter[indice] == 123 || caracter[indice] == 125)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            //()
                        } else if (caracter[indice] == 40 || caracter[indice] == 41)
                        {
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            // ;
                        } else if (caracter[indice] == 59)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            llenarListaExpresiones(expresion);

                            if (lexema.Length > 0)
                            {
                                //llenarListaTokens(numCaracter - 1, lexema, fila, columna);
                                listaTokens.Add(new Token(numCaracter, "Tk_IdentificadorDestino", lexema, fila, columna, lexema));
                                lexema = "";
                            }
                            expresion = "";

                            // * + - /
                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47)
                        {
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);


                            estado = 3;

                            // <
                        } else if (caracter[indice] == 60)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 5;
                        }


                        break;

                    case 2:
                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47) {

                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 3;
                        }

                        break;

                    case 3:

                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 4;

                        } else if (caracter[indice] == 40)
                        {
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 1;
                        }


                        break;

                    case 4:

                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47)
                        {
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            estado = 3;

                        } else if (caracter[indice] == 41 || caracter[indice] == 125)
                        {
                            estado = 1;
                            indice--;
                        }


                        break;

                    case 5:

                        columna++;

                        // >
                        if (caracter[indice] == 62)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            // ;
                        }
                        else if (caracter[indice] == 59)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);


                        } else if (Char.IsLetter(caracter[indice]))
                        {
                            estado = 6;
                            numCaracter++;
                            lexema += caracter[indice];


                            // "
                        }
                        else if (caracter[indice] == 34)
                        {                            
                            estado = 11;

                            // }
                        } else if (caracter[indice] == 125)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 1;
                        }

                        break;

                    case 6:

                        columna++;

                        if (Char.IsLetter(caracter[indice]))
                        {

                            lexema += caracter[indice];


                        }
                        else if (Char.IsDigit(caracter[indice]))
                        {

                            lexema += caracter[indice];

                            // _
                        }
                        else if (caracter[indice] == 95)
                        {

                            lexema += caracter[indice];


                            // coma
                        } else if (caracter[indice] == 44)
                        {
                            numCaracter++;

                            if (lexema.Equals("Valor") | lexema.Equals("Operador"))
                            {
                                llenarListaTokens(numCaracter - 1, lexema, fila, columna);
                                estado = 5;
                                lexema = "";
                            }
                            else
                            {
                                listaTokens.Add(new Token(numCaracter - 1, "Tk_Identificador", lexema, fila, columna, "“" + lexema + "”["));
                                lexema = "";
                                estado = 5;

                            }

                            listaTokens.Add(new Token(numCaracter, "Tk_Coma", caracter[indice].ToString(), fila, columna, ""));
                            //llenarListaTokens(numCaracter - 1, lexema, fila, columna);


                            // punto
                        } else if (caracter[indice] == 46)
                        {
                            numCaracter++;
                            listaTokens.Add(new Token(numCaracter - 1, "Tk_IdentificadorOrigen", lexema, fila, columna, "“lexema”:"));
                            listaTokens.Add(new Token(numCaracter, "Tk_Punto", caracter[indice].ToString(), fila, columna, ""));
                            //llenarListaTokens(numCaracter - 1, lexema, fila, columna);                            
                            lexema = "";

                            estado = 7;


                            // <
                        } else if (caracter[indice] == 60)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter - 1, lexema, fila, columna);
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);                            
                            lexema = "";
                            estado = 5;

                        } 


                        break;

                    case 7:

                        if (Char.IsLetter(caracter[indice]))
                        {
                            columna++;
                            lexema += caracter[indice];
                            estado = 8;
                        }
                        break;

                    case 8:

                        columna++;

                        if (Char.IsLetter(caracter[indice]))
                        {
                            lexema += caracter[indice];
                            estado = 8;

                            //Guion
                        } else if (caracter[indice] == 45)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, lexema, fila, columna);
                            lexema = "";

                            lexema += caracter[indice];

                            estado = 9;
                        }

                        break;

                    case 9:

                        columna++;

                        // >
                        if (caracter[indice] == 62)
                        {
                            numCaracter++;
                            lexema += caracter[indice];
                            llenarListaTokens(numCaracter, lexema, fila, columna);
                            lexema = "";
                            estado = 10;

                        }


                        break;

                    case 10:

                        columna++;

                        if (Char.IsLetter(caracter[indice]))
                        {
                            lexema += caracter[indice];

                        }
                        else if (Char.IsDigit(caracter[indice]))
                        {
                            lexema += caracter[indice];


                            // _
                        } else if (cadena[indice] == 95)
                        {
                            lexema += caracter;

                            // ;
                        } else if (cadena[indice] == 59)
                        {
                            numCaracter++;
                            listaTokens.Add(new Token(numCaracter, "Tk_IdentificadorDestino", lexema, fila, columna, ""));
                            lexema = "";
                            estado = 5;
                            
                            indice--;
                        }

                        break;

                    case 11:

                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            
                            lexema += caracter[indice];
                            //llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 12;

                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47)
                        {
                                
                            lexema += caracter[indice];                            
                            //listaTokens.Add(new Token(numCaracter, "Tk_Etiqueta", lexema, fila, columna, ""));

                            estado = 13;
                        }
                        break;

                    case 12:

                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            
                            lexema += caracter[indice];
                            
                            

                            // "
                        }else if(caracter[indice] == 34)
                        {
                            //listaTokens.Add(new Token(numCaracter, "Tk_Etiqueta", lexema, fila, columna, ""));
                            estado = 14;
                            
                        }

                        break;

                    case 13:

                        columna++;

                        //          "
                        if(caracter[indice] == 34)
                        {
                            estado = 14;
                        }

                        break;

                    case 14:

                        numCaracter++;
                        listaTokens.Add(new Token(numCaracter, "Tk_Etiqueta", lexema, fila, columna, "“<f0>|"+lexema+"|<f1>” shape =”record”"));
                        lexema = "";
                        indice--;
                        estado = 5;
                        break;                    

                    case -1:
                        break;

                }
            }
            

        }

        private void llenarListaTokens(int numCaracter, String lexema, int fila, int columna )
        {
                
            switch (lexema)
            {
                case "Resultado":                    
                    listaTokens.Add(new Token(numCaracter, "Tk_Resultado", lexema, fila, columna, ""));

                    break;

                case "Graficar":                    
                    listaTokens.Add(new Token(numCaracter, "Tk_Graficar", lexema, fila, columna, "digraph G{graph[rankdir = “TB”];"));
                    break;

                case "Node":
                    listaTokens.Add(new Token(numCaracter, "Tk_Nodo", lexema, fila, columna, ""));

                    break;

                case "Valor":
                    listaTokens.Add(new Token(numCaracter, "Tk_Tipo", lexema, fila, columna, "label = "));
                    break;

                case "Operador":
                    listaTokens.Add(new Token(numCaracter, "Tk_Tipo", lexema, fila, columna, ""));
                    break;

                case "IZQ":
                    listaTokens.Add(new Token(numCaracter, "Tk_VerticeIZQ", lexema, fila, columna, "f0"));
                    break;

                case "DER":
                    listaTokens.Add(new Token(numCaracter, "Tk_VerticeDER", lexema, fila, columna, "f1"));
                    break;

                case "->":
                    listaTokens.Add(new Token(numCaracter, "Tk_Flecha", lexema, fila, columna, ""));
                    break;

                case "0":

                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));

                    break;

                case "1":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "2":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "3":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "4":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "5":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "6":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "7":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "8":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "9":
                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", lexema, fila, columna, ""));
                    break;

                case "+":
                    listaTokens.Add(new Token(numCaracter, "Tk_Operador", lexema, fila, columna, ""));
                    break;

                case "-":
                    listaTokens.Add(new Token(numCaracter, "Tk_Operador", lexema, fila, columna, ""));
                    break;

                case "*":
                    listaTokens.Add(new Token(numCaracter, "Tk_Operador", lexema, fila, columna, ""));
                    break;

                case "/":
                    listaTokens.Add(new Token(numCaracter, "Tk_Operador", lexema, fila, columna, ""));
                    break;

                case "{":
                    listaTokens.Add(new Token(numCaracter, "Tk_LlaveAbierta", lexema, fila, columna, "node[fontsize = “16” shape = “ellipse”];"));
                    
                    break;

                case "}":
                    listaTokens.Add(new Token(numCaracter, "Tk_LlaveCerrada", lexema, fila, columna, "}"));
                    break;

                case "(":
                    listaTokens.Add(new Token(numCaracter, "Tk_ParentesisAbierto", lexema, fila, columna, ""));
                    break;

                case ")":
                    listaTokens.Add(new Token(numCaracter, "Tk_ParentesisCerrado", lexema, fila, columna, ""));
                    break;

                case "<":
                    listaTokens.Add(new Token(numCaracter, "Tk_MenorQue", lexema, fila, columna, ""));
                    break;

                case ">":
                    listaTokens.Add(new Token(numCaracter, "Tk_MayorQue", lexema, fila, columna, "]"));
                    break;

                case ";":
                    listaTokens.Add(new Token(numCaracter, "Tk_PuntoYComa", lexema, fila, columna, ";"));
                    break;                

        
            }



        }

        private void llenarListaExpresiones(String simbolo)
        {
            listaExpresiones.Add(new Expresion(simbolo));
                 
        }

        
        public void mostrarExpresiones()
        {
            for (int i = 0; i < listaExpresiones.Count; i++)
            {
                Console.WriteLine("La expresión es: " + listaExpresiones[i].getSimbolo());
            }

            listaExpresiones.Clear();
        }

    }
}
