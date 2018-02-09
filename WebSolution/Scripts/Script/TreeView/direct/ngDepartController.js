MVCApp.registerCtrl('tvDepartmentProfileCtrl', tvDepartmentProfileCtrl);
MVCApp.registerCtrl('tvAddDepartmentProfileCtrl', tvAddDepartmentProfileCtrl);

function tvDepartmentProfileCtrl($http, $scope,$routeParams, ngTreeViewFactory,blockUIFactory) {
    var vm = this;
    vm.req={};
    vm.formData = {};
    vm.abortRequest = abortRequest;
    vm.getDepartmentProfile = getDepartmentProfile;

    vm.key = '';
    vm.path = '';
    vm.directory = '';
    vm.page = '';
    //vm.getDataProfile = getDataProfile;

    vm.key = $routeParams.path;
    vm.path = vm.key.split('/');
    vm.directory = vm.path[0];
    var dir = vm.directory
    vm.page = vm.path[1];

    vm.req = { departID: '' };
    vm.formData = {
        DepartID: '',
        DepartName: '',
        DepartSN: '',
        DepartCode: '',
        Memo: ''
    };
    vm.req.departID = $routeParams.id


    var requestInfo = null;
    function abortRequest()
    {
        return (requestInfo && requestInfo.abort());
    }

    function getDepartmentProfile()
    {
        blockUIFactory.StartBlock('vm.blockNgView');
        vm.isLoading = true;
        (requestInfo = ngTreeViewFactory.GetDataProfile('/Home/getDepartProfileData', vm.req))
         .then(function (answer) {
             blockUIFactory.StopBlock('vm.blockNgView');
             vm.formData = answer.data[0];
             vm.isLoading = false;
         }, function (error) {
             //alert("Error!");
             blockUIFactory.StopBlock('vm.blockNgView');
             console.log(error);
             vm.isLoading = false;
         })
    };
    getDepartmentProfile();
};

function tvAddDepartmentProfileCtrl($http,$scope,$routeParams){
    var vm = this;
};