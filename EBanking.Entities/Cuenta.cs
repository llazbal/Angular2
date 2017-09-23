using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Entities
{
    public class Cuenta
    {
        public int CuentaID { get; set; }
        public decimal Saldo { get; set; }
        public int UsuarioID { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
    }
}
