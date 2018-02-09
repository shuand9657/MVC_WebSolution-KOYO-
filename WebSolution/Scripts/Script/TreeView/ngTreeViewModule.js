'use strict';
MVCApp.config(config);

function config($routeProvider, $httpProvider,
                $controllerProvider, $compileProvider, $filterProvider,
                $provide) {

    $routeProvider
         .when('/tv/new/:path*', {
                templateUrl: function (urlPatten) {
                    return "../templates/" + urlPatten.path + "/new.html";
                }
            })
        .when('/tv/:path*/:id', {
            templateUrl: function (urlPatten) {
                return "../templates/" + urlPatten.path + ".html";
            }
        }).when('/t1/:path*/:id',{
            templateUrl: function (urlPatten) {
                return urlPatten.path;
            }
        })
    ;
}