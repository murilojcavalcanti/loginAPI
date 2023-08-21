using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using usuarioApi.Data;
using usuarioApi.Data.Dtos;
using usuarioApi.Models;
using usuarioApi.Services;

namespace usuarioApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private UsuarioService UsuarioService;
  
    public UsuarioController(UsuarioService _CadastroService)
    {
        UsuarioService = _CadastroService;
    }


    [HttpPost("Cadastro")]
    public async Task<IActionResult> CadastraUsuario (CreateUsuarioDTO usuarioDto)
    {
            await UsuarioService.Cadastra(usuarioDto);
            return Ok("Usuário cadastrado!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDTO loginDTO)
    {
        var token =  await UsuarioService.Login(loginDTO);
        return Ok(token);
    }

}
