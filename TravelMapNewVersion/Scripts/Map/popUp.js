(function () {
    var app = angular.module('app', []);

    app.controller('PopUpController', function ($scope, $http) {
        $scope.currentCountry = null;
        $scope.message = null;

        $scope.clickMap = function (newCountry) {
            $scope.currentCountry = newCountry;
            //console.log($scope.currentCountry);
            $scope.$apply();
        };

        $scope.clickCancel = function () {
            $scope.currentCountry = null;
            //console.log('cancel click');
        };

        $scope.clickOK = function () {
            $http.post('/Map/SetTravel', {
                country: $scope.currentCountry,
                start: '01.01.1980',
                end: '01.01.2000'
            }).success(function (data, status, headers, config) {
                console.log('success');
            }).error(function (data, status, headers, config) {
                console.log('error');
            });
            console.log($scope.message);
            $scope.currentCountry = null;
        }
    });
})();
