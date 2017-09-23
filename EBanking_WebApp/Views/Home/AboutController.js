
console.log("about");

angular.module('eBanking_WebAPP').register.controller('aboutController', ['$routeParams', '$location'
    , function ($routeParams, $location) {

    "use strict";

    var vm = this;

    this.initializeController = function () {
        vm.title = "Acerca de ...";
    }

}]);
