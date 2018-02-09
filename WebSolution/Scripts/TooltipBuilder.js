'use strict';

MVCApp.directive('tooltipBuilder', tooltipBuilder);

function tooltipBuilder()
{
    return {
        restrict: 'E',
        template: '<span class="validation-summary-errors" tooltips tooltip-template="test" >*</span>',
        link: function (scope, iElm, iAttrs)
        {

        }
    }
}