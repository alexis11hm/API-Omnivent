using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Ventas;
using Sistema.Web.Models.Ventas.FormaPago;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class FormasPagoController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public FormasPagoController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/FormasPago/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<FormaPagoViewModel>> Listar()
        {
            var formasPago = await _context.FormasPago.ToListAsync();

            return formasPago.Select(fop => new FormaPagoViewModel
            {
                FopId = fop.FopId,
                FopDescripcion = fop.FopDescripcion,
            });

        }

        // PUT: api/FormasPago/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {

            foreach (ActualizarViewModel formaPago in model)
            {

                if (formaPago.FopId <= 0)
                {
                    return BadRequest(formaPago);
                }

                var fop = await _context.FormasPago.FirstOrDefaultAsync(f => f.FopId == formaPago.FopId);

                if (fop == null)
                {
                    return NotFound(formaPago);
                }

                fop.FopId = formaPago.FopId;
                fop.FopDescripcion = formaPago.FopDescripcion;
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

        // POST: api/FormasPago/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            List<FormaPago> formasPago = new List<FormaPago>();

            model.ForEach(formaPago =>
            {
                FormaPago fop = new FormaPago
                {
                    FopId = formaPago.FopId,
                    FopDescripcion = formaPago.FopDescripcion,
                };
                formasPago.Add(fop);
            });

            await _context.FormasPago.AddRangeAsync(formasPago);
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

        // DELETE: api/FormasPago/Eliminar/
        [Authorize(Roles = "super")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] int[] ids)
        {
            foreach (int id in ids)
            {
                var formaPago = await _context.FormasPago.FindAsync(id);
                if (formaPago == null)
                {
                    return NotFound(id);
                }

                _context.FormasPago.Remove(formaPago);
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
    }
}