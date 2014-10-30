var app = angular.module('app');

app.controller('EditProfileCtrl', function($scope, $http) {

	$scope.init = function(userId) {
		$scope.userId = userId;
	};

	//todo: check if this initializations needed
	$scope.editPhone = false;
	$scope.emailChanged = false;
	$scope.phoneChanged = false;

	$scope.formPictureData = function () {
		var base64Picture = arrayBufferToBase64($scope.model.Photo);
		return "data:image/jpg;base64," + base64Picture;
	};

	$scope.$evalAsync(function() {
		$http.get("/User/JIndex/" + $scope.userId)
			.success(function(data, status) {
				$scope.model = data;
				$scope.photoData = $scope.formPictureData();
				// save model to allow canceling changes
				$scope.unmodifiedModel = angular.copy($scope.model);
				// ...
			}).error(function(data, status) {
				$scope.error = status;
				//alert("error in get--" + status + "--" + data.substring(0,100));
			});
	});

	$scope.uploadFile = function(files) {
		var fd = new FormData();
		//Take the first selected file
		fd.append("file", files[0]);

		$http.post("/User/SaveUserpic", fd, {
				withCredentials: true,
				headers: { 'Content-Type': undefined },
				transformRequest: angular.identity
			})
			.success(function(data, status) {
				console.log('ok');
				$scope.model.Photo = data;
				$scope.photoData = $scope.formPictureData();
			})
			.error(function(data, status) {
				console.log('err');
			});
	};



	$scope.saveEmail = function() {

		// todo: check if Email is not empty

		$http.post("/User/SaveEmail", { id: $scope.userId, newEmail: $scope.model.Email })
			.success(function(data1, status, headers, config1) {
				// todo: display somehow that it's really saved
				$scope.editEmail = false;
				//console.log(data1);
			})
			.error(function(data1, status, headers, config1) {
				// todo: display error
				//console.log(status);
			});
	}

	$scope.saveAll = function () {

		$http.post("/User/Save", $scope.model)// { id: $scope.userId, email: $scope.model.Email })

			.success(function (data1, status, headers, config1) {
				// todo: display somehow that it's really saved
				$scope.editEmail = false;
				//console.log(data1);
			})
			.error(function (data1, status, headers, config1) {
				// todo: display error
				console.log(status);
			});
	}

	//#region Checking edit onBlur 

	$scope.checkEmailEdit = function() {
		if ($scope.model.Email == $scope.unmodifiedModel.Email) {
			$scope.emailChanged = false;
			$scope.editEmail = false;
		}
	}

	$scope.checkPhoneEdit = function() {
		if ($scope.model.Phone == $scope.unmodifiedModel.Phone) {
			$scope.phoneChanged = false;
			$scope.editPhone = false;
		}
	}

	$scope.checkSurnameEdit = function() {
		if ($scope.model.Surname == $scope.unmodifiedModel.Surname) {
			$scope.surnameChanged = false;
			$scope.editSurname = false;
		}
	}

	//#endregion

	//#region onCancel

	$scope.cancelEmailEdit = function() {
		$scope.editEmail = false;
		$scope.model.Email = $scope.unmodifiedModel.Email;
		$scope.emailChanged = false;
	}

	$scope.cancelPhoneEdit = function() {
		$scope.editPhone = false;
		$scope.model.Phone = $scope.unmodifiedModel.Phone;
		$scope.phoneChanged = false;
	}

	$scope.cancelSurnameEdit = function() {
		$scope.editSurname = false;
		$scope.model.Surname = $scope.unmodifiedModel.Surname;
		$scope.surnameChanged = false;
	}

	//#endregion

	function arrayBufferToBase64(buffer) {
		var binary = '';
		var bytes = new Uint8Array(buffer);
		var len = bytes.byteLength;
		for (var i = 0; i < len; i++) {
			binary += String.fromCharCode(bytes[i]);
		}
		return window.btoa(binary);
	}
});
