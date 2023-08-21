using usuarioApi.Data.Dtos;
using AutoMapper;
using usuarioApi.Models;

namespace usuarioApi.Profiles;
public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
       CreateMap<CreateUsuarioDTO, Usuario>();
    }

}
