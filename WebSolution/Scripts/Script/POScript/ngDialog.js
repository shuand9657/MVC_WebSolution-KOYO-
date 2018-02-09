var ngDialogApp = angular.module('ngDialogApp', ['ngAnimate', 'ui.bootstrap', 'kendo.directives', 'ngDialog','ngRoute']);


ngDialogApp.config(function ($routeProvider) {
    $routeProvider.when('/Add', {
        templateUrl: '_add',
        controller: 'AddCtrl'
    }).when('/Edit', {
        templateUrl: '_edit',
        controller: 'ngDialogCtrl'
    }).when('/Read', {
        templateUrl: '_read',
        controller: 'ngDialogCtrl'
    }).otherwise({ redirectTo: 'Read' });
})
    .controller('AddCtrl', function ($scope) {
        $scope.message = "Add Controller Called";
    });

ngDialogApp.factory('ngDialogFactory', function () {
    var factory = {
        GetGridDataSource: function (url, formData) {
            var gridDataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        method: 'post',
                        url: url,
                        data: formData,
                        type: 'json'
                    }
                },
                schema: {
                    data: 'data',
                    total: 'total'
                },
                serverPaging: true,
                pageSize: 5
            });
            return gridDataSource;
        }
    };
    return factory;
    
});

ngDialogApp.controller('ngDialogCtrl', function ($scope, $rootScope, $http, ngDialog, ngDialogFactory) {

    $scope.formData = {
        MasterID: 0,
        ProjectID: 0,
        SupplierID: 0,
        POID: 0,
        PONo : ""
    };

    $("#PODetailGrid").hide();

    $scope.SearchPOView = function() {
        ngDialog.openConfirm({
            template: 'serachDialog',
            closeByEscape: true,
            closeByDocument: true,
            className: 'ngdialog-theme-plain custom-width',
            preCloseCallback: 'preCloseCallbackOnScope',
            scope: $scope
        });
    };
    $scope.preCloseCallbackOnScope = function (value) {
        if (confirm('Confirm close Search Window? (Value = ' + value + ' )')) {
            return true;
        }
        return false;
    };

    $scope.SearchPONoClick = function () {
        $("#POKendoGrid").data('kendoGrid').dataSource.read();
    };

    $scope.POMainGridOptions = {
        dataSource: ngDialogFactory.GetGridDataSource('/Home/GetPOViewItems', $scope.formData),
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

    function SelectPODetail(e) {
        //$("#PODetailGrid").hide();
        e.preventDefault();
        var item = this.dataItem($(e.currentTarget).closest("tr"));
        $scope.formData.POID = item.POID;
        //$scope.PODetailGridOptions = function () {
        //    return {
        //        dataSource: ngDialogFactory.GetGridDataSource('/Home/GetPODetailItem', $scope.formData),
        //        serverFiltering: true,
        //        selectable: "row",
        //        sortable: true,
        //        pageable: {
        //            pageSize: 10,
        //            refresh: true,
        //            pageSizes: ['All', 5, 10, 20, 50],
        //            buttonCount: 5
        //        },
        //        editable: true,
        //        columns: [
        //            { field: 'POID', title: 'POID', hidden: true },
        //            { field: 'PartsID', title: 'Parts ID', hidden: true },
        //            { field: "PartsCode", title: "Parts Code", width: "auto" },
        //            { field: "PartsName", title: "Parts Name", width: "auto" },
        //            { field: "Qty", title: "Quantity", width: "auto" }
        //        ]
        //    };
        //};
        $("#PODetailGrid").data('kendoGrid').dataSource.read();
        $('#PODetailGrid').data('kendoGrid').refresh();
        //$("#PODetailGrid").show();
    };

    $scope.PODetailGridOptions = {
        dataSource: ngDialogFactory.GetGridDataSource('/Home/GetPODetailItem', $scope.formData),
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




});