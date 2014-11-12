
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

    };

    $scope.show = function (group) {
        group.show = !group.show;
    };

    $scope.$evalAsync(function () {
        $http.get('/User/GetUserTravels/' + $scope.userId + "?groupByCountry=true").
            success(function (data, status, headers, config) {
                $scope.travelGroups = data;
                for (var i = 0; i < $scope.travelGroups.length; i++) {
                    for (var j = 0; j < $scope.travelGroups[i].travels.length; j++) {
                        $scope.travelGroups[i].travels[j].startDate = fixDate($scope.travelGroups[i].travels[j].startDate);
                        $scope.travelGroups[i].travels[j].endDate = fixDate($scope.travelGroups[i].travels[j].endDate);
                    }
                }
            }).
            error(function (data, status, headers, config) {
            });
    });
});