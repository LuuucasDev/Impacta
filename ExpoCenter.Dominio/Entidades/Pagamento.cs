using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoCenter.Dominio.Entidades
{
    public class Pagamento
    {
        public int Id { get; set; }
        public Guid IdProduto { get; set; }
        public Guid IdCartao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public int Status { get; set; }
    }
}
