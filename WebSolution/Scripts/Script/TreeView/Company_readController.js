'use strict';

MVCApp.controller('Company_readController', Company_readController)

function Company_readController($scope , $http,blockUI , $routeParams){
    var vm = this;
    vm.key = $routeParams.path.split("_");
    vm.dir = vm.key[0];
    vm.page = "_"+vm.key[1];
    vm.reqID =$routeParams.id;

    vm.message = "the path is : "+vm.dir + " and the page is : "+vm.page + " and requested id is : " + vm.reqID;

    
}