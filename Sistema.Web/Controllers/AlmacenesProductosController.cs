﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.AlmacenProducto;

namespace Sistema.Web.Controllers
{
    //Ruta para acceder a los métodos del controlador
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenesProductosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public AlmacenesProductosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/AlmacenesProductos/Listar
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<AlmacenProductoViewModel>> Listar()
        {
            var almacenProducto = await _context.AlmacenProductos.Include(alm => alm.almacen).Include(p => p.producto).ToListAsync();


            return almacenProducto.Select(almp => new AlmacenProductoViewModel
            {
                AlmId = almp.AlmId,
                ProId = almp.ProId,
                AlpStockActual = almp.AlpStockActual,
                almacen = almp.almacen.AlmDescripcion,
                producto = almp.producto.ProCodigoBarras
            });
        }

        // GET: api/AlmacenesProductos/Existencias
        //[Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ExistenciasPorAlmacenPorProductoViewModel>> Existencias()
        {
            var almacenProducto = await _context.AlmacenProductos.Include(alm => alm.almacen).Include(p => p.producto).Include(s => s.almacen.sucursal).ToListAsync();

            IEnumerable<ExistenciasPorAlmacenPorProductoViewModel> existencias =  almacenProducto.Select(almp => new ExistenciasPorAlmacenPorProductoViewModel
            {
                ProId = almp.ProId,
                productoDescripcion = almp.producto.ProDescripcion,
                productoIdentificacion = almp.producto.ProIdentificacion,
                almacen = almp.almacen.AlmDescripcion,
                AlpStockActual = almp.AlpStockActual,
                sucursal = almp.almacen.sucursal.SucNombre
            });

            foreach(ExistenciasPorAlmacenPorProductoViewModel existencia in existencias){
            }


            return almacenProducto.Select(almp => new ExistenciasPorAlmacenPorProductoViewModel
            {
                ProId = almp.ProId,
                productoDescripcion = almp.producto.ProDescripcion,
                productoIdentificacion = almp.producto.ProIdentificacion,
                almacen = almp.almacen.AlmDescripcion,
                AlpStockActual = almp.AlpStockActual,
                sucursal = almp.almacen.sucursal.SucNombre
            });
        }

        // PUT: api/AlmacenesProductos/Actualizar
        [Authorize(Roles = "super")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] List<ActualizarViewModel> model)
        {
            foreach (ActualizarViewModel almacenProducto in model)
            {
                if (almacenProducto.AlmId <= 0 || almacenProducto.ProId <= 0)
                {
                    return BadRequest(almacenProducto);
                }

                var a = await _context.AlmacenProductos.FirstOrDefaultAsync(almp => almp.AlmId == almacenProducto.AlmId && almp.ProId == almacenProducto.ProId);

                if (a == null)
                {
                    return NotFound();
                }

                a.AlmId = almacenProducto.AlmId;
                a.ProId = almacenProducto.ProId;
                a.AlpStockActual = almacenProducto.AlpStockActual;
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

        // POST: api/AlmacenesProductos/Crear
        [Authorize(Roles = "super")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] List<CrearViewModel> model)
        {
            List<AlmacenProducto> almacenesProductos = new List<AlmacenProducto>();

            model.ForEach(almacenProducto =>
            {
                AlmacenProducto a = new AlmacenProducto
                {
                    AlmId = almacenProducto.AlmId,
                    ProId = almacenProducto.ProId,
                    AlpStockActual = almacenProducto.AlpStockActual
                };
                almacenesProductos.Add(a);
            });

            await _context.AlmacenProductos.AddRangeAsync(almacenesProductos);
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

        // DELETE: api/AlmacenesProductos/Eliminar
        [Authorize(Roles = "super")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] List<EliminarViewModel> almacenesProductosEliminar)
        {

            foreach (EliminarViewModel almacenProducto in almacenesProductosEliminar)
            {

                var almPro = await _context.AlmacenProductos.FirstOrDefaultAsync(
                    almp => almp.AlmId == almacenProducto.AlmId && almp.ProId == almacenProducto.ProId);

                if (almPro == null)
                {
                    return NotFound();
                }

                _context.AlmacenProductos.Remove(almPro);

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

        // GET: api/AlmacenesProductos/MostrarExistencias
        //[Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> MostrarExistencias()
        {
            var existencias = await _context.Existencias.FromSql("ObtenerExistenciasAlmacenes").ToListAsync();

            return existencias.Select(ext => new SelectViewModel
            {
                ProId = ext.ProId,
                ProDescripcion = ext.ProDescripcion,
                ProIdentificacion = ext.ProIdentificacion,
                AlmacenAguascalientes = ext.AlmacenAguascalientes,
                AlmacenGeneral = ext.AlmacenGeneral,
                SucNombre = ext.SucNombre
            });
        }

        // GET: api/AlmacenesProductos/MostrarPorVenta/1
        [Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<AlmacenProductoViewModel>> MostrarPorAlmacen([FromRoute] int id)
        {
            var almacenProducto = await _context.AlmacenProductos.Include(almp => almp.almacen).Include(p => p.producto).Where(al => al.AlmId == id).ToListAsync();

            return almacenProducto.Select(almp => new AlmacenProductoViewModel
            {
                AlmId = almp.AlmId,
                ProId = almp.ProId,
                AlpStockActual = almp.AlpStockActual,
                almacen = almp.almacen.AlmDescripcion,
                producto = almp.producto.ProCodigoBarras
            });
        }
    }
}