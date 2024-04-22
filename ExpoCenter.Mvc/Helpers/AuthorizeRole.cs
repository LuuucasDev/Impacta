using ExpoCenter.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace ExpoCenter.Mvc.Helpers
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params PerfilUsuario[] perfis)
        {
            //foreach (var perfil in perfis)
            //{
            //    Roles = Roles + perfil + ",";
            //}

            //Roles = Roles.TrimEnd(',');

            Roles = String.Join(',', perfis);
        }
    }
}
