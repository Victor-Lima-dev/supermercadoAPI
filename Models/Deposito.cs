using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace supermercadoAPI.Models
{
    public class Deposito
    {
        public int DepositoId { get; set; }
        public List<ItemDeposito> Itens { get; set; }

        public Deposito()
        {
            Itens = new List<ItemDeposito>();
        }

        
    }
}