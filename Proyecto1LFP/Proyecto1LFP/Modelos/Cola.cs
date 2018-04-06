using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Modelos
{
    class Cola
    {
        private List<Celda> listaCeldas;

        private int idCelda;

        public Cola(int idCelda)
        {
            this.idCelda = idCelda;
            this.listaCeldas = new List<Celda>();
        }

        public int getIdCelda()
        {
            return idCelda;
        }

        public List<Celda> getListaCeldas()
        {
            return listaCeldas;
        }
    }
}
