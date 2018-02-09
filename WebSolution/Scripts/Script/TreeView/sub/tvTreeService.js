'use strict';

angular.module("MVCApp.tv2").factory('loadFactory', loadFactory);

function loadFactory($http, $q) {
    var factory = {
        GetDataProfile: GetDataProfile
    };    
    return factory;
    function GetDataProfile(url, ID) {
        var deferred = $q.defer();
        var promise =
            $http({
                method: 'post',
                url: url,
                data: $.param(ID),
                dataType: 'JSON',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        promise.then(function (answer) {
            answer.status = true;
            deferred.resolve(answer);
        }, function (error) {
            error.status = false;
            deferred.reject(error);
        });
        return promise;
       
    }
}