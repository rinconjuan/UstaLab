﻿using System;
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
    public class RespuestaUsuarios
    {
        public bool EstadoLogin { get; set; }
        public Usuarios Usuario { get; set; }

    }
}
