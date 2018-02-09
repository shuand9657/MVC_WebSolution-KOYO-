(function () {
    'use strict';
    var ngval = angular.module('ngval', []);

    ngval.directive('ngval', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, iElm, iAttrs, ngModel) {
                //var vm = this;
                //vm.SummaryError = [];
                var ErrorMessage = [];
                scope.validationSummary=[];
                var messages = angular.fromJson(iAttrs.ngval);
                var fieldName = iAttrs.name;
                var getErrors = function () {
                    var errors = [];

                    for (var item = 0 ; item < scope.validationSummary.length; item++) {
                        if (scope.validationSummary[item].name == fieldName) {
                            var a = scope.validationSummary[item].name.indexOf(fieldName);
                            //if (~a) {
                            scope.validationSummary.splice(item, 1);
                            //}
                        }
                    }

                    for (var i = 0 ; i < messages.length; i++) {
                        if (messages[i].Type == "length") {
                            if (ngModel.$error.maxlength) {
                                errors.push({name:fieldName, validator: messages[i].Type, message: messages[i].Message });
                                scope.validationSummary.push({ name: fieldName, validator: messages[i].Type, message: messages[i].Message });
                            }
                            if (ngModel.$error.minlength) {
                                errors.push({ name: fieldName, validator: messages[i].Type, message: messages[i].Message });
                                scope.validationSummary.push({ name: fieldName, validator: messages[i].Type, message: messages[i].Message });
                            }
                        }
                        else if (ngModel.$error[messages[i].Type]) {
                            errors.push({ name: fieldName, validator: messages[i].Type, message: messages[i].Message });
                            scope.validationSummary.push({ name: fieldName, validator: messages[i].Type, message: messages[i].Message });
                        }
                    }
                    return errors;
                };
               


                scope.$watch(function () {
                    
                    return ngModel.$modelValue;
                }, function () {
                    ngModel.ngval = {
                        hasError: ngModel.$invalid,
                        errors: getErrors(),
                        dirty: (ngModel.$dirty)
                    };
                });
                
            }
        };
    }]);


})();