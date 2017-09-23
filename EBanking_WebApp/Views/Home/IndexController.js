
console.log("index");

angular.module('eBanking_WebAPP').register.controller('indexController',
    ['$routeParams', '$location', function ($routeParams, $location) {

    "use strict";

    var vm = this;

    this.initializeController = function () {
        vm.title = "Index Page";
    }

}]);


