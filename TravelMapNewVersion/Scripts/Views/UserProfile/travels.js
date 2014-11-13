app = angular.module("app");

app.controller("travelController", function ($scope, $http) {

    
    $scope.travels = [];

    $scope.init = function (userId) {
        $scope.userId = userId;
        console.log(userId);
        updateMap(userId);
    };

    $scope.clickMap = function (selectedCountry) {
        $http.get('/User/GetTravelsForCountry', {
            params: { countryTitle: selectedCountry.id, userId: $scope.userId }
        }).success(function (data, status, headers, config) {
            $scope.travels.length = 0;
            for (var i = 0; i < data.length ; i++) {
                $scope.travels.push(data[i]);
            }
        });
    };

    $scope.showReports = function(travel) {
        console.log(travel);
    };


});