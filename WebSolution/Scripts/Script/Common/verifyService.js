MVCApp.factory('setTokenFactory', setTokenFactory);
MVCApp.factory('doVerifyFactory', doVerifyFactory);


function setTokenFactory($cookieStore, $http) {
    var factory = {
        setToken: setToken,
        clearToken: clearToken
    };
    return factory;
    
    function setToken(token) {
        $http.defaults.headers.common.Authorization = token;
        $cookieStore.put('AuthToken', token);
    };

    function clearToken() {
        document.execCommand("ClearAuthenticationCache");
        $cookieStore.remove('AuthToken');
        $http.defaults.headers.common.Authorization = '';
    };
    
};


function doVerifyFactory($http, $q) {
    var factory = {
        DoLoginAction: DoLoginAction,
        DoLogoutAction:DoLogoutAction
    };

    return factory;
    
    function DoLoginAction(url,loginData) {
        var deferred = $q.defer;
        var promise =
            $http({
                method: 'POST',
                url: url,
                data: loginData,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (answer) {
                answer.status = true;
                deferred.resolve(answer);
                if (answer.data) {
                    if (!answer.data.Result) {
                        setTokenFactory.clearToken();
                        alert("Error on Login!");
                        vm.loginData = {
                            username: '',
                            password: ''
                        };
                    }
                    else {
                        var authToken = answer.headers('AuthToken');
                        $rootScope.token = authToken
                        setTokenFactory.setToken(authToken);
                        vm.loginData = {
                            username: '',
                            password: ''
                        };
                        alert("Welcome User : " + answer.data.Message + " Login complete, redirecting..");
                        window.location.href = '/Home/POProfile';
                    }
                }
                else alert("No response from server...");
            },function(error){
                error.status = true;
                deferred.reject(error);
            });
        return promise;
    };

    function DoLogoutAction(url) {
        var deferred = $q.defer;
        var promise =
            $http({
                method: 'POST',
                url: url,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (answer) {
                answer.status = true;
                deferred.resolve(answer);
            }, function (error) {
                error.status = true;
                deferred.reject(error);
            });
        return promise;
    };

    
};