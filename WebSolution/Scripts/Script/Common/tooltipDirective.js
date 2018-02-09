(function () {
    var tooltipHelper = angular.module('tooltipHelper', []);
    tooltipHelper.directive('tooltipHelper', function () {
        return function (scope, element, attrs) {
            element.tooltip({
                trigger: "hover",
                placement: "bottom"
            });
        };
    });
})();

