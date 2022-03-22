using System;
using System.Web.Mvc;
using UstaLab.Models;


namespace UstaLab.Utils
{

    public class AdministrarException
    {
        protected MensajeErrorItem AdministrarExcepcion(Exception exception)
        {
            Exception ex;
            MensajeErrorItem objMensajeErrorItem = new MensajeErrorItem();
            if (exception is AggregateException aggregateException)
                ex = aggregateException.GetBaseException();
            else
                ex = exception;

            if(ex is ExcepcionMessage message)
            {
                objMensajeErrorItem.CodigoError = message.Codigo;
                objMensajeErrorItem.MensajeError = message.Descripcion;
            }
            else
            {
                objMensajeErrorItem.CodigoError = "APIR00";
                objMensajeErrorItem.MensajeError = "Error inesperado contacte con el administradors";
            }


            return objMensajeErrorItem;
        }
    }
}
