using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Ventas;
using Sistema.Web.Models.Ventas.Venta;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VentasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Ventas/Listar
        [HttpGet("[action]")]
        public async Task <IEnumerable<VentaViewModel>> Listar()
        {
            var venta = await _context.Ventas.ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                VtaId = v.VtaId,
                VtaFolioVenta = v.VtaFolioVenta,
                VtaFecha = v.VtaFecha,
                VtaTotal = v.VtaTotal,
                VtaEstatus = v.VtaEstatus,
                SucId = v.SucId,
                VndId = v.VndId,
                LipId = v.LipId,
            });

        }

        // GET: api/Ventas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var v = await _context.Ventas.FindAsync(id);

            if (v == null)
            {
                return NotFound();
            }

            return Ok(new VentaViewModel {
                VtaId = v.VtaId,
                VtaFolioVenta = v.VtaFolioVenta,
                VtaFecha = v.VtaFecha,
                VtaTotal = v.VtaTotal,
                VtaEstatus = v.VtaEstatus,
                SucId = v.SucId,
                VndId = v.VndId,
                LipId = v.LipId,
            });
        }

        // PUT: api/Ventas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.VtaId <= 0)
            {
                return BadRequest();
            }

            var v = await _context.Ventas.FirstOrDefaultAsync(vta => vta.VtaId == model.VtaId);

            if (v == null)
            {
                return NotFound();
            }

            v.VtaId = model.VtaId;
            v.VtaFolioVenta = model.VtaFolioVenta;
            v.VtaFecha = model.VtaFecha;
            v.VtaTotal = model.VtaTotal;
            v.VtaEstatus = model.VtaEstatus;
            v.SucId = model.SucId;
            v.VndId = (int)model.VndId;
            v.LipId = (int)model.LipId;

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

        // POST: api/Ventas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Venta v = new Venta
            {
                VtaId = model.VtaId,
                VtaFolioVenta = model.VtaFolioVenta,
                VtaFecha = model.VtaFecha,
                VtaTotal = model.VtaTotal,
                VtaEstatus = model.VtaEstatus,
                SucId = model.SucId,
                VndId = (int)model.VndId,
                LipId = (int)model.LipId
            };

            _context.Ventas.Add(v);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Ventas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }           

            return Ok(venta);
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.VtaId == id);
        }
    }
}