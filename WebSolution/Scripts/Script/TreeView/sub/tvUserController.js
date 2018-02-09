'use strict';

angular.module('MVCApp.tv2').controller('tvUserCtrl',tvUserCtrl);
function tvUserCtrl($routeParams,$http, $scope, loadFactory){
    var vm = this;

    vm.path = $routeParams.path;
    vm.id = $routeParams.id;

    vm.message = " ID : " + vm.id + " , Path : " + vm.path;

        vm.req = {};
        vm.formData = {};
        vm.getUserProfile = getUserProfile;
        vm.req = { userID: '' };
    vm.formData = {
        UserID: '',
        Username: '',
        Role: '',
        FirstName: '',
        LastName: '',
        Memo: ''
    };
    vm.req.userID = $routeParams.id;
    function getUserProfile() {
        loadFactory.GetDataProfile('/Home/getUserProfileData', vm.req)
         .then(function (answer) {
             vm.formData = answer.data[0];
             console.log(vm.formData);
         }, function (error) {
             alert("Error!");
             console.log(error);
         })
    };
    getUserProfile();
}
