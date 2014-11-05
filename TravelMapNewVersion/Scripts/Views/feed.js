var app = angular.module('app');

app.controller('FeedController', function ($scope, $http) {

	$scope.init = function (userId) {
		$scope.userId = userId;
	};

	$scope.$evalAsync(function () {
		$http.get("/Feed/GetPosts/" + $scope.userId)
			.success(function (data, status) {
				$scope.posts = data;
			}).error(function (data, status) {
				$scope.error = status;
			});
	});


});