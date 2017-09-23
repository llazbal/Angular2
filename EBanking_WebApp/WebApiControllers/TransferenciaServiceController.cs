using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EBanking.Interfaces;
using System.Net.Http;
using EBanking_WebApp.Models;
using System.Net;
using EBanking.Entities;
using EBanking.Business;

namespace EBanking_WebApp.WebApiControllers
{
    [RoutePrefix("api/TransferenciaService")]
    public class TransferenciaServiceController : ApiController
    {
        [Inject]
        public ITransferenciaService _transferenciaService { get; set; }

        /// <summary>
        /// Get Transferencias
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transferenciaViewModel"></param>
        /// <returns></returns>
        [Route("CreateTransferencia")]
        [HttpPost]
        public HttpResponseMessage CreateTransferencia(HttpRequestMessage request, [FromBody] TransferenciaViewModel transferenciaViewModel)
        {
            TransactionalInformation transaction;

            _transferenciaService = new TransferenciaBusinessService();

            Transferencia transferencia = new Transferencia();
            transferencia.CuentaIdDestino = transferenciaViewModel.CuentaIdDestino;
            transferencia.CuentaIdOrigen = transferenciaViewModel.CuentaIdOrigen;
            transferencia.Descripcion = transferenciaViewModel.Descripcion;
            transferencia.Fecha = DateTime.Now;// transferenciaViewModel.Fecha;
            transferencia.Monto = transferenciaViewModel.Monto;
            //transferencia.TransferenciaID = transferenciaViewModel.TransferenciaID;

            _transferenciaService.SaveTransferencia(transferencia, out transaction);
            if (transaction.ReturnStatus == false)
            {
                transferenciaViewModel.ReturnStatus = false;
                transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;
                transferenciaViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.BadRequest, transferenciaViewModel);
                return responseError;

            }

            transferenciaViewModel.ReturnStatus = true;
            transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.OK, transferenciaViewModel);
            return response;

        }

        /// <summary>
        /// Get Transferencias
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transferenciaViewModel"></param>
        /// <returns></returns>
        [Route("GetTransferenciasByUsuario")]
        [HttpPost]
        public HttpResponseMessage GetTransferenciasByUsuario(HttpRequestMessage request, [FromBody] TransferenciaViewModel transferenciaViewModel)
        {
            TransactionalInformation transaction;

            int currentPageNumber = transferenciaViewModel.CurrentPageNumber;
            int pageSize = transferenciaViewModel.PageSize;
            string sortExpression = transferenciaViewModel.SortExpression;
            string sortDirection = transferenciaViewModel.SortDirection;

            _transferenciaService = new TransferenciaBusinessService();

            List<Transferencia> transferencias = _transferenciaService.GetTransferenciasByUsuario(1, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                transferenciaViewModel.ReturnStatus = false;
                transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;
                transferenciaViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.BadRequest, transferenciaViewModel);
                return responseError;

            }

            transferenciaViewModel.TotalPages = transaction.TotalPages;
            transferenciaViewModel.TotalRows = transaction.TotalRows;
            transferenciaViewModel.Transferencias = transferencias;
            transferenciaViewModel.ReturnStatus = true;
            transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.OK, transferenciaViewModel);
            return response;

        }

        /// <summary>
        /// Get Transferencia 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="TransferenciaViewModel"></param>
        /// <returns></returns>
        [Route("GetTransferencia")]
        [HttpPost]
        public HttpResponseMessage GetTransferencia(HttpRequestMessage request, [FromBody] TransferenciaViewModel transferenciaViewModel)
        {

            TransactionalInformation transaction;

            int cuentaIdOrigen = transferenciaViewModel.CuentaIdOrigen;

            _transferenciaService = new TransferenciaBusinessService();
            Transferencia transferencia = _transferenciaService.GetTransferenciaByTransferenciaID(cuentaIdOrigen, out transaction);
            if (transaction.ReturnStatus == false)
            {
                transferenciaViewModel.ReturnStatus = false;
                transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;
                transferenciaViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.BadRequest, transferenciaViewModel);
                return responseError;

            }

            transferenciaViewModel.CuentaIdDestino = transferencia.CuentaIdDestino;
            transferenciaViewModel.CuentaIdOrigen = transferencia.CuentaIdOrigen;
            transferenciaViewModel.Descripcion = transferencia.Descripcion;
            transferenciaViewModel.Fecha = transferencia.Fecha;
            transferenciaViewModel.Monto = transferencia.Monto;
            transferenciaViewModel.TransferenciaID = transferencia.TransferenciaID;

            transferenciaViewModel.ReturnStatus = true;
            transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.OK, transferenciaViewModel);
            return response;

        }

        /// <summary>
        /// Get Transferencias
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transferenciaViewModel"></param>
        /// <returns></returns>
        [Route("GetTransferenciasByCuentaIdOrigen")]
        [HttpPost]
        public HttpResponseMessage GetTransferenciasByCuentaIdOrigen(HttpRequestMessage request, [FromBody] TransferenciaViewModel transferenciaViewModel)
        {
            TransactionalInformation transaction;

            int currentPageNumber = transferenciaViewModel.CurrentPageNumber;
            int pageSize = transferenciaViewModel.PageSize;
            string sortExpression = transferenciaViewModel.SortExpression;
            string sortDirection = transferenciaViewModel.SortDirection;

            _transferenciaService = new TransferenciaBusinessService();

            List<Transferencia> transferencias = _transferenciaService.GetTransferenciasByCuentaIdOrigen(1, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                transferenciaViewModel.ReturnStatus = false;
                transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;
                transferenciaViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.BadRequest, transferenciaViewModel);
                return responseError;

            }

            transferenciaViewModel.TotalPages = transaction.TotalPages;
            transferenciaViewModel.TotalRows = transaction.TotalRows;
            transferenciaViewModel.Transferencias = transferencias;
            transferenciaViewModel.ReturnStatus = true;
            transferenciaViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<TransferenciaViewModel>(HttpStatusCode.OK, transferenciaViewModel);
            return response;

        }
    }
}