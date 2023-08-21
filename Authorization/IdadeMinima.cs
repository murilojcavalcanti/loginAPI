using Microsoft.AspNetCore.Authorization;

namespace usuarioApi.Authorization
{
    public class IdadeMinima:IAuthorizationRequirement
    {
        public IdadeMinima(int _Idade)
        {
            Idade = _Idade;
        }
        public int Idade { get; set; }
    }
}
