using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cartera_Cripto.Models
{
    public class Cliente
    {
        //Primero creo los modelos
        [Key]
        public int id { get; set; }
        [NotNull]
        public string name { get; set; }
        [NotNull, MinLength(7),]
        public string email { get; set; }
        public string password { get; set; }

    }
}
