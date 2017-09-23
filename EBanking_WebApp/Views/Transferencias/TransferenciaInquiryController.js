
console.log("transferencia inquiry");

angular.module('eBanking_WebAPP').register.controller('transferenciaInquiryController', ['$routeParams', '$location', 'ajaxService', 'dataGridService', 'alertService',
    function ($routeParams, $location, ajaxService, dataGridService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            vm.title = "Listado de Transferencias";

            vm.alerts = [];
            vm.closeAlert = alertService.closeAlert;

            dataGridService.initializeTableHeaders();

            dataGridService.addHeader("Transferencia ID", "TransferenciaID");
            dataGridService.addHeader("Cuenta origen", "CuentaIdOrigen");
            dataGridService.addHeader("Cuenta destino", "CuentaIdDestino");
            dataGridService.addHeader("Fecha", "Fecha");
            dataGridService.addHeader("Monto", "Monto");
            dataGridService.addHeader("Descripción", "Descripcion");

            vm.tableHeaders = dataGridService.setTableHeaders();
            vm.defaultSort = dataGridService.setDefaultSort("Transferencia ID");

            vm.currentPageNumber = 1;
            vm.sortExpression = "TransferenciaID";
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
            ajaxService.ajaxPost(inquiry, "api/TransferenciaService/GetTransferenciasByUsuario", this.getTransferenciasOnSuccess, this.getTransferenciasOnError);
        }

        this.prepareSearch = function () {

            var inquiry = new Object();

            inquiry.currentPageNumber = vm.currentPageNumber;
            inquiry.sortExpression = vm.sortExpression;
            inquiry.sortDirection = vm.sortDirection;
            inquiry.pageSize = vm.pageSize;

            return inquiry;

        }

        this.getTransferenciasOnSuccess = function (response) {
            vm.transferencias = response.transferencias;
            vm.totalTransferencias = response.totalRows;
            vm.totalPages = response.totalPages;
        }

        this.getTransferenciasOnError = function (response) {
            alertService.RenderErrorMessage(response.ReturnMessage);
        }


        this.newTransferncia = function () {

            window.location.href = 'Transferencias/TransferenciaMaintenance';

        }


    }]);