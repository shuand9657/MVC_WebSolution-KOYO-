
MVCApp.controller('ngTreeViewCtrl', ngTreeViewCtrl);
MVCApp.controller('tvDirectCtrl', tvDirectCtrl);

function ngTreeViewCtrl($http, $scope, ngTreeViewFactory, $timeout, ngDialog, $q, blockUI, blockUIFactory, httpFactory)
{
    var vm = this;
    vm.dynamicTreeOptions = {};
    vm.dynamicTreeViewDatasource = {};
    vm.treesource = {};
    vm.formData = {};
    vm.companyShow = false;
    vm.departShow = false;
    vm.userShow = false;
    vm.abortRequest = abortRequest;

    vm.toogleBlock = function (httpBlock)
    {
        var blockName = blockUI.instances.get(httpBlock);
        blockUI.start();
        $timeout(function ()
        {
            blockUI.stop();
        }, 5000);
    }
    
    vm.toogleBlock2 = function (httpBlock)
    {
        blockUIFactory.StartBlock(httpBlock);
    }
    vm.blockState = blockUI.state()
    
    vm.select = { pid: 0 };

    var requestInfo = null;
    function abortRequest()
    {
        return (vm.treesource && vm.treesource.abort());
    }
    vm.treesource = ngTreeViewFactory.getTreeViewData('/Home/asyncTVonDemand', 'vm.httpBlock');

    vm.dynamicTreeOptions ={
        loadOnDemand: true,
        dataSource: vm.treesource,
        dataTextField: 'text'
    };
    vm.testTreeOptions={
        loadOnDemand: true,
        dataSource: vm.treesource,
        dataTextField: 'text'
    }
};

function tvDirectCtrl($http, $scope, $routeParams, ngTreeViewFactory) {
};
