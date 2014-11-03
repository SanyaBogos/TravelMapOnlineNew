function fixDate(unfixedDate) {
    unfixedDate = unfixedDate.replace("/", "");
    unfixedDate = unfixedDate.replace("Date", "");
    unfixedDate = unfixedDate.replace("(", "");
    unfixedDate = unfixedDate.replace(")", "");
    unfixedDate = unfixedDate.replace("/", "");
    return unfixedDate;
}

var app = angular.app("app");

app.controller("ReportsController", function ($scope, $http) {
    var reports = [];
    $scope.report = {};
    $scope.init = function (travelId) {
        $http.get('/Post/GetTravelReports/' + travelId).
            success(function(data, status, headers, config) {
                reports = data;
                $scope.report = reports[0];
            });
    };

    $scope.isLast = function () {
        return reports.indexOf($scope.report) == reports.length - 1;
    };

    $scope.isFirst = function () {
        return reports.indexOf($scope.report) == 0;
    };

    $scope.nextReport = function () {
        if (!$scope.isLast()) {
            $scope.report = reports[reports.indexOf($scope.report) + 1];
        }
    };

    $scope.prevReport = function () {
        if (!$scope.isFirst()) {
            $scope.report = reports[reports.indexOf($scope.report) - 1];
        }
    };

});
