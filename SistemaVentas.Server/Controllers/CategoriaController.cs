using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Server.DTOs;
using SistemaVentas.Server.Repository;

namespace SistemaVentas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaController(IMapper mapper, ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<CategoriaDTO>> _response = new ResponseDTO<List<CategoriaDTO>>();
            try
            {
                List<CategoriaDTO> _listaCtegorias = new List<CategoriaDTO>();
                _listaCtegorias = _mapper.Map<List<CategoriaDTO>>(await _categoriaRepository.Lista());
                if(_listaCtegorias.Count() > 0)
                {
                    _response = new ResponseDTO<List<CategoriaDTO>>() { status = true, msg = "ok", value = _listaCtegorias };
                }
                else
                {
                    _response = new ResponseDTO<List<CategoriaDTO>>() { status = false, msg = "No se encontraron categorias", value = null };
                }

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO<List<CategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
