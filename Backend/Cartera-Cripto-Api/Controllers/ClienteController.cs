using Cartera_Cripto.Models;
using Cartera_Cripto.Data;
using Cartera_Cripto.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace Cartera_Cripto.Controllers
{
    // 4- Creo el controlador del cliente
    // que va a manejar las peticiones HTTP relacionadas

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // creo la variable _context para acceder
        // a la base de datos desde el controlador.

        private readonly AppDBcontext _context;

        // Constructor del controlador que recibe el contexto de la base de datos
        // para poder interactuar con la base de datos por inyección de dependencias.
        public ClienteController(AppDBcontext context)
        {
            _context = context;
        }

        // Método HttpGet para obtener todos los clientes

        // async Task <ActionResult<IEnumerable<Cliente>>> significa que
        // devolverá una tarea asíncrona que, cuando se complete, retorna 
        // una respuesta de acción con una lista de clientes.

        // _context.Clientes accede a la tabla Clientes en la base de datos.

        // .ToListAsync() es un método asíncrono que obtiene todos los registros y 
        // los convierte en una lista de tipo Cliente.

        // await espera a que termine la consulta a la base de datos
        // antes de devolver el resultado.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var clientes = await _context.Clientes.ToListAsync();
            // Oculta la contraseña de todos los clientes
            clientes.ForEach(c => c.password = null);
            return clientes;
        }

        // Método HTTP GET para obtener un cliente específico por id

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            // Busca el cliente por id en la base de datos
            var cliente = await _context.Clientes.FindAsync(id);

            // Si no se encuentra el cliente, devuelve un NotFound (404)
            if (cliente == null)
            {
                return NotFound();
            }

            // Si se encuentra, devuelve el cliente
            cliente.password = null; // No devolver la contraseña
            return cliente;
        }

        // Método HTTP POST para registrar un nuevo cliente
        [HttpPost("register")]
        // AllowAnonymous permite que este endpoint sea accesible sin autenticación
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Cliente cliente)
        {
            try
            {
                // Validar que el email no exista
                var existe = await _context.Clientes.AnyAsync(c => c.email == cliente.email);
                if (existe)
                    return BadRequest(new { message = "El email ya está registrado." });

                // Validar que trajo contraseña y email
                if (string.IsNullOrWhiteSpace(cliente.email) || string.IsNullOrWhiteSpace(cliente.password))
                    return BadRequest(new { message = "Email y contraseña son obligatorios." });

                // Guardar cliente
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                // Devolver solo los datos públicos, no la contraseña
                return Ok(new
                {
                    Id = cliente.id,
                    Email = cliente.email,
                    Nombre = cliente.name
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Cliente loginCliente)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.email == loginCliente.email && c.password == loginCliente.password);

            if (cliente == null)
                return Unauthorized(new { message = "Email o contraseña incorrectos" });

            // ¡No devuelvas la contraseña!
            cliente.password = null;
            return Ok(cliente);
        }

        // Método HTTP PATCH para actualizar parcialmente un cliente

        [HttpPatch("{id}")]
        public async Task<ActionResult<Cliente>> Patch(int id, Cliente cliente)
        {
            if (id != cliente.id)
            {
                // Si el id del cliente no coincide con el id de la URL, devuelve un BadRequest (400)
                return BadRequest("El ID del cliente no coincide.");
            }

            // Marca el estado del cliente como modificado
            _context.Entry(cliente).State = EntityState.Modified;

            // Intenta guardar los cambios en la base de datos  
            await _context.SaveChangesAsync();

            // Retorna NoContent (204) si la actualización fue exitosa
            return NoContent();
        }
    }
}

