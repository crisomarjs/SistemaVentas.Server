using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Server.DTOs;
using SistemaVentas.Server.Models;
using SistemaVentas.Server.Repository;

namespace SistemaVentas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVentaRepository _ventaRepository;
        public VentaController(IMapper mapper, IVentaRepository ventaRepository)
        {
            _mapper = mapper;
            _ventaRepository = ventaRepository;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO request)
        {
            ResponseDTO<VentaDTO> _ResponseDTO = new ResponseDTO<VentaDTO>();
            try
            {

                Venta venta_creada = await _ventaRepository.Registrar(_mapper.Map<Venta>(request));
                request = _mapper.Map<VentaDTO>(venta_creada);

                if (venta_creada.IdVenta != 0)
                {
                    _ResponseDTO = new ResponseDTO<VentaDTO>() { status = true, msg = "ok", value = request };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<VentaDTO>() { status = false, msg = "No se pudo registrar la venta" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<VentaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            ResponseDTO<List<VentaDTO>> _ResponseDTO = new ResponseDTO<List<VentaDTO>>();

            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaInicio is null ? "" : fechaFin;

            try
            {

                List<VentaDTO> vmHistorialVenta = _mapper.Map<List<VentaDTO>>(await _ventaRepository.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin));

                if (vmHistorialVenta.Count > 0)
                {
                    _ResponseDTO = new ResponseDTO<List<VentaDTO>>() { status = true, msg = "ok", value = vmHistorialVenta };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<List<VentaDTO>>() { status = false, msg = "No se pudo registrar la venta" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<VentaDTO>>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);

            }

        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            ResponseDTO<List<ReporteDTO>> _ResponseDTO = new ResponseDTO<List<ReporteDTO>>();
            try
            {

                List<ReporteDTO> listaReporte = _mapper.Map<List<ReporteDTO>>(await _ventaRepository.Reporte(fechaInicio, fechaFin));

                if (listaReporte.Count > 0)
                {
                    _ResponseDTO = new ResponseDTO<List<ReporteDTO>>() { status = true, msg = "ok", value = listaReporte };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<List<ReporteDTO>>() { status = false, msg = "No se pudo registrar la venta" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<ReporteDTO>>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);

            }

        }
    }
}
