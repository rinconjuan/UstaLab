using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace UstaLab.Models
{
    public class MensajeError : Exception
    {
        
        public MensajeError(string mensajeError = default(string))
        {
            this.Mensaje = mensajeError;
        }

        public string Mensaje{ get; set; }        

        
    }
}
