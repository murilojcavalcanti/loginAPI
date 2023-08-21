using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace usuarioApi.Authorization;

public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
    {
        var dataNascimentoClaim = context
            .User.FindFirst(claim => 
            claim.Type == ClaimTypes.DateOfBirth);

        if(dataNascimentoClaim is null) return Task.CompletedTask;

        var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

        var idadeusuario = DateTime.Today.Year - dataNascimento.Year;
        
        if (dataNascimento > DateTime.Today.AddYears(-idadeusuario))
        {
            idadeusuario--;
        } 
        
        if (idadeusuario >= requirement.Idade)
            context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}
