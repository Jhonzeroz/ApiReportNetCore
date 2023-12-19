using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using ReportApis.Models;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Text;


namespace RESTAPI_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;


        private readonly IConfiguration _config;
        private readonly REPORTESAPPSContext _dbcontext;

        public AutenticacionController(IConfiguration config, REPORTESAPPSContext dbContext)
        {
            _config = config;
            _dbcontext = dbContext;
            secretKey = _config.GetSection("Settings")?.GetSection("SecretKey")?.Value;
        }


        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Usuario request)
        {
            var usuario = _dbcontext.Usuarios.FirstOrDefault(u => u.CorreoUser == request.CorreoUser && u.PassUser == request.PassUser);

            if (usuario != null)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.CorreoUser));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(95),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { message = "Bienvenido a nuestro sistema de reportes", result = true, data = new { token = tokencreado, correo = usuario.CorreoUser, nombre = usuario.NombreUser, iduser = usuario.IdUser } });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Valida por favor la informacion ingresada", result = false, data = (object)null });
            }
        }




    }
}
