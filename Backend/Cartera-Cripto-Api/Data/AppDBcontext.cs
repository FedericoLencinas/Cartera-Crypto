using Microsoft.EntityFrameworkCore;

namespace Cartera_Cripto.Data
{
    // 2 Una vez instalados los paquetes hacemos la clase que herede de DbContext

    public class AppDBcontext : DbContext
    {
        // 3 En esta parte definimos el constructor de la clase
        // Permite que Entity Framework Core configure correctamente el
        // contexto de base de datos usando las opciones que le pases desde
        // la configuración de la aplicación.
        public AppDBcontext(DbContextOptions<AppDBcontext> options) : base(options) { }

        // Definimos las propiedades DbSet para las entidades
        // que queremos mapear a la base de datos. Hacemos la migración
        public DbSet<Models.Cliente> Clientes { get; set; }
        public DbSet<Models.Transaccion> Transacciones { get; set; }
    }


}


    


