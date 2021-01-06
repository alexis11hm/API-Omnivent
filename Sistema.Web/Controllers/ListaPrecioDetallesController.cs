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
        public async Task<IEnumerable<ListaPrecioDetalleViewModel>> Mostrar([FromRoute] int id)
        {

            var lpd = await _context.ListaPrecioDetalles.Include(lp => lp.listaPrecio).Include(p => p.producto).Where(lipd => lipd.ProId == id).ToListAsync();

            return lpd.Select(lp => new ListaPrecioDetalleViewModel
            {
                LipId = lp.LipId,
                ProId = lp.ProId,
                LipDetSinIva = lp.LipDetSinIva,
                LipDetConIva = lp.LipDetConIva,
                listaPrecio = lp.listaPrecio.LipNombre,
                producto = lp.producto.ProDescripcion
            });
        }

        // PUT: api/ListaPrecioDetalles/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {
            foreach (ActualizarViewModel listaPrecioDetalles in model)
            {
                /*if (listaPrecioDetalles.NumLip <= 0)
                {
                    return BadRequest(listaPrecioDetalles);
                }*/
                if (listaPrecioDetalles.LipId <= 0)
                {
                    return BadRequest(listaPrecioDetalles);
                }
                if (listaPrecioDetalles.ProId <= 0)
                {
                    return BadRequest(listaPrecioDetalles);
                }

                var lpd = await _context.ListaPrecioDetalles.FirstOrDefaultAsync(
                    lipd => lipd.LipId == listaPrecioDetalles.LipId
                    && lipd.ProId == listaPrecioDetalles.ProId);

                if (lpd == null)
                {
                    return NotFound(listaPrecioDetalles.NumLip);
                }

                lpd.LipId = listaPrecioDetalles.LipId;
                lpd.ProId = listaPrecioDetalles.ProId;
                lpd.LipDetSinIva = listaPrecioDetalles.LipDetSinIva;
                lpd.LipDetConIva = listaPrecioDetalles.LipDetConIva;
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

        // DELETE: api/ListaPrecioDetalles/Eliminar
        [Authorize(Roles = "super")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] List<EliminarViewModel> model)
        {

             foreach(EliminarViewModel eliminar in model){
                
                var listaPrecioDetalles = await _context.ListaPrecioDetalles.FirstOrDefaultAsync(
                    lipd => lipd.LipId == eliminar.LipId
                    && lipd.ProId == eliminar.ProId);

                if (listaPrecioDetalles == null)
                {
                    return NotFound();
                }

                _context.ListaPrecioDetalles.Remove(listaPrecioDetalles);

            };
            
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

        private bool ListaPrecioDetallesExists(int id)
        {
            return _context.ListaPrecioDetalles.Any(e => e.NumLip == id);
        }
    }
}