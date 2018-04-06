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

        private int idCola;

        public Cola()
        {

        }

        public Cola(int idCola)
        {
            this.idCola = idCola;
            this.listaCeldas = new List<Celda>();
        }

        public int getIdCelda()
        {
            return idCola;
        }

        public List<Celda> getListaCeldas()
        {
            return listaCeldas;
        }
    }
}
