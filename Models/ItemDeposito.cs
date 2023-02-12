using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace supermercadoAPI.Models
{
    public class ItemDeposito
    {
        public int ItemDepositoId {get; set;}
        public int Quantidade {get; set;}
        public Produto Produto {get; set;}

        
        
    }
}