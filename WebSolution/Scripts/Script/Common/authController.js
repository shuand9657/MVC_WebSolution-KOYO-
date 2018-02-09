'use strict';
MVCApp.controller('AuthCtrl', authCtrl);

function authCtrl(httpFactory, UserProfileService,localStorageService,$http) {
    var vm = this;
    vm.Login = Login;
    vm.Logout = Logout;
    vm.loginData={
        username: "",
        password: "",
        keyCulture: 0
    };
    vm.AuthLogin = authLogin;

    vm.clicked = false;
    httpFactory.HttpPostAsync('/Home/getKeyCultureSource')
        .then(function (data) {
            vm.KeyCultureSource = data;
        }, function(data, status, headers, config) {
            console.log(status);
        });

    function authLogin() {
        vm.clicked = true;
        var promise = UserProfileService.AuthLogin(vm.loginData);
        var a = {keyCulture:vm.loginData.keyCulture};
        
        promise.then(function (success) {
            localStorageService.set("authorizationData", success.data);
            localStorageService.set("UserProfile", a);
            window.location.href = "/";
        }, function (error) {
            vm.clicked = false;
            localStorageService.remove("authorizationData");
            console.log(error);
        });
        
    }

    function Login() {
        vm.clicked = true;
        var promise = UserProfileService.Login(vm.loginData);
        //console.log(a);
        promise.success(function (data, status, headers, config) {
            vm.loginData = {
                username: '',
                password: ''
            };
            if (data) {
                if (!data.Result.Result) {
                    alert("Error on Login!");
                    vm.clicked = false;
                }
                else {
                    var authToken = headers('AuthToken');
                    if (localStorageService.isSupported) {
                        localStorageService.set("UserProfile", data.UserProfile);
                    }
                    vm.clicked = true;

                    if (data.isRedirect) {
                        window.location.href = data.RedirectUrl;
                    }
                }
            }
            else alert("No response from server...");
            vm.clicked = false;
        }).error(function (data, status, headers, config) {

            localStorageService.remove("UserProfile");
            console.log("error login");
            vm.clicked = false;
        });
    };
    
    function Logout() {
        var promise = UserProfileService.Logout();
        promise.success(function (data, status, headers, config) {
            if (data.Result) {
                localStorageService.remove("UserProfile");
                localStorageService.remove("authorizationData");
                alert("Logout Success!!");
                window.location.href = data.RedirectUrl;
            }
        }).error(function (data, status, headers, config) {
            alert("Error on Loging Out....!!");
            console.log("Error : " + data + '\n' + status);
            localStorageService.remove("UserProfile");
            localStorageService.remove("authorizationData");
        });

    };
}