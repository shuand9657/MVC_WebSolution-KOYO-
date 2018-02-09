MVCApp.factory('utilityServices', utilityServices);

function utilityServices($http,$q)
{
    var factory = {
        onLoadRootScopeErrMsg : onLoadRootScopeErrMsg
    };
    return factory;

    function onLoadRootScopeErrMsg()
    {
        var deferred = $q.deferred();
        var promise = $http({
            method: 'post',
            datatype: 'json',
            url: '/Home/getRootScopeErrMsg'
        });
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
}