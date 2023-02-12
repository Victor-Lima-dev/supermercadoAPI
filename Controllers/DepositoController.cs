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
            _deposito.Itens = _context.ItemDeposito.ToList();
            return _deposito.Itens;
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
            //instanciar itemDeposito
            var depositoItem = new ItemDeposito
                {
                    Produto = produto,
                    Quantidade = quantidade
                };


            _context.ItemDeposito.Add(depositoItem);
            await _context.SaveChangesAsync();
           _deposito.Itens = _context.ItemDeposito.ToList();
            return Ok(_deposito.Itens);

            
        }
    
    }

}