using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosUniversity.Models
{
    public partial class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get;set; }
    }
}
