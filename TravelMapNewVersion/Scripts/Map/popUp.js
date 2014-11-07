(function () {
    var app = angular.module('app', ['ngSanitize']);

    app.controller('PopUpController', ['$scope', '$http', function ($scope, $http) {
        $scope.currentCountry = null;
        $scope.message = null;        
        var currentCountryId=null,
            userId = null;
        $scope.travels = [];
        $scope.clickMap = function (newCountry) {
            var travels = $scope.travels;
            $scope.currentCountry = newCountry.title;            
            currentCountryId = newCountry.id;
            console.log(currentCountryId);
            //travel.html
            $http.get('/User/GetCurrentUser', {
            }).success(function (data, status, headers, config) {
                userId = data;
                console.log(currentCountryId);
                $http.get('/User/GetTravelsForCountry', {
                    params: { countryTitle: currentCountryId, userId: userId }
                }).success(function (data, status, headers, config) {
                    
                    for (var i = 0; i<data.length ;i++) {
                        var travel = {};
                        //console.log(data);
                        travel.start = data[i].startDate;
                        travel.end = data[i].endDate;
                        travel.id = data[i].travelId;
                        travels.push(travel);
                    }
                    //console.log(travels);//I have I'll show it
                });
            });
            $scope.$apply();
        }        
        
        
        
        


    }]);



    app.controller('travelController', ['$scope', '$http', function ($scope, $http) {
        this.travel = {};

        this.clickCancel = function () {
            $scope.$parent.currentCountry = null;
            this.travel = {};
            $scope.$parent.travels = [];
        };
        

        this.clickOK = function () {
            var trvl = this.travel;
            trvl.html = document.getElementsByClassName('nicEdit-main')[0].innerHTML;
            $http.post('/Map/SetTravel', {
                country: $scope.$parent.currentCountry,
                start: new Date(this.travel.start).getTime() / 1000,
                end: new Date(this.travel.end).getTime() / 1000,
            }).success(function (data, status, headers, config) {
                console.log(trvl);
                if (trvl.html) {
                    $http.post('/Post/PostReport', { text: trvl.html, travelId: data, title: trvl.title });
                    
                        
                }

                $scope.$parent.currentCountry = null;
                $scope.$parent.travels = [];
                trvl = {};
                updateMap();



            }).error(function (data, status, headers, config) {
                alert(status);
            });
        };
    }]);

})();