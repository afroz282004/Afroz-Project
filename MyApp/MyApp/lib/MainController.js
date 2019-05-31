var app = angular.module('MyMainApp', []);
app.controller('MyMainController', ['$scope', '$http', function ($scope, $http) {
   
    $http.get("/home/Get_Data").then(function (d) {
        $scope.record = d.data;
    }, function () {
        alert('Failed');
    });
}]);