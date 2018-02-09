MVCApp.factory('ngTreeViewProfileFactory', ngTreeViewProfileFactory);

function ngTreeViewProfileFactory($http,$q , httpFactory) {
    var factory = {
        GetUserAccItems: GetUserAccItems,
        GetUserAccDetail : GetUserAccDetail,
        GetOrgDetail: GetOrgDetail,
        GetDepartDetail: GetDepartDetail,
        doDDLFirstItem: doDDLFirstItem

    };
    return factory;

    function GetUserAccItems(itemData) {
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync('/Home/getUserAccItems',itemData);
        kendoConsole.log("3. before promise..")
        promise.then(function (answer) {
            answer.status = true;
            deferred.resolve(answer);
            kendoConsole.log("4. inside promise : request...");
        }, function (error) {
            error.status = false;
            deferred.reject(error);
        });
        kendoConsole.log("5. outside promise");
        return promise;
    };

    function GetUserAccDetail(UserID) {
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync('/Home/getUserAccDetail', UserID);
        promise.then(function (answer) {
            answer.status = true;
            deferred.resolve(answer);
        }, function (error) {
            error.status = false;
            deferred.reject(error);
        });
        return promise;
    };

    function GetOrgDetail(MasterID) {
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync('/Home/getOrgDetail', MasterID);
        promise.then(function (answer) {
            answer.status = true;
            deferred.resolve(answer);
            console.log(answer);
        }, function (error) {
            error.status = false;
            deferred.reject(error);
        });
        promise.abort = function ()
        {
            promise.cancel();
            //deferred.resolve();
        };
        promise.finally(function ()
        {
            console.log("Cleaning objcet preference..");
            promise.abort = angular.noop;
            deferred = promise = null;
        })
        return promise;
    };
    function GetDepartDetail(DepartID) {
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync('/Home/getDepartDetail', DepartID);
        promise.then(function (answer) {
            answer.status = true;
            deferred.resolve(answer);
        }, function (error) {
            error.status = false;
            deferred.reject(error);
        });
        return promise;
    };

    function doDDLFirstItem(item) {
        var ddl = [];
        //for (var i in item) {
        //    ddl.push(item[i]);
        //}
        ddl = item;
        ddl.unshift({ Text: "Select an Item..", Value: "0" });
        return ddl;
    };
}