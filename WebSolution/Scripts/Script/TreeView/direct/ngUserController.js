MVCApp.registerCtrl('tvUserProfileCtrl', tvUserProfileCtrl);
MVCApp.registerCtrl('tvAddUserProfileCtrl', tvAddUserProfileCtrl);

function tvUserProfileCtrl($http, $scope, $routeParams, ngTreeViewFactory, blockUIFactory) {
    var vm = this;
    vm.req = {};
    vm.formData = {};
    vm.getUserProfile = getUserProfile;
    vm.key = '';
    vm.path = '';
    vm.directory = '';
    vm.page = '';
    vm.abortRequest = abortRequest;

    vm.key = $routeParams.path;
    vm.path = vm.key.split('/');
    vm.directory = vm.path[0];
    var dir = vm.directory
    vm.page = vm.path[1];

    vm.req = { userID: '' };
    vm.formData = {
        UserID: '',
        Username: '',
        Role: '',
        FirstName: '',
        LastName: '',
        Memo: ''
    };
    var requestInfo = null;
    function abortRequest()
    {
        return (requestInfo && requestInfo.abort());
    }
    vm.req.userID = $routeParams.id;
    function getUserProfile()
    {
        blockUIFactory.StartBlock('vm.blockNgView');
        requestInfo =
        (ngTreeViewFactory.GetDataProfile('/Home/getUserProfileData', vm.req))
         .then(function (answer)
         {
             vm.formData = answer.data[0];
             blockUIFactory.StopBlock('vm.blockNgView');
         }, function (error)
         {
             blockUIFactory.StopBlock('vm.blockNgView');
             console.log(error);
         });
    };
    getUserProfile();
};

function tvAddUserProfileCtrl($http,$scope){
    var vm = this;
};