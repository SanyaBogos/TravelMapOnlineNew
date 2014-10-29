(function () {
    var app = angular.module('app', []);

    app.controller('PopUpController', function ($scope, $http) {
        $scope.currentCountry = null;
        $scope.message = null;
        $scope.clickMap = function (newCountry) {
            $scope.currentCountry = newCountry;
            //console.log($scope.currentCountry);
            $scope.$apply();
        }      
       
    });



    app.controller('travelController', function ($scope, $http) {
        this.travel = {};

        this.clickCancel = function () {            
            $scope.$parent.currentCountry = null;
        };

        

        this.clickOK = function () {            
            this.travel.start = this.travel.start.split("-");
            this.travel.end = this.travel.end.split("-");           
            $http.post('/Map/SetTravel', {                
                country: $scope.$parent.currentCountry,
                start: this.travel.start[1]+'.'+this.travel.start[2]+'.'+this.travel.start[0],
                end: this.travel.end[1] + '.' + this.travel.end[2] + '.' + this.travel.end[0]
            }).success(function (data, status, headers, config) {
                console.log('success');
            }).error(function (data, status, headers, config) {
                console.log('error');
            });
            $scope.$parent.currentCountry = null;
        }
    });

})();