'use strict';

angular.module('MVCApp.tv2',['ngRoute'])
    .config(config);

function config( $routeProvider){
    $routeProvider.when('/read/Company/:id',{
        templateUrl:'../templates/TreeView/CompanyProfileRead.html',
        controller: 'tvCompanyCtrl',
        controllerAs: 'vm'
    })
    .when('/read/Department/:id',{
        templateUrl:'../templates/TreeView/DepartProfileRead.html',
        controller:'tvDepartCtrl',
        controllerAs:'vm'
    })
    .when('/read/User/:id',{
        templateUrl: '../templates/TreeView/UserProfileRead.html',
        controller: 'tvUserCtrl',
        controllerAs:'vm'
    })
    .otherwise('/');
};

