'use strict';
MVCApp.controller('poProfileCtrl', poProfileCtrl);
MVCApp.controller('poAddProfileCtrl', poAddProfileCtrl);

function poProfileCtrl($scope, $rootScope, ngDialog, poProfileFactory, httpFactory)
{

    var vm = this;
    vm.formData = {};
    vm.detailData = {};
    vm.POMainGridOptions = {};
    vm.SearchPOView = SearchPOView;
    vm.LogoutClick = LogoutClick;
    vm.DetailShow = true;
    vm.createDetailGrid = createDetailGrid;
    vm.savePOProfileAction = savePOProfileAction
    vm.MasterItemsChange = MasterItemsChange;
    vm.SearchPONoClick = SearchPONoClick;
    vm.PODetailGridOptions = {};

    vm.formData = {
        MasterID: 0,
        ProjectID: 0,
        SupplierID: 0,
        PONo: ""
    };

    vm.detailData = { POID: 0 };

    //search dialog pop-up window
    function SearchPOView() {
        ngDialog.openConfirm({
            template: '../templates/_searchPOViewDialog.html',
            closeByEscape:true,
            className: 'ngdialog-theme-plain custom-width',
            preCloseCallback: 'preCloseCallbackOnScope',
            scope: $scope
        });
    };

    $scope.preCloseCallbackOnScope = function(value) {
        if (confirm('Confirm close Search Window? (Value = ' + value + ' )')) {
            return true;
        }
        return false;
    };

    function LogoutClick() {
        ngDialog.openConfirm({
            template: '/Login/Login',
            closeByEscape: true,
            className: 'ngdialog-theme-plain custom-width',
            scope: $scope
        });
    };
    //連動drop down list的資料↓
    //MasterID顯示↓


        poProfileFactory.onFormLoadMasterListItems()
            .then(function (answer) {
                vm.MasterItems = answer.data;
            }, function (error) {
                alert("Error Retriving Master List Item!! " + error);
                console.log(error);
            });

    function MasterItemsChange() {
        poProfileFactory.onSelectedItemsChange('/Home/GetProjectItemsList', vm.formData)
            .then(function (answer) {
                vm.ProjectItems = answer;
            }, function (error) {
                alert("error Retriving List Item!");
            });
    };
        
    function SearchPONoClick() {
        $("#POKendoGrid").data('kendoGrid').dataSource.read();
        $('#POKendoGrid').data('kendoGrid').refresh();
    };
    vm.POMainGridOptions = {
        dataSource: poProfileFactory.GetGridDataSource('/Home/GetPOViewItems', vm.formData),
        serverFiltering: true,
        selectable: "row",
        sortable: true,
        pageable: {
            pageSize: 5,
            refresh: true,
            pageSizes: ['All', 5, 10, 20, 50],
            buttonCount: 5
        },
        editable: false,
        columns: [
            {
                title: "Details",
                command: { text: "Select", click: SelectPODetail },
                sortable: false,
                filterable: false,
                width: '80px'
            },
            { field: 'POID', title: 'POID', hidden: true },
            { field: "MasterOrgSN", title: "Master Name", width: '80px' },
            { field: "ProjectName", title: "Project Name", width: '150px' },
            { field: "SupplierOrgSN", title: "Supplier Name", width: '120px' },
            { field: "PONo", title: "Purchase Order No ", width: '150px' },
            { field: "CreateDate", title: "Create Date ", width: '200px', template: "#= kendo.toString(kendo.parseDate(CreateDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #" },
            { field: "UpdateDate", title: "Last Update Time", width: '200px', template: "#= kendo.toString(kendo.parseDate(UpdateDate, 'yyyy-MM-dd'), 'dd/MM/yyyy hh:mm') #" }

        ]
    };
    createDetailGrid();
    function SelectPODetail(e) {
       
        e.preventDefault();
        var item = this.dataItem($(e.currentTarget).closest("tr"));
        vm.formData = item;
        vm.detailData.POID = item.POID;
        $("#PODetailGrid").data('kendoGrid').dataSource.read();
        $('#PODetailGrid').data('kendoGrid').refresh();

        vm.DetailShow = false;
    };
    function createDetailGrid() {
        vm.PODetailGridOptions = {
            dataSource: poProfileFactory.GetGridDataSource('/Home/GetPODetailItem', vm.detailData),
            autoBind:false,
            serverFiltering: true,
            selectable: "row",
            sortable: true,
            pageable: {
                pageSize: 10,
                refresh: true,
                pageSizes: ['All', 5, 10, 20, 50],
                buttonCount: 5
            },
            editable: true,
            columns: [
                { field: 'POID', title: 'POID', hidden: true },
                { field: 'PartsID', title: 'Parts ID', hidden: true },
                { field: "PartsCode", title: "Parts Code", width: "auto" },
                { field: "PartsName", title: "Parts Name", width: "auto" },
                { field: "Qty", title: "Quantity", width: "auto" }
            ]
        };
    };

    function savePOProfileAction() {
        httpFactory.HttpPostAsync('/Home/DoPOProfileAction', vm.formData)
            .then(function (answer) {
                alert("Success!");
            }, function (error) {
                alert("Error on Saving State! " + error);
                console.log(error);
            })
    };
};


function poAddProfileCtrl($http,poProfileFactory,httpFactory) {
    var vm = this;
    var today = ""
    vm.formData = {};
    vm.DetailShow = true;
    vm.CreateDateOptions = {};
    vm.MasterItemsChange = MasterItemsChange;
    vm.ProjectItemsChange = ProjectItemsChange;
    vm.savePOProfileAction = savePOProfileAction
    vm.ProjectItems = "";

    today = new Date();
    vm.today = today;

    vm.formData = {
        MasterID: '0',
        ProjectID: '0',
        SupplierID: '0',
        PONo: 'PO-',
        
    };

    poProfileFactory.onFormLoadMasterListItems()
        .then(function (answer) {
            vm.MasterItems = answer;
        }, function (error) {
            alert("Error Retriving Master List Item!! " + error);
            console.log(error);
        });

    function MasterItemsChange() {
        poProfileFactory.onSelectedItemsChange('/Home/GetProjectItemsList', vm.formData)
            .then(function (answer) {
                vm.ProjectItems = answer;
            }, function (error) {
                alert("error Retriving Project List Item!");
            });
    };

    function ProjectItemsChange() {
        poProfileFactory.onSelectedItemsChange('/Home/GetSupplieItemsListNoLimits', vm.formData)
            .then(function (answer) {
                vm.SupplierItems = answer;
            }, function (error) {
                alert("error Retriving Supplier List Item!");
            });
    };

    function savePOProfileAction() {
        httpFactory.HttpDBAction('/Home/DoPOProfileAction', vm.formData)
            .then(function (answer) {
                alert("Success!");
            }, function (error) {
                alert("Error on Saving State! " + error);
                console.log(error);
            })
    };
};
