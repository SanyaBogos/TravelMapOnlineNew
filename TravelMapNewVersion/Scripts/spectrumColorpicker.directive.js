(function (undefined) {
    'use strict';
    angularSpectrumColorpicker.directive('spectrumColorpicker', function () {
        return {
            restrict: 'E',
            require: 'ngModel',
            scope: false,
            replace: true,
            template: '<span><input class="input-small" /></span>',
            link: function ($scope, $element, attrs, $ngModel) {

                var $input = $element.find('input');
                var fallbackValue = $scope.$eval(attrs.fallbackValue);
                var format = $scope.$eval(attrs.format) || undefined;

                function setViewValue(color) {
                    var value = fallbackValue;

                    if (color) {
                        value = color.toString(format);
                    } else if (angular.isUndefined(fallbackValue)) {
                        value = color;
                    }

                    $ngModel.$setViewValue(value);
                }

                var onChange = function (color) {
                    $scope.$apply(function () {
                        setViewValue(color);
                    });
                };
                var onToggle = function () {
                    $input.spectrum('toggle');
                    return false;
                };
                var options = angular.extend({
                    color: $ngModel.$viewValue,
                    change: onChange,
                    move: onChange,
                    hide: onChange
                }, $scope.$eval(attrs.options));


                if (attrs.triggerId) {
                    angular.element(document.body).on('click', '#' + attrs.triggerId, onToggle);
                }


                $ngModel.$render = function () {
                    $input.spectrum('set', $ngModel.$viewValue || '');
                };

                if (options.color) {
                    $input.spectrum('set', options.color || '');
                    setViewValue(options.color);
                }

                $input.spectrum(options);

                $scope.$on('$destroy', function () {
                    $input.spectrum('destroy');
                });
            }
        };
    });
})();