using AutoMapper;
using Microsoft.AspNetCore.Identity;
using usuarioApi.Data.Dtos;
using usuarioApi.Models;

namespace usuarioApi.Services;

public class UsuarioService
{
    private IMapper Mapper;
    private UserManager<Usuario> UserManager;
    private SignInManager<Usuario> SignInManager;
    private TokenService TokenService;

    public UsuarioService(IMapper _Mapper, UserManager<Usuario> _UserManager, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        Mapper = _Mapper;
        UserManager = _UserManager;
        SignInManager = signInManager;
        TokenService = tokenService;
    }
    public async Task Cadastra(CreateUsuarioDTO dto)
    {
        Usuario usuario = Mapper.Map<Usuario>(dto);

        IdentityResult resultado = await UserManager.CreateAsync(usuario, dto.Password);

        if (!resultado.Succeeded)  throw new ApplicationException("Falha ao cadastrar usuário!");
    }

    public async Task<string> Login(LoginUsuarioDTO usuarioDto)
    {
        var resultado = await SignInManager.PasswordSignInAsync(usuarioDto.Username, usuarioDto.Password, false, false);
        
        if (!resultado.Succeeded) throw new ApplicationException("Usuario ou senha incorretos");

        var usuario = SignInManager.UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == usuarioDto.Username.ToUpper());

        var token = TokenService.GenerateToken(usuario);
        
        return token;
    }
}
