using Proyecto1LFP.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1LFP.Modelos
{
    class Archivo
    {
        private Operacion operacion = new Operacion();
        private Mensaje mensaje = Envio.mensaje;

        private List<Token> listaTokens = Analizador.listaTokens;
        private List<Token> listaErrores = Analizador.listaErrores;
        private List<Operacion> listaRespuestas = Operacion.listaRespuestas;
        private List<String> listaGraficasCod = Grafica.listaDeGraficas;

        private Operacion op = new Operacion();
        private Grafica grafica = new Grafica();

        private String nombre;
        private String extension;
        private String ruta;            


        public Archivo()
        {
            this.ruta = "C:\\Users\\ang_e\\Documents\\";
        }

        public Archivo(String nombre, String extension)
        {
            this.nombre = nombre;
            this.extension = extension;
            this.ruta = "C:/Users/ang_e/Documents/";
        }
        
        public void crearReporteResultado()
        {
            try
            {
                if(listaRespuestas.Count > 0)
                {
                    File.WriteAllText(ruta + nombre + "." + extension, reporteDeResultados());
                    mensaje.setMensaje("Archivo de expresiones aritméticas creado");
                    abrirDocumento(ruta, nombre, extension);
                }
                else
                {
                    mensaje.setMensaje("Error de sintáxis");
                }                
            }
            catch (Exception e)
            {
                mensaje.setMensaje("Hubo un problema al generar el html de resultados");
            }

        }

        private string reporteDeResultados()
        {
            String contenido = "<html>\n"
                  + "<head>"
                  + "<utf-8>"
                  + "\n<title>REPORTE DE RESULTADOS</title>\n"
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
                  + "<caption> <h3>Reporte de expresiones </h3> </caption>"
                  + "<tr>"
                  + "<th> <strong>   </strong>  </th>\n"
                  + "<th> <strong> Expresion   </strong>  </th>\n"
                  + "<th> <strong> Respuesta   </strong>  </th>\n"                  
                  + "</tr>";

            foreach (Operacion operacion in listaRespuestas)
            {
                contenido += "<tr>\n"
                        + "<td align = 'center' >" + operacion.getIdRespuesta() + "</td>\n"
                        + "<td align = 'center' >" + operacion.getExpAritmetica() + "</td>\n"
                        + "<td align = 'center' >" + operacion.getResultado() + "</td>\n"

                        + "</tr>\n";
            }


            contenido += "</table>\n</body>\n</html>";



            return contenido;
        }
     
        public void crearReporteErrores()
        {
            try
            {
                File.WriteAllText(ruta+nombre+"."+extension, reporteDeErrores());
                mensaje.setMensaje("Existen errores en la el texto");
                abrirDocumento(ruta, nombre, extension);
            }
            catch(Exception e)
            {
                mensaje.setMensaje("Hubo un problema al generar el html de errores");
            }

        }
     
        public void generarImagenDeDot()
        {
            grafica.generarCodigo();
            generarDOT();

            //String ruta = Path.Combine(Application.StartupPath, "Imagenes\\");

            //String ruta = "C:\\Users\\ang_e\\Documents\\Imagenes\\";
            String ruta = "Grafos\\";
            //String imagen = "..\\..\\Modelos\\Imagenes\\";
            String imagen = Path.Combine(Application.StartupPath, ruta);

            

            if (listaGraficasCod.Count > 0)
            {
                for (int i = 0; i < listaGraficasCod.Count; i++)
                {

                    //generarImagen("grafo" + i + ".dot", ruta);
                    //generarImagen("grafo"+i+".dot", "~/Modelos/Imagenes/");
                    generarImagen("grafo"+i+".dot", imagen);
                    abrirDocumento(imagen, "grafo"+i+".png", "");
                    //generarReporteDeArbolHTMl("ReporteArbol", "html", this.ruta, "grafo" + i + ".png");
                }

                mensaje.setMensaje("Grafo creado");
            }                                  
        }

        public void generarReporteDeArbolHTMl(String nombreArchivo, String extension, String rutaAbrir, String nombreArbol )
        {
            try
            {
                File.WriteAllText(@ruta + nombreArchivo+"."+extension, reporteArbol());
                mensaje.setMensaje("Archivo de árbol creado");
                abrirDocumento(ruta, nombreArchivo, extension);
            }
            catch (Exception e)
            {
                mensaje.setMensaje("Hubo un problema al generar el html de árboles");
            }
        }

        private string reporteArbol()
        {
            //String rutaImagenes = "~\\bin\\Debug\\Grafos\\";
            String rutaImagenes = @"Modelos\Imagenes\";        
            //String imagen = Path.Combine(Application.StartupPath, rutaImagenes);
            //String imagen = Path.Combine(Application.StartupPath, rutaImagenes);

            //String imagen = @"/Proyecto1LFP/Proyecto1LFP/bin/Debug/";

            String contenido = "<html>\n"
               + "<head>"
               + "<utf-8>"
               + "\n<title>REPORTE DE ARBOL</title>\n"
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
               + "<table border = '1' align = 'center'>";

               for (int i = 0; i < listaGraficasCod.Count; i++)
            {
                contenido += "< img src = " + '"' + rutaImagenes + "grafo" + i +".png"+'"' + "/> ";
            }


            contenido += "</table>\n</body>\n</html>";
            return contenido;
        }

        private void generarImagen(string nombre, string ruta )
        {           
            try
            {
                var command = string.Format("dot -Tpng "+this.ruta+nombre+" -o {1}", Path.Combine(ruta, nombre), Path.Combine(ruta, nombre.Replace(".dot", ".png")));

                var procStarInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);

                var proc = new System.Diagnostics.Process();

                proc.StartInfo = procStarInfo;

                proc.Start();

                proc.WaitForExit();

                //MessageBox.Show("Imagen creada sin problemas", "Información");

            }
            catch(Exception e)
            {
                mensaje.setMensaje("No se pudo generar la imagen del grafo");
            }

        }
        
        public void createHTML()
        {            
            
            try
            {
                if(listaTokens.Count > 0)
                {
                    File.WriteAllText(@ruta + nombre + "." + extension, reporteSimbolos());
                    //MessageBox.Show("Reporte de tokens generado correctamente");
                    mensaje.setMensaje("Archivo de tokens creado");

                    abrirDocumento(ruta, nombre, extension);
                }                
            }
            catch (Exception e)
            {
                mensaje.setMensaje("Hubo un problema al generar el html de tokens");
            }
        }

        private int numArbol = 0;

        private void generarDOT()
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = null;

                //Write a line of text
                if (listaGraficasCod.Count > 0)
                {
                    for (int i = 0; i < listaGraficasCod.Count; i++)
                    {
                        sw = new StreamWriter(@ruta + "grafo" + i + ".dot");
                        sw.WriteLine(listaGraficasCod[i]);

                        //Close the file
                        sw.Close();
                    }
                    
                }
                
                
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
            String doc = "";


            try
            {
                if (!extension.Equals(""))
                {
                    String path = ruta + nombre + "." + extension;
                    doc = Path.Combine(Application.StartupPath, path);
                }
                else
                {
                    doc = Path.Combine(Application.StartupPath, ruta + nombre);
                }


                Process.Start(doc);
            }
            catch (Exception)
            {
                mensaje.setMensaje("No existe el documento en la carpeta");
            }                                 
        }
    }   
}
