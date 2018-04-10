using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Message
{
    class Mensaje
    {        

        private String mensaje = "";

        public void setMensaje(String mensaje)
        {
            this.mensaje = mensaje;          
        }
        
        public String enviarMensaje()
        {
            return mensaje;
        }

    }
}
