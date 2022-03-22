using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace UstaLab.Models
{
    public class MensajeErrorItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MensajeErrorItem" /> class.
        /// </summary>
        /// <param name="codigoError">código del error.</param>
        /// <param name="mensajeError">Mensaje Error.</param>
        public MensajeErrorItem(string codigoError = default(string), string mensajeError = default(string))
        {
            this.CodigoError = codigoError;
            this.MensajeError = mensajeError;
        }

        /// <summary>
        /// código del error
        /// </summary>
        /// <value>código del error</value>        
        public string CodigoError { get; set; }

        /// <summary>
        /// Mensaje Error
        /// </summary>
        /// <value>Mensaje Error</value>        
        public string MensajeError { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MensajeErrorItem {\n");
            sb.Append("  CodigoError: ").Append(CodigoError).Append("\n");
            sb.Append("  MensajeError: ").Append(MensajeError).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        

        /// <summary>
        /// Returns true if MensajeErrorItem instances are equal
        /// </summary>
        /// <param name="input">Instance of MensajeErrorItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MensajeErrorItem input)
        {
            if (input == null)
                return false;

            return
                (
                    this.CodigoError == input.CodigoError ||
                    (this.CodigoError != null &&
                    this.CodigoError.Equals(input.CodigoError))
                ) &&
                (
                    this.MensajeError == input.MensajeError ||
                    (this.MensajeError != null &&
                    this.MensajeError.Equals(input.MensajeError))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.CodigoError != null)
                    hashCode = hashCode * 59 + this.CodigoError.GetHashCode();
                if (this.MensajeError != null)
                    hashCode = hashCode * 59 + this.MensajeError.GetHashCode();
                return hashCode;
            }
        }

        
    }
}
