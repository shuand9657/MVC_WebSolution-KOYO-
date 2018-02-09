'use strict';
var MVCApp = angular.module('MVCApp', ['ngAnimate',
                          'ui.bootstrap',
                          'kendo.directives',
                          'ngDialog',
                          'ngval',
                          'ngRoute',
                          'ngCookies',
                          'ngMessages',
                          'ngSanitize',
                          'ngMaterial',
                          'blockUI',
                          '720kb.tooltips',
                          'LocalStorageModule'
])
    .config(config);

function config($routeProvider,
                $httpProvider,
                $controllerProvider,
                $compileProvider,
                $filterProvider,
                $provide,
                blockUIConfig,
                tooltipsConfProvider,
                localStorageServiceProvider
                ) {
    
    MVCApp.registerCtrl = $controllerProvider.register;
    
    $httpProvider.interceptors.push('authInterceptorService');
    //$httpProvider.interceptors.push('ClientLogService');

    blockUIConfig.autoBlock = false;

    tooltipsConfProvider.configure({'smart':true, 'speed': 'fast'});
    
    localStorageServiceProvider.setPrefix('MVCUserProfile').setStorageType('sessionStorage').setNotify(true, true);

    location.hash='#/';
    $routeProvider.when('/po/Add', {
        templateUrl: '../templates/_add.html',
        controller: 'poAddProfileCtrl'
    }).when('/po/Edit', {
        templateUrl: '../templates/_edit.html',
        controller: 'poProfileCtrl'
    }).when('/po/Read', {
        templateUrl: '../templates/_read.html',
        controller: 'poProfileCtrl'
    }).when('/po/temp/read',{
        templateUrl : '/templates/POProfile',
        controller: 'poProfileCtrl'
    })
      .otherwise({ redirectTo: '/' });

};

MVCApp.directive('ngModel', function attributeNgModelDirective()
{
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl)
        {
            ctrl.attributes = attrs;
        }
    };
});