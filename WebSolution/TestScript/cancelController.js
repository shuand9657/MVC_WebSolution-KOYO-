'use strict';

MVCApp.controller('cancelTokenCtrl', cancelTokenCtrl);

function cancelTokenCtrl(blockUIFactory, httpFactory)
{
    var vm = this;
    vm.trigger1 = trigger1;
    vm.trigger2 = trigger2;
    vm.abortRequest = abortRequest;
    vm.cancelRequest = cancelRequest;
    vm.eventFire = eventFire;

    var requestInfo = null

    function trigger1(blockID)
    {
        blockUIFactory.StartBlock(blockID);
        var someValue = {
            number: 1,
            text: "test String"
        };
        var ranID = { a: Math.random() };
        alert(ranID.a);
        (requestInfo = httpFactory.HttpPost('/Home/testSomeLongTask',ranID))
            .success(function (data, status, headers, config)
            {
                //options.success(data);
                blockUIFactory.StopBlock(blockID);
                console.log("success getting data");
            }).error(function (data, status, headers, config)
            {
                blockUIFactory.StopBlock(blockID);
                console.log(status);
            });
        requestInfo.abort = function ()
        {
            requestInfo.CancelRequest();
        };
    };
    var request2 = null;
    function trigger2(blockID)
    {
        blockUIFactory.StartBlock(blockID);
        var Value = {
            number: 2, text: "text text"
        };
        var ranID = { a: Math.random() };
        alert(ranID.a);
        request2 = httpFactory.HttpPost('/Home/LongTaskAgain', ranID);
        request2.success(function (data, status, headers, config)
        {
            //options.success(data);
            blockUIFactory.StopBlock(blockID);
            console.log("success getting data");
        }).error(function (data, status, headers, config)
        {
            blockUIFactory.StopBlock(blockID);
            console.log(status);
        });
        request2.abort = function ()
        {
            request2.CancelRequest();
        };
    }

    function abortRequest()
    {
        return (requestInfo && requestInfo.abort());
    }
    function cancelRequest()
    {
        return (request2 && request2.abort());
    }

    function eventFire()
    {
        var promise = httpFactory.HttpPostAsync("/Home/getHttpModuleAction");
        promise.success(function(data,status,headers,config){
            vm.moduleResult = data;
        }).error(function(data,status,headers,config){
            console.log("error");
        });
    }

}