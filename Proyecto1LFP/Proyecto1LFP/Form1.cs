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
        private List<Token> listaTokens = Analizador.listaTokens;
        private List<Token> listaErrores = Analizador.listaErrores;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxAnalalizador.Text = "";            
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

        private void enviarAAnalizador()
        {
            String cadena = txtBoxAnalalizador.Text;

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

            if (listaErrores.Count == 0)
            {
                generarReporteDeTokens();
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            analizador.mostrarExpresiones();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirArchivo();
        }

        private void abrirArchivo()
        {
            txtBoxAnalalizador.Text = "";
            String texto;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo";
            ofd.Filter = "Documentos lf (*.lf)|*.lf";

            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    texto = System.IO.File.ReadAllText(ofd.FileName);
                    this.txtBoxAnalalizador.Text = texto;
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
    }
}
