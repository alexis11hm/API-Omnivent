using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.Almacen;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public AlmacenesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Almacenes/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<AlmacenViewModel>> Listar()
        {
            var almacenes = await _context.Almacenes.Include(suc => suc.sucursal).ToListAsync();

            return almacenes.Select(alm => new AlmacenViewModel
            {
                AlmId = alm.AlmId,
                AlmDescripcion = alm.AlmDescripcion,
                AlmEstatus = alm.AlmEstatus,
                SucId = alm.SucId,
                sucursal = alm.sucursal.SucNombre
            });
        }

        // PUT: api/Almacenes/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {

            foreach (ActualizarViewModel almacen in model)
            {

                if (almacen.AlmId <= 0)
                {
                    return BadRequest(almacen);
                }

                var alm = await _context.Almacenes.FirstOrDefaultAsync(a => a.AlmId == almacen.AlmId);

                if (alm == null)
                {
                    return NotFound(almacen);
                }

                alm.AlmId = almacen.AlmId;
                alm.AlmDescripcion = almacen.AlmDescripcion;
                alm.AlmEstatus = almacen.AlmEstatus;
                alm.SucId = almacen.SucId;
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

        // POST: api/Almacenes/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            List<Almacen> almacenes = new List<Almacen>();

            model.ForEach(almacen =>
            {
                Almacen alm = new Almacen
                {
                    AlmId = almacen.AlmId,
                    AlmDescripcion = almacen.AlmDescripcion,
                    AlmEstatus = almacen.AlmEstatus,
                    SucId = almacen.SucId,
                };
                almacenes.Add(alm);
            });

            await _context.Almacenes.AddRangeAsync(almacenes);
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

        // DELETE: api/Almacenes/Eliminar/
        [Authorize(Roles = "super")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] int[] ids)
        {
            foreach (int id in ids)
            {
                var almacen = await _context.Almacenes.FindAsync(id);
                if (almacen == null)
                {
                    return NotFound(id);
                }

                _context.Almacenes.Remove(almacen);
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