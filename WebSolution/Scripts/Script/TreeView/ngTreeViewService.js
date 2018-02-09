
angular.module("MVCApp").factory('ngTreeViewFactory', ngTreeViewFactory);


function ngTreeViewFactory($http, $q, $timeout, httpFactory, blockUIFactory)
{
    var factory = {
        getTreeViewData: getTreeViewData,
        GetDataProfile: GetDataProfile,
        getDataSource : getDataSource
    };
    
    return factory;

    function GetDataProfile(url, ID)
    {

        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync(url, ID);
        promise.then(function (answer)
        {
            answer.status = true;
            deferred.resolve(answer);
        }, function (error)
        {
            error.status = false;
            deferred.reject(error);
        });
        promise.abort = function ()
        {
            promise.cancel();
        }
        return promise;
    }

    function getDataSource(url, blockID)
    {
        var deferred = $q.defer();
        var promise = httpFactory.HttpPostAsync(url);
        promise.then(function (answer)
        {
            answer.status = true;
            deferred.resolve(answer);
            //ngTreeViewFactory.getTreeViewData(answer, blockID);
        }, function (error)
        {
            error.status = false;
            deferred.resolve(error);
        });
        promise.abort = function ()
        {
            promise.cancel();
        };
        return promise;
    }

    function getTreeViewData(url, blockID)
    {
        var deferred = $q.defer();
        var promise = null;
        var dataSource = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (options)
                {
                    blockUIFactory.StartBlock(blockID);
                    if (options.data.ID)
                    {
                        var items = dataSource.get(options.data.ID);
                    }
                    else var items = { Level: null, rootID: null };
                    var item = {
                        nodeID: items.nodeID,
                        Level: items.Level,
                        rootID: items.rootID
                    };
                    promise = httpFactory.HttpPostAsync(url, item);
                    promise.success(function (data, status, headers, config)
                    {
                        options.success(data.items);
                        blockUIFactory.StopBlock(blockID);

                    }).error(function (data, status, headers, config)
                    {
                        if (data === 0)
                        {
                            blockUIFactory.StopBlock(blockID);
                        }
                        else
                        {
                            options.error(status);
                            alert('Error getting tree view Data!' + status);
                            blockUIFactory.StopBlock(blockID);
                        }
                    });
                    promise.abort = function ()
                    {
                        promise.cancel();
                    };
                    promise.finally(function ()
                    {
                        console.log("Cleaning object preference..");
                        promise.abort = angular.noop;
                        deferred = promise = null;
                    });
                }
            },
            schema: {
                //data: 'items',
                model: {
                    id: 'ID',
                    text: 'text',
                    MasterID: 'nodeID',
                    hasChildren: 'hasChildren'
                }
            }
        });
        dataSource.abort = function ()
        {
            promise.cancel();
        }

        return dataSource;
    };
};

