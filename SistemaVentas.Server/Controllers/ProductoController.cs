using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Server.DTOs;
using SistemaVentas.Server.Models;
using SistemaVentas.Server.Repository;

namespace SistemaVentas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductoRepository _productoRepository;
        public ProductoController(IMapper mapper, IProductoRepository productoRepository)
        {
            _mapper = mapper;
            _productoRepository = productoRepository;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<ProductoDTO>> response = new ResponseDTO<List<ProductoDTO>>();
            try
            {
                List<ProductoDTO> listaProductos = new List<ProductoDTO>();
                IQueryable<Producto> query = await _productoRepository.Consultar();
                query = query.Include(p => p.IdCategoriaNavigation);

                listaProductos = _mapper.Map<List<ProductoDTO>>(query.ToList());
                if(listaProductos.Count() > 0)
                {
                    response = new ResponseDTO<List<ProductoDTO>>() { status= true, msg = "ok", value = listaProductos };
                }
                else
                {
                    response = new ResponseDTO<List<ProductoDTO>>() { status = false, msg = "No hay productos registrados", value = null };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO<List<ProductoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ProductoDTO producto)
        {
            ResponseDTO<ProductoDTO> response = new ResponseDTO<ProductoDTO>();
            try
            {
                Producto _producto = _mapper.Map<Producto>(producto);
                Producto productoCreado = await _productoRepository.Crear(_producto);
                if (productoCreado.IdProducto != 0)
                {
                    response = new ResponseDTO<ProductoDTO>() { status = true, msg = "ok", value = _mapper.Map<ProductoDTO>(productoCreado) };
                }
                else
                {
                    response = new ResponseDTO<ProductoDTO>() { status = false, msg = "No se pudo guardar el producto", value = null };
                }
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO<ProductoDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductoDTO producto)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            try
            {
                Producto _producto = _mapper.Map<Producto>(producto);
                Producto productoEditar = await _productoRepository.Obtener(u => u.IdProducto == _producto.IdProducto);
                if (productoEditar != null)
                {
                    productoEditar.Nombre = _producto.Nombre;
                    productoEditar.IdCategoria = _producto.IdCategoria;
                    productoEditar.Stock = _producto.Stock;
                    productoEditar.Precio = _producto.Precio;

                    bool resp = await _productoRepository.Editar(productoEditar);

                    if(resp)
                    {
                        response = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    }
                    else
                    {
                        response = new ResponseDTO<bool>() { status = false, msg = "No se pudo editar el producto" };
                    }
                }
                else
                {
                    response = new ResponseDTO<bool>() { status = false, msg = "No se encontró el Producto" };
                }
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            try
            {
                Producto productoEliminar = await _productoRepository.Obtener(u => u.IdProducto == id);
                if (productoEliminar != null)
                {
                    bool resp = await _productoRepository.Eliminar(productoEliminar);
                    if (resp)
                    {
                        response = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    }
                    else
                    {
                        response = new ResponseDTO<bool>() { status = false, msg = "No se pudo eliminar el producto" };
                    }
                }
                else
                {
                    response = new ResponseDTO<bool>() { status = false, msg = "No se encontró el Producto" };
                }
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
