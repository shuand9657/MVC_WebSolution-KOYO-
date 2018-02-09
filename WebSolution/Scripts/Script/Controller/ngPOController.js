'use strict';

MVCApp.controller('tempNgPOCtrl',tempNgPOCtrl);

function tempNgPOCtrl(){
    var vm = this;
    vm.formData = {};
    vm.doPOProfile = doPOProfile;

    function doPOProfile(){
    alert("alert!");
    };

};