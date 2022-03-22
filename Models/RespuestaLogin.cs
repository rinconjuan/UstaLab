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
    public class RespuestaLogin
    {
        public int idUser { get; set; }
        public bool statusLogin { get; set; }
    }
}
