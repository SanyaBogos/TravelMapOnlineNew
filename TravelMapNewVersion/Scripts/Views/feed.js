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
				for (var i = 0, lenPosts = $scope.posts.length; i < lenPosts; i++) {
					if ($scope.posts[i].PostId == postId) {
						$scope.posts[i].Likes = data;
						break;
					}
				};
			})
			.error(function(data, status) {
				$scope.error = status;
			});
	}

	$scope.removeLike = function (postId) {
		$http.post("/Feed/RemoveLike", { postId: postId })
			.success(function(data, status, headers, config) {
				for (var i = 0, lenPosts = $scope.posts.length; i < lenPosts; i++) {
					if ($scope.posts[i].PostId == postId) {
						$scope.posts[i].Likes = data;
						break;
					}
				};
			})
			.error(function(data, status) {
				$scope.error = status;
			});
	}

	$scope.showNumLikes = function(likes) {
		return likes.length > 0;
	}

	$scope.checkLiked = function(likes) {
		for (var i = 0, len = likes.length; i < len; i++) {
			if (likes[i].UserId == $scope.userId) {
				return true;
			}
		}
		return false;
	}


});