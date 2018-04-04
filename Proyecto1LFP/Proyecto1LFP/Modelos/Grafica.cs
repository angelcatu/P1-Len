using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Grafica
    {

        private List<Token> listaTokens = Analizador.listaTokens;

        public Image generarArbolDeExpresiones()
        {
            Image image = null;

            

            return image;
        } 


        public String generarCodigo()
        {
            String codigoGraphviz = "";

            foreach (Token token in listaTokens)
            {

                if (!token.getCodigoGraphviz().Equals("")){
                    codigoGraphviz += token.getCodigoGraphviz();
                }                
            }

            return codigoGraphviz;
        }

    }
}
