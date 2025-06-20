using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Cartera_Cripto.Models
{
    public class Transaccion
    {
        //Primero creo los modelos
        [Key]
        public int id { get; set; }
        [NotNull, Required]
        public string action { get; set; }
        [NotNull, Required]
        public string crypto_code { get; set; }
        [NotNull, Required]
        public int ClienteId { get; set; }
        
        [JsonIgnore]  // Esto hace que no aparezca cliente en JSON
        public Cliente? Cliente { get; set; }
        [NotNull]
        public float crypto_amount { get; set; }
        [NotNull]
        public float money { get; set; }
        [NotNull, Required]
        public DateTime datetime { get; set; }
    }
}
