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
            console.log(this.travel);
            $scope.$parent.currentCountry = null;
            this.travel = {};
        };



        this.clickOK = function () {
            var trvl = this.travel;
            //this.travel.start = this.travel.start.split("-");
            //this.travel.end = this.travel.end.split("-");
            $http.post('/Map/SetTravel', {
                country: $scope.$parent.currentCountry,
                start: new Date(this.travel.start).getTime() / 1000,
                end: new Date(this.travel.end).getTime() / 1000,
            }).success(function (data, status, headers, config) {
                console.log(data);
                console.log(trvl.message);

                $http.post('/Post/PostReport', {
                    text: trvl.message,
                    travelId: data
                });
                    console.log('success');
                }).error(function (data, status, headers, config) {
                    console.log('error');
                });
            this.travel = {};
            $scope.$parent.currentCountry = null;
        }
    });

})();