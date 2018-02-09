'use strict';

MVCApp.directive('ngvalKoyo', ngvalKoyo);

function ngvalKoyo(validationFactory , $window)
{
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, iElm, iAttrs, ngModel)
        {
            iElm.bind('blur', function ()
            {
                var ngmodel = ngModel;
                var errors = [];
                var getErrors = function ()
                {
                    errors = validationFactory.ValidateElem(ngmodel);
                    return errors;
                };

                ngModel.ngvalKoyo = {
                    hasError: ngModel.$invalid,
                    errors : getErrors()
                };

                scope.$apply(iAttrs.ngvalKoyo);
            });
        }
    };
};


