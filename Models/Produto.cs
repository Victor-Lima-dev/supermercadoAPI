using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace supermercadoAPI.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public double Preco { get; set; }

    }
}