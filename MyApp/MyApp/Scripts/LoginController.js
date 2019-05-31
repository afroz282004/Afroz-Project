
var myapp = angular.module("MyLogin", []);
myapp.controller("MyLoginController", function ($scope, $http) {

    alert('HHHH');
    $scope.chkLogin = function () {

        $http({
            method: 'POST',
            url: '/Login/LoginAuthentication',
            data: $scope.loginData
        }).success(function (d) {

            $scope.loginData = null;
            alert(d);
            window.location = "/Home/Show_Date";
        }).error(function () {
            alert('Failed');
        });
    };

   
});