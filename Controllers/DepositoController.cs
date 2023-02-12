using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supermercadoAPI.context;
using supermercadoAPI.Models;

namespace supermercadoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepositoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Deposito _deposito;
        private readonly ItemDeposito _itemDeposito;


        public DepositoController(AppDbContext context, Deposito deposito, ItemDeposito itemDeposito)
        {
            _context = context;
            _deposito = deposito;
            _itemDeposito = itemDeposito;
        }


        //ver deposito
        [HttpGet]
        public ActionResult<List<ItemDeposito>> Get()
        {
            _context.Produtos.ToList();
            return _context.ItemDeposito.ToList();
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
            var verificarItemDeposito = _context.ItemDeposito.FirstOrDefault(item => item.Produto.ProdutoId == id);
            if (verificarItemDeposito != null)
            {
                verificarItemDeposito.Quantidade += quantidade;
                 await _context.SaveChangesAsync();
                return Ok(verificarItemDeposito);
            }

            else
            {
                //criar item deposito
                _itemDeposito.Produto = produto;
                _itemDeposito.Quantidade = quantidade;

                _context.ItemDeposito.Add(_itemDeposito);
                await _context.SaveChangesAsync();
                _deposito.Itens = _context.ItemDeposito.ToList();
                return Ok(_deposito.Itens);
            }

        }

        //remover produto
        [HttpDelete("remover/{id}")]
        public async Task<ActionResult> RemoverProduto(int id, int quantidade)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound("Nenhum item com esse produto encontrado");
            }
            var itemDeposito = await _context.ItemDeposito.FirstOrDefaultAsync(item => item.Produto.ProdutoId == id);
           
            if (itemDeposito.Quantidade > quantidade)
            {
                itemDeposito.Quantidade -= quantidade;
                await _context.SaveChangesAsync();
                return Ok(itemDeposito);
            }

            else if (itemDeposito.Quantidade == quantidade)
            {
                _context.ItemDeposito.Remove(itemDeposito);
                await _context.SaveChangesAsync();
                _deposito.Itens = _context.ItemDeposito.ToList();
                return Ok(_deposito.Itens);
            }

            else
            {
                return BadRequest("Não pode remover mais do que tem no deposito");
            }

            
        }

        //limpa o deposito
        [HttpDelete("limpar")]
        public async Task<ActionResult> LimparDeposito()
        {
            _context.ItemDeposito.RemoveRange(_context.ItemDeposito);
            await _context.SaveChangesAsync();
            _deposito.Itens = _context.ItemDeposito.ToList();
            return Ok(_deposito.Itens);
        }
    }

}