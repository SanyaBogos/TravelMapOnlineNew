
app  = angular.module("app", ['ngRoute']);

app.controller("ProfileController", function ($scope, $http) {

    $scope.travelsSelected = true;
    $scope.notesSelected = false;
    $scope.friendsSelected = false;
    $scope.personalInfoSelected = false;
    $scope.user = {};


    $scope.init = function (userId) {
        $scope.userId = userId;
        loadUser();
    };

    $scope.showTravels = function() {
        hideAll();
        $scope.travelsSelected = true;
    };
    $scope.showNotes = function () {
        hideAll();
        $scope.notesSelected = true;
    };
    $scope.showFriends = function () {
        hideAll();
        $scope.friendsSelected = true;
    };
    $scope.showPersonalInfo = function () {
        hideAll();
        $scope.personalInfoSelected = true;
    };

    function hideAll() {
        $scope.travelsSelected = false;
        $scope.notesSelected = false;
        $scope.friendsSelected = false;
        $scope.personalInfoSelected = false;
    }
    function arrayBufferToBase64(buffer) {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    }

    function loadUser() {
        $http.get("/User/JIndex/" + $scope.userId)
            .success(function (data, status) {
                $scope.user = data;
                if (!$scope.user.Photo) {
                    $scope.user.Photo = "http://localhost:29834/Images/icon_user.png";
                } else {
                    var base64Picture = arrayBufferToBase64($scope.user.Photo);
                    $scope.user.Photo = "data:image/jpg;base64," + base64Picture;
                }
            }).error(function (data, status) {
            });
    }

});