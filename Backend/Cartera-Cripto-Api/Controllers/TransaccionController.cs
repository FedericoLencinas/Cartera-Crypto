using Cartera_Cripto.Models;
using Cartera_Cripto.Data;
using Cartera_Cripto.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cartera_Cripto.Controllers
{
    // 4- Creo el controlador de la transaccion
    // que va a manejar las peticiones HTTP relacionadas

    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        // creo la variable _context para acceder
        // a la base de datos desde el controlador.

        private readonly AppDBcontext _context;

        // Constructor del controlador que recibe el contexto de la base de datos
        // para poder interactuar con la base de datos por inyección de dependencias.
        public TransaccionController(AppDBcontext context)
        {
            _context = context;
        }

        // Método HttpGet para obtener todas las transacciones

        // async Task <ActionResult<IEnumerable<Transaccion>>> significa que
        // devolverá una tarea asíncrona que, cuando se complete, retorna 
        // una respuesta de acción con una lista de transacciones.

        // _context.Transacciones accede a la tabla Transacciones en la base de datos.

        // .ToListAsync() es un método asíncrono que obtiene todos los registros y 
        // los convierte en una lista de tipo Transaccion.

        // await espera a que termine la consulta a la base de datos
        // antes de devolver el resultado.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            var transacciones = await _context.Transacciones
                .Include(t => t.Cliente)
                .Select(t => new {
                    t.id,
                    t.action,
                    t.crypto_code,
                    t.ClienteId,
                    cliente_nombre = t.Cliente != null ? t.Cliente.name : "",
                    t.crypto_amount,
                    t.money,
                    t.datetime
                })
                .ToListAsync();

            return Ok(transacciones);
        }
        

        // Método HTTP GET para obtener una transacción específica por id

        [HttpGet("{id}")]

        public async Task<ActionResult<Transaccion>> Get(int id)
        {
            // Busca la transacción por id en la base de datos
            // y también incluye los datos del cliente relacionados.
            // No uso FindAsync porque quiero incluir datos relacionados
            // y FindAsync(id) solo busca por id sin incluir relaciones.
            var transaccion = await _context.Transacciones.Include
                (t => t.Cliente).FirstOrDefaultAsync(t => t.id == id);

            // Si no se encuentra la transacción, devuelve un NotFound (404)
            if (transaccion == null)
            {
                return NotFound();
            }

            // Si se encuentra, devuelve la transacción
            return transaccion;
        }

        // Método HTTP POST para crear una nueva transacción    
        [HttpPost]
        public async Task<IActionResult> Post(Transaccion transaccion)
        {
            // Verificá que cliente existe
            var cliente = await _context.Clientes.FindAsync(transaccion.ClienteId);
            if (cliente == null)
            {
                return BadRequest("Cliente no existe.");
            }

            // Asignar la referencia para que EF entienda la relación
            transaccion.Cliente = cliente;

            // Guardar la transacción
            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();

            // Para devolver la transacción con datos del cliente cargados
            var transaccionConCliente = await _context.Transacciones
                .Include(t => t.Cliente)
                .FirstOrDefaultAsync(t => t.id == transaccion.id);

            return Ok(transaccionConCliente);
        }

        // Método HTTP PATCH para actualizar una transacción existente por id

        [HttpPatch("{id}")]
        public async Task<ActionResult<Transaccion>> Patch(int id, Transaccion transaction)
        {
            // Compara el id de la transacción proporcionada con el id del parámetro
            // si no coinciden, devuelve un BadRequest con un mensaje de error.
            if (id != transaction.id)
            {
                return BadRequest("El ID de la transacción no coincide con el ID proporcionado.");
            }

            // Marca la transacción como modificada en el contexto de la base de datos.

            _context.Entry(transaction).State = EntityState.Modified;

            // Intenta guardar los cambios en la base de datos de forma asíncrona.
            await _context.SaveChangesAsync();

            // Retorna NoContent (204) si la actualización fue exitosa.
            return NoContent();
        }

        // DELETE api/<TransaccionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Busca la transacción por id en la base de datos.
            var transaccion = await _context.Transacciones.FindAsync(id);

            if (transaccion == null)
            {
                // Si no se encuentra la transacción, devuelve un NotFound (404).
                return NotFound();
            }

            // Si se encuentra, elimina la transacción del
            // contexto de la base de datos.
            _context.Transacciones.Remove(transaccion);

            // Guarda los cambios en la base de datos de forma asíncrona.
            await _context.SaveChangesAsync();

            // Retorna NoContent (204) si la eliminación fue exitosa.
            return NoContent();
        }

        // Método HTTP GET para obtener el estado actual de las criptomonedas

        [HttpGet("estado")]

        // Función para obtener el precio actual en ARS de una cripto usando la API de CriptoYa
        private async Task<decimal> ObtenerPrecioActualARS(HttpClient httpClient, string cryptoCode)
        {
            // Construyo la URL para consultar el precio de la cripto (en minúsculas)
            string url = $"https://criptoya.com/api/{cryptoCode.ToLower()}/ars";

            // Hago la petición GET a la API
            var response = await httpClient.GetAsync(url);

            // Si la respuesta no fue exitosa, devuelvo 0 como precio (podés mejorar manejo)
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            // Leo el contenido JSON de la respuesta como string
            var jsonString = await response.Content.ReadAsStringAsync();

            // Parseo el JSON para poder extraer campos
            using var jsonDoc = JsonDocument.Parse(jsonString);

            // Busco la propiedad "price" dentro del JSON
            if (jsonDoc.RootElement.TryGetProperty("price", out JsonElement priceElement))
            {
                // Intento convertir "price" a decimal
                if (priceElement.TryGetDecimal(out decimal price))
                {
                    return price;
                }
            }

            // Si no encontré el precio, devuelvo 0
            return 0;
        }
    }
}
