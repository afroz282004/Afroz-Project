var app = angular.module("Loginapp", []);
app.controller("LoginController", function ($scope, $http) {
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