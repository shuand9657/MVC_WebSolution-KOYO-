'use strict';

angular.module('MVCApp.tv2').controller('tvCompanyCtrl',tvCompanyCtrl);
function tvCompanyCtrl($routeParams,$http, $scope, loadFactory){
    var vm = this;

    vm.path = $routeParams.path;
    vm.id = $routeParams.id;

    vm.message = " ID : " + vm.id + " , Path : " + vm.path;

        vm.req = {};
        vm.formData = {};
        vm.getCompanyProfile = getCompanyProfile;
        vm.req.masterID = $routeParams.id;
        vm.formData = {
            MasterID: '',
            OrgSN: '',
            OrgName: ''
        };
        function getCompanyProfile() {
            //blockUI.start("Fetching data, Please wait...");
            loadFactory.GetDataProfile('/Home/getCompanyProfileData', vm.req)
                .then(function (answer) {
                    vm.formData = answer.data[0];
                    console.log(vm.formData);
                }, function (error) {
                    alert("error!");
                    console.log(error);
                });
        };

        getCompanyProfile();
}
