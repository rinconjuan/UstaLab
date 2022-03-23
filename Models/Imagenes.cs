using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace UstaLab.Models
{
    public class Imagenes
    {
        [Key]
        public int idImagen { get; set; }
        public string DataImagen { get; set; }

        public string NameImagen {get; set; }
        public string MimeType { get; set; }


    }
}
