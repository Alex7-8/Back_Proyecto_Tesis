using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LogicLayer.Helper
{
    public class JWTHelper
    {


        public string TokenDispositivo(string Id_Dispositivo, string secretKey) 
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("IdDispositivo", Id_Dispositivo));
           

            var tokenDescription = new SecurityTokenDescriptor()
            {

                Subject = claims,
                Expires = null,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(createdToken);
        }


        public string TokenDispositivoValidacion(string Id_Dispositivo, string Id_usuario, string secretKey)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("IdDispositivo", Id_Dispositivo));
            claims.AddClaim(new Claim("IdUsuario", Id_usuario));

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(createdToken);
        }


        public string CreateToken(string ID_USUARIO, string DESCRIPCION_ROLES, string ID_SUCURSAL, string NOMBRE_SUCURSAL, string LOGO_SUCURSAL, string NOMBRE_EMPLEADO, string FOTO_EMPLEADO,string CORREO, string secretKey)
        {

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("IdUsuario", ID_USUARIO));
            claims.AddClaim(new Claim("Nombre", NOMBRE_EMPLEADO));
            claims.AddClaim(new Claim("Correo", CORREO));
            claims.AddClaim(new Claim("IdSucursal", ID_SUCURSAL));
            claims.AddClaim(new Claim("NombreSucursal", NOMBRE_SUCURSAL));
            claims.AddClaim(new Claim("LogoSucursal", LOGO_SUCURSAL));
            claims.AddClaim(new Claim("FotoEmpleado", FOTO_EMPLEADO));


            string[] arrayRols = DESCRIPCION_ROLES.Split(';');
           
            /*Roles*/
            foreach (string rol in arrayRols)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, rol));
            }

          
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = claims,
                //Expires = DateTime.UtcNow.AddHours(.20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}