function fixDate(unfixedDate) {
    unfixedDate = unfixedDate.replace("/", "");
    unfixedDate = unfixedDate.replace("Date", "");
    unfixedDate = unfixedDate.replace("(", "");
    unfixedDate = unfixedDate.replace(")", "");
    unfixedDate = unfixedDate.replace("/", "");
    return unfixedDate;
}

app = angular.module("app");

app.controller("ReportsController", function ($scope, $http) {
    var reports = [];
    var unmodifiedCopy = {};
    $scope.report = {};
    $scope.editMode = false;
    var isNew = false;

    $scope.init = function (travelId) {
        $scope.travelId = travelId;
        $http.get('/Post/GetTravelReports/' + travelId).
            success(function (data, status, headers, config) {
                reports = data;
                $scope.report = reports[0];
            });
    };

    $scope.isLast = function () {
        console.log(reports);
        return reports.indexOf($scope.report) == reports.length - 1;
    };

    $scope.isFirst = function () {
        return reports.indexOf($scope.report) == 0 || $scope.report === undefined;
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
    $scope.addReport = function () {
        $scope.editMode = true;
        isNew = true;
        $scope.report = {};
    };
    $scope.editReport = function () {
        angular.copy($scope.report, unmodifiedCopy);
        $scope.editMode = true;
        isNew = false;
    };

    $scope.saveReport = function () {
        if (!$scope.report.text) {
            alert("Enter some text");
        }
        if (isNew) {
            addReport();
        } else {
            editReport();
        }
    };
    $scope.cancel = function () {
        if (confirm("Discard changes?")) {
            angular.copy(unmodifiedCopy, $scope.report);

            $scope.editMode = false;
        }
    };
    $scope.getControlButtonsClass = function (actionName) {
        if (actionName != 'add' && reports.length == 0) {
            return "disabled-button";
        }
        if ($scope.editMode) {
            return "disabled-button";
        }
    };

    function addReport() {
        $http.post('/Post/PostReport', { text: $scope.report.text, travelId: $scope.travelId, title: $scope.report.title }).
            success(function (data, status, headers, config) {
                $scope.report.postId = data;
                $scope.editMode = false;
                reports.push($scope.report);
            });
    }


    $scope.deleteReport = function () {
        if (!confirm("Are you sure?")) {
            return;
        }
        $http.get('/Post/DeleteReport?reportId=' + $scope.report.postId).
            success(function (data, status, headers, config) {
                reports.splice(reports.indexOf($scope.report), 1);
                $scope.report = reports[0];
            });

    };

    function editReport() {
        $http.post('/Post/EditReport', { text: $scope.report.text, title: $scope.report.title, postId: $scope.report.postId }).
            success(function (data, status, headers, config) {
                $scope.editMode = false;
                //reports.push($scope.report);
            });
    };

});
