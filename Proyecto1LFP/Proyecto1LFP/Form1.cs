using Proyecto1LFP.Modelos;
using Proyecto1LFP.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto1LFP
{
    public partial class Calculadora : Form
    {

        private Analizador analizador = new Analizador();
        private Operacion operacion = new Operacion();
        private Envio envio = new Envio();
        private Mensaje mensaje = Envio.mensaje;
        private List<Token> listaTokens = Analizador.listaTokens;
        private List<Token> listaErrores = Analizador.listaErrores;
        private List<Expresion> listaExpresiones = Analizador.listaExpresiones;
        private List<String> listaGraficas = Grafica.listaDeGraficas;

        private String mensajes = "";
        private String pathDeArchivo = "";
                
        public Calculadora()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxAnalizador.Text = "";
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                enviarAAnalizador();
            }
            catch (Exception l)
            {
                MessageBox.Show("Ocurrió un problema en la ejecución, algo está mal escrito", "Error");
            }
        }

        private void generarReporteErrores()
        {
            Archivo archivo = new Archivo("Reporte de errores", "html");
            archivo.crearReporteErrores();
        }

        private void generarReporteDeTokens()
        {
            Archivo archivo = new Archivo("ReporteTokens", "html");
            archivo.createHTML();
        }

        private void generarImagenArbol()
        {
            Archivo archivo = new Archivo();
            archivo.generarImagenDeDot();
        }

        private void generarArbol()
        {
            //Archivo archivo = new Archivo("ReporteArbol", "html");
            //archivo.generarReporteDeArbolHTMl();
        }

        private void enviarAAnalizador()
        {
            String cadena = txtBoxAnalizador.Text;

            analizador.setColumna(0);
            analizador.setFila(1);
            analizador.setNumCaracter(0);
            analizador.setNumError(0);

            setIndiceInicioPintura(0);
            setIndiceToken(0);

            if (cadena.Length > 0)
            {
                analizador.analizarEntrada(cadena);
            }
            else
            {
                if (listaTokens != null)
                {
                    listaTokens.Clear();
                    analizador.analizarEntrada(cadena);
                }
                else
                {
                    analizador.analizarEntrada(cadena);
                }
            }
            
            if (listaErrores.Count == 0)
            {
                cambiarColor();

                generarReporteDeTokens();
                log();
                generarImagenArbol();
                log();
                //generarArbol();                

                if (listaExpresiones.Count > 0)
                {
                    operacion.setIdRespuesta(0);
                    operacion.operarExpresion();
                    log();
                    
                }


                listaTokens.Clear();
                listaGraficas.Clear();

            }
            else
            {
                generarReporteErrores();

                log();

                for (int i = 0; i < listaErrores.Count; i++)
                {
                    String envio = "Error: "+listaErrores[i].getLexema() + ", Línea: " + listaErrores[i].getFila() +
                        ", Columna: " + listaErrores[i].getColumna();

                    mensaje.setMensaje(envio);
                    log();
                }

                listaErrores.Clear();
                listaTokens.Clear();
            }

        }

        public void log()
        {

            if(txtBoxLog.Text.Length > 0)
            {
                mensajes = envio.enviarMensaje();

                if(mensajes.Length > 0)
                {
                    mensajes = string.Format(envio.enviarMensaje(), Environment.NewLine);
                    txtBoxLog.Text += mensajes + Environment.NewLine;
                    envio.borrarMensaje();
                }
                
                
            }
            else
            {
                mensajes = envio.enviarMensaje();
                if(mensajes.Length > 0)
                {
                    mensajes = string.Format(envio.enviarMensaje(), Environment.NewLine);
                    txtBoxLog.Text += mensajes + Environment.NewLine;
                    envio.borrarMensaje();
                }
                            
            }

            
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            enviarAAnalizador();


        }

        private int indiceInicioPintura = 0;
        private int indiceToken = 0;

        public void setIndiceToken(int incideInicio)
        {
            this.indiceToken = incideInicio;
        }

        public void setIndiceInicioPintura(int indiceInicioPintura)
        {
            this.indiceInicioPintura = indiceInicioPintura;
        }


        private void cambiarColor()
        {

            String texto = txtBoxAnalizador.Text;
            String concatenacion = "";

            for (int inicio = 0; inicio < texto.Length; inicio++)
            {
                concatenacion += texto[inicio];

                if (concatenacion.Equals("\n"))
                {
                    concatenacion = "";
                    this.indiceInicioPintura = inicio + 1;
                }
                else if (concatenacion.Equals("\t"))
                {
                    concatenacion = "";
                    this.indiceInicioPintura = inicio + 1;
                }
                else if (concatenacion.Equals(" "))
                {
                    concatenacion = "";
                    this.indiceInicioPintura = inicio + 1;
                }
                else if (concatenacion.Equals("\""))
                {
                    txtBoxAnalizador.SelectionStart = indiceInicioPintura;
                    txtBoxAnalizador.SelectionLength = inicio;
                    txtBoxAnalizador.SelectionColor = Color.Green;
                    concatenacion = "";
                    this.indiceInicioPintura = inicio + 1;
                }
                else if (concatenacion.Equals(listaTokens[indiceToken].getLexema()))
                {
                    pintar(this.indiceInicioPintura, (inicio), indiceToken);
                    this.indiceInicioPintura = inicio + 1;
                    concatenacion = "";
                    indiceToken++;
                    inicio = esFinalDeTexto(inicio, texto.Length);


                }
            }
        }

        private int esFinalDeTexto(int inicio, int texto)
        {
            if (indiceToken == listaTokens.Count - 1)
            {
                inicio = texto;
            }


            return inicio;
        }


        private void pintar(int indiceInicioPintura, int length, int indiceToken)
        {

            txtBoxAnalizador.SelectionStart = indiceInicioPintura;
            txtBoxAnalizador.SelectionLength = length;

            switch (listaTokens[indiceToken].getToken())
            {

                case "Tk_Numero":

                    txtBoxAnalizador.SelectionColor = Color.Blue;

                    break;

                case "Tk_Suma":
                    txtBoxAnalizador.SelectionColor = Color.Purple;
                    break;

                case "Tk_Resta":
                    txtBoxAnalizador.SelectionColor = Color.Gray;
                    break;

                case "Tk_Multiplicacion":
                    txtBoxAnalizador.SelectionColor = Color.Orange;
                    break;

                case "Tk_Division":
                    txtBoxAnalizador.SelectionColor = Color.LightBlue;
                    break;

                case "Tk_Etiqueta":
                    txtBoxAnalizador.SelectionColor = Color.Green;
                    break;

                case "Tk_IdentificadorDestino":
                    txtBoxAnalizador.SelectionColor = Color.Red;
                    break;

                case "Tk_IdentificadorOrigen":
                    txtBoxAnalizador.SelectionColor = Color.Red;
                    break;

                case "Tk_Identificador":
                    txtBoxAnalizador.SelectionColor = Color.Red;
                    break;

                default:
                    txtBoxAnalizador.SelectionColor = Color.Black;
                    break;
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirArchivo();
        }

        private void abrirArchivo()
        {
            txtBoxAnalizador.Text = "";
            String texto;
            String saveText = txtBoxAnalizador.Text;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo";
            ofd.Filter = "Documentos lf (*.lf)|*.lf";

            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    texto = System.IO.File.ReadAllText(ofd.FileName);
                    this.txtBoxAnalizador.Text = texto;

                    pathDeArchivo = ofd.FileName;

                }                                
            }
            catch (Exception e)
            {
                mensajes = "Error: no se pudo abrir el archivo";
                txtBoxLog.Text += mensajes + Environment.NewLine;
            }

        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            analizador.mostrarExpresiones();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(pathDeArchivo.Length > 0)
            {
                sobreescribir(pathDeArchivo);

            }
            else
            {
                guardarArchivo();
            }

            
        }

        private void sobreescribir(string pathDeArchivo)
        {
            try
            {
                StreamWriter writer = new StreamWriter(pathDeArchivo);

                writer.WriteLine(txtBoxAnalizador.Text);

                writer.Close();

                mensajes = "Archivo sobreescrito";
                
                //mensajes = string.Format(envio.enviarMensaje(), Environment.NewLine);
                txtBoxLog.Text += mensajes + Environment.NewLine;
                mensajes = "";
            }
            catch (Exception e)
            {
                mensajes = "Ocurrió un problema en la sobreescritura";
                txtBoxLog.Text += mensaje + Environment.NewLine;

                mensajes = "";
            }
        }

        private void guardarArchivo()
        {
            
            SaveFileDialog sfd = new SaveFileDialog();

            try
            {
                sfd.Filter = "Archivos lf (*.lf)|*.lf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, txtBoxAnalizador.Text);

                    pathDeArchivo = sfd.FileName;

                    mensajes = "Archivo guardado";
                    txtBoxLog.Text += mensajes + Environment.NewLine;
                    mensajes = "";
                }
            }
            catch(Exception e)
            {
                mensajes = "No se pudo guardar el archivo";
                txtBoxLog.Text += mensajes + Environment.NewLine;
                mensajes = "";
            }                                     
        }

    

        private void acercaDeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            String acercaDe = "Proyecto 1 Lengujes Formales de Programación " + Environment.NewLine +
                "Angel Manuel Elias Catú " + Environment.NewLine +
                "201403982";

            MessageBox.Show(acercaDe);
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardarArchivo();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBoxAnalizador.Text = "";
            pathDeArchivo = "";

            mensajes = "Nuevo archivo";
            txtBoxLog.Text += mensajes + Environment.NewLine;
            mensajes = "";
        }

        private void manualTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo manualTecnico = new Archivo();
            manualTecnico.abrirManualTecnico();
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo manualUsuario = new Archivo();
            manualUsuario.abrirManualUsuario();
        }
    }
}