MVCApp.factory('poProfileFactory', poProfileFactory);

function poProfileFactory($http, $q,$timeout, httpFactory) {
    var factory = {
        GetGridDataSource: GetGridDataSource,
        onSelectedItemsChange: onSelectedItemsChange,
        onFormLoadMasterListItems: onFormLoadMasterListItems,
        DoPOProfileAction: DoPOProfileAction
    };
    return factory;

    //function go here

    function GetGridDataSource(url, formData) {
        
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
    };

    function onSelectedItemsChange(url, formData) {
        
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync(url, formData);
        promise.then(function (answer) {
            answer.status = true;
            deferred.resolve(answer);
        }, function (error) {
            error.status = false;
            deferred.reject(error);
        });
        return promise;
    };

    function onFormLoadMasterListItems() {
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync('/Home/GetMasterItemsList');
        promise.then(function (answer) {
            answer.status = true;
            deferred.resolve(answer);
        }, function (error) {
            error.status = false;
            deferred.reject(error);
        });
        return promise;
    };

    function DoPOProfileAction(url, formData) {
        
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync(url, formData);
            
        promise.then(function (answer)
        {
            answer.status = true;
            deferred.resolve(answer);
        }, function (error)
        {
            error.status = false;
            deferred.reject(error);
        });
        return promise;
    }


};
