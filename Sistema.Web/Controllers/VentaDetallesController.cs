using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Ventas;
using Sistema.Web.Models.Ventas.VentaDetalle;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class VentaDetallesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VentaDetallesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/VentaDetalles/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<VentaDetalleViewModel>> Listar()
        {
            var ventasDetalles = await _context.VentaDetalles.Include(vta => vta.VtaId).Include(p => p.producto).ToListAsync();

            return ventasDetalles.Select(v => new VentaDetalleViewModel
            {
                VedId = v.VedId,
                VtaId = v.VtaId,
                ProId = v.ProId,
                VedPrecio = v.VedPrecio,
                VedDescuento = v.VedDescuento,
                VedCantidad = v.VedCantidad,
                Venta = v.venta.VtaFolioVenta.ToString(),
                Producto = v.producto.ProDescripcion
            });
        }

        // PUT: api/VentaDetalles/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {
            foreach (ActualizarViewModel ventaDetalle in model)
            {
                if (ventaDetalle.VedId <= 0 || ventaDetalle.VtaId <= 0 || ventaDetalle.ProId <= 0)
                {
                    return BadRequest(ventaDetalle);
                }

                var v = await _context.VentaDetalles.FirstOrDefaultAsync(vta => vta.VedId == ventaDetalle.VedId);

                if (v == null)
                {
                    return NotFound();
                }

                v.VedId = ventaDetalle.VedId;
                v.VtaId = ventaDetalle.VtaId;
                v.ProId = ventaDetalle.ProId;
                v.VedPrecio = ventaDetalle.VedPrecio;
                v.VedDescuento = ventaDetalle.VedDescuento;
                v.VedCantidad = ventaDetalle.VedCantidad;
            }

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

        // POST: api/VentaDetalles/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            List<VentaDetalle> ventasDetalles = new List<VentaDetalle>();

            model.ForEach(ventaDetalle =>
            {
                VentaDetalle v = new VentaDetalle
                {
                    VedId = ventaDetalle.VedId,
                    VtaId = ventaDetalle.VtaId,
                    ProId = ventaDetalle.ProId,
                    VedPrecio = ventaDetalle.VedPrecio,
                    VedDescuento = ventaDetalle.VedDescuento,
                    VedCantidad = ventaDetalle.VedCantidad
                };
                ventasDetalles.Add(v);
            });

            await _context.VentaDetalles.AddRangeAsync(ventasDetalles);
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

        // DELETE: api/VentaDetalles/Eliminar/1
        [Authorize(Roles = "super")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int[] ids)
        {
            foreach (int id in ids)
            {
                var ventaDetalle = await _context.VentaDetalles.FindAsync(id);
                if (ventaDetalle == null)
                {
                    return NotFound(id);
                }

                _context.VentaDetalles.Remove(ventaDetalle);
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

        // GET: api/VentaDetalles/MostrarPorVenta/1
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<VentaDetalleViewModel>> MostrarPorVenta([FromRoute] int id)
        {

            var ved = await _context.VentaDetalles.Include(vta => vta.VtaId).Include(p => p.producto).Where(vtaDet => vtaDet.VtaId == id).ToListAsync();

            return ved.Select(vtaDet => new VentaDetalleViewModel
            {
                VedId = vtaDet.VedId,
                VtaId = vtaDet.VtaId,
                ProId = vtaDet.ProId,
                VedPrecio = vtaDet.VedPrecio,
                VedDescuento = vtaDet.VedDescuento,
                VedCantidad = vtaDet.VedCantidad,
                Venta = vtaDet.venta.VtaFolioVenta.ToString(),
                Producto = vtaDet.producto.ProDescripcion
            });
        }

        // GET: api/VentaDetalles/MostrarPorProducto/1
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<VentaDetalleViewModel>> MostrarPorProducto([FromRoute] int id)
        {

            var ved = await _context.VentaDetalles.Include(vta => vta.VtaId).Include(p => p.producto).Where(vtaDet => vtaDet.ProId == id).ToListAsync();

            return ved.Select(vtaDet => new VentaDetalleViewModel
            {
                VedId = vtaDet.VedId,
                VtaId = vtaDet.VtaId,
                ProId = vtaDet.ProId,
                VedPrecio = vtaDet.VedPrecio,
                VedDescuento = vtaDet.VedDescuento,
                VedCantidad = vtaDet.VedCantidad,
                Venta = vtaDet.venta.VtaFolioVenta.ToString(),
                Producto = vtaDet.producto.ProDescripcion
            });
        }
    }
}