(function() {
	'use strict';

	var app = angular.module('app', [
		//'ngAnimate',
		//'ngRoute',
	]);

	app.controller('UPCtrl', function($scope) {
		$scope.editPhone = false;
	});
})();