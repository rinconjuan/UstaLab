using System;

namespace UstaLab.Models
{
    public class DatosAgenda
    {
      
        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
        public int IdUser { get; set; }
        public string Modo { get; set; }
    }
}
