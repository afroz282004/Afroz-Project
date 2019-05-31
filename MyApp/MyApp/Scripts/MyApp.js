var app = angular.module('MyApp',[]);
app.controller('menuController', ['$scope', '$http', function ($scope, $http) {
    $scope.SiteMenu = [];
    $scope.btntext = "Save";
    $scope.globleVarriable = "";
    //var data = $scope.record;

    $http.get('/home/GetSiteMenu').then(function (data) {
        $scope.SiteMenu = data.data;
    }, function (error) {
        alert('Error');
        });

    if (!($scope.globleVarriable === 'Get_Data')) {
        alert($scope.globleVarriable);
        $http.get("/home/Get_Data").then(function (d) {
            $scope.record = d.data;
            $scope.globleVarriable = "Get_Data";
        }, function () {
            alert('Failed');
        });
    }
    else {
        alert($scope.globleVarriable);
    }

    $scope.clickAdd = function () {
        $scope.btntext = "Save";
        window.location = "/Home/Index";
        
    };
    $scope.clickAdddropdown = function () {

        window.location = "/Home/Dropdownlist";
    };

   

}]);