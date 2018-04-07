using Proyecto1LFP.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1LFP
{
    public partial class Form1 : Form
    {

        private Analizador analizador = new Analizador();
        private Operacion operacion = new Operacion();
        private List<Token> listaTokens = Analizador.listaTokens;
        private List<Token> listaErrores = Analizador.listaErrores;
        private List<Expresion> listaExpresiones = Analizador.listaExpresiones;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxAnalizador.Text = "";            
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            enviarAAnalizador();
                        
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
            Archivo archivo = new Archivo("ReporteArbol", "html");
            archivo.generarReporteDeArbolHTMl();
        }

        private void enviarAAnalizador()
        {
            String cadena = txtBoxAnalizador.Text;

            analizador.setColumna(0);
            analizador.setFila(1);
            analizador.setNumCaracter(0);
            analizador.setNumError(0);

            if(cadena.Length > 0)
            {
                analizador.analizarEntrada(cadena);                
            }
            else
            {
                if(listaTokens != null)
                {
                    listaTokens.Clear();
                    analizador.analizarEntrada(cadena);
                }
                else
                {
                    analizador.analizarEntrada(cadena);
                }             
            }

            cambiarColor();

            if (listaErrores.Count == 0)
            {
                //generarReporteDeTokens();
                //generarImagenArbol();
                //generarArbol();
                                
                if(listaExpresiones.Count > 0)
                {
                    operacion.setIdRespuesta(0);
                    operacion.operarExpresion();
                }


                listaTokens.Clear();

            }
            else
            {
                generarReporteErrores();
                listaErrores.Clear();
            }

        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            enviarAAnalizador();
            

        }

        private int indiceInicioPintura = 0;
        private int longitudTokenPintura = 0;

        private void cambiarColor()
        {
            
            String texto = txtBoxAnalizador.Text;
            String concatenacion = "";

            for (int inicio = 0; inicio < texto.Length; inicio++)
            {
                concatenacion += texto[inicio];

                if(indiceInicioPintura > 0)
                {
                    this.indiceInicioPintura = inicio;
                }
                
                for (int indiceToken = 0; indiceToken < listaTokens.Count; indiceToken++)
                {
                    if (concatenacion.Equals(listaTokens[indiceToken].getLexema()))
                    {

                        if (concatenacion.Equals(";"))
                        {

                        }

                        pintar(this.indiceInicioPintura, (concatenacion.Length-1+this.indiceInicioPintura), indiceToken);
                        this.indiceInicioPintura = inicio;
                        concatenacion = "";

                    }
                    else
                    {
                        
                    }

                    
                }                
            }                                                      
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
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo";
            ofd.Filter = "Documentos lf (*.lf)|*.lf";

            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    texto = System.IO.File.ReadAllText(ofd.FileName);
                    this.txtBoxAnalizador.Text = texto;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo abrir el archivo lf");
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
    }
}
