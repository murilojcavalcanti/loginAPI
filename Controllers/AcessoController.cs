using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace usuarioApi.Controllers;

[ApiController]
[Route("[Controller]")]

public class AcessoController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy ="IdadeMinima")]
    public IActionResult Get()
    {
        return Ok("Acesso permitido");
    }
}
