'use strict;'
MVCApp.factory('httpFactory', httpFactory);
MVCApp.factory('authInterceptorService', authInterceptorService);
MVCApp.factory('ClientLogService', clientLogService);

function httpFactory($http, $q, $log)
{
    var factory = {
        HttpPostAsync: HttpPostAsync,
        HttpPostSync: HttpPostSync,
        HttpGetAsync: HttpGetAsync,
        HttpGetSync: HttpGetSync,
        HttpPost :  HttpPost
    };
    return factory;

    function HttpPost(url, item) {
        var deferred = $q.defer();
        var promise = $http.post(url, item);
        promise.success(function (data) {
            deferred.resolve(data);
        }).error(function (data, status) {
            console.log(data);
        });
        return deferred.promise;
    }

    function HttpPostAsync(url, item)
    {
        var deferred = $q.defer();
        var promise = null;
        if (item == null)
        {
            promise =
            $http({
                method: 'POST',
                url: url,
                dataType: 'JSON',
                timeout: deferred.promise,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
        else
        {
            promise =
            $http({
                method: 'POST',
                url: url,
                data: $.param(item),
                dataType: 'json',
                timeout: deferred.promise,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
        promise.cancel = function ()
        {
            deferred.resolve();
            $log.error("Request aborted!");
        };
        
        return promise;
    };

    function HttpPostSync(url, item)
    {
        var deferred = $q.defer();
        var promise = HttpPostAsync(url, item);
        promise.success(function (data)
        {
            deferred.resolve(data);
        }).error(function (data, status)
        {
            console.log(status);
            return;
        });
        promise.cancel = function ()
        {
            deferred.resolve();
            $log.error("Request aborted!");
        }
        return deferred.promise;
    };

    function HttpGetAsync(url, item)
    {
        var deferred = $q.defer();
        var promise = null;
        if (item == null)
        {
            promise = $http({
                method: 'get',
                url: url,
                dataType: 'JSON',
                timeout: deferred.promise,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
        else
        {
            promise = $http({
                method: 'GET',
                url: ur,
                data: $.param(item),
                dataType: 'json',
                timeout: deferred.promise,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
        promise.cancel = function ()
        {
            deferred.resolve();
            $log.error("Request aborted!");
        }
        return promise;
    }

    function HttpGetSync(url, item)
    {
        var deferred = $q.defer();
        var promise = HttpGetAsync(url, item);
        promise.success(function (data)
        {
            deferred.resolve(data);
        }).error(function (data, status)
        {
            console.log(status);
            return;
        });
        return deferred.promise;
    }
    
};

function authInterceptorService($q, $location, localStorageService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.access_token;
        }
        console.log(config.url);
        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            window.location.href = '/Login/Login';
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}

function clientLogService() {
    var clientLogFactory = {};
    

    var _request = function (config) {
        console.log(config.url);
    }
    clientLogFactory.request = _request;

    return clientLogFactory;
}