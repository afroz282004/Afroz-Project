//angular.module("MyApp").register.controller('HomeAngularJS', ['$routeParams', '$location', function ($routeParams, $location) {

//    //"use strict";

//    var vm = this;

//    this.initializeController = function () {
//        vm.title = "Home Page";
//    };

//}]);


var m = angular.module('MyApp1', []);
alert('Daishboard.Js');
m.controller("menuController1", ['$scope', '$http', function ($scope, $http) {
    alert('HHCC');
    $http.get("/home/Get_Data").then(function (d) {
        $scope.record = d.data;
    }, function () {
        alert('Failed');
    });
}]);
