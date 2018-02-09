(function(){
    'use strict';
    MVCApp.registerCtrl('tvCompanyProfileCtrl', tvCompanyProfileCtrl);
    MVCApp.registerCtrl('tvAddCompanyProfileCtrl', tvAddCompanyProfileCtrl);
    MVCApp.registerCtrl('tvCompanyProfileCtrl2', tvCompanyProfileCtrl2);

})();



function tvCompanyProfileCtrl($http, $scope, $routeParams, ngTreeViewFactory,  $timeout, ngTreeViewProfileFactory,blockUIFactory) {
    var vm = this;
    vm.req = {};
    vm.formData = {};
    vm.getCompanyProfile = getCompanyProfile;

    vm.key = '';
    vm.path = '';
    vm.directory = '';
    vm.page = '';
    //vm.getDataProfile = getDataProfile;
    vm.abortRequest = abortRequest;
    vm.key = $routeParams.path;
    vm.path = vm.key.split('_');
    vm.directory = vm.path[0];
    var dir = vm.directory
    vm.page = vm.path[1];

    vm.req.masterID = $routeParams.id;
    vm.formData = {
        MasterID: '',
        OrgSN: '',
        OrgName: ''
    };
    var requestInfo = null;
    function abortRequest()
    {
        return (requestInfo && requestInfo.abort());
    }
    function getCompanyProfile()
    {
        blockUIFactory.StartBlock('vm.blockNgView');
        vm.isLoading = true;
        (requestInfo = ngTreeViewFactory.GetDataProfile('/Home/getCompanyProfileData', vm.req))
            .then(function (answer) {
                blockUIFactory.StopBlock('vm.blockNgView');
                vm.formData = answer.data[0];
                vm.isLoading = false;
            }, function (error) {
                blockUIFactory.StopBlock('vm.blockNgView');
                console.log(error);
                vm.isLoading = false;
            });
    };

    getCompanyProfile();
};

function tvAddCompanyProfileCtrl($http,$scope,$routeParams,ngTreeViewFactory){
    var vm = this;
    console.log('tvAddCompanyProfileCtrl');
};

function tvCompanyProfileCtrl2($http, $scope, ngTreeViewFactory, ngTreeViewProfileFactory) {
    var vm = this;
    vm.OrgDetail = [];
    vm.DepartDetail = [];
    vm.UserAccItemsAjs = [];
    vm.OrgItemsChange = OrgItemsChange;
    vm.DepartItemsChange = DepartItemsChange;
    vm.ddlMasterChange = ddlMasterChange;
    vm.UserAccChange = UserAccChange;
    vm.onLoadMasterDetail = onLoadMasterDetail;
    vm.onLoadDepartDetail = onLoadDepartDetail;

    function onLoadMasterDetail() {
        $http({
            method: 'POST',
            url: '/Home/getOrgDetail'
        }).success(function (data, status, headers, config) {
            vm.OrgDetail = data;
        }).error(function (data, status, headers, config) {
            console.log(data);
        })
    }
    function onLoadDepartDetail() {
        $http({
            method: 'Post',
            url: '/Home/getDepartDetail'
        }).success(function (data, status, headers, config) {
            vm.DepartDetail = data;
        }).error(function (data, status, headers, config) {
            console.log(data);
        });
    };
    vm.onLoadMasterDetail();
    vm.onLoadDepartDetail();

    function UserAccChange(UserID) {
        if (UserID > 0) {
            var itemData = {
                UserID: UserID
            };
            vm.formData.FirstName = "";
            vm.formData.LastName = "";
            vm.formData.Gender = "";
            vm.formData.CreateDate = "";
            ngTreeViewProfileFactory.GetUserAccDetail(itemData)
                .then(function (answer) {
                    vm.formData.FirstName = answer.data[0].FirstName;
                    vm.formData.LastName = answer.data[0].LastName;
                    vm.formData.Gender = answer.data[0].Gender;
                    vm.formData.CreateDate = answer.data[0].CreateDate;
                }, function (error) {
                    alert("Error !!");
                    console.log(error);
                });
        }
        else {
            vm.formData.UserID = "0";
            vm.formData.UserID2 = "0";
            vm.formData.FirstName= "";
            vm.formData.LastName= "";
            vm.formData.Gender= "";
            vm.formData.CreateDate="";
            
        }
    };

    function OrgItemsChange(MasterID) {
        //console.log(MasterID);
        if (MasterID > 0)
        {
            vm.formData.OrgName = vm.OrgDetail[MasterID-1].OrgName;
        }
        else {
            vm.formData.OrgName = "";
        }
    };

    function DepartItemsChange(DepartID) {
        if (DepartID > 0) {
            vm.formData.DepartSN = vm.DepartDetail[DepartID - 1].DepartSN;
            vm.formData.DepartCode = vm.DepartDetail[DepartID - 1].DepartCode;
            vm.UserAccItemsAjs = [];

            var itemData = {
                MasterID: vm.formData.MasterID,
                DepartID: DepartID
            };
            kendoConsole.log("1. before service : start request items");
            ngTreeViewProfileFactory.GetUserAccItems(itemData)
                .then(function (answer) {
                    var ddlItem = ngTreeViewProfileFactory.doDDLFirstItem(answer.data);
                    vm.UserAccountItems = answer.data;
                    vm.UserAccItemsAjs = ddlItem;
                    vm.formData.UserID = vm.UserAccItemsAjs[0].Value;
                    vm.formData.UserID2 = ddlItem[0].Value;
                       
                    
                    kendoConsole.log("2. in service : done");
                }, function (error) {
                    alert("Error !!");
                    console.log(error);
                });
        }
        else {
            vm.formData.DepartSN = "";
            vm.formData.DepartCode = "";
        }
        kendoConsole.log("6. done!");
    };

    

    function ddlMasterChange(MasterItem) {
        kendoConsole.log("Selected value is : " + MasterItem);
    }
};