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
        public ActionResult<List<Produto>> Get()
        {
            return _caixa.Produtos;
        }

        //adicionar produto
        [HttpPost("adicionar/{id}")]
        public async Task<ActionResult> AdicionarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            _caixa.AdicionarProduto(produto);
            return Ok(_caixa.Produtos);
        }

        //remover produto
        [HttpPost("remover/{id}")]
        public async Task<ActionResult> RemoverProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            _caixa.RemoverProduto(produto);
            return Ok();
        }

        //calcular total
        [HttpGet("total")]
        public ActionResult<double> CalcularTotal()
        {
            _caixa.CalcularTotal();
            return _caixa.Total;
        }
        

    }
}