using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.Producto;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProductosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Productos/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProductoViewModel>> Listar()
        {
            var producto = await _context.Productos.ToListAsync();

            return producto.Select(p => new ProductoViewModel
            {
                ProId = p.ProId,
                ProDescripcion = p.ProDescripcion,
                ProCodigoBarras = p.ProCodigoBarras,
                ProIdentificacion = p.ProIdentificacion,
                Familia = p.Familia,
                SubFamilia = p.SubFamilia,
                ProPrecioGeneralIva = p.ProPrecioGeneralIva,
                ProCostoGeneralIva = p.ProCostoGeneralIva
            });

        }

        // GET: api/Productos/Mostrar/1
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var p = await _context.Productos.FindAsync(id);

            if (p == null)
            {
                return NotFound();
            }

            return Ok(new SelectViewModel
            {
                ProId = p.ProId,
                ProDescripcion = p.ProDescripcion,
                ProCodigoBarras = p.ProCodigoBarras,
                ProIdentificacion = p.ProIdentificacion,
                Familia = p.Familia,
                SubFamilia = p.SubFamilia,
                ProPrecioGeneralIva = p.ProPrecioGeneralIva,
                ProCostoGeneralIva = p.ProCostoGeneralIva
            });
        }

        // PUT: api/Productos/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.ProId <= 0)
            {
                return BadRequest();
            }

            var p = await _context.Productos.FirstOrDefaultAsync(pro => pro.ProId == model.ProId);

            if (p == null)
            {
                return NotFound();
            }

            p.ProId = model.ProId;
            p.ProDescripcion = model.ProDescripcion;
            p.ProCodigoBarras = model.ProCodigoBarras;
            p.ProIdentificacion = model.ProIdentificacion;
            p.Familia = model.Familia;
            p.SubFamilia = model.SubFamilia;
            p.ProPrecioGeneralIva = model.ProPrecioGeneralIva;
            p.ProCostoGeneralIva = model.ProCostoGeneralIva;

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

        // POST: api/Productos/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            List<Producto> productos = new List<Producto>();

            model.ForEach(producto =>
            {
                Producto p = new Producto
                {
                    ProId = producto.ProId,
                    ProDescripcion = producto.ProDescripcion,
                    ProCodigoBarras = producto.ProCodigoBarras,
                    ProIdentificacion = producto.ProIdentificacion,
                    Familia = producto.Familia,
                    SubFamilia = producto.SubFamilia,
                    ProPrecioGeneralIva = producto.ProPrecioGeneralIva,
                    ProCostoGeneralIva = producto.ProCostoGeneralIva,
                };
                productos.Add(p);
            });

            await _context.Productos.AddRangeAsync(productos);
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

        // DELETE: api/Productos/Eliminar/1
        [Authorize(Roles = "super")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(producto);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProId == id);
        }
    }
}