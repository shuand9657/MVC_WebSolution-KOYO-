'use strict';

MVCApp.service('UserProfileService',userProfileService);
MVCApp.factory('setTokenFactory', setTokenFactory);

function userProfileService(httpFactory,localStorageService,setTokenFactory,$http){
    var vm = this;

    //login or logout function
    vm.Login = _login;
    vm.Logout = _logout;
    vm.AuthLogin = _authLogin;

    function _getOAuthToken(loginData) {
        var oauthData = 'grant_type=password&username=' + loginData.username + '&password=' + loginData.password + "&client_id=abc&client_secrect=def";
        var token = $http.post("/OAuth/Token", oauthData);
        token.then(function (success) {
            localStorageService.set("authorizationData", success.data);
        }, function (error) {

            localStorageService.remove("authorizationData");
            console.log(error);
        });
    }

    function _authLogin(loginData) {
        var oauthData = 'grant_type=password&username=' + loginData.username + '&password=' + loginData.password + "&client_id=abc&client_secrect=def";
        var token = $http.post("/OAuth/Token", oauthData);
        return token;
        
    }

    function _login(loginData) {
        _getOAuthToken(loginData);
        var promise = httpFactory.HttpPostAsync('/Login/Login', loginData);
        return promise;
    }

    function _logout(){
        var promise = httpFactory.HttpPostAsync('/Login/Logout');
        return promise;
    }

}

function setTokenFactory($cookieStore, $http) {
    var factory = {
        setToken: setToken,
        clearToken: clearToken
    };
    return factory;
    
    function setToken(token) {
        $http.defaults.headers.common.Authorization = token;
    };

    function clearToken() {
        document.execCommand("ClearAuthenticationCache");
        $http.defaults.headers.common.Authorization = '';
    };
    
};
