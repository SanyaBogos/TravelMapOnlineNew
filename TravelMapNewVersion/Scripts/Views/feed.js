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

	$scope.like = function(postId) {
		$http.post("/Feed/Like", { postId: postId })
			.success(function(data, status, headers, config) {
				$scope.post.liked = true;
				//reports.push($scope.report);
			});
	}

	$scope.removeLike = function (postId) {
		$http.post("/Feed/RemoveLike", { postId: postId })
			.success(function (data, status, headers, config) {
				$scope.post.liked = false;
				//reports.push($scope.report);
			});
	}


});