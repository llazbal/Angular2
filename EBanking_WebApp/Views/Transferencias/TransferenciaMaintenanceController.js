
console.log("transferencia maintenance");

angular.module('eBanking_WebAPP').register.controller('transferenciaMaintenanceController',
    ['$routeParams', '$location', 'ajaxService', 'alertService',
    function ($routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {
            vm.title = "Nueva transferencia de fondos";

            vm.messageBox = "";
            vm.alerts = [];

            var cuentaID = ($routeParams.id || "");

            vm.cuentaIdDestino = '0';
            vm.cuentaIdOrigen = '0';
            vm.descripcion = '';
            vm.monto = '0';
            vm.transferenciaID = '0';

            vm.cuentaID = cuentaID;
            var cuenta = new Object();
            cuenta.cuentaID = 1;//cuentaID
            ajaxService.ajaxPost(cuenta, "api/CuentaService/GetCuentas", this.getCuentasOnSuccess, this.getCuentasOnError);
        }

        this.closeAlert = function (index) {
            vm.alerts.splice(index, 1);
        };

        this.getCuentas = function () {
            var inquiry = vm.prepareSearch();
            ajaxService.ajaxPost(inquiry, "api/CuentaService/GetCuentas", this.getCuentasOnSuccess, this.getCuentasOnError);
        }

        this.getCuentasOnSuccess = function (response) {
            vm.cuentas = response.cuentas;
        }

        this.getCuentasOnError = function (response) {

        }

        this.saveTransferencia = function () {
            var transferencia = new Object();
            transferencia.cuentaIdDestino = vm.cuentaIdDestino;
            transferencia.cuentaIdOrigen = vm.cuentaIdSel;//vm.cuentaIdOrigen;
            transferencia.descripcion = vm.descripcion;
            transferencia.fecha = vm.fecha;
            transferencia.monto = vm.monto;
            transferencia.transferenciaID = vm.transferenciaID;

            ajaxService.ajaxPost(transferencia, "api/TransferenciaService/CreateTransferencia", this.createTransferenciaOnSuccess, this.createTransferenciaOnError);

        }

        this.createTransferenciaOnSuccess = function (response) {
            vm.clearValidationErrors();
            alertService.renderSuccessMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            vm.cuentaID = response.cuentaID;

            vm.cuentaIdDestino = '0';
            vm.cuentaIdOrigen = '0';
            vm.descripcion = '';
            vm.monto = '0';
        }

        this.createTransferenciaOnError = function (response) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

        this.clearValidationErrors = function () {
            vm.cuentaCodeInputError = false;
        }

        this.showError = function (strError) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(strError);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

    }]);
