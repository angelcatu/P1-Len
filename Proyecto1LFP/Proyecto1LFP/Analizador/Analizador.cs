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

                            if (lexema.Equals("Resultado") | lexema.Equals("Graficar"))
                            {
                                numCaracter++;
                                llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            }
                            else
                            {
                                estado = 1;
                            }


                        } else if (Char.IsDigit(caracter[indice]))
                        {
                            estado = 2;
                            expresion += caracter[indice];


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
                                llenarListaTokens(numCaracter - 1, lexema, fila, columna);
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
                            estado = 12;
                        }

                        break;

                    case 6:

                        columna++;

                        if (Char.IsLetter(caracter[indice]))
                        {
                            numCaracter++;
                            lexema += caracter[indice];


                        }
                        else if (Char.IsDigit(caracter[indice]))
                        {
                            numCaracter++;
                            lexema += caracter[indice];

                            // _
                        }
                        else if (caracter[indice] == 95)
                        {
                            numCaracter++;
                            lexema += caracter[indice];


                            // coma
                        } else if (caracter[indice] == 44)
                        {
                            numCaracter++;
                            listaTokens.Add(new Token(numCaracter, "Tk_Coma", caracter[indice].ToString(), fila, columna));
                            llenarListaTokens(numCaracter - 1, lexema, fila, columna);
                            estado = 6;
                            lexema = "";

                            // punto
                        } else if (caracter[indice] == 46)
                        {
                            numCaracter++;
                            listaTokens.Add(new Token(numCaracter, "Tk_Punto", caracter[indice].ToString(), fila, columna));


                            listaTokens.Add(new Token(numCaracter, "Tk_IdentificadorOrigen", lexema, fila, columna));
                            //llenarListaTokens(numCaracter - 1, lexema, fila, columna);                            
                            lexema = "";

                            estado = 7;
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
                            estado = 7;

                            //Guion
                        } else if (caracter[indice] == 45)
                        {

                            llenarListaTokens(numCaracter - 1, lexema, fila, columna);
                            lexema = "";

                            lexema += caracter[indice];

                            estado = 7;
                        }

                        break;

                    case 9:

                        columna++;

                        // >
                        if (caracter[indice] == 62)
                        {
                            lexema += caracter[indice];
                            llenarListaTokens(numCaracter - 1, lexema, fila, columna);
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

                        } else if (cadena[indice] == 95)
                        {
                            lexema += caracter;

                            // ;
                        } else if (cadena[indice] == 59)
                        {
                            estado = 1;
                            indice--;
                        }

                        break;

                    case 11:

                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            lexema += caracter[indice];
                            estado = 12;

                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47)
                        {
                            lexema += caracter[indice];
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
                        listaTokens.Add(new Token(numCaracter, "Tk_Etiqueta", lexema, fila, columna, ""));
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
