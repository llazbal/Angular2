
console.log("cuenta maintenance");

angular.module('eBanking_WebAPP').register.controller('cuentaMaintenanceController',
    ['$routeParams', '$location', 'ajaxService', 'alertService',
    function ($routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            vm.title = "Datos de cuenta";

            vm.messageBox = "";
            vm.alerts = [];

            var cuentaID = ($routeParams.id || "");
            vm.tipoC = '';

            if (cuentaID == "") {
                vm.cuentaID = "0";
                vm.tipoCuenta = "";
                vm.saldo = "0";
                vm.usuarioID = "0";
            }
            else {
                vm.cuentaID = cuentaID;
                var cuenta = new Object();
                cuenta.cuentaID = cuentaID
                ajaxService.ajaxPost(cuenta, "api/CuentaService/GetCuenta", this.getCuentaOnSuccess, this.getCuentaOnError);
            }

        }

        this.closeAlert = function (index) {
            vm.alerts.splice(index, 1);
        };

        this.getCuentaOnSuccess = function (response) {

            vm.cuentaID = response.cuentaID;
            vm.tipoCuenta = response.tipoCuenta.nombre;
            vm.Saldo = response.saldo;
            vm.UsuarioID = response.usuarioID;
        }

        this.getCuentaOnError = function (response) {

        }


        this.saveCuenta = function () {

            var cuenta = new Object();
            cuenta.cuentaID = vm.cuentaID
            cuenta.TipoCuenta = vm.TipoCuenta;
            cuenta.Saldo = vm.Saldo;
            cuenta.UsuarioID = vm.UsuarioID;

            if (cuenta.cuentaID == "0") {
                ajaxService.ajaxPost(cuenta, "api/CuentaService/CreateCuenta", this.createCuentaOnSuccess, this.createCuentaOnError);
            }
            else {
                ajaxService.ajaxPost(cuenta, "api/CuentaService/UpdateCuenta", this.updateCuentaOnSuccess, this.updateCuentaOnError);
            }

        }

        this.createCuentaOnSuccess = function (response) {
            vm.clearValidationErrors();
            alertService.renderSuccessMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            vm.cuentaID = response.cuentaID;
        }

        this.createCuentaOnError = function (response) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

        this.updateCuentaOnSuccess = function (response) {
            vm.clearValidationErrors();
            alertService.renderSuccessMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
        }

        this.updateCuentaOnError = function (response) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

        this.clearValidationErrors = function () {
            vm.cuentaCodeInputError = false;
        }

        this.cuentaInquiry = function () {

            window.location.href = 'Cuentas/CuentaInquiry';

        }

    }]);
