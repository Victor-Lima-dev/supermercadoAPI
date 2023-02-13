using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using supermercadoAPI.context;
using supermercadoAPI.Models;

namespace supermercadoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaixaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Caixa _caixa;

        public CaixaController(AppDbContext context, Caixa caixa)
        {
            _context = context;
            _caixa = caixa;
        }

        //ver caixa
        [HttpGet]
        public ActionResult<List<ItemCaixa>> Get()
        {
            return _caixa.Produtos;
        }

        //adicionar produto
        [HttpPost("adicionar/{id}")]
        public async Task<ActionResult> AdicionarProduto(int id, int quantidade)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            //encontrar qual item tem esse produto
            var verificarItemCaixa = _caixa.Produtos.Find(item => item.Produto.ProdutoId == id);
            if (verificarItemCaixa != null)
            {
                verificarItemCaixa.Quantidade += quantidade;
                return Ok(verificarItemCaixa);
            }


            else
            {
                var itemCaixa = new ItemCaixa
                {
                    Produto = produto,
                    Quantidade = quantidade
                };
                _caixa.AdicionarProduto(itemCaixa);
                return Ok(itemCaixa);
            }
        }

        //remover produto
        [HttpPost("remover/{id}")]
        public async Task<ActionResult> RemoverProduto(int id, int quantidade)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            //encontrar qual item tem essse produto
            var itemCaixa = _caixa.Produtos.Find(item => item.Produto.ProdutoId == id);
            itemCaixa.Quantidade -= quantidade;

            if (itemCaixa.Quantidade == 0)
            {
                _caixa.RemoverProduto(itemCaixa);
            }
            return Ok();
        }

        //calcular total
        [HttpGet("total")]
        public ActionResult<double> CalcularTotal()
        {
            _caixa.CalcularTotal();
            return _caixa.Total;
        }

       //realizar venda
       //assincrono
        [HttpPost("venda")]
        public async Task<ActionResult> RealizarVenda()
        {
            _context.Produtos.ToList();
             _caixa.RealizarVenda(_context.ItemDeposito.ToList(), _caixa.Produtos.ToList());
             _caixa.Produtos.Clear();
            await _context.SaveChangesAsync();

            return Ok(_context.ItemDeposito.ToList());
        }
       

       
    }
}