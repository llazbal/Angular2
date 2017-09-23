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
    [RoutePrefix("api/CuentaService")]
    public class CuentaServiceController : ApiController
    {

        [Inject]
        public ICuentaService _cuentaService { get; set; }

        /// <summary>
        /// Get Cuentas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cuentaViewModel"></param>
        /// <returns></returns>
        [Route("GetCuentas")]
        [HttpPost]
        public HttpResponseMessage GetCuentas(HttpRequestMessage request, [FromBody] CuentaViewModel cuentaViewModel)
        {
            TransactionalInformation transaction;
            
            int currentPageNumber = cuentaViewModel.CurrentPageNumber;
            if (currentPageNumber == 0) currentPageNumber = 1;
            int pageSize = cuentaViewModel.PageSize;
            if (pageSize == 0) pageSize = 1;
            string sortExpression = cuentaViewModel.SortExpression;
            string sortDirection = cuentaViewModel.SortDirection;

            _cuentaService = new CuentaBusinessService();

            List<Cuenta> cuentas = _cuentaService.GetCuentasByUsuario(1, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                cuentaViewModel.ReturnStatus = false;
                cuentaViewModel.ReturnMessage = transaction.ReturnMessage;
                cuentaViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<CuentaViewModel>(HttpStatusCode.BadRequest, cuentaViewModel);
                return responseError;

            }

            cuentaViewModel.TotalPages = transaction.TotalPages;
            cuentaViewModel.TotalRows = transaction.TotalRows;
            cuentaViewModel.Cuentas = cuentas;
            cuentaViewModel.ReturnStatus = true;
            cuentaViewModel.ReturnMessage = transaction.ReturnMessage;
            
            var response = Request.CreateResponse<CuentaViewModel>(HttpStatusCode.OK, cuentaViewModel);
            return response;

        }

        /// <summary>
        /// Get Cuenta
        /// </summary>
        /// <param name="request"></param>
        /// <param name="CuentaViewModel"></param>
        /// <returns></returns>
        [Route("GetCuenta")]
        [HttpPost]
        public HttpResponseMessage GetCuenta(HttpRequestMessage request, [FromBody] CuentaViewModel cuentaViewModel)
        {

            TransactionalInformation transaction;

            int cuentaID = cuentaViewModel.CuentaID;

            _cuentaService = new CuentaBusinessService();
            Cuenta cuenta = _cuentaService.GetCuentaByCuentaID(cuentaID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                cuentaViewModel.ReturnStatus = false;
                cuentaViewModel.ReturnMessage = transaction.ReturnMessage;
                cuentaViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<CuentaViewModel>(HttpStatusCode.BadRequest, cuentaViewModel);
                return responseError;

            }

            cuentaViewModel.CuentaID = cuenta.CuentaID;
            cuentaViewModel.Saldo = cuenta.Saldo;
            cuentaViewModel.TipoCuenta = cuenta.TipoCuenta;
            cuentaViewModel.UsuarioID = cuenta.UsuarioID;

            cuentaViewModel.ReturnStatus = true;
            cuentaViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<CuentaViewModel>(HttpStatusCode.OK, cuentaViewModel);
            return response;

        }        
    }
}