using System.Collections.Generic;

namespace UstaLab.Models
{
    public class RespuestaAgenda
    {
        public bool Disponibilidad { get; set; }
        public int IdUser { get; set; }
        public bool Acceso { get; set; }

        public List<Agenda> Agendas { get; set; }
    }
}
