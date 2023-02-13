using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace supermercadoAPI.Models
{
    public class Caixa
    {
        public List<ItemCaixa> Produtos { get; set; }
        public double Total { get; set; }

        public Caixa()
        {
            Produtos = new List<ItemCaixa>();
        }

        public void AdicionarProduto(ItemCaixa item)
        {
            Produtos.Add(item);
            
        }

        public void RemoverProduto(ItemCaixa produto)
        {
            Produtos.Remove(produto);
        }

        public void CalcularTotal()
        {
            Total = Produtos.Sum(produto => produto.Produto.Preco * produto.Quantidade);
        }

        //realizar venda
        public void RealizarVenda(List<ItemDeposito> itensDeposito, List<ItemCaixa> itensCaixa)
        {
            foreach (var itemCaixa in itensCaixa)
            {
                var itemDeposito = itensDeposito.Find(item => item.Produto.ProdutoId == itemCaixa.Produto.ProdutoId);

                //verificar se tem estoque
                if (itemDeposito.Quantidade < itemCaixa.Quantidade)
                {
                    throw new Exception("Não há estoque suficiente para realizar a venda");
                }
                else if (itemDeposito.Quantidade == itemCaixa.Quantidade)
                {
                   itemDeposito.Quantidade = 0;
                }
                else
                {
                    itemDeposito.Quantidade -= itemCaixa.Quantidade;
                }
            }
            
        }        
}
}