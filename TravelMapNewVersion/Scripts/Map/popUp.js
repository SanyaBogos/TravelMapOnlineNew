(function () {
    var app = angular.module('app', []);

    app.controller('PopUpController', function ($scope) {
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

        }
    });
})();