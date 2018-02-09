'use strict';

angular.module('MVCApp.tv2').controller('tvDepartCtrl',tvDepartCtrl);
function tvDepartCtrl($routeParams,$http, $scope, loadFactory){
    var vm = this;

    vm.path = $routeParams.path;
    vm.id = $routeParams.id;

    vm.message = " ID : " + vm.id + " , Path : " + vm.path;

        vm.req = {};
        vm.formData = {};
        vm.getDepartmentProfile = getDepartmentProfile;
        vm.req = { departID: '' };
    vm.formData = {
        DepartID: '',
        DepartName: '',
        DepartSN: '',
        DepartCode: '',
        Memo: ''
    };

    vm.req.departID = $routeParams.id

    function getDepartmentProfile() {
        loadFactory.GetDataProfile('/Home/getDepartProfileData', vm.req)
         .then(function (answer) {
             vm.formData = answer.data[0];
             console.log(vm.formData);
         }, function (error) {
             alert("Error!");
             console.log(error);
         })
    };
    getDepartmentProfile();
};
