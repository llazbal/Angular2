using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBanking.DAL.DataServices;
using EBanking.Entities;
using EBanking.Common;

namespace EBanking.Business
{
    public class CuentaBusinessService : EBanking.Interfaces.ICuentaService
    {
        /// <summary>
        /// Cuentas by Usuario
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns>List<Cuenta> object</returns>
        public List<Cuenta> GetCuentasByUsuario(int usuarioID)
        {
            ICuentaDataService iCuentaDataService = new CuentaDataService();
            return iCuentaDataService.GetCuentasByUsuario(usuarioID);
        }

        /// <summary>
        /// Cuenta by Cuenta
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <returns>Cuenta object</returns>
        public Cuenta GetCuentaByCuentaID(int cuentaID)
        {
            ICuentaDataService iCuentaDataService = new CuentaDataService();
            return iCuentaDataService.GetCuentaByCuentaID(cuentaID);
        }
        
        /// <summary>
        /// Cuenta by Cuenta
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <returns>Cuenta object</returns>
        public List<Cuenta> GetCuentasByUsuario(int usuarioID, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<Cuenta> cuentas = new List<Cuenta>();

            try
            {
                ICuentaDataService iCuentaDataService = new CuentaDataService();
                cuentas = iCuentaDataService.GetCuentasByUsuario(usuarioID);

                transaction.TotalPages = Utilities.CalculateTotalPages(cuentas.Count, pageSize);
                transaction.TotalRows = cuentas.Count;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }


            return cuentas;
        }

        /// <summary>
        /// Cuenta by Cuenta
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <returns>Cuenta object</returns>
        public Cuenta GetCuentaByCuentaID(int cuentaID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            Cuenta cuenta = new Cuenta();

            try
            {
                ICuentaDataService iCuentaDataService = new CuentaDataService();
                cuenta = iCuentaDataService.GetCuentaByCuentaID(cuentaID);
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            
            return cuenta;
        }
    }
}
