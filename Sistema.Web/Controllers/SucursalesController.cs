using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.Sucursal;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public SucursalesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Sucursales/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SucursalViewModel>> Listar()
        {
            var sucursal = await _context.Sucursales.ToListAsync();

            return sucursal.Select(suc => new SucursalViewModel
            {
                SucId = suc.SucId,
                SucNombre = suc.SucNombre
            });

        }

        // GET: api/Sucursales/Mostrar/1
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var s = await _context.Sucursales.FindAsync(id);

            if (s == null)
            {
                return NotFound();
            }

            return Ok(new SelectViewModel
            {
                SucId = s.SucId,
                SucNombre = s.SucNombre
            });
        }

        // PUT: api/Sucursales/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {

            foreach (ActualizarViewModel sucursal in model)
            {

                if (sucursal.SucId <= 0)
                {
                    return BadRequest(sucursal);
                }

                var s = await _context.Sucursales.FirstOrDefaultAsync(suc => suc.SucId == sucursal.SucId);

                if (s == null)
                {
                    return NotFound(sucursal.SucId);
                }

                s.SucId = sucursal.SucId;
                s.SucNombre = sucursal.SucNombre;
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

        // POST: api/Sucursales/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            List<Sucursal> sucursales = new List<Sucursal>();

            model.ForEach(sucursal =>
            {
                Sucursal s = new Sucursal
                {
                    SucId = sucursal.SucId,
                    SucNombre = sucursal.SucNombre
                };
                sucursales.Add(s);
            });

            await _context.Sucursales.AddRangeAsync(sucursales);
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

        // DELETE: api/Sucursales/Eliminar/
        [Authorize(Roles = "super")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] Int16[] ids)
        {
            foreach (Int16 id in ids)
            {
                var sucursal = await _context.Sucursales.FindAsync(id);
                if (sucursal == null)
                {
                    return NotFound(id);
                }

                _context.Sucursales.Remove(sucursal);
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

        private bool SucursalExists(int id)
        {
            return _context.Sucursales.Any(e => e.SucId == id);
        }
    }
}