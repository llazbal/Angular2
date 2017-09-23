using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBanking.Entities;

namespace EBanking.DAL.DataServices
{
    public class CuentaDataService : ICuentaDataService
    {
        /// <summary>
        /// Cuentas by Usuario
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns>List<Cuenta> object</returns>
        public List<Entities.Cuenta> GetCuentasByUsuario(int usuarioID)
        {
            try
            {
                using (EBankingDataDataContext db = GenericDataService.DataContext)
                {
                    List<Entities.Cuenta> list = new List<Entities.Cuenta>();
                    var results = db.Cuentas_SEL_ALL_BYUSER(usuarioID);

                    foreach (var c in results)
                    {
                        Entities.Cuenta cuenta = new Entities.Cuenta();
                        cuenta.CuentaID = c.CuentaId;
                        cuenta.Saldo = c.Saldo;
                        cuenta.UsuarioID = c.UsuarioId;
                        cuenta.TipoCuenta = new Entities.TipoCuenta();
                        cuenta.TipoCuenta.TipoCuentaID = c.TipoCuentaId;
                        cuenta.TipoCuenta.Nombre = c.TipoCuenta;

                        list.Add(cuenta);
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Cuenta by Cuenta
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <returns>Cuenta object</returns>
        public Entities.Cuenta GetCuentaByCuentaID(int cuentaID)
        {
            try
            {
                using (EBankingDataDataContext db = GenericDataService.DataContext)
                {
                    Entities.Cuenta cuenta = null;

                    var results = db.Cuenta_SEL_ALL_BYCuentaID(cuentaID);
                    foreach (var c in results)
                    {
                        cuenta = new Entities.Cuenta();
                        cuenta.CuentaID = c.CuentaId;
                        cuenta.Saldo = c.Saldo;
                        cuenta.UsuarioID = c.UsuarioId;
                        cuenta.TipoCuenta = new Entities.TipoCuenta();
                        cuenta.TipoCuenta.TipoCuentaID = c.TipoCuentaId;
                        cuenta.TipoCuenta.Nombre = c.TipoCuenta;
                    }
                    return cuenta;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
