using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.ListaPrecio;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class ListaPreciosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ListaPreciosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/ListaPrecios/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListaPrecioViewModel>> Listar()
        {
            var listaPrecio = await _context.ListaPrecios.ToListAsync();

            return listaPrecio.Select(lp => new ListaPrecioViewModel
            {
                LipId = lp.LipId,
                LipNombre = lp.LipNombre
            });

        }

        // GET: api/ListaPrecios/Mostrar/1
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var lp = await _context.ListaPrecios.FindAsync(id);

            if (lp == null)
            {
                return NotFound();
            }

            return Ok(new SelectViewModel
            {
                LipId = lp.LipId,
                LipNombre = lp.LipNombre
            });
        }

        // PUT: api/ListaPrecios/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.LipId <= 0)
            {
                return BadRequest();
            }

            var lp = await _context.ListaPrecios.FirstOrDefaultAsync(lip => lip.LipId == model.LipId);

            if (lp == null)
            {
                return NotFound();
            }

            lp.LipId = model.LipId;
            lp.LipNombre = model.LipNombre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/ListaPrecios/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            List<ListaPrecio> listasPrecios = new List<ListaPrecio>();

            model.ForEach(lista =>
            {
                ListaPrecio lp = new ListaPrecio
                {
                    LipId = lista.LipId,
                    LipNombre = lista.LipNombre
                };
                listasPrecios.Add(lp);
            });

            await _context.ListaPrecios.AddRangeAsync(listasPrecios);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // DELETE: api/ListaPrecios/Eliminar/1
        [Authorize(Roles = "super")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listaPrecios = await _context.ListaPrecios.FindAsync(id);
            if (listaPrecios == null)
            {
                return NotFound();
            }

            _context.ListaPrecios.Remove(listaPrecios);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(listaPrecios);
        }

        private bool ListaPreciosExists(int id)
        {
            return _context.ListaPrecios.Any(e => e.LipId == id);
        }
    }
}