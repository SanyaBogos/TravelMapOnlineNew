(function() {
	var app = angular.module('app', []);

	app.controller('UPCtrl', function($scope, $http) {

		$scope.init = function(userId) {
			$scope.userId = userId;
		};

		$scope.editPhone = false;

		$scope.$evalAsync(function() {
			$http.get("/User/JIndex/" + $scope.userId)
				.success(function(data, status) {
					$scope.model = data;
					// ...
				}).error(function(data, status) {
					$scope.error = status;
					//alert("error in get--" + status + "--" + data.substring(0,100));
				});
		});

		$scope.saveEmail = function() {

			// todo: check if Email is not empty

			$http.post("/User/SaveEmail", { id: $scope.userId, newEmail: $scope.model.Email })
				.success(function(data1, status, headers, config1) {
					// todo: display somehow that it's realy saved
					$scope.editEmail = false;
					//console.log(data1);
				})
				.error(function(data1, status, headers, config1) {
					// todo: display error
					//console.log(status);
				});
		}
	});
})();