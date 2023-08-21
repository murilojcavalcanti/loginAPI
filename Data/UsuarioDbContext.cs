using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using usuarioApi.Models;

namespace usuarioApi.Data
{
    public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        public UsuarioDbContext
            (DbContextOptions<UsuarioDbContext> opts) : base(opts) { }
    }
}
