using Cartera_Cripto.Models;
using System.Text.RegularExpressions; // para usar Regex

namespace Cartera_Cripto.Validaciones
{
    //Abstracta ya que no se va a instanciar directamente, solo agrupa
    // las validaciones o utilidades.
    internal abstract class Validacion
    {
        public static bool ValidarEmail(string email)
        {
            // Utilizo una expresión regular para validar el email
            
            string MustBeEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            // ^[\w-\.]+ → El email debe empezar con letras, números, guiones o puntos.
            // @ → Debe tener un símbolo arroba.
            // ([\w-]+\.)+ → Después del arroba, debe haber uno o más grupos de
            // letras/números/guiones seguidos de un punto (subdominios).
            //[\w-]{2,4}$ → Debe terminar con 2 a 4 letras/números/guiones
            //(el dominio, como .com, .net, .ar, etc.).

            // Creo una instancia de Regex con la expresión regular
            // para validar el email.

            Regex regex = new Regex(MustBeEmail);

            // Uso el método IsMatch para verificar si el email cumple
            // con la expresión regular.
            // En caso de que sí, retorna true, de lo contrario false.
            return regex.IsMatch(email);

        }
    }
}
