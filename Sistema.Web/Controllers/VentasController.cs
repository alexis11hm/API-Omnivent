using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Ventas;
using Sistema.Web.Models.Ventas.Venta;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
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
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task <IEnumerable<VentaViewModel>> Listar()
        {
            var venta = await _context.Ventas.ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                VtaId = v.VtaId,
                VtaFolioVenta = v.VtaFolioVenta,
                VtaFecha = v.VtaFecha.ToString("dd/MM/yyyy"),
                VtaTotal = v.VtaTotal,
                VtaEstatus = v.VtaEstatus,
                Sucursal = v.Sucursal,
                Vendedor = v.Vendedor,
                ListaPrecios = v.ListaPrecios,
            });

        }

        // GET: api/Ventas/Mostrar/1
        [Authorize(Roles = "super,administrador,consultor")]
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
                VtaFecha = v.VtaFecha.ToString("dd/MM/yyyy"),
                VtaTotal = v.VtaTotal,
                VtaEstatus = v.VtaEstatus,
                Sucursal = v.Sucursal,
                Vendedor = v.Vendedor,
                ListaPrecios = v.ListaPrecios,
            });
        }

        // PUT: api/Ventas/Actualizar
        [Authorize(Roles = "super")]
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
            v.Sucursal = model.Sucursal;
            v.Vendedor = model.Vendedor;
            v.ListaPrecios = model.ListaPrecios;

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
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            List<Venta> ventas = new List<Venta>();

            model.ForEach(venta => {
                Venta v = new Venta
                {
                    VtaId = venta.VtaId,
                    VtaFolioVenta = venta.VtaFolioVenta,
                    VtaFecha = venta.VtaFecha,
                    VtaTotal = venta.VtaTotal,
                    VtaEstatus = venta.VtaEstatus,
                    Sucursal = venta.Sucursal,
                    Vendedor = venta.Vendedor,
                    ListaPrecios = venta.ListaPrecios
                };
                ventas.Add(v);
            });

            await _context.Ventas.AddRangeAsync(ventas);
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

        // DELETE: api/Ventas/Eliminar/1
        [Authorize(Roles = "super")]
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
                return BadRequest(ex);
            }           

            return Ok(venta);
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.VtaId == id);
        }
    }
}