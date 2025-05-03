using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Server.DTOs;
using SistemaVentas.Server.Repository;

namespace SistemaVentas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepository _dashBoardRepository;
        public DashBoardController(IMapper mapper, IDashBoardRepository dashBoardRepository)
        {
            _mapper = mapper;
            _dashBoardRepository = dashBoardRepository;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            ResponseDTO<DashBoardDTO> _response = new ResponseDTO<DashBoardDTO>();
            try
            {
                DashBoardDTO vmDashBoard = new DashBoardDTO();

                vmDashBoard.TotalVentas = await _dashBoardRepository.TotalVentasUltimaSemana();
                vmDashBoard.TotalIngresos = await _dashBoardRepository.TotalIngresosUltimaSemana();
                vmDashBoard.TotalProductos = await _dashBoardRepository.TotalProductos();

                List<VentaSemanaDTO> listaVentasSemana = new List<VentaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await _dashBoardRepository.VentasUltimaSemana())
                {
                    listaVentasSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                vmDashBoard.VentasUltimaSemana = listaVentasSemana;
                _response = new ResponseDTO<DashBoardDTO>() { status = true, msg = "ok", value = vmDashBoard };
                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO<DashBoardDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
