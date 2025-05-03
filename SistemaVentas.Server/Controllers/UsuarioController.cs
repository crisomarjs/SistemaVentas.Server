﻿using AutoMapper;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<UsuarioDTO>> _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>();

            try
            {
                List<UsuarioDTO> ListaUsuarios = new List<UsuarioDTO>();
                IQueryable<Usuario> query = await _usuarioRepository.Consultar();
                query = query.Include(r => r.IdRolNavigation);

                ListaUsuarios = _mapper.Map<List<UsuarioDTO>>(query.ToList());

                if (ListaUsuarios.Count > 0)
                {
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = true, msg = "ok", value = ListaUsuarios };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = "", value = null };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            ResponseDTO<Usuario> _ResponseDTO = new ResponseDTO<Usuario>();
            try
            {
                Usuario _usuario = await _usuarioRepository.Obtener(u => u.Correo == correo && u.Clave == clave);

                if (_usuario != null)
                {
                    _ResponseDTO = new ResponseDTO<Usuario>() { status = true, msg = "ok", value = _usuario };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<Usuario>() { status = false, msg = "no encontrado", value = null };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<Usuario>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO request)
        {
            ResponseDTO<UsuarioDTO> _ResponseDTO = new ResponseDTO<UsuarioDTO>();
            try
            {
                Usuario _usuario = _mapper.Map<Usuario>(request);

                Usuario _usuarioCreado = await _usuarioRepository.Crear(_usuario);

                if (_usuarioCreado.IdUsuario != 0)
                {
                    _ResponseDTO = new ResponseDTO<UsuarioDTO>() { status = true, msg = "ok", value = _mapper.Map<UsuarioDTO>(_usuarioCreado) };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<UsuarioDTO>() { status = false, msg = "No se pudo crear el usuario" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<UsuarioDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO request)
        {
            ResponseDTO<UsuarioDTO> _ResponseDTO = new ResponseDTO<UsuarioDTO>();
            try
            {
                Usuario _usuario = _mapper.Map<Usuario>(request);
                Usuario _usuarioParaEditar = await _usuarioRepository.Obtener(u => u.IdUsuario == _usuario.IdUsuario);

                if (_usuarioParaEditar != null)
                {

                    _usuarioParaEditar.NombreApellidos = _usuario.NombreApellidos;
                    _usuarioParaEditar.Correo = _usuario.Correo;
                    _usuarioParaEditar.IdRol = _usuario.IdRol;
                    _usuarioParaEditar.Clave = _usuario.Clave;

                    bool respuesta = await _usuarioRepository.Editar(_usuarioParaEditar);

                    if (respuesta)
                    {
                        _ResponseDTO = new ResponseDTO<UsuarioDTO>() { status = true, msg = "ok", value = _mapper.Map<UsuarioDTO>(_usuarioParaEditar) };
                    }
                    else
                    {
                        _ResponseDTO = new ResponseDTO<UsuarioDTO>() { status = false, msg = "No se pudo editar el usuario" };
                    }
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<UsuarioDTO>() { status = false, msg = "No se encontró el usuario" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<UsuarioDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }



        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<string> _ResponseDTO = new ResponseDTO<string>();
            try
            {
                Usuario _usuarioEliminar = await _usuarioRepository.Obtener(u => u.IdUsuario == id);

                if (_usuarioEliminar != null)
                {

                    bool respuesta = await _usuarioRepository.Eliminar(_usuarioEliminar);

                    if (respuesta)
                    {
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    }
                    else
                    {
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el usuario", value = "" };
                    }
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
    }
}
