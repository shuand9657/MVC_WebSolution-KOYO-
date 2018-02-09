'use strict';
MVCApp.service('VariableService', varService);

function varService()
{
    var vm = this;
    
    var ErrMessageCollection = [];

    vm.getErrorMessageCollection = function () { return ErrMessageCollection; }
    vm.setErrorMessageCollection = function (data) { ErrMessageCollection = data;}
}