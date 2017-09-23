using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBanking.DAL.DataServices;
using EBanking.Entities;
using EBanking.Common;
using FluentValidation.Results;

namespace EBanking.Business
{
    public class TransferenciaBusinessService : EBanking.Interfaces.ITransferenciaService
    {
        /// <summary>
        /// Save Transferencia
        /// </summary>
        /// <param name="transferencia"></param>
        /// <param name="transaction"></param>
        public void SaveTransferencia(Transferencia transferencia, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                TransferenciaBusinessRules transferenciaBusinessRules = new TransferenciaBusinessRules(transferencia);
                ValidationResult results = transferenciaBusinessRules.Validate(transferencia);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                }
                else
                {
                    ITransferenciaDataService iTransferenciaDataService = new TransferenciaDataService();
                    iTransferenciaDataService.SaveTransferencia(transferencia);

                    transaction.ReturnMessage.Add("Transferencia realizada con éxito.");
                    transaction.ReturnStatus = true;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
        }

        /// <summary>
        /// Transferencias by UsuarioID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns>List<Transferencia> object</returns>
        public List<Transferencia> GetTransferenciasByUsuario(int usuarioID, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<Transferencia> Transferencias = new List<Transferencia>();

            try
            {
                ITransferenciaDataService iTransferenciaDataService = new TransferenciaDataService();
                Transferencias = iTransferenciaDataService.GetTransferenciasByUsuario(usuarioID);

                transaction.TotalPages = Utilities.CalculateTotalPages(Transferencias.Count, pageSize);
                transaction.TotalRows = Transferencias.Count;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }


            return Transferencias;
        }

        /// <summary>
        /// Transferencia by transferenciaID
        /// </summary>
        /// <param name="transferenciaID"></param>
        /// <returns>Transferencia object</returns>
        public Transferencia GetTransferenciaByTransferenciaID(int transferenciaID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            Transferencia Transferencia = new Transferencia();

            try
            {
                ITransferenciaDataService iTransferenciaDataService = new TransferenciaDataService();
                Transferencia = iTransferenciaDataService.GetTransferenciaByTransferenciaID(transferenciaID);
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }

            return Transferencia;
        }

        /// <summary>
        /// Transferencia by cuentaIdOrigen
        /// </summary>
        /// <param name="TransferenciaID"></param>
        /// <returns>List<Entities.Transferencia> object</returns>
        public List<Entities.Transferencia> GetTransferenciasByCuentaIdOrigen(int cuentaIdOrigen, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<Entities.Transferencia> transferencias = new List<Entities.Transferencia>();

            try
            {
                ITransferenciaDataService iTransferenciaDataService = new TransferenciaDataService();
                transferencias = iTransferenciaDataService.GetTransferenciasByCuentaIdOrigen(cuentaIdOrigen);

                transaction.TotalPages = Utilities.CalculateTotalPages(transferencias.Count, pageSize);
                transaction.TotalRows = transferencias.Count;

                transaction.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }

            return transferencias;
        }
    }
}
