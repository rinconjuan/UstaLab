using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UstaLab.Utils
{
    public class ExcepcionMessage: Exception
    {

        public ExcepcionMessage(string _codigo, string _mensaje)
        {
            Codigo = _codigo;
            Descripcion = _mensaje;
        }

        public string Codigo { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Sobrecargar la función del mensaje de excepcion
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[" + Codigo + "] : " + Descripcion;
        }
    }
}
