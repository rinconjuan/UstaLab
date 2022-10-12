using System;
using System.ComponentModel.DataAnnotations;
namespace UstaLab.Models
{
    public class Registro
    {
        [Key]
        public int IdRegistro {get;set;}

        public DateTime Fecha { get;set;}

        public string NombrePrueba { get;set;}

        public string Imagen { get;set;}    

        public int idUser { get;set;}
        public string NameImagen { get; set; }

        public string MimeType { get; set; }

    }
}
