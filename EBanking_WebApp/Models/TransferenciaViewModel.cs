using EBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBanking_WebApp.Models
{
    /// <summary>
    /// Transferencia View Model
    /// </summary>
    public class TransferenciaViewModel : TransactionalInformation
    {
        public int TransferenciaID { get; set; }
        public int CuentaIdOrigen { get; set; }
        public int CuentaIdDestino { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public List<Transferencia> Transferencias { get; set; }
    }
}