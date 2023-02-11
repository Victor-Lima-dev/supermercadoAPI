using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace supermercadoAPI.Models
{
    public class Caixa
    {
        public List<Produto> Produtos { get; set; }
        public double Total { get; set; }

        public Caixa()
        {
            Produtos = new List<Produto>();
        }

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }

        public void RemoverProduto(Produto produto)
        {
            Produtos.Remove(produto);
        }

        public void CalcularTotal()
        {
            Total = Produtos.Sum(produto => produto.Preco);
        }
    }
}