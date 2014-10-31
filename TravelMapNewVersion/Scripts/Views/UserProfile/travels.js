
function fixDate(unfixedDate) {
    unfixedDate = unfixedDate.replace("/", "");
    unfixedDate = unfixedDate.replace("Date", "");
    unfixedDate = unfixedDate.replace("(", "");
    unfixedDate = unfixedDate.replace(")", "");
    unfixedDate = unfixedDate.replace("/", "");
    return unfixedDate;
}

app = angular.module("app");
app.controller("travelController", function ($scope, $http) {
    $scope.init = function (userId) {
        $scope.userId = userId;
    };
    $scope.travelGroups = [];
    $scope.messages = [];
    this.id = 0;
    this.click = function (id) {
        $http.get('/Post/GetTravelReports/' + id).
            success(function (data, status, headers, config) {
                $scope.messages = data;
                console.log($scope.messages[0].time);
                var d = +fixDate($scope.messages[0].time);
                $scope.messages[0].time = (new Date(d)).toDateString();

            }).
            error(function (data, status, headers, config) {
            });

    };
    $scope.show = function(group) {
        group.show = !group.show;
    };

    $scope.$evalAsync(function () {
        $http.get('/User/GetUserTravels/' + $scope.userId + "?groupByCountry=true").
            success(function (data, status, headers, config) {
                $scope.travelGroups = data;
                for (var i = 0; i < $scope.travelGroups.length; i++) {
                    //$scope.travelGroups[i].show = true;
                    for (var j = 0; j < $scope.travelGroups[i].travels.length; j++) {
                        console.log($scope.travelGroups[i].travels[j]);
                        $scope.travelGroups[i].travels[j].startDate = fixDate($scope.travelGroups[i].travels[j].startDate);
                        $scope.travelGroups[i].travels[j].endDate = fixDate($scope.travelGroups[i].travels[j].endDate);

                    }
                }
                //$scope.travelGroups.travels[i].startDate = fixDate($scope.travelGroups[i].startDate);
                //$scope.travelGroups.travels[i].endDate = fixDate($scope.travelGroups[i].endDate);
            }).
            error(function (data, status, headers, config) {
            });
    });
});