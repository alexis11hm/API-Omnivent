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
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {
            foreach (ActualizarViewModel listaPrecios in model)
            {
                if (listaPrecios.LipId <= 0)
                {
                    return BadRequest(listaPrecios);
                }

                var lp = await _context.ListaPrecios.FirstOrDefaultAsync(lip => lip.LipId == listaPrecios.LipId);

                if (lp == null)
                {
                    return NotFound(listaPrecios.LipId);
                }

                lp.LipId = listaPrecios.LipId;
                lp.LipNombre = listaPrecios.LipNombre;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Guardar Excepción
                return BadRequest(ex);
            }

            return Ok();
        }

        // POST: api/ListaPrecios/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
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

        // DELETE: api/ListaPrecios/Eliminar/
        [Authorize(Roles = "super")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] int[] ids)
        {
            foreach (int id in ids)
            {
                var listaPrecios = await _context.ListaPrecios.FindAsync(id);
                if (listaPrecios == null)
                {
                    return NotFound(id);
                }
                _context.ListaPrecios.Remove(listaPrecios);
            }

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

        private bool ListaPreciosExists(int id)
        {
            return _context.ListaPrecios.Any(e => e.LipId == id);
        }
    }
}