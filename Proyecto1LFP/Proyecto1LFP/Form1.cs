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

            if(cadena.Length > 0)
            {
                analizador.analizarEntrada(cadena);
                listaTokens.Clear();
            }
            else
            {
                listaTokens.Clear();
                analizador.analizarEntrada(cadena);
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
    }
}
