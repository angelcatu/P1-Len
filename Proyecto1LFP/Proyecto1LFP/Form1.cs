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
                Archivo archivo = new Archivo("ReporteTokens", "html");
                archivo.createHTML();
                listaTokens.Clear();
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
    }
}
