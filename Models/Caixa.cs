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

        public void AdicionarProduto(Produto produto, int quantidade)
        {

            for (int i = 0; i < quantidade; i++)
            {
                Produtos.Add(produto);
            }
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