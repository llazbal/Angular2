using EBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Interfaces
{
    public interface ITransferenciaService
    {
        /// <summary>
        /// Save Transferencia
        /// </summary>
        /// <param name="transferencia"></param>
        /// <param name="transaction"></param>
        void SaveTransferencia(Transferencia transferencia, out TransactionalInformation transaction);

        /// <summary>
        /// Transferencias by UsuarioID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns>List<Transferencia> object</returns>
        List<Entities.Transferencia> GetTransferenciasByUsuario(int usuarioID, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction);

        /// <summary>
        /// Transferencia by transferenciaID
        /// </summary>
        /// <param name="transferenciaID"></param>
        /// <returns>Transferencia object</returns>
        Entities.Transferencia GetTransferenciaByTransferenciaID(int transferenciaID, out TransactionalInformation transaction);

        /// <summary>
        /// Transferencia by cuentaIdOrigen
        /// </summary>
        /// <param name="TransferenciaID"></param>
        /// <returns>List<Entities.Transferencia> object</returns>
        List<Entities.Transferencia> GetTransferenciasByCuentaIdOrigen(int cuentaIdOrigen, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction);
    }
}
