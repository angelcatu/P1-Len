using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Token
    {

        private int id;
        private String token;
        private String lexema;
        private int fila;
        private int columna;
        private String codigoGraphviz;
        private String descripcion;

        public Token(int id, String token, String lexema, int fila, int columna, String codigoGraphviz)
        {
            this.id = id;
            this.token = token;
            this.lexema = lexema;
            this.fila = fila;
            this.columna = columna;
            this.codigoGraphviz = codigoGraphviz;
        }

        public Token(int id, String lexema, String descripcion, int fila, int columna)
        {
            this.id = id;            
            this.lexema = lexema;
            this.descripcion = descripcion;
            this.fila = fila;
            this.columna = columna;
            
        }


        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public String getToken()
        {
            return token;
        }

        public String getLexema()
        {
            return lexema;
        }

        public String getDescripcion()
        {
            return descripcion;
        }

        public int getFila()
        {
            return fila;
        }

        public void setFila(int fila)
        {
            this.fila = fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public void setColumna(int columna)
        {
            this.columna = columna;
        }

        public String getCodigoGraphviz()
        {
            return codigoGraphviz;
        }
    }
}
