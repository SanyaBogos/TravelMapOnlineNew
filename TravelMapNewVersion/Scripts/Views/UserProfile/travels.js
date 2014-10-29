
function fixDate(unfixedDate) {
    unfixedDate = unfixedDate.replace("/", "");
    unfixedDate = unfixedDate.replace("Date", "");
    unfixedDate = unfixedDate.replace("(", "");
    unfixedDate = unfixedDate.replace(")", "");
    unfixedDate = unfixedDate.replace("/", "");
    return unfixedDate;
}

app = angular.module("app");
app.controller("travelController", function($scope, $http) {
    $scope.init = function(userId) {
        $scope.userId = userId;
    };
    $scope.travels = [];
    $scope.$evalAsync(function() {
        $http.get('/User/GetUserTravels/' + $scope.userId).
            success(function(data, status, headers, config) {
                $scope.travels = data;
                for (var i = 0; i < $scope.travels.length; i++) {
                    $scope.travels[i].startDate = fixDate($scope.travels[i].startDate);
                    $scope.travels[i].endDate = fixDate($scope.travels[i].endDate);
                    console.log($scope.travels[i].startDate);
                }
            }).
            error(function(data, status, headers, config) {
            });
    });
});