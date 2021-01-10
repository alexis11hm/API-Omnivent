using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Ventas;
using Sistema.Web.Models.Ventas.FlujoEfectivo;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class FlujosEfectivoController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public FlujosEfectivoController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/FlujosEfectivo/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<FlujoEfectivoViewModel>> Listar()
        {
            var flujosEfectivo = await _context.FlujosEfectivo.Include(fop => fop.formaPago).ToListAsync();

            return flujosEfectivo.Select(fle => new FlujoEfectivoViewModel
            {
                FleId = fle.FleId,
                FleFecha = fle.FleFecha.ToString("dd/MM/yyyy"),
                FleImporte = fle.FleImporte,
                FopId = fle.FopId,
                FleTipo = fle.FleTipo,
                FleReferencia = fle.FleReferencia,
                FleObservaciones = fle.FleObservaciones,
                FleDescripcion = fle.FleDescripcion,
                Sucursal = fle.Sucursal,
                CacId = fle.CacId,
                FormaPago = fle.formaPago.FopDescripcion
            });
        }

        // PUT: api/FlujosEfectivo/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {

            foreach (ActualizarViewModel flujoEfectivo in model)
            {

                if (flujoEfectivo.FleId <= 0)
                {
                    return BadRequest(flujoEfectivo);
                }

                var fle = await _context.FlujosEfectivo.FirstOrDefaultAsync(f => f.FleId == flujoEfectivo.FleId);

                if (fle == null)
                {
                    return NotFound(flujoEfectivo);
                }

                fle.FleId = flujoEfectivo.FleId;
                fle.FleFecha = flujoEfectivo.FleFecha;
                fle.FleImporte = flujoEfectivo.FleImporte;
                fle.FopId = flujoEfectivo.FopId;
                fle.FleTipo = flujoEfectivo.FleTipo;
                fle.FleReferencia = flujoEfectivo.FleReferencia;
                fle.FleObservaciones = flujoEfectivo.FleObservaciones;
                fle.FleDescripcion = flujoEfectivo.FleDescripcion;
                fle.Sucursal = flujoEfectivo.Sucursal;
                fle.CacId = flujoEfectivo.CacId;
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

        // POST: api/FlujosEfectivo/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            List<FlujoEfectivo> flujosEfectivo = new List<FlujoEfectivo>();

            model.ForEach(flujoEfectivo =>
            {
                FlujoEfectivo fle = new FlujoEfectivo
                {
                    FleId = flujoEfectivo.FleId,
                    FleFecha = flujoEfectivo.FleFecha,
                    FleImporte = flujoEfectivo.FleImporte,
                    FopId = flujoEfectivo.FopId,
                    FleTipo = flujoEfectivo.FleTipo,
                    FleReferencia = flujoEfectivo.FleReferencia,
                    FleObservaciones = flujoEfectivo.FleObservaciones,
                    FleDescripcion = flujoEfectivo.FleDescripcion,
                    Sucursal = flujoEfectivo.Sucursal,
                    CacId = flujoEfectivo.CacId
                };
                flujosEfectivo.Add(fle);
            });

            await _context.FlujosEfectivo.AddRangeAsync(flujosEfectivo);
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

        // DELETE: api/FlujosEfectivo/Eliminar/
        [Authorize(Roles = "super")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] int[] ids)
        {
            foreach (int id in ids)
            {
                var flujoEfectivo = await _context.FlujosEfectivo.FindAsync(id);
                if (flujoEfectivo == null)
                {
                    return NotFound(id);
                }

                _context.FlujosEfectivo.Remove(flujoEfectivo);
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