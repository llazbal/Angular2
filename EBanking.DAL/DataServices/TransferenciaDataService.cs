using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBanking.Entities;
using EBanking.Exceptions;

namespace EBanking.DAL.DataServices
{
    public class TransferenciaDataService : ITransferenciaDataService
    {
        private Transferencia SetTransferenciaObject(Entities.Transferencia transferencia)
        {
            Transferencia t = new Transferencia();
            t.CuentaIdDestino = transferencia.CuentaIdDestino;
            t.CuentaIdOrigen = transferencia.CuentaIdOrigen;
            t.Descripcion = transferencia.Descripcion;
            t.Fecha = transferencia.Fecha;
            t.Monto = transferencia.Monto;
            t.TransferenciaId = transferencia.TransferenciaID;
            return t;
        }

        /// <summary>
        /// Save Transferencia
        /// </summary>
        /// <param name="transferencia"></param>
        public void SaveTransferencia(EBanking.Entities.Transferencia transferencia)
        {
            try
            {
                using (EBankingDataDataContext db = GenericDataService.DataContext)
                {
                    //db.Transferencias.InsertOnSubmit(SetTransferenciaObject(transferencia));
                    //db.SubmitChanges();
                    var results = db.Transferencias_INS_NEW(transferencia.CuentaIdOrigen, transferencia.CuentaIdDestino, transferencia.Monto, transferencia.Descripcion, transferencia.Fecha);
                    if (results > 0)
                    {
                        transferencia.TransferenciaID = results;
                    }
                    else
                    {
                        if (results == -100)
                        {
                            throw new TransactionException(TransactionException.Errors_Transaction.ERR_TRANSACTION_100_SAVE);
                        }else if (results == -101)
                        {
                            throw new TransactionException(TransactionException.Errors_Transaction.ERR_TRANSACTION_101_UPDATE);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Transferencias by Usuario
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns>List<Transferencia> object</returns>
        public Entities.Transferencia GetTransferenciaByTransferenciaID(int transferenciaID)
        {
            try
            {
                Entities.Transferencia transferencia = null;
                using (EBankingDataDataContext db = GenericDataService.DataContext)
                {
                    var results = db.Transferencias_SEL_ALL_BYTransferenciaID(transferenciaID);

                    foreach (var c in results)
                    {
                        transferencia = new Entities.Transferencia();
                        transferencia.CuentaIdDestino = c.CuentaIdDestino;
                        transferencia.CuentaIdOrigen = c.CuentaIdOrigen;
                        transferencia.Descripcion = c.Descripcion;
                        transferencia.Fecha = c.Fecha;
                        transferencia.Monto = c.Monto;
                        transferencia.TransferenciaID = c.TransferenciaId;
                    }
                    return transferencia;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Transferencias by Usuario
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns>List<Transferencia> object</returns>
        public List<Entities.Transferencia> GetTransferenciasByUsuario(int usuarioID)
        {
            try
            {
                List<Entities.Transferencia> list = new List<Entities.Transferencia>();
                using (EBankingDataDataContext db = GenericDataService.DataContext)
                {
                    var results = db.Transferencias_SEL_ALL_BYUsuarioId(usuarioID);

                    foreach (var c in results)
                    {
                        Entities.Transferencia transferencia = new Entities.Transferencia();
                        transferencia.CuentaIdDestino = c.CuentaIdDestino;
                        transferencia.CuentaIdOrigen = c.CuentaIdOrigen;
                        transferencia.Descripcion = c.Descripcion;
                        transferencia.Fecha = c.Fecha;
                        transferencia.Monto = c.Monto;
                        transferencia.TransferenciaID = c.TransferenciaId;

                        list.Add(transferencia);
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
        /// Transferencia by Transferencia
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <returns>Transferencia object</returns>
        public List<Entities.Transferencia> GetTransferenciasByCuentaIdOrigen(int cuentaIdOrigen)
        {
            try
            {
                List<Entities.Transferencia> list = new List<Entities.Transferencia>();
                using (EBankingDataDataContext db = GenericDataService.DataContext)
                {
                    Entities.Transferencia transferencia = null;

                    var results = db.Transferencias_SEL_ALL_BYCuentaIdOrigen(cuentaIdOrigen);
                    foreach (var c in results)
                    {
                        transferencia = new Entities.Transferencia();
                        transferencia.CuentaIdDestino = c.CuentaIdDestino;
                        transferencia.CuentaIdOrigen = c.CuentaIdOrigen;
                        transferencia.Descripcion = c.Descripcion;
                        transferencia.Fecha = c.Fecha;
                        transferencia.Monto = c.Monto;
                        transferencia.TransferenciaID = c.TransferenciaId;

                        list.Add(transferencia);
                    }
                    return list;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
