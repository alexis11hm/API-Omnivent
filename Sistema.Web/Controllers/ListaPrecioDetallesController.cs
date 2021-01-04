using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public ListaPrecioDetallesController(DbContextSistema context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/ListaPrecioDetalles/Listar
        //[Authorize(Roles = "super,administrador,consultor")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListaPrecioDetalleViewModel>> Listar()
        {
            var listaDetalle = await _context.ListaPrecioDetalles.ToListAsync();

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

        
    }
}