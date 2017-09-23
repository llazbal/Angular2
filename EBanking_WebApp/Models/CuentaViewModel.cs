using EBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBanking_WebApp.Models
{
    /// <summary>
    /// Cuenta View Model
    /// </summary>
    public class CuentaViewModel : TransactionalInformation
    {
        public int CuentaID { get; set; }
        public decimal Saldo { get; set; }
        public int UsuarioID { get; set; }
        public TipoCuenta TipoCuenta { get; set; }

        public List<Cuenta> Cuentas { get; set; }
    }
}