
console.log("cuenta inquiry");

angular.module('eBanking_WebAPP').register.controller('cuentaInquiryController', ['$routeParams', '$location', 'ajaxService', 'dataGridService', 'alertService',
    function ($routeParams, $location, ajaxService, dataGridService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            vm.title = "Listado de Cuentas";

            vm.alerts = [];
            vm.closeAlert = alertService.closeAlert;

            dataGridService.initializeTableHeaders();

            dataGridService.addHeader("Cuenta ID", "CuentaID");
            dataGridService.addHeader("Tipo cuenta", "tipoCuenta.nombre");
            dataGridService.addHeader("Saldo", "Saldo");
            dataGridService.addHeader("Usuario ID", "UsuarioID");

            vm.tableHeaders = dataGridService.setTableHeaders();
            vm.defaultSort = dataGridService.setDefaultSort("Cuenta ID");

            vm.currentPageNumber = 1;
            vm.sortExpression = "CuentaID";
            vm.sortDirection = "ASC";
            vm.pageSize = 15;

            this.executeInquiry();

        }

        this.closeAlert = function (index) {
            vm.alerts.splice(index, 1);
        };

        this.changeSorting = function (column) {

            dataGridService.changeSorting(column, vm.defaultSort, vm.tableHeaders);

            vm.defaultSort = dataGridService.getSort();
            vm.sortDirection = dataGridService.getSortDirection();
            vm.sortExpression = dataGridService.getSortExpression();
            vm.currentPageNumber = 1;

            vm.executeInquiry();

        };

        this.setSortIndicator = function (column) {
            return dataGridService.setSortIndicator(column, vm.defaultSort);
        };

        this.pageChanged = function () {
            this.executeInquiry();
        }

        this.executeInquiry = function () {
            var inquiry = vm.prepareSearch();
            ajaxService.ajaxPost(inquiry, "api/CuentaService/GetCuentas", this.getCuentasOnSuccess, this.getCuentasOnError);
        }

        this.prepareSearch = function () {

            var inquiry = new Object();

            inquiry.currentPageNumber = vm.currentPageNumber;
            inquiry.sortExpression = vm.sortExpression;
            inquiry.sortDirection = vm.sortDirection;
            inquiry.pageSize = vm.pageSize;

            return inquiry;

        }

        this.getCuentasOnSuccess = function (response) {
            vm.cuentas = response.cuentas;
            vm.totalCuentas = response.totalRows;
            vm.totalPages = response.totalPages;
        }

        this.getCuentasOnError = function (response) {
            alertService.RenderErrorMessage(response.ReturnMessage);
        }


    }]);