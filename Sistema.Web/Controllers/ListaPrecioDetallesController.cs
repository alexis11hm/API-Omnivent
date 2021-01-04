using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.ListaPrecioDetalle;


namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class ListaPrecioDetallesController : ControllerBase
    {
        private readonly DbContextSistema _context;
        private readonly IConfiguration _config;

        public ListaPrecioDetallesController(DbContextSistema context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/ListaPrecioDetalles/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListaPrecioDetalleViewModel>> Listar()
        {
            var listaDetalle = await _context.ListaPrecioDetalles.Include(lp => lp.listaPrecio).Include(p => p.producto).ToListAsync();


            return listaDetalle.Select(lp => new ListaPrecioDetalleViewModel
            {
                LipId = lp.LipId,
                ProId = lp.ProId,
                LipDetSinIva = lp.LipDetSinIva,
                LipDetConIva = lp.LipDetConIva,
                listaPrecio = lp.listaPrecio.LipNombre,
                producto = lp.producto.ProDescripcion
            });

        }

        // GET: api/ListaPrecioDetalles/Mostrar/1
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var lpd = await _context.ListaPrecioDetalles.Include(lp => lp.listaPrecio).Include(p => p.producto).FirstOrDefaultAsync(lipd => lipd.ProId == id);

            if (lpd == null)
            {
                return NotFound();
            }

            return Ok(new ListaPrecioDetalleViewModel
            {
                LipId = lpd.LipId,
                ProId = lpd.ProId,
                LipDetSinIva = lpd.LipDetSinIva,
                LipDetConIva = lpd.LipDetConIva,
                listaPrecio = lpd.listaPrecio.LipNombre,
                producto = lpd.producto.ProDescripcion
            });
        }

        // PUT: api/ListaPrecioDetalles/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.NumLip <= 0)
            {
                return BadRequest();
            }
            if (model.LipId <= 0)
            {
                return BadRequest();
            }
            if (model.ProId <= 0)
            {
                return BadRequest();
            }

            var lpd = await _context.ListaPrecioDetalles.FirstOrDefaultAsync(lipd => lipd.NumLip == model.NumLip);

            if (lpd == null)
            {
                return NotFound();
            }

            lpd.LipId = model.LipId;
            lpd.ProId = model.ProId;
            lpd.LipDetSinIva = model.LipDetSinIva;
            lpd.LipDetConIva = model.LipDetConIva;

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

        // POST: api/ListaPrecioDetalles/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            List<ListaPrecioDetalle> listasPrecioDetalles = new List<ListaPrecioDetalle>();

            model.ForEach(lista =>
            {
                ListaPrecioDetalle lpd = new ListaPrecioDetalle
                {
                    LipId = lista.LipId,
                    ProId = lista.ProId,
                    LipDetSinIva = lista.LipDetSinIva,
                    LipDetConIva = lista.LipDetConIva
                };
                listasPrecioDetalles.Add(lpd);
            });

            await _context.ListaPrecioDetalles.AddRangeAsync(listasPrecioDetalles);
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

        // DELETE: api/ListaPrecioDetalles/Eliminar/1
        [Authorize(Roles = "super")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listaPrecioDetalles = await _context.ListaPrecioDetalles.FindAsync(id);
            if (listaPrecioDetalles == null)
            {
                return NotFound();
            }

            _context.ListaPrecioDetalles.Remove(listaPrecioDetalles);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(listaPrecioDetalles);
        }

        private bool ListaPrecioDetallesExists(int id)
        {
            return _context.ListaPrecioDetalles.Any(e => e.NumLip == id);
        }
    }
}