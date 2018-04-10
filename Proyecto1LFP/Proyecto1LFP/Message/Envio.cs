using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1LFP.Message
{
    class Envio
    {

        public static Mensaje mensaje = new Mensaje();        
    
        public String enviarMensaje()
        {
            return mensaje.enviarMensaje();
        }

        public void borrarMensaje()
        {
            mensaje.setMensaje("");
        }

    }
}
