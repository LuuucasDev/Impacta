using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GatewayPagamento.WebApi.Helpers
{
    public static class EnumHelper
    {
        public static string ObterDescricao(this Enum valor)
        {
            var campo = valor.GetType().GetField(valor.ToString());

            if (campo == null) return string.Empty;

            var atributo = Attribute.GetCustomAttribute(campo, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return atributo == null ? valor.ToString() : atributo.Description;

        }
    }
}