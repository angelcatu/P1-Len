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
        public static List<String> listaDeGraficas = new List<string>();

        public void generarCodigo()
        {
            String codigoGraphviz = "";

            try
            {
                for (int i = 0; i < listaTokens.Count; i++)
                {
                    if (!listaTokens[i].getCodigoGraphviz().Equals(""))
                    {

                        if (listaTokens[i].getLexema().Equals(";"))
                        {
                            if (listaTokens[i - 1].getLexema().Equals("}"))
                            {
                                if (!codigoGraphviz.Equals(""))
                                {
                                    agregarGraficasCod(codigoGraphviz);
                                    codigoGraphviz = "";
                                }
                            }
                            else
                            {
                                codigoGraphviz += listaTokens[i].getCodigoGraphviz();
                            }
                        }else if (listaTokens[i].getLexema().Equals("}"))
                        {
                            if (listaTokens[i - 1].getLexema().Equals(";"))
                            {
                                codigoGraphviz += listaTokens[i].getCodigoGraphviz();
                            }
                        }
                        else
                        {
                            codigoGraphviz += listaTokens[i].getCodigoGraphviz();
                        }
                        
                    }                    
                }
            }
            catch(Exception e)
            {
                
            }                     
        }

        private void agregarGraficasCod(String codigoGraphviz)
        {
            listaDeGraficas.Add(codigoGraphviz);
        }

    }
}
