using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Graphviz
    {

        private String codigo;

        public Graphviz(String codigo)
        {
            this.codigo = codigo;
        }

        public String getCodigo()
        {
            return codigo;
        }

    }
}
