using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1LFP.Modelos
{
    class Archivo
    {

        private List<Token> listaTokens = Analizador.listaTokens;
        private List<Token> listaErrores = Analizador.listaErrores;

        private Operacion operacion;
        private Grafica grafica = new Grafica();

        private String nombre;
        private String extension;
        private String ruta;            


        public Archivo(String nombre, String extension)
        {
            this.nombre = nombre;
            this.extension = extension;
            this.ruta = "C:/Users/ang_e/Documents/";
        }
        
        private void crearReporteResultado()
        {

        }

        private void crearReporteTokens()
        {

        }

        public void crearReporteErrores()
        {
            try
            {
                File.WriteAllText(ruta+nombre+"."+extension, reporteDeErrores());
                MessageBox.Show("Existen errores en la consola");
                abrirDocumento(ruta, nombre, extension);
            }
            catch(Exception e)
            {
                MessageBox.Show("Hubo un problema al generar el html de errores");
            }

        }
     
        public void crearReporteArbol()
        {
            generarDOT();
        }        

        public void createHTML()
        {            
            
            try
            {
                File.WriteAllText(@ruta+nombre+"."+extension, reporteSimbolos());                
                MessageBox.Show("Reporte de tokens generado correctamente");

                abrirDocumento(ruta, nombre, extension);
            }
            catch (Exception e)
            {
                MessageBox.Show("Hubo un problema al generar el html de tokens");
            }
        }

        private void generarDOT()
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(@ruta+"grafo.dot");

                //Write a line of text
                if(grafica.generarCodigo().Length > 0)
                {
                    sw.WriteLine(grafica.generarCodigo());
                }
                
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private String reporteSimbolos()
        {
            String contenido = "<html>\n"
               + "<head>"
               + "<utf-8>"
               + "\n<title>REPORTE DE TOKEN</title>\n"
               + "<style>"
               + "body{"
               + "background-color: #ead48a;"
               + "}"
               + ""
               + "table: hover{"
               + "width: 50%;"
               + "}"
               + ""
               + "th{"
               + "height: 25px;"
               + "}"
               + "</style>"
               + ""
               + "</head>"
               + "<body align='center'>\n"
               + "<table border = '1' align = 'center'>"
               + "<caption> <h3>Reporte de tokens </h3> </caption>"
               + "<tr>"
               + "<th> <Strong>IdToken</Strong></th>"
               + "<th> <strong> Token   </strong>  </th>\n"
               + "<th> <strong> Lexema   </strong>  </th>\n"
               + "<th> <strong> Fila   </strong>  </th>\n"
               + "<th> <strong> Columna   </strong>  </th>\n"
               + "</tr>";

            foreach (Token token in listaTokens)
            {
                contenido += "<tr>\n"
                        + "<td align = 'center' >" + token.getId() + "</td>\n"
                        + "<td align = 'center' >" + token.getToken() + "</td>\n"
                        + "<td align = 'center' >" + token.getLexema() + "</td>\n"
                        + "<td align = 'center' >" + token.getFila() + "</td>\n"
                        + "<td align = 'center' >" + token.getColumna() + "</td>\n"

                        + "</tr>\n";
            }


            contenido += "</table>\n</body>\n</html>";



            return contenido;
        }

        private string reporteDeErrores()
        {
            String contenido = "<html>\n"
              + "<head>"
              + "<utf-8>"
              + "\n<title>REPORTE DE ERRORES</title>\n"
              + "<style>"
              + "body{"
              + "background-color: #ead48a;"
              + "}"
              + ""
              + "table: hover{"
              + "width: 50%;"
              + "}"
              + ""
              + "th{"
              + "height: 25px;"
              + "}"
              + "</style>"
              + ""
              + "</head>"
              + "<body align='center'>\n"
              + "<table border = '1' align = 'center'>"
              + "<caption> <h3>Reporte de errores </h3> </caption>"
              + "<tr>"
              + "<th> <Strong>IdToken</Strong></th>"
              + "<th> <strong>Lexema  </strong>  </th>\n"
              + "<th> <strong> Descripcion   </strong>  </th>\n"
              + "<th> <strong> Fila   </strong>  </th>\n"
              + "<th> <strong> Columna   </strong>  </th>\n"
              + "</tr>";

            foreach (Token token in listaErrores)
            {
                contenido += "<tr>\n"
                        + "<td align = 'center' >" + token.getId() + "</td>\n"
                        + "<td align = 'center' >" + token.getLexema() + "</td>\n"
                        + "<td align = 'center' >" + token.getDescripcion() + "</td>\n"
                        + "<td align = 'center' >" + token.getFila() + "</td>\n"
                        + "<td align = 'center' >" + token.getColumna() + "</td>\n"

                        + "</tr>\n";
            }


            contenido += "</table>\n</body>\n</html>";



            return contenido;
        }

        private void abrirDocumento(String ruta, String nombre, String extension)
        {

            
                String path = this.ruta + this.nombre + "." + extension;

                String doc = Path.Combine(Application.StartupPath, path);

                Process.Start(doc);                        
        }
    }   
}
