using Proyecto1LFP.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Analizador 
    {
        private Mensaje mensajeClase = new Mensaje();

        public static List<Token> listaTokens = new List<Token>();
        public static List<Token> listaErrores = new List<Token>();
        public static List<Expresion> listaExpresiones = new List<Expresion>();

        private String mensaje;

        private int estado = 0;
        private int indice = 0;        
        private String lexema;
        private String expresion = "";
        private String expresionOriginal = "";
        private String numero;
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

        public void setMensaje(String mensaje)
        {
            this.mensaje = mensaje;
        }

        public String getMensaje()
        {
            return mensaje;
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
                            fila++;
                            columna = 0;

                            //Espacio
                        } else if (caracter[indice] == 32)
                        {                            
                            columna++;

                            //Regreso de 
                        }
                        else if (caracter[indice] == 13)
                        {
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
                            expresionOriginal += caracter[indice];
                            numero += caracter[indice];
                            

                            // {}
                        } else if (caracter[indice] == 123 || caracter[indice] == 125)
                        {

                            if(caracter[indice] == 125)
                            {
                                if (!numero.Equals(""))
                                {
                                    numCaracter++;
                                    listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                    numero = "";
                                }
                            }                            
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            //()
                        } else if (caracter[indice] == 40 || caracter[indice] == 41)
                        {

                            if (!numero.Equals(""))
                            {
                                numCaracter++;
                                listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                numero = "";
                            }

                            numCaracter++;
                            expresion += caracter[indice];
                            expresionOriginal += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            // ;
                        } else if (caracter[indice] == 59)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);


                            if(!expresion.Equals("") && !expresionOriginal.Equals(""))
                            {
                                llenarListaExpresiones(expresion, expresionOriginal);                                
                            }
                            

                            if (lexema.Length > 0)
                            {
                                //llenarListaTokens(numCaracter - 1, lexema, fila, columna);
                                listaTokens.Add(new Token(numCaracter, "Tk_IdentificadorDestino", lexema, fila, columna, lexema));
                                lexema = "";
                            }
                            expresion = "";
                            expresionOriginal = "";

                            // * + - /
                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47)
                        {

                            if (!numero.Equals(""))
                            {
                                numCaracter++;
                                listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                numero = "";
                            }
                            // *
                            if (caracter[indice] == 42)
                            {
                                //Error
                                numError++;
                                columna++;
                                Token token = new Token(numError, caracter[indice].ToString(), "Error de sintaxis", fila, columna);
                                listaErrores.Add(token);
                                // +
                            }
                            else if (caracter[indice] == 43)
                            {
                                expresionOriginal += caracter[indice];

                                numCaracter++;
                                expresion += caracter[indice];
                                llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                                numCaracter++;
                                expresion += "1";
                                llenarListaTokens(numCaracter, "1", fila, columna);

                                numCaracter++;
                                expresion += "*";
                                llenarListaTokens(numCaracter, "*", fila, columna);

                                estado = 3;

                                // -
                            }
                            else if (caracter[indice] == 45)
                            {
                                expresionOriginal += caracter[indice];

                                numCaracter++;
                                expresion += caracter[indice];
                                llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                                numCaracter++;
                                expresion += "1";
                                llenarListaTokens(numCaracter, "1", fila, columna);

                                numCaracter++;
                                expresion += "*";
                                llenarListaTokens(numCaracter, "*", fila, columna);

                                estado = 3;
                                // /
                            }
                            else if (caracter[indice] == 47)
                            {
                                //Error
                                numError++;
                                columna++;
                                Token token = new Token(numError, caracter[indice].ToString(), "Error de sintaxis", fila, columna);
                                listaErrores.Add(token);
                            }

                            

                            // <
                        } else if (caracter[indice] == 60)
                        {
                            numCaracter++;
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 5;
                        }

                        //Salto
                        else if (caracter[indice] == 10)
                        {
                            
                            fila++;
                            columna = 0;                                                                   
                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            
                            columna++;
                        }

                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }

                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Desconocido1", fila, columna);
                            listaErrores.Add(token);
                        }


                        break;

                    case 2:
                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            numCaracter++;
                            expresionOriginal += caracter[indice];
                            expresion += caracter[indice];
                            numero += caracter[indice];

                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47) {

                            if (!numero.Equals(""))
                            {
                                numCaracter++;
                                listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                numero = "";
                            }

                            numCaracter++;
                            expresionOriginal += caracter[indice];
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            estado = 3;
                        }
                        else if (caracter[indice] == 10)
                        {
                            estado = 2;
                            fila++;
                            columna = 0;                           

                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 2;
                            columna++;

                            // (
                        }else if(caracter[indice] == 40)
                        {
                            expresionOriginal += caracter[indice];

                            if (!numero.Equals(""))
                            {
                                numCaracter++;
                                listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                numero = "";
                            }

                            numCaracter++;
                            expresion += "*";                            
                            llenarListaTokens(numCaracter, "*", fila, columna);
                            
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 3;

                            
                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una numero o un operador", fila, columna);
                            listaErrores.Add(token);
                        }

                        break;

                    case 3:

                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {
                            numCaracter++;
                            expresionOriginal += caracter[indice];
                            if (listaTokens[listaTokens.Count - 1].getLexema().Equals("1")){
                                listaTokens.RemoveAt(listaTokens.Count - 1);
                                
                            }

                            expresion += caracter[indice];                            
                            numero += caracter[indice];
                            estado = 4;

                        } else if (caracter[indice] == 40)
                        {
                            expresionOriginal += caracter[indice];
                            if (!numero.Equals(""))
                            {
                                numCaracter++;
                                listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                numero = "";
                            }

                            if (listaTokens[listaTokens.Count-1].getToken().Equals("Tk_Numero"))
                            {
                                numCaracter++;
                                expresion += "*";
                                llenarListaTokens(numCaracter, "*", fila, columna);
                            }
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 1;
                        }
                        else if (caracter[indice] == 10)
                        {
                            estado = 3;
                            fila++;
                            columna = 0;
                           
                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 3;
                            columna++;

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una digito o un (", fila, columna);
                            listaErrores.Add(token);
                        }


                        break;

                    case 4:

                        columna++;

                        if (Char.IsDigit(caracter[indice]))
                        {

                            expresionOriginal += caracter[indice];
                            numCaracter++;
                            expresion += caracter[indice];
                            numero += caracter[indice];

                        } else if (caracter[indice] == 42 || caracter[indice] == 43 || caracter[indice] == 45 || caracter[indice] == 47)
                        {
                            if (!numero.Equals(""))
                            {
                                numCaracter++;
                                listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                numero = "";
                            }


                            expresionOriginal += caracter[indice];
                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);

                            estado = 3;



                        } else if (caracter[indice] == 41 || caracter[indice] == 125)
                        {
                            estado = 1;
                            indice--;

                        }
                        else if (caracter[indice] == 40)
                        {

                            if (!numero.Equals(""))
                            {
                                numCaracter++;
                                listaTokens.Add(new Token(numCaracter, "Tk_Numero", numero, fila, columna, ""));
                                numero = "";
                            }

                            expresionOriginal += caracter[indice];
                            numCaracter++;
                            expresion += "*";
                            llenarListaTokens(numCaracter, "*", fila, columna);

                            numCaracter++;
                            expresion += caracter[indice];
                            llenarListaTokens(numCaracter, caracter[indice].ToString(), fila, columna);
                            estado = 3;
                           
                        } else if (caracter[indice] == 10)
                        {
                            estado = 4;
                            fila++;
                            columna = 0;


                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 4;
                            columna++;

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Desconocido", fila, columna);
                            listaErrores.Add(token);
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
                        else if (caracter[indice] == 10)
                        {
                            estado = 5;
                            fila++;
                            columna = 0;
                           
                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 5;
                            columna++;

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Desconocido", fila, columna);
                            listaErrores.Add(token);
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
                                listaTokens.Add(new Token(numCaracter - 1, "Tk_Identificador", lexema, fila, columna, " \"" + lexema + "\"["));
                                lexema = "";
                                estado = 5;

                            }

                            listaTokens.Add(new Token(numCaracter, "Tk_Coma", caracter[indice].ToString(), fila, columna, ""));
                            //llenarListaTokens(numCaracter - 1, lexema, fila, columna);


                            // punto
                        } else if (caracter[indice] == 46)
                        {
                            numCaracter++;
                            listaTokens.Add(new Token(numCaracter - 1, "Tk_IdentificadorOrigen", lexema, fila, columna, " \""+lexema+"\":"));
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
                        else if (caracter[indice] == 10)
                        {
                            estado = 6;
                            fila++;
                            columna = 0;                           

                        }
                        else if (caracter[indice] == 32)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Desconocido", fila, columna);
                            listaErrores.Add(token);
                        }

                        break;

                    case 7:

                        if (Char.IsLetter(caracter[indice]))
                        {
                            columna++;
                            lexema += caracter[indice];
                            estado = 8;
                        }
                        else if (caracter[indice] == 10)
                        {
                            estado = 7;
                            fila++;
                            columna = 0;

                            
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 7;
                            columna++;

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
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
                        else if (caracter[indice] == 10)
                        {
                            estado = 8;
                            fila++;
                            columna = 0;

                            
                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 8;
                            columna++;

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una letra o un guion", fila, columna);
                            listaErrores.Add(token);
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
                        else if (caracter[indice] == 13)
                        {
                            estado = 9;
                            fila++;
                            columna = 0;

                            //Espacio

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una signo >", fila, columna);
                            listaErrores.Add(token);
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
                            listaTokens.Add(new Token(numCaracter, "Tk_IdentificadorDestino", lexema, fila, columna, " \""+lexema+"\""));
                            lexema = "";
                            estado = 5;
                            
                            indice--;
                        }
                        else if (caracter[indice] == 10)
                        {
                            estado = 10;
                            fila++;
                            columna = 0;
                            
                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 10;
                            columna++;

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una letra o digito", fila, columna);
                            listaErrores.Add(token);
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
                        else if (caracter[indice] == 10)
                        {
                            estado = 11;
                            fila++;
                            columna = 0;
                           
                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 11;
                            columna++;

                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una operador o un digito", fila, columna);
                            listaErrores.Add(token);
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
                        else if (caracter[indice] == 10)
                        {
                            estado = 12;
                            fila++;
                            columna = 0;                            
                       
                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una digito", fila, columna);
                            listaErrores.Add(token);
                        }


                        break;

                    case 13:

                        columna++;

                        //          "
                        if(caracter[indice] == 34)
                        {
                            estado = 14;
                        }
                        else if (caracter[indice] == 10)
                        {
                            estado = 13;
                            fila++;
                            columna = 0;
                            
                            //Espacio
                        }
                        else if (caracter[indice] == 32)
                        {
                            estado = 13;
                            columna++;
                        }
                        else if (caracter[indice] == 13)
                        {
                            columna++;
                        }
                        else if (caracter[indice] == 09)
                        {
                            columna++;
                        }
                        else
                        {
                            //Error
                            numError++;
                            columna++;
                            Token token = new Token(numError, caracter[indice].ToString(), "Se esperaba una comilla", fila, columna);
                            listaErrores.Add(token);
                        }

                        break;

                    case 14:

                        numCaracter++;
                        listaTokens.Add(new Token(numCaracter, "Tk_Etiqueta", lexema, fila, columna, " \"<f0>|"+lexema+"|<f1>\" shape =\"record\"]"));
                        lexema = "";
                        indice--;
                        estado = 5;
                        break;                    

                    case -1:
                        break;

                }
            }

            if(listaTokens.Count > 0)
            {
                mensaje = "Lista de tokens creada";
                mensajeClase.setMensaje(mensaje);
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
                    listaTokens.Add(new Token(numCaracter, "Tk_Graficar", lexema, fila, columna, " digraph G{graph[rankdir = \"TB\"]; node[fontsize = \"16\" shape = \"ellipse\"];"));
                    break;

                case "Node":
                    listaTokens.Add(new Token(numCaracter, "Tk_Nodo", lexema, fila, columna, ""));

                    break;

                case "Valor":
                    listaTokens.Add(new Token(numCaracter, "Tk_Tipo", lexema, fila, columna, " label ="));
                    break;

                case "Operador":
                    listaTokens.Add(new Token(numCaracter, "Tk_Tipo", lexema, fila, columna, " label ="));
                    break;

                case "IZQ":
                    listaTokens.Add(new Token(numCaracter, "Tk_VerticeIZQ", lexema, fila, columna, " f0"));
                    break;

                case "DER":
                    listaTokens.Add(new Token(numCaracter, "Tk_VerticeDER", lexema, fila, columna, " f1"));
                    break;

                case "->":
                    listaTokens.Add(new Token(numCaracter, "Tk_Flecha", lexema, fila, columna, "->"));
                    break;
                                
                case "+":
                    listaTokens.Add(new Token(numCaracter, "Tk_Suma", lexema, fila, columna, ""));
                    break;

                case "-":
                    listaTokens.Add(new Token(numCaracter, "Tk_Resta", lexema, fila, columna, ""));
                    break;

                case "*":
                    listaTokens.Add(new Token(numCaracter, "Tk_Multiplicacion", lexema, fila, columna, ""));
                    break;

                case "/":
                    listaTokens.Add(new Token(numCaracter, "Tk_Division", lexema, fila, columna, ""));
                    break;

                case "{":
                    listaTokens.Add(new Token(numCaracter, "Tk_LlaveAbierta", lexema, fila, columna, ""));
                    
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
                    listaTokens.Add(new Token(numCaracter, "Tk_MayorQue", lexema, fila, columna, ""));
                    break;

                case ";":

                    int tamaño = listaTokens.Count();
                    listaTokens.Add(new Token(numCaracter, "Tk_PuntoYComa", lexema, fila, columna, ";"));

                    //if (listaTokens[tamaño-1].getLexema().Equals("}"))
                    //{
                    //    listaTokens.Add(new Token(numCaracter, "Tk_PuntoYComa", lexema, fila, columna, ""));
                    //}
                    //else
                    //{
                    //    listaTokens.Add(new Token(numCaracter, "Tk_PuntoYComa", lexema, fila, columna, ";"));
                    //}

                    break;                

        
            }



        }

        private void llenarListaExpresiones(String simbolo, String expresionOriginal)
        {
            listaExpresiones.Add(new Expresion(simbolo, expresionOriginal));
                 
        }

        
        public void mostrarExpresiones()
        {
            for (int i = 0; i < listaExpresiones.Count; i++)
            {
                Console.WriteLine("La expresión original es: " + listaExpresiones[i].getExpresionOriginal());
            }

            listaExpresiones.Clear();
        }
        
    }
}
