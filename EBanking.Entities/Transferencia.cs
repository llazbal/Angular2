using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Entities
{
    public class Transferencia
    {
        public int TransferenciaID { get; set; }
        public int CuentaIdOrigen { get; set; }
        public int CuentaIdDestino { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
