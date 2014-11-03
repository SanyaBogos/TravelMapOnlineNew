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
        var travel1 = { 'start': '2014-10-13', 'end': '2014-11-13', 'message': "You can terminate your session by clicking the log out button in the top right corner. This prevents unauthorized use of your mail.com mailbox, for example, by clicking on the back button of your browser. Please remember to always click on the log out button for your own safety when exiting your mailbox." },
            travel2 = { 'start': '2014-2-13', 'end': '2014-2-155', 'message': "You can terminate your session by clicking the log out button in the top right corner. This prevents unauthorized use of your mail.com mailbox, for example, by clicking on the back button of your browser. Please remember to always click on the log out button for your own safety when exiting your mailbox." };
        $scope.travels = [travel1,travel2];
      

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